using System.Windows.Forms;

namespace RedimensionarIcono.WinForms.Dialogs
{
    partial class SettingsDialog
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtRc;
        private TextBox txtLink;
        private Button btnBrowseRc;
        private Button btnBrowseLink;
        private Button btnOk;
        private Button btnCancel;
        private Label label1;
        private Label label2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            label1 = new Label();
            txtRc = new TextBox();
            btnBrowseRc = new Button();
            label2 = new Label();
            txtLink = new TextBox();
            btnBrowseLink = new Button();
            btnOk = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 22);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 0;
            label1.Text = "Ruta rc.exe:";
            // 
            // txtRc
            // 
            txtRc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRc.Location = new Point(100, 19);
            txtRc.Name = "txtRc";
            txtRc.Size = new Size(400, 23);
            txtRc.TabIndex = 1;
            // 
            // btnBrowseRc
            // 
            btnBrowseRc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowseRc.Location = new Point(516, 17);
            btnBrowseRc.Name = "btnBrowseRc";
            btnBrowseRc.Size = new Size(75, 25);
            btnBrowseRc.TabIndex = 2;
            btnBrowseRc.Text = "Buscar";
            btnBrowseRc.Click += btnBrowseRc_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 66);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 3;
            label2.Text = "Ruta link.exe:";
            // 
            // txtLink
            // 
            txtLink.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtLink.Location = new Point(100, 62);
            txtLink.Name = "txtLink";
            txtLink.Size = new Size(400, 23);
            txtLink.TabIndex = 4;
            // 
            // btnBrowseLink
            // 
            btnBrowseLink.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowseLink.Location = new Point(516, 62);
            btnBrowseLink.Name = "btnBrowseLink";
            btnBrowseLink.Size = new Size(75, 25);
            btnBrowseLink.TabIndex = 5;
            btnBrowseLink.Text = "Buscar";
            btnBrowseLink.Click += btnBrowseLink_Click;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.Location = new Point(100, 109);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(85, 28);
            btnOk.TabIndex = 6;
            btnOk.Text = "Aceptar";
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(506, 109);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(85, 28);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancelar";
            btnCancel.Click += btnCancel_Click;
            // 
            // SettingsDialog
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnCancel;
            ClientSize = new Size(640, 166);
            Controls.Add(label1);
            Controls.Add(txtRc);
            Controls.Add(btnBrowseRc);
            Controls.Add(label2);
            Controls.Add(txtLink);
            Controls.Add(btnBrowseLink);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ajustes";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
