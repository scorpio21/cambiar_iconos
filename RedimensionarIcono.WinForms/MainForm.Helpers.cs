using System;
using System.Drawing;
using System.Windows.Forms;

namespace RedimensionarIcono.WinForms
{
    // Métodos de ayuda para la UI (clase parcial)
    public partial class MainForm
    {
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

        // Helper para simular transparencia reparentando el control al host
        private void MakeTransparentOn(Control ctrl, Control host)
        {
            if (ctrl == null || host == null || ctrl.Parent == host) return;
            var screenPos = ctrl.Parent != null
                ? ctrl.Parent.PointToScreen(ctrl.Location)
                : this.PointToScreen(ctrl.Location);
            var newPos = host.PointToClient(screenPos);
            ctrl.Parent = host;
            ctrl.Location = newPos;
            ctrl.BackColor = Color.Transparent;
            ctrl.BringToFront();
        }

        // --- Drag & Drop overlay ---

        /// <summary>
        /// Crea el panel overlay que aparece al arrastrar una imagen válida.
        /// Usa Panel + Paint manual para que el texto y el fondo sean siempre visibles
        /// (Label con Enabled=false aplica color de sistema encima de ForeColor).
        /// </summary>
        private void InitDropOverlay()
        {
            _dropOverlay = new Panel
            {
                Visible  = false,
                AutoSize = false,
                Cursor   = Cursors.Default,
                // Sin AllowDrop: los eventos de arrastre suben al Form
            };

            _dropOverlay.Paint += (s, pe) =>
            {
                var g  = pe.Graphics;
                g.SmoothingMode    = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                var rc = _dropOverlay.ClientRectangle;

                // Fondo oscuro sólido navy
                using (var bg = new SolidBrush(Color.FromArgb(45, 45, 90)))
                    g.FillRectangle(bg, rc);

                // Borde punteado blanco con margen interior
                var brc = rc;
                brc.Inflate(-8, -8);
                using (var pen = new System.Drawing.Pen(Color.White, 2f))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawRectangle(pen, brc);
                }

                // Texto centrado en blanco
                using var font  = new Font("Segoe UI", 16f, FontStyle.Bold, GraphicsUnit.Point);
                using var brush = new SolidBrush(Color.White);
                var sf = new StringFormat
                {
                    Alignment     = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                };
                g.DrawString("🖼️  Suelta aquí", font, brush, (RectangleF)rc, sf);
            };

            Controls.Add(_dropOverlay);

            DragLeave += MainForm_DragLeave;
            Resize    += (s, ev) => PositionDropOverlay();
        }

        /// <summary>Muestra u oculta el overlay posicionado sobre pbPreview.</summary>
        private void ShowDropOverlay(bool show)
        {
            if (_dropOverlay == null) return;
            if (show)
            {
                PositionDropOverlay();
                _dropOverlay.BringToFront();
                _dropOverlay.Visible = true;
                _dropOverlay.Invalidate(); // forzar repintado
            }
            else
            {
                _dropOverlay.Visible = false;
            }
        }

        /// <summary>Posiciona el overlay exactamente sobre pbPreview con un margen interior.</summary>
        private void PositionDropOverlay()
        {
            if (_dropOverlay == null || pbPreview == null) return;
            const int margin = 6;
            var loc = pbPreview.Parent != null && pbPreview.Parent != this
                ? pbPreview.Parent.PointToScreen(pbPreview.Location)
                : PointToScreen(pbPreview.Location);
            var local = PointToClient(loc);
            _dropOverlay.Location = new Point(local.X + margin, local.Y + margin);
            _dropOverlay.Size     = new Size(pbPreview.Width - margin * 2, pbPreview.Height - margin * 2);
        }
    }
}
