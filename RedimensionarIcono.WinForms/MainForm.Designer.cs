using System.Drawing;
using System.Windows.Forms;

namespace RedimensionarIcono.WinForms
{
    partial class MainForm : Form
    {
        private System.ComponentModel.IContainer components = null;

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
            this.btnLoad = new Button();
            this.pbPreview = new PictureBox();
            this.lblSize = new Label();
            this.cbSize = new ComboBox();
            this.lblBg = new Label();
            this.btnPickColor = new Button();
            this.pnlColor = new Panel();
            this.chkTransparent = new CheckBox();
            this.btnResize = new Button();
            this.btnSaveOne = new Button();
            this.btnSaveBasics = new Button();
            this.cbFormat = new ComboBox();
            this.lblFormat = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            //
            // btnLoad
            //
            this.btnLoad.Location = new Point(20, 20);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new Size(140, 32);
            this.btnLoad.Text = "Cargar imagen";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            //
            // pbPreview
            //
            this.pbPreview.BorderStyle = BorderStyle.FixedSingle;
            this.pbPreview.Location = new Point(20, 70);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new Size(300, 300);
            this.pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
            //
            // lblSize
            //
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new Point(340, 80);
            this.lblSize.Text = "Tamaño:";
            //
            // cbSize
            //
            this.cbSize.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbSize.Location = new Point(410, 76);
            this.cbSize.Name = "cbSize";
            this.cbSize.Size = new Size(120, 28);
            //
            // lblBg
            //
            this.lblBg.AutoSize = true;
            this.lblBg.Location = new Point(340, 120);
            this.lblBg.Text = "Fondo:";
            //
            // btnPickColor
            //
            this.btnPickColor.Location = new Point(410, 116);
            this.btnPickColor.Size = new Size(80, 28);
            this.btnPickColor.Text = "Color";
            this.btnPickColor.Click += new System.EventHandler(this.btnPickColor_Click);
            //
            // pnlColor
            //
            this.pnlColor.Location = new Point(500, 116);
            this.pnlColor.Size = new Size(30, 28);
            this.pnlColor.BackColor = Color.White;
            this.pnlColor.BorderStyle = BorderStyle.FixedSingle;
            //
            // chkTransparent
            //
            this.chkTransparent.AutoSize = true;
            this.chkTransparent.Location = new Point(340, 160);
            this.chkTransparent.Text = "Fondo transparente (PNG)";
            //
            // lblFormat
            //
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new Point(340, 200);
            this.lblFormat.Text = "Formato:";
            //
            // cbFormat
            //
            this.cbFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbFormat.Location = new Point(410, 196);
            this.cbFormat.Size = new Size(120, 28);
            //
            // btnResize
            //
            this.btnResize.Location = new Point(340, 240);
            this.btnResize.Size = new Size(120, 36);
            this.btnResize.Text = "Redimensionar";
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            //
            // btnSaveOne
            //
            this.btnSaveOne.Location = new Point(470, 240);
            this.btnSaveOne.Size = new Size(120, 36);
            this.btnSaveOne.Text = "Guardar";
            this.btnSaveOne.Click += new System.EventHandler(this.btnSaveOne_Click);
            //
            // btnSaveBasics
            //
            this.btnSaveBasics.Location = new Point(340, 285);
            this.btnSaveBasics.Size = new Size(250, 36);
            this.btnSaveBasics.Text = "Guardar básicos";
            this.btnSaveBasics.Click += new System.EventHandler(this.btnSaveBasics_Click);
            //
            // MainForm
            //
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(620, 400);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.pbPreview);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.cbSize);
            this.Controls.Add(this.lblBg);
            this.Controls.Add(this.btnPickColor);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.chkTransparent);
            this.Controls.Add(this.lblFormat);
            this.Controls.Add(this.cbFormat);
            this.Controls.Add(this.btnResize);
            this.Controls.Add(this.btnSaveOne);
            this.Controls.Add(this.btnSaveBasics);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Redimensionar Icono (WinForms)";
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Button btnLoad;
        private PictureBox pbPreview;
        private Label lblSize;
        private ComboBox cbSize;
        private Label lblBg;
        private Button btnPickColor;
        private Panel pnlColor;
        private CheckBox chkTransparent;
        private Button btnResize;
        private Button btnSaveOne;
        private Button btnSaveBasics;
        private ComboBox cbFormat;
        private Label lblFormat;
    }
}
