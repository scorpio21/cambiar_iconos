using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using RedimensionarIcono.WinForms.Services;

namespace RedimensionarIcono.WinForms.Dialogs
{
    internal sealed class SettingsDialog : Form
    {
        private readonly TextBox _txtRc;
        private readonly TextBox _txtLink;
        private readonly Button _btnBrowseRc;
        private readonly Button _btnBrowseLink;
        private readonly Button _btnOk;
        private readonly Button _btnCancel;

        public string? RcPath => string.IsNullOrWhiteSpace(_txtRc.Text) ? null : _txtRc.Text.Trim();
        public string? LinkPath => string.IsNullOrWhiteSpace(_txtLink.Text) ? null : _txtLink.Text.Trim();

        public SettingsDialog()
        {
            Text = "Ajustes";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(560, 145);

            var lblRc = new Label { Left = 12, Top = 20, Text = "Ruta rc.exe:", AutoSize = true };
            _txtRc = new TextBox { Left = 100, Top = 16, Width = 360 };
            _btnBrowseRc = new Button { Left = 470, Top = 15, Width = 75, Text = "Buscar" };

            var lblLink = new Label { Left = 12, Top = 60, Text = "Ruta link.exe:", AutoSize = true };
            _txtLink = new TextBox { Left = 100, Top = 56, Width = 360 };
            _btnBrowseLink = new Button { Left = 470, Top = 55, Width = 75, Text = "Buscar" };

            _btnOk = new Button { Left = 380, Top = 100, Width = 80, Text = "Aceptar" };
            _btnCancel = new Button { Left = 470, Top = 100, Width = 80, Text = "Cancelar" };

            _btnBrowseRc.Click += (_, __) => BrowseExe(_txtRc, "rc.exe|rc.exe|Todos (*.*)|*.*");
            _btnBrowseLink.Click += (_, __) => BrowseExe(_txtLink, "link.exe|link.exe|Todos (*.*)|*.*");
            _btnOk.Click += (_, __) =>
            {
                if (!ValidatePaths()) return;
                DialogResult = DialogResult.OK;
            };
            _btnCancel.Click += (_, __) => DialogResult = DialogResult.Cancel;

            Controls.AddRange(new Control[] { lblRc, _txtRc, _btnBrowseRc, lblLink, _txtLink, _btnBrowseLink, _btnOk, _btnCancel });

            // Cargar valores actuales
            _txtRc.Text = IconService.GetRcPath() ?? string.Empty;
            _txtLink.Text = IconService.GetLinkPath() ?? string.Empty;
        }

        private void BrowseExe(TextBox target, string filter)
        {
            using var dlg = new OpenFileDialog
            {
                Title = "Seleccionar ejecutable",
                Filter = filter,
                CheckFileExists = true
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                target.Text = dlg.FileName;
            }
        }

        private bool ValidatePaths()
        {
            if (!string.IsNullOrWhiteSpace(_txtRc.Text) && !File.Exists(_txtRc.Text))
            {
                MessageBox.Show(this, "La ruta de rc.exe no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(_txtLink.Text) && !File.Exists(_txtLink.Text))
            {
                MessageBox.Show(this, "La ruta de link.exe no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
