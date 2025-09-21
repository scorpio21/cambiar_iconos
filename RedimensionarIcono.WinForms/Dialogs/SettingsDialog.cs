using System;
using System.IO;
using System.Windows.Forms;
using RedimensionarIcono.WinForms.Services;

namespace RedimensionarIcono.WinForms.Dialogs
{
    internal sealed partial class SettingsDialog : Form
    {
        public string? RcPath => string.IsNullOrWhiteSpace(txtRc.Text) ? null : txtRc.Text.Trim();
        public string? LinkPath => string.IsNullOrWhiteSpace(txtLink.Text) ? null : txtLink.Text.Trim();

        public SettingsDialog()
        {
            InitializeComponent();
            // Cargar valores actuales
            txtRc.Text = IconService.GetRcPath() ?? string.Empty;
            txtLink.Text = IconService.GetLinkPath() ?? string.Empty;
            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }

        private void btnBrowseRc_Click(object? sender, EventArgs e)
        {
            BrowseExe(txtRc, "rc.exe|rc.exe|Todos (*.*)|*.*");
        }

        private void btnBrowseLink_Click(object? sender, EventArgs e)
        {
            BrowseExe(txtLink, "link.exe|link.exe|Todos (*.*)|*.*");
        }

        private void btnOk_Click(object? sender, EventArgs e)
        {
            if (!ValidatePaths()) return;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private static void BrowseExe(TextBox target, string filter)
        {
            using var dlg = new OpenFileDialog
            {
                Title = "Seleccionar ejecutable",
                Filter = filter,
                CheckFileExists = true
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                target.Text = dlg.FileName;
            }
        }

        private bool ValidatePaths()
        {
            if (!string.IsNullOrWhiteSpace(txtRc.Text) && !File.Exists(txtRc.Text))
            {
                MessageBox.Show(this, "La ruta de rc.exe no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtLink.Text) && !File.Exists(txtLink.Text))
            {
                MessageBox.Show(this, "La ruta de link.exe no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
