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
            btnLoad = new Button();
            pbPreview = new PictureBox();
            lblSize = new Label();
            cbSize = new ComboBox();
            lblBg = new Label();
            btnPickColor = new Button();
            pnlColor = new Panel();
            chkTransparent = new CheckBox();
            btnResize = new Button();
            btnSaveOne = new Button();
            btnSaveBasics = new Button();
            cbFormat = new ComboBox();
            lblFormat = new Label();
            btnSaveIcoMulti = new Button();
            lblMobile = new Label();
            pbMobile = new PictureBox();
            lblManifest = new Label();
            txtManifest = new TextBox();
            btnGenManifest = new Button();
            btnCopyManifest = new Button();
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            salirToolStripMenuItem = new ToolStripMenuItem();
            txtBase = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pbPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMobile).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(12, 231);
            btnLoad.Margin = new Padding(3, 2, 3, 2);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(122, 24);
            btnLoad.TabIndex = 9;
            btnLoad.Text = "Cargar imagen";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // pbPreview
            // 
            pbPreview.AllowDrop = true;
            pbPreview.BorderStyle = BorderStyle.FixedSingle;
            pbPreview.Location = new Point(12, 54);
            pbPreview.Margin = new Padding(3, 2, 3, 2);
            pbPreview.Name = "pbPreview";
            pbPreview.Size = new Size(200, 173);
            pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pbPreview.TabIndex = 10;
            pbPreview.TabStop = false;
            pbPreview.DragDrop += MainForm_DragDrop;
            pbPreview.DragEnter += MainForm_DragEnter;
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.Location = new Point(240, 57);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(53, 15);
            lblSize.TabIndex = 11;
            lblSize.Text = "Tamaño:";
            // 
            // cbSize
            // 
            cbSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSize.Location = new Point(301, 54);
            cbSize.Margin = new Padding(3, 2, 3, 2);
            cbSize.Name = "cbSize";
            cbSize.Size = new Size(106, 23);
            cbSize.TabIndex = 12;
            // 
            // lblBg
            // 
            lblBg.AutoSize = true;
            lblBg.Location = new Point(240, 87);
            lblBg.Name = "lblBg";
            lblBg.Size = new Size(44, 15);
            lblBg.TabIndex = 13;
            lblBg.Text = "Fondo:";
            // 
            // btnPickColor
            // 
            btnPickColor.Location = new Point(301, 84);
            btnPickColor.Margin = new Padding(3, 2, 3, 2);
            btnPickColor.Name = "btnPickColor";
            btnPickColor.Size = new Size(70, 21);
            btnPickColor.TabIndex = 14;
            btnPickColor.Text = "Color";
            btnPickColor.Click += btnPickColor_Click;
            // 
            // pnlColor
            // 
            pnlColor.BackColor = Color.White;
            pnlColor.BorderStyle = BorderStyle.FixedSingle;
            pnlColor.Location = new Point(380, 84);
            pnlColor.Margin = new Padding(3, 2, 3, 2);
            pnlColor.Name = "pnlColor";
            pnlColor.Size = new Size(26, 22);
            pnlColor.TabIndex = 15;
            // 
            // chkTransparent
            // 
            chkTransparent.AutoSize = true;
            chkTransparent.Location = new Point(243, 110);
            chkTransparent.Margin = new Padding(3, 2, 3, 2);
            chkTransparent.Name = "chkTransparent";
            chkTransparent.Size = new Size(164, 19);
            chkTransparent.TabIndex = 16;
            chkTransparent.Text = "Fondo transparente (PNG)";
            chkTransparent.CheckedChanged += chkTransparent_CheckedChanged;
            // 
            // btnResize
            // 
            btnResize.Location = new Point(12, 259);
            btnResize.Margin = new Padding(3, 2, 3, 2);
            btnResize.Name = "btnResize";
            btnResize.Size = new Size(105, 27);
            btnResize.TabIndex = 19;
            btnResize.Text = "Redimensionar";
            btnResize.Click += btnResize_Click;
            // 
            // btnSaveOne
            // 
            btnSaveOne.Location = new Point(123, 260);
            btnSaveOne.Margin = new Padding(3, 2, 3, 2);
            btnSaveOne.Name = "btnSaveOne";
            btnSaveOne.Size = new Size(89, 27);
            btnSaveOne.TabIndex = 20;
            btnSaveOne.Text = "Guardar";
            btnSaveOne.Click += btnSaveOne_Click;
            // 
            // btnSaveBasics
            // 
            btnSaveBasics.Location = new Point(243, 247);
            btnSaveBasics.Margin = new Padding(3, 2, 3, 2);
            btnSaveBasics.Name = "btnSaveBasics";
            btnSaveBasics.Size = new Size(144, 27);
            btnSaveBasics.TabIndex = 21;
            btnSaveBasics.Text = "Guardar básicos";
            btnSaveBasics.Click += btnSaveBasics_Click;
            // 
            // cbFormat
            // 
            cbFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFormat.Items.AddRange(new object[] { "PNG", "JPG", "ICO" });
            cbFormat.Location = new Point(301, 133);
            cbFormat.Margin = new Padding(3, 2, 3, 2);
            cbFormat.Name = "cbFormat";
            cbFormat.Size = new Size(106, 23);
            cbFormat.TabIndex = 18;
            // 
            // lblFormat
            // 
            lblFormat.AutoSize = true;
            lblFormat.Location = new Point(240, 136);
            lblFormat.Name = "lblFormat";
            lblFormat.Size = new Size(55, 15);
            lblFormat.TabIndex = 17;
            lblFormat.Text = "Formato:";
            // 
            // btnSaveIcoMulti
            // 
            btnSaveIcoMulti.Location = new Point(243, 278);
            btnSaveIcoMulti.Margin = new Padding(3, 2, 3, 2);
            btnSaveIcoMulti.Name = "btnSaveIcoMulti";
            btnSaveIcoMulti.Size = new Size(164, 27);
            btnSaveIcoMulti.TabIndex = 2;
            btnSaveIcoMulti.Text = "Guardar ICO multi-tamaño";
            btnSaveIcoMulti.Click += btnSaveIcoMulti_Click;
            // 
            // lblMobile
            // 
            lblMobile.AutoSize = true;
            lblMobile.Location = new Point(333, 187);
            lblMobile.Name = "lblMobile";
            lblMobile.Size = new Size(65, 15);
            lblMobile.TabIndex = 3;
            lblMobile.Text = "Vista móvil";
            // 
            // pbMobile
            // 
            pbMobile.BorderStyle = BorderStyle.FixedSingle;
            pbMobile.Location = new Point(243, 164);
            pbMobile.Margin = new Padding(3, 2, 3, 2);
            pbMobile.Name = "pbMobile";
            pbMobile.Size = new Size(84, 72);
            pbMobile.SizeMode = PictureBoxSizeMode.Zoom;
            pbMobile.TabIndex = 4;
            pbMobile.TabStop = false;
            // 
            // lblManifest
            // 
            lblManifest.AutoSize = true;
            lblManifest.Location = new Point(12, 304);
            lblManifest.Name = "lblManifest";
            lblManifest.Size = new Size(93, 15);
            lblManifest.TabIndex = 5;
            lblManifest.Text = "Bloque Manifest";
            // 
            // txtManifest
            // 
            txtManifest.Location = new Point(9, 321);
            txtManifest.Margin = new Padding(3, 2, 3, 2);
            txtManifest.Multiline = true;
            txtManifest.Name = "txtManifest";
            txtManifest.ReadOnly = true;
            txtManifest.ScrollBars = ScrollBars.Vertical;
            txtManifest.Size = new Size(263, 73);
            txtManifest.TabIndex = 6;
            // 
            // btnGenManifest
            // 
            btnGenManifest.Location = new Point(278, 361);
            btnGenManifest.Margin = new Padding(3, 2, 3, 2);
            btnGenManifest.Name = "btnGenManifest";
            btnGenManifest.Size = new Size(131, 24);
            btnGenManifest.TabIndex = 7;
            btnGenManifest.Text = "Generar manifest";
            btnGenManifest.Click += btnGenManifest_Click;
            // 
            // btnCopyManifest
            // 
            btnCopyManifest.Location = new Point(278, 319);
            btnCopyManifest.Margin = new Padding(3, 2, 3, 2);
            btnCopyManifest.Name = "btnCopyManifest";
            btnCopyManifest.Size = new Size(93, 24);
            btnCopyManifest.TabIndex = 8;
            btnCopyManifest.Text = "Copiar JSON";
            btnCopyManifest.Click += btnCopyManifest_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(416, 24);
            menuStrip1.TabIndex = 0;
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { salirToolStripMenuItem });
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(60, 20);
            archivoToolStripMenuItem.Text = "&Archivo";
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
            salirToolStripMenuItem.Size = new Size(139, 22);
            salirToolStripMenuItem.Text = "&Salir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            // 
            // txtBase
            // 
            txtBase.Location = new Point(140, 233);
            txtBase.Margin = new Padding(3, 2, 3, 2);
            txtBase.Name = "txtBase";
            txtBase.Size = new Size(72, 23);
            txtBase.TabIndex = 1;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(416, 404);
            Controls.Add(txtBase);
            Controls.Add(menuStrip1);
            Controls.Add(btnSaveIcoMulti);
            Controls.Add(lblMobile);
            Controls.Add(pbMobile);
            Controls.Add(lblManifest);
            Controls.Add(txtManifest);
            Controls.Add(btnGenManifest);
            Controls.Add(btnCopyManifest);
            Controls.Add(btnLoad);
            Controls.Add(pbPreview);
            Controls.Add(lblSize);
            Controls.Add(cbSize);
            Controls.Add(lblBg);
            Controls.Add(btnPickColor);
            Controls.Add(pnlColor);
            Controls.Add(chkTransparent);
            Controls.Add(lblFormat);
            Controls.Add(cbFormat);
            Controls.Add(btnResize);
            Controls.Add(btnSaveOne);
            Controls.Add(btnSaveBasics);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Redimensionar Icono (WinForms)";
            Load += MainForm_Load;
            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;
            ((System.ComponentModel.ISupportInitialize)pbPreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMobile).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private Button btnSaveIcoMulti;
        private Label lblMobile;
        private PictureBox pbMobile;
        private Label lblManifest;
        private TextBox txtManifest;
        private Button btnGenManifest;
        private Button btnCopyManifest;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem salirToolStripMenuItem;
        private TextBox txtBase;
    }
}
