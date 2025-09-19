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
            this.lblBase = new Label();
            this.txtBase = new TextBox();
            this.btnSaveIcoMulti = new Button();
            this.lblMobile = new Label();
            this.pbMobile = new PictureBox();
            this.lblManifest = new Label();
            this.txtManifest = new TextBox();
            this.btnGenManifest = new Button();
            this.btnCopyManifest = new Button();
            this.gbPaso1 = new GroupBox();
            this.gbPaso2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMobile)).BeginInit();
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
            this.pbPreview.AllowDrop = true;
            this.pbPreview.BackgroundImageLayout = ImageLayout.Tile;
            this.pbPreview.DragEnter += new DragEventHandler(this.MainForm_DragEnter);
            this.pbPreview.DragDrop += new DragEventHandler(this.MainForm_DragDrop);
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
            this.chkTransparent.CheckedChanged += new System.EventHandler(this.chkTransparent_CheckedChanged);
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
            this.cbFormat.Items.AddRange(new object[] { "PNG", "JPG", "ICO" });
            //
            // btnResize
            //
            this.btnResize.Location = new Point(340, 240);
            this.btnResize.Size = new Size(120, 36);
            this.btnResize.Text = "Redimensionar";
            this.btnResize.FlatStyle = FlatStyle.Flat;
            this.btnResize.BackColor = Color.FromArgb(255, 0, 0);
            this.btnResize.ForeColor = Color.White;
            this.btnResize.UseVisualStyleBackColor = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            //
            // btnSaveOne
            //
            this.btnSaveOne.Location = new Point(470, 240);
            this.btnSaveOne.Size = new Size(120, 36);
            this.btnSaveOne.Text = "Guardar";
            this.btnSaveOne.FlatStyle = FlatStyle.Standard;
            this.btnSaveOne.Click += new System.EventHandler(this.btnSaveOne_Click);
            //
            // btnSaveBasics
            //
            this.btnSaveBasics.Location = new Point(340, 285);
            this.btnSaveBasics.Size = new Size(250, 36);
            this.btnSaveBasics.Text = "Guardar básicos";
            this.btnSaveBasics.FlatStyle = FlatStyle.Flat;
            this.btnSaveBasics.BackColor = Color.FromArgb(255, 0, 0);
            this.btnSaveBasics.ForeColor = Color.White;
            this.btnSaveBasics.UseVisualStyleBackColor = false;
            this.btnSaveBasics.Click += new System.EventHandler(this.btnSaveBasics_Click);

            // lblBase
            this.lblBase.AutoSize = true;
            this.lblBase.Location = new Point(340, 20);
            this.lblBase.Text = "Nombre base:";

            // txtBase
            this.txtBase.Location = new Point(340, 42);
            this.txtBase.Size = new Size(250, 27);

            // btnSaveIcoMulti
            this.btnSaveIcoMulti.Location = new Point(340, 330);
            this.btnSaveIcoMulti.Size = new Size(250, 36);
            this.btnSaveIcoMulti.Text = "Guardar ICO multi-tamaño";
            this.btnSaveIcoMulti.Click += new System.EventHandler(this.btnSaveIcoMulti_Click);

            // lblMobile
            this.lblMobile.AutoSize = true;
            this.lblMobile.Location = new Point(20, 380);
            this.lblMobile.Text = "Vista móvil";

            // pbMobile
            this.pbMobile.BorderStyle = BorderStyle.FixedSingle;
            this.pbMobile.Location = new Point(20, 405);
            this.pbMobile.Name = "pbMobile";
            this.pbMobile.Size = new Size(96, 96);
            this.pbMobile.SizeMode = PictureBoxSizeMode.Zoom;
            this.pbMobile.BackgroundImageLayout = ImageLayout.Tile;

            // lblManifest
            this.lblManifest.AutoSize = true;
            this.lblManifest.Location = new Point(140, 380);
            this.lblManifest.Text = "Bloque Manifest";

            // txtManifest
            this.txtManifest.Location = new Point(140, 405);
            this.txtManifest.Multiline = true;
            this.txtManifest.ScrollBars = ScrollBars.Vertical;
            this.txtManifest.Size = new Size(450, 120);
            this.txtManifest.ReadOnly = true;

            // btnGenManifest
            this.btnGenManifest.Location = new Point(140, 530);
            this.btnGenManifest.Size = new Size(150, 32);
            this.btnGenManifest.Text = "Generar manifest";
            this.btnGenManifest.Click += new System.EventHandler(this.btnGenManifest_Click);

            // btnCopyManifest
            this.btnCopyManifest.Location = new Point(300, 530);
            this.btnCopyManifest.Size = new Size(150, 32);
            this.btnCopyManifest.Text = "Copiar JSON";
            this.btnCopyManifest.Click += new System.EventHandler(this.btnCopyManifest_Click);

            // gbPaso1 (visual)
            this.gbPaso1.Text = "Paso 1: Seleccionar imagen";
            this.gbPaso1.Location = new Point(12, 8);
            this.gbPaso1.Size = new Size(330, 372);

            // gbPaso2 (visual)
            this.gbPaso2.Text = "Paso 2: Configurar icono";
            this.gbPaso2.Location = new Point(330, 8);
            this.gbPaso2.Size = new Size(278, 372);
            //
            // MainForm
            //
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(620, 580);
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(this.MainForm_DragEnter);
            this.DragDrop += new DragEventHandler(this.MainForm_DragDrop);
            this.Controls.Add(this.gbPaso1);
            this.Controls.Add(this.gbPaso2);
            this.Controls.Add(this.lblBase);
            this.Controls.Add(this.txtBase);
            this.Controls.Add(this.btnSaveIcoMulti);
            this.Controls.Add(this.lblMobile);
            this.Controls.Add(this.pbMobile);
            this.Controls.Add(this.lblManifest);
            this.Controls.Add(this.txtManifest);
            this.Controls.Add(this.btnGenManifest);
            this.Controls.Add(this.btnCopyManifest);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbMobile)).EndInit();
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
        private GroupBox gbPaso1;
        private GroupBox gbPaso2;
        private Label lblBase;
        private TextBox txtBase;
        private Button btnSaveIcoMulti;
        private Label lblMobile;
        private PictureBox pbMobile;
        private Label lblManifest;
        private TextBox txtManifest;
        private Button btnGenManifest;
        private Button btnCopyManifest;
    }
}
