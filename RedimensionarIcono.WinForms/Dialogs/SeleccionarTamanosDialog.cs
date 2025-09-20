using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RedimensionarIcono.WinForms.Dialogs
{
    internal sealed class SeleccionarTamanosDialog : Form
    {
        private readonly CheckedListBox _list;
        private readonly Button _btnTodos;
        private readonly Button _btnNinguno;
        private readonly Button _btnOk;
        private readonly Button _btnCancelar;

        public int[] TamanosSeleccionados { get; private set; } = Array.Empty<int>();

        public SeleccionarTamanosDialog(IEnumerable<int> sugeridos)
        {
            Text = "Seleccionar tamaños";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(260, 320);

            _list = new CheckedListBox
            {
                Dock = DockStyle.Top,
                Height = 230
            };

            var tamanos = sugeridos.Distinct().OrderBy(x => x).ToArray();
            if (tamanos.Length == 0)
            {
                tamanos = new[] { 16, 20, 24, 32, 48, 64, 96, 128, 180, 192, 256, 512 };
            }
            foreach (var t in tamanos)
            {
                _list.Items.Add(t, true);
            }

            _btnTodos = new Button { Text = "Todos", Left = 10, Top = 240, Width = 60 };
            _btnNinguno = new Button { Text = "Ninguno", Left = 80, Top = 240, Width = 70 };
            _btnOk = new Button { Text = "Aceptar", Left = 160, Top = 240, Width = 80 };
            _btnCancelar = new Button { Text = "Cancelar", Left = 160, Top = 275, Width = 80 };

            _btnTodos.Click += (_, __) =>
            {
                for (int i = 0; i < _list.Items.Count; i++) _list.SetItemChecked(i, true);
            };
            _btnNinguno.Click += (_, __) =>
            {
                for (int i = 0; i < _list.Items.Count; i++) _list.SetItemChecked(i, false);
            };
            _btnOk.Click += (_, __) =>
            {
                TamanosSeleccionados = _list.CheckedItems.Cast<object>().Select(o => (int)o).OrderBy(x => x).ToArray();
                if (TamanosSeleccionados.Length == 0)
                {
                    MessageBox.Show(this, "Selecciona al menos un tamaño.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DialogResult = DialogResult.OK;
            };
            _btnCancelar.Click += (_, __) => { DialogResult = DialogResult.Cancel; };

            Controls.AddRange(new Control[] { _list, _btnTodos, _btnNinguno, _btnOk, _btnCancelar });
        }
    }
}
