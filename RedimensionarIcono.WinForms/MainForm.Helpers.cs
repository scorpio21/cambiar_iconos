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
            // Coordenadas absolutas actuales
            var screenPos = ctrl.Parent != null
                ? ctrl.Parent.PointToScreen(ctrl.Location)
                : this.PointToScreen(ctrl.Location);
            // Nueva ubicación relativa al host
            var newPos = host.PointToClient(screenPos);
            ctrl.Parent = host;
            ctrl.Location = newPos;
            ctrl.BackColor = Color.Transparent;
            ctrl.BringToFront();
        }
    }
}
