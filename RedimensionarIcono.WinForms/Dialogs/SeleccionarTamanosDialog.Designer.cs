using System.Windows.Forms;

namespace RedimensionarIcono.WinForms.Dialogs
{
    partial class SeleccionarTamanosDialog
    {
        private System.ComponentModel.IContainer components = null;
        private CheckedListBox clbSizes;
        private Button btnTodos;
        private Button btnNinguno;
        private Button btnOk;
        private Button btnCancelar;

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
            clbSizes = new CheckedListBox();
            btnTodos = new Button();
            btnNinguno = new Button();
            btnOk = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // clbSizes
            // 
            clbSizes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            clbSizes.CheckOnClick = true;
            clbSizes.Location = new Point(12, 12);
            clbSizes.Name = "clbSizes";
            clbSizes.Size = new Size(396, 310);
            clbSizes.TabIndex = 0;
            // 
            // btnTodos
            // 
            btnTodos.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnTodos.Location = new Point(12, 350);
            btnTodos.Name = "btnTodos";
            btnTodos.Size = new Size(80, 28);
            btnTodos.TabIndex = 1;
            btnTodos.Text = "Todos";
            // 
            // btnNinguno
            // 
            btnNinguno.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnNinguno.Location = new Point(98, 350);
            btnNinguno.Name = "btnNinguno";
            btnNinguno.Size = new Size(90, 28);
            btnNinguno.TabIndex = 2;
            btnNinguno.Text = "Ninguno";
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(224, 350);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(85, 28);
            btnOk.TabIndex = 3;
            btnOk.Text = "Aceptar";
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(315, 350);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(93, 28);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            // 
            // SeleccionarTamanosDialog
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnCancelar;
            ClientSize = new Size(420, 426);
            Controls.Add(clbSizes);
            Controls.Add(btnTodos);
            Controls.Add(btnNinguno);
            Controls.Add(btnOk);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SeleccionarTamanosDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Seleccionar tamaños";
            ResumeLayout(false);
        }
    }
}
