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
        /// Crea el label overlay que aparece al arrastrar una imagen válida sobre la ventana.
        /// Se posiciona sobre pbPreview y permanece oculto por defecto.
        /// </summary>
        private void InitDropOverlay()
        {
            _dropOverlay = new Label
            {
                Text = "🖼️  Suelta aquí",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(210, 30, 30, 60),
                Visible = false,
                AutoSize = false,
                Enabled = false,   // pasar eventos de ratón al Form
                Cursor  = Cursors.Default,
            };

            // Borde punteado decorativo
            _dropOverlay.Paint += (s, pe) =>
            {
                var rc = _dropOverlay.ClientRectangle;
                rc.Inflate(-8, -8);
                using var pen = new System.Drawing.Pen(Color.White, 2f);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                pe.Graphics.DrawRectangle(pen, rc);
            };

            Controls.Add(_dropOverlay);

            // Suscribir DragLeave
            DragLeave += MainForm_DragLeave;

            // Reposicionar si la ventana cambia de tamaño
            Resize += (s, ev) => PositionDropOverlay();
        }

        /// <summary>Muestra u oculta el overlay ajustando su posición sobre pbPreview.</summary>
        private void ShowDropOverlay(bool show)
        {
            if (_dropOverlay == null) return;
            if (show)
            {
                PositionDropOverlay();
                _dropOverlay.BringToFront();
                _dropOverlay.Visible = true;
            }
            else
            {
                _dropOverlay.Visible = false;
            }
        }

        /// <summary>Posiciona el overlay exactamente sobre pbPreview con un pequeño margen.</summary>
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

