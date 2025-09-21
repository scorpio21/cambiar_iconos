using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RedimensionarIcono.WinForms.Dialogs
{
    internal sealed partial class SeleccionarTamanosDialog : Form
    {
        public int[] TamanosSeleccionados { get; private set; } = Array.Empty<int>();

        public SeleccionarTamanosDialog(IEnumerable<int> sugeridos)
        {
            InitializeComponent();

            var tamanos = sugeridos.Distinct().OrderBy(x => x).ToArray();
            if (tamanos.Length == 0)
            {
                tamanos = new[] { 16, 20, 24, 32, 48, 64, 96, 128, 180, 192, 256, 512 };
            }
            foreach (var t in tamanos)
            {
                clbSizes.Items.Add(t, true);
            }

            btnTodos.Click += (_, __) =>
            {
                for (int i = 0; i < clbSizes.Items.Count; i++) clbSizes.SetItemChecked(i, true);
            };
            btnNinguno.Click += (_, __) =>
            {
                for (int i = 0; i < clbSizes.Items.Count; i++) clbSizes.SetItemChecked(i, false);
            };
            btnOk.Click += (_, __) =>
            {
                TamanosSeleccionados = clbSizes.CheckedItems.Cast<object>().Select(o => (int)o).OrderBy(x => x).ToArray();
                if (TamanosSeleccionados.Length == 0)
                {
                    MessageBox.Show(this, "Selecciona al menos un tamaño.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DialogResult = DialogResult.OK;
            };
            btnCancelar.Click += (_, __) => { DialogResult = DialogResult.Cancel; };

            AcceptButton = btnOk;
            CancelButton = btnCancelar;
        }
    }
}
