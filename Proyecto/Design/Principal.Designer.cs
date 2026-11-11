namespace Proyecto.Design
{
    partial class Principal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            btnSalir = new Button();
            btnSimulador = new Button();
            btnSensores = new Button();
            btnZonas = new Button();
            lblMarca = new Label();
            Logo = new PictureBox();
            panelContenido = new Panel();
            lblDatos = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.FromArgb(13, 34, 65);
            splitContainer1.Panel1.Controls.Add(btnSalir);
            splitContainer1.Panel1.Controls.Add(btnSimulador);
            splitContainer1.Panel1.Controls.Add(btnSensores);
            splitContainer1.Panel1.Controls.Add(btnZonas);
            splitContainer1.Panel1.Controls.Add(lblMarca);
            splitContainer1.Panel1.Controls.Add(Logo);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.FromArgb(3, 14, 32);
            splitContainer1.Panel2.Controls.Add(panelContenido);
            splitContainer1.Panel2.Controls.Add(lblDatos);
            splitContainer1.Size = new Size(1280, 720);
            splitContainer1.SplitterDistance = 230;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.TabIndex = 0;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(13, 34, 65);
            btnSalir.Dock = DockStyle.Bottom;
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.FlatAppearance.MouseOverBackColor = Color.FromArgb(214, 69, 69);
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Bahnschrift Condensed", 15.75F);
            btnSalir.ForeColor = Color.FromArgb(254, 255, 255);
            btnSalir.Location = new Point(0, 666);
            btnSalir.Name = "btnSalir";
            btnSalir.Padding = new Padding(22, 0, 0, 0);
            btnSalir.Size = new Size(230, 54);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.TextAlign = ContentAlignment.MiddleLeft;
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnSimulador
            // 
            btnSimulador.BackColor = Color.FromArgb(13, 34, 65);
            btnSimulador.Dock = DockStyle.Top;
            btnSimulador.FlatAppearance.BorderSize = 0;
            btnSimulador.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 102, 187);
            btnSimulador.FlatStyle = FlatStyle.Flat;
            btnSimulador.Font = new Font("Bahnschrift Condensed", 15.75F);
            btnSimulador.ForeColor = Color.FromArgb(254, 255, 255);
            btnSimulador.Location = new Point(0, 288);
            btnSimulador.Name = "btnSimulador";
            btnSimulador.Padding = new Padding(22, 0, 0, 0);
            btnSimulador.Size = new Size(230, 54);
            btnSimulador.TabIndex = 5;
            btnSimulador.Text = "Simulador";
            btnSimulador.TextAlign = ContentAlignment.MiddleLeft;
            btnSimulador.UseVisualStyleBackColor = false;
            btnSimulador.Click += btnSimulador_Click;
            // 
            // btnSensores
            // 
            btnSensores.BackColor = Color.FromArgb(13, 34, 65);
            btnSensores.Dock = DockStyle.Top;
            btnSensores.FlatAppearance.BorderSize = 0;
            btnSensores.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 102, 187);
            btnSensores.FlatStyle = FlatStyle.Flat;
            btnSensores.Font = new Font("Bahnschrift Condensed", 15.75F);
            btnSensores.ForeColor = Color.FromArgb(254, 255, 255);
            btnSensores.Location = new Point(0, 234);
            btnSensores.Name = "btnSensores";
            btnSensores.Padding = new Padding(22, 0, 0, 0);
            btnSensores.Size = new Size(230, 54);
            btnSensores.TabIndex = 1;
            btnSensores.Text = "Sensores";
            btnSensores.TextAlign = ContentAlignment.MiddleLeft;
            btnSensores.UseVisualStyleBackColor = false;
            btnSensores.Click += btnSensores_Click;
            // 
            // btnZonas
            // 
            btnZonas.BackColor = Color.FromArgb(13, 34, 65);
            btnZonas.Dock = DockStyle.Top;
            btnZonas.FlatAppearance.BorderSize = 0;
            btnZonas.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 102, 187);
            btnZonas.FlatStyle = FlatStyle.Flat;
            btnZonas.Font = new Font("Bahnschrift Condensed", 15.75F);
            btnZonas.ForeColor = Color.FromArgb(254, 255, 255);
            btnZonas.Location = new Point(0, 180);
            btnZonas.Name = "btnZonas";
            btnZonas.Padding = new Padding(22, 0, 0, 0);
            btnZonas.Size = new Size(230, 54);
            btnZonas.TabIndex = 0;
            btnZonas.Text = "Zonas verdes";
            btnZonas.TextAlign = ContentAlignment.MiddleLeft;
            btnZonas.UseVisualStyleBackColor = false;
            btnZonas.Click += btnZonas_Click;
            // 
            // lblMarca
            // 
            lblMarca.Dock = DockStyle.Top;
            lblMarca.Font = new Font("Bahnschrift Condensed", 22.25F);
            lblMarca.ForeColor = Color.FromArgb(254, 255, 255);
            lblMarca.Location = new Point(0, 120);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(230, 60);
            lblMarca.TabIndex = 3;
            lblMarca.Text = "EcoFacility";
            lblMarca.TextAlign = ContentAlignment.TopCenter;
            // 
            // Logo
            // 
            Logo.BackColor = Color.Transparent;
            Logo.Dock = DockStyle.Top;
            Logo.Image = Properties.Resources.image_removebg_preview;
            Logo.Location = new Point(0, 0);
            Logo.Name = "Logo";
            Logo.Size = new Size(230, 120);
            Logo.SizeMode = PictureBoxSizeMode.Zoom;
            Logo.TabIndex = 4;
            Logo.TabStop = false;
            // 
            // panelContenido
            // 
            panelContenido.BackColor = Color.FromArgb(3, 14, 32);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(0, 0);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new Size(1049, 692);
            panelContenido.TabIndex = 0;
            // 
            // lblDatos
            // 
            lblDatos.Dock = DockStyle.Bottom;
            lblDatos.Font = new Font("Bahnschrift Condensed", 11.25F);
            lblDatos.ForeColor = Color.FromArgb(174, 180, 194);
            lblDatos.Location = new Point(0, 692);
            lblDatos.Name = "lblDatos";
            lblDatos.Padding = new Padding(16, 0, 0, 0);
            lblDatos.Size = new Size(1049, 28);
            lblDatos.TabIndex = 1;
            lblDatos.Text = "Datos en:";
            lblDatos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Principal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(3, 14, 32);
            ClientSize = new Size(1280, 720);
            Controls.Add(splitContainer1);
            MinimumSize = new Size(1180, 680);
            Name = "Principal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EcoFacility - Gestión hídrica de zonas verdes";
            Load += Principal_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Button btnSensores;
        private Button btnSimulador;
        private Button btnZonas;
        private Label lblMarca;
        private PictureBox Logo;
        private Button btnSalir;
        private Panel panelContenido;
        private Label lblDatos;
    }
}
