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

        public MainForm()
        {
            InitializeComponent();
            // Poblar tamaños
            cbSize.Items.AddRange(_sizes.Select(s => (object)$"{s}x{s}").ToArray());
            cbSize.SelectedIndex = Array.IndexOf(_sizes, 192);
            // Formatos
            cbFormat.Items.AddRange(new object[] { "PNG", "JPG" });
            cbFormat.SelectedIndex = 0;
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
                    ToggleActions(true);
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
            if (transparent) fmtName = "PNG";
            using var dlg = new SaveFileDialog
            {
                Title = "Guardar icono",
                FileName = $"icon-{size}x{size}.{(fmtName == "PNG" ? "png" : "jpg")}",
                Filter = fmtName == "PNG" ? "PNG (*.png)|*.png" : "JPG (*.jpg)|*.jpg"
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                GuardarBitmap(_lastResized, dlg.FileName, fmtName);
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
                var basics = new (int size, string name)[] {
                    (16, "favicon-16x16"),
                    (20, "icon-20x20"),
                    (24, "icon-24x24"),
                    (32, "favicon-32x32"),
                    (180, "apple-touch-icon"),
                    (192, "icon-192x192"),
                    (512, "icon-512x512")
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
    }
}
