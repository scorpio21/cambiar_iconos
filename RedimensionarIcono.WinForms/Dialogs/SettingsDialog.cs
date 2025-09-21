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
            ClientSize = new Size(640, 180);

            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 3,
                RowCount = 2,
                Padding = new Padding(10),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90f));

            var lblRc = new Label { Text = "Ruta rc.exe:", AutoSize = true, Anchor = AnchorStyles.Left };
            _txtRc = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
            _btnBrowseRc = new Button { Text = "Buscar", Width = 80, Anchor = AnchorStyles.Right };

            var lblLink = new Label { Text = "Ruta link.exe:", AutoSize = true, Anchor = AnchorStyles.Left };
            _txtLink = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
            _btnBrowseLink = new Button { Text = "Buscar", Width = 80, Anchor = AnchorStyles.Right };

            tlp.Controls.Add(lblRc, 0, 0);
            tlp.Controls.Add(_txtRc, 1, 0);
            tlp.Controls.Add(_btnBrowseRc, 2, 0);
            tlp.Controls.Add(lblLink, 0, 1);
            tlp.Controls.Add(_txtLink, 1, 1);
            tlp.Controls.Add(_btnBrowseLink, 2, 1);

            var pnlButtons = new Panel { Dock = DockStyle.Bottom, Height = 48, Padding = new Padding(10, 5, 10, 10) };
            var flow = new FlowLayoutPanel { Dock = DockStyle.Right, FlowDirection = FlowDirection.LeftToRight, AutoSize = true };
            _btnOk = new Button { Text = "Aceptar", Width = 90 };
            _btnCancel = new Button { Text = "Cancelar", Width = 90 };
            flow.Controls.AddRange(new Control[] { _btnOk, _btnCancel });
            pnlButtons.Controls.Add(flow);

            _btnBrowseRc.Click += (_, __) => BrowseExe(_txtRc, "rc.exe|rc.exe|Todos (*.*)|*.*");
            _btnBrowseLink.Click += (_, __) => BrowseExe(_txtLink, "link.exe|link.exe|Todos (*.*)|*.*");
            _btnOk.Click += (_, __) =>
            {
                if (!ValidatePaths()) return;
                DialogResult = DialogResult.OK;
            };
            _btnCancel.Click += (_, __) => DialogResult = DialogResult.Cancel;

            Controls.Add(pnlButtons);
            Controls.Add(tlp);

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
