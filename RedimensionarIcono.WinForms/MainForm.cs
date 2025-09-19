using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RedimensionarIcono.WinForms
{
    public partial class MainForm : Form
    {
        private Bitmap? _original;
        private Bitmap? _lastResized;
        private readonly int[] _sizes = new[] { 16, 20, 24, 32, 36, 48, 72, 96, 120, 144, 152, 167, 180, 192, 256, 384, 512 };
        private Color _bgColor = Color.White;
        private Bitmap? _checkerBg;

        public MainForm()
        {
            InitializeComponent();
            // Poblar tamaños
            cbSize.Items.AddRange(_sizes.Select(s => (object)$"{s}x{s}").ToArray());
            cbSize.SelectedIndex = Array.IndexOf(_sizes, 192);
            // Formatos
            UpdateFormatOptions();
            // Fondo checker para visualizar transparencia
            ApplyCheckerBackground();
            // Reorganizar layout en pasos con TableLayoutPanel
            SetupLayout();
            ToggleActions(false);
        }

        private void ToggleActions(bool enabled)
        {
            cbSize.Enabled = enabled;
            btnPickColor.Enabled = enabled;
            chkTransparent.Enabled = enabled;
            cbFormat.Enabled = enabled;
            btnResize.Enabled = enabled;
            btnSaveOne.Enabled = enabled && _lastResized != null;
            btnSaveBasics.Enabled = enabled;
        }

        private void btnLoad_Click(object? sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Title = "Seleccionar imagen",
                Filter = "Imágenes|*.png;*.jpg;*.jpeg;*.webp;*.bmp;*.gif|Todos los archivos|*.*"
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    _original?.Dispose();
                    using var loaded = new Bitmap(dlg.FileName);
                    _original = new Bitmap(loaded);
                    pbPreview.Image = new Bitmap(_original);
                    // Sugerir nombre base desde archivo
                    try
                    {
                        var baseName = Path.GetFileNameWithoutExtension(dlg.FileName) ?? "icon";
                        txtBase.Text = baseName;
                    }
                    catch { /* ignore */ }
                    ToggleActions(true);
                    UpdateFormatOptions();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "No se pudo cargar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPickColor_Click(object? sender, EventArgs e)
        {
            using var dlg = new ColorDialog { Color = _bgColor, FullOpen = true };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _bgColor = dlg.Color;
                pnlColor.BackColor = _bgColor;
            }
        }

        private void btnResize_Click(object? sender, EventArgs e)
        {
            if (_original == null)
            {
                MessageBox.Show(this, "Primero carga una imagen.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var size = GetSelectedSize();
            var transparent = chkTransparent.Checked;
            try
            {
                _lastResized?.Dispose();
                _lastResized = Redimensionar(_original, size, size, transparent ? (Color?)null : _bgColor);
                pbPreview.Image = new Bitmap(_lastResized);
                btnSaveOne.Enabled = true;
                // Vista móvil (96x96)
                using var mobile = Redimensionar(_original, 96, 96, transparent ? (Color?)null : _bgColor);
                pbMobile.Image = new Bitmap(mobile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudo redimensionar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveOne_Click(object? sender, EventArgs e)
        {
            if (_lastResized == null)
            {
                MessageBox.Show(this, "Primero redimensiona una imagen.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var size = GetSelectedSize();
            var fmtName = (cbFormat.SelectedItem?.ToString() ?? "PNG").ToUpperInvariant();
            var transparent = chkTransparent.Checked;
            // Forzar PNG si transparente
            if (transparent && fmtName != "ICO") fmtName = "PNG";
            var baseName = string.IsNullOrWhiteSpace(txtBase.Text) ? $"icon" : SanitizeBase(txtBase.Text);
            using var dlg = new SaveFileDialog
            {
                Title = "Guardar icono",
                FileName = $"{baseName}-{size}x{size}.{(fmtName == "PNG" ? "png" : fmtName == "JPG" ? "jpg" : "ico")}",
                Filter = fmtName == "PNG" ? "PNG (*.png)|*.png" : (fmtName == "JPG" ? "JPG (*.jpg)|*.jpg" : "ICO (*.ico)|*.ico")
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (fmtName == "ICO")
                {
                    GuardarComoIco(_lastResized, dlg.FileName);
                }
                else
                {
                    GuardarBitmap(_lastResized, dlg.FileName, fmtName);
                }
                MessageBox.Show(this, "Imagen guardada.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSaveBasics_Click(object? sender, EventArgs e)
        {
            if (_original == null)
            {
                MessageBox.Show(this, "Primero carga una imagen.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using var dlg = new FolderBrowserDialog { Description = "Selecciona carpeta destino" };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var transparent = chkTransparent.Checked;
                var baseName = string.IsNullOrWhiteSpace(txtBase.Text) ? "icon" : SanitizeBase(txtBase.Text);
                var basics = new (int size, string name)[] {
                    (16, $"{baseName}-16x16"),
                    (20, $"{baseName}-20x20"),
                    (24, $"{baseName}-24x24"),
                    (32, $"{baseName}-32x32"),
                    (180, $"{baseName}-180x180"),
                    (192, $"{baseName}-192x192"),
                    (512, $"{baseName}-512x512")
                };
                foreach (var b in basics)
                {
                    using var bmp = Redimensionar(_original, b.size, b.size, transparent ? (Color?)null : _bgColor);
                    var path = Path.Combine(dlg.SelectedPath, $"{b.name}.png"); // PNG para consistencia
                    GuardarBitmap(bmp, path, "PNG");
                }
                MessageBox.Show(this, "Básicos guardados.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string SanitizeBase(string input)
        {
            var s = input.Normalize().ToLowerInvariant();
            var invalid = Path.GetInvalidFileNameChars();
            s = new string(s.Select(c => invalid.Contains(c) ? '-' : c).ToArray());
            s = System.Text.RegularExpressions.Regex.Replace(s, "[^a-z0-9-_]", "-");
            s = System.Text.RegularExpressions.Regex.Replace(s, "-+", "-");
            s = s.Trim('-','_');
            if (string.IsNullOrEmpty(s)) s = "icon";
            return s;
        }

        private int GetSelectedSize()
        {
            var sel = cbSize.SelectedItem?.ToString() ?? "192x192";
            var parts = sel.Split('x');
            if (int.TryParse(parts[0], out int s)) return s;
            return 192;
        }

        private static Bitmap Redimensionar(Bitmap original, int w, int h, Color? bg)
        {
            var bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            using var g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            // Fondo (si hay color)
            if (bg.HasValue)
            {
                using var brush = new SolidBrush(Color.FromArgb(204, bg.Value)); // ~0.8 opacidad
                g.FillRectangle(brush, 0, 0, w, h);
            }
            // Mantener proporción
            float ar = (float)original.Width / original.Height;
            float drawW, drawH, offX = 0, offY = 0;
            if (ar > 1)
            {
                drawW = w; drawH = w / ar; offY = (h - drawH) / 2f;
            }
            else
            {
                drawH = h; drawW = h * ar; offX = (w - drawW) / 2f;
            }
            var dest = new RectangleF(offX, offY, drawW, drawH);
            g.DrawImage(original, dest);
            return bmp;
        }

        private static void GuardarBitmap(Bitmap bmp, string path, string fmtName)
        {
            if (fmtName.Equals("PNG", StringComparison.OrdinalIgnoreCase))
            {
                bmp.Save(path, ImageFormat.Png);
            }
            else // JPG
            {
                // Guardar con calidad 90
                var codec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(c => c.MimeType == "image/jpeg");
                if (codec == null)
                {
                    bmp.Save(path, ImageFormat.Jpeg);
                    return;
                }
                using var p = new EncoderParameters(1);
                p.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 90L);
                bmp.Save(path, codec, p);
            }
        }

        // Actualiza las opciones del combo de formato en función de la transparencia
        private void UpdateFormatOptions()
        {
            var current = (cbFormat.SelectedItem?.ToString() ?? "PNG").ToUpperInvariant();
            var transparent = chkTransparent.Checked;
            cbFormat.Items.Clear();
            if (transparent)
            {
                cbFormat.Items.AddRange(new object[] { "PNG", "ICO" });
                if (current == "JPG") current = "PNG";
            }
            else
            {
                cbFormat.Items.AddRange(new object[] { "PNG", "JPG", "ICO" });
            }
            var idx = cbFormat.Items.IndexOf(current);
            cbFormat.SelectedIndex = idx >= 0 ? idx : 0;
        }

        // Fondo de tablero (checker) para visualizar transparencia en previews
        private void ApplyCheckerBackground()
        {
            _checkerBg?.Dispose();
            int cell = 8;
            int size = cell * 2;
            var c1 = Color.FromArgb(230, 230, 230);
            var c2 = Color.FromArgb(200, 200, 200);
            _checkerBg = new Bitmap(size, size, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(_checkerBg))
            using (var b1 = new SolidBrush(c1))
            using (var b2 = new SolidBrush(c2))
            {
                g.FillRectangle(b1, 0, 0, cell, cell);
                g.FillRectangle(b2, cell, 0, cell, cell);
                g.FillRectangle(b2, 0, cell, cell, cell);
                g.FillRectangle(b1, cell, cell, cell, cell);
            }
            pbPreview.BackgroundImage = _checkerBg;
            pbMobile.BackgroundImage = _checkerBg;
        }

        // Reorganiza controles dentro de los GroupBox con TableLayoutPanel
        private void SetupLayout()
        {
            // Paso 1: Cargar + Preview
            var tlp1 = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(8),
                AutoSize = false
            };
            tlp1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlp1.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            btnLoad.Margin = new Padding(0, 0, 0, 8);
            pbPreview.Dock = DockStyle.Fill;
            tlp1.Controls.Add(btnLoad, 0, 0);
            tlp1.Controls.Add(pbPreview, 0, 1);
            gbPaso1.Controls.Clear();
            gbPaso1.Controls.Add(tlp1);

            // Paso 2: Configuración + Vista móvil
            var tlp2 = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 7,
                Padding = new Padding(8),
                AutoSize = false
            };
            tlp2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            tlp2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));
            for (int i = 0; i < 7; i++) tlp2.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Fila 0: Nombre base
            lblBase.Margin = new Padding(0, 6, 8, 6);
            txtBase.Margin = new Padding(0, 4, 0, 4);
            tlp2.Controls.Add(lblBase, 0, 0);
            tlp2.Controls.Add(txtBase, 1, 0);

            // Fila 1: Tamaño
            lblSize.Margin = new Padding(0, 6, 8, 6);
            cbSize.Margin = new Padding(0, 4, 0, 4);
            tlp2.Controls.Add(lblSize, 0, 1);
            tlp2.Controls.Add(cbSize, 1, 1);

            // Fila 2: Fondo (Color + panel + Transparente)
            var pnlFondo = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
            btnPickColor.Margin = new Padding(0, 0, 6, 0);
            pnlColor.Margin = new Padding(0, 0, 12, 0);
            chkTransparent.Margin = new Padding(0, 6, 0, 0);
            pnlFondo.Controls.Add(btnPickColor);
            pnlFondo.Controls.Add(pnlColor);
            pnlFondo.Controls.Add(chkTransparent);
            lblBg.Margin = new Padding(0, 6, 8, 6);
            tlp2.Controls.Add(lblBg, 0, 2);
            tlp2.Controls.Add(pnlFondo, 1, 2);

            // Fila 3: Formato
            lblFormat.Margin = new Padding(0, 6, 8, 6);
            cbFormat.Margin = new Padding(0, 4, 0, 4);
            tlp2.Controls.Add(lblFormat, 0, 3);
            tlp2.Controls.Add(cbFormat, 1, 3);

            // Fila 4: Acciones 1 (Redimensionar, Guardar)
            var pnlAcciones1 = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
            btnResize.Margin = new Padding(0, 0, 8, 0);
            btnSaveOne.Margin = new Padding(0, 0, 0, 0);
            pnlAcciones1.Controls.Add(btnResize);
            pnlAcciones1.Controls.Add(btnSaveOne);
            tlp2.Controls.Add(new Label(), 0, 4); // vacío para alinear
            tlp2.Controls.Add(pnlAcciones1, 1, 4);

            // Fila 5: Acciones 2 (Guardar básicos, ICO multi)
            var pnlAcciones2 = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
            btnSaveBasics.Margin = new Padding(0, 0, 8, 0);
            btnSaveIcoMulti.Margin = new Padding(0, 0, 0, 0);
            pnlAcciones2.Controls.Add(btnSaveBasics);
            pnlAcciones2.Controls.Add(btnSaveIcoMulti);
            tlp2.Controls.Add(new Label(), 0, 5);
            tlp2.Controls.Add(pnlAcciones2, 1, 5);

            // Fila 6: Vista móvil
            var pnlMobile = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
            lblMobile.Margin = new Padding(0, 6, 12, 6);
            pbMobile.Margin = new Padding(0, 0, 0, 0);
            pnlMobile.Controls.Add(lblMobile);
            pnlMobile.Controls.Add(pbMobile);
            tlp2.SetColumnSpan(pnlMobile, 2);
            tlp2.Controls.Add(pnlMobile, 0, 6);

            gbPaso2.Controls.Clear();
            gbPaso2.Controls.Add(tlp2);
        }

        // Guardar un ICO de un solo tamaño usando HICON
        private static void GuardarComoIco(Bitmap bmp, string path)
        {
            using var icon = Icon.FromHandle(bmp.GetHicon());
            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            icon.Save(fs);
        }

        // --- Drag & Drop ---
        private void MainForm_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[]) (e.Data.GetData(DataFormats.FileDrop) ?? Array.Empty<string>());
                if (files.Length > 0 && EsImagen(files[0]))
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void MainForm_DragDrop(object? sender, DragEventArgs e)
        {
            try
            {
                var files = (string[]) (e.Data?.GetData(DataFormats.FileDrop) ?? Array.Empty<string>());
                if (files.Length == 0) return;
                var path = files[0];
                if (!EsImagen(path)) return;
                CargarImagenDesdeRuta(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudo cargar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool EsImagen(string path)
        {
            string[] exts = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif", ".webp" };
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return exts.Contains(ext);
        }

        private void CargarImagenDesdeRuta(string path)
        {
            _original?.Dispose();
            using var loaded = new Bitmap(path);
            _original = new Bitmap(loaded);
            pbPreview.Image = new Bitmap(_original);
            ToggleActions(true);
        }

        // --- ICO multi-res ---
        private void btnSaveIcoMulti_Click(object? sender, EventArgs e)
        {
            if (_original == null)
            {
                MessageBox.Show(this, "Primero carga una imagen.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using var dlg = new SaveFileDialog
            {
                Title = "Guardar .ico multi-tamaño",
                FileName = $"{(string.IsNullOrWhiteSpace(txtBase.Text) ? "icon" : SanitizeBase(txtBase.Text))}-multires.ico",
                Filter = "ICO (*.ico)|*.ico"
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var transparent = chkTransparent.Checked;
                var sizes = new [] { 16, 20, 24, 32, 48, 64, 128, 256 };
                var bmps = sizes.Select(s => Redimensionar(_original!, s, s, transparent ? (Color?)null : _bgColor)).ToList();
                try
                {
                    SaveMultiIcon(dlg.FileName, sizes, bmps);
                    MessageBox.Show(this, ".ICO multi-res guardado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    foreach (var b in bmps) b.Dispose();
                }
            }
        }

        // Escribir un .ico con múltiples entradas (cada entrada como PNG con alfa)
        private static void SaveMultiIcon(string path, int[] sizes, System.Collections.Generic.List<Bitmap> bitmaps)
        {
            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);
            // ICONDIR
            bw.Write((ushort)0);      // reserved
            bw.Write((ushort)1);      // type = 1 (icon)
            bw.Write((ushort)sizes.Length); // count

            // Codificar PNGs en memoria
            var pngBytes = new System.Collections.Generic.List<byte[]>();
            foreach (var bmp in bitmaps)
            {
                using var ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Png);
                pngBytes.Add(ms.ToArray());
            }

            int dirSize = 6 + 16 * sizes.Length;
            int offset = dirSize;

            // ICONDIRENTRYs
            for (int i = 0; i < sizes.Length; i++)
            {
                int w = sizes[i];
                int h = sizes[i];
                var data = pngBytes[i];
                bw.Write((byte)(w == 256 ? 0 : w)); // width (0 => 256)
                bw.Write((byte)(h == 256 ? 0 : h)); // height
                bw.Write((byte)0);                  // color count
                bw.Write((byte)0);                  // reserved
                bw.Write((ushort)1);                // planes
                bw.Write((ushort)32);               // bit count
                bw.Write((uint)data.Length);        // bytes in res
                bw.Write((uint)offset);             // image offset
                offset += data.Length;
            }

            // Escribir los PNG concatenados
            foreach (var data in pngBytes)
            {
                bw.Write(data);
            }
        }

        // --- Manifest JSON ---
        private void btnGenManifest_Click(object? sender, EventArgs e)
        {
            var baseName = string.IsNullOrWhiteSpace(txtBase.Text) ? "icon" : SanitizeBase(txtBase.Text);
            var icons = new (int size, string path, string purpose)[]
            {
                (192, $"img/{baseName}-192x192.png", "any maskable"),
                (512, $"img/{baseName}-512x512.png", "any maskable")
            };
            using var sw = new StringWriter();
            sw.WriteLine("\"icons\": [");
            for (int i = 0; i < icons.Length; i++)
            {
                var ic = icons[i];
                sw.Write($"  {{ \"src\": \"{ic.path}\", \"sizes\": \"{ic.size}x{ic.size}\", \"type\": \"image/png\", \"purpose\": \"{ic.purpose}\" }}");
                sw.WriteLine(i < icons.Length - 1 ? "," : string.Empty);
            }
            sw.Write("]");
            txtManifest.Text = sw.ToString();
        }

        private void btnCopyManifest_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtManifest.Text))
            {
                Clipboard.SetText(txtManifest.Text);
                MessageBox.Show(this, "Bloque manifest copiado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chkTransparent_CheckedChanged(object? sender, EventArgs e)
        {
            UpdateFormatOptions();
        }
    }
}
