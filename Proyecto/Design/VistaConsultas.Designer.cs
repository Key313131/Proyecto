namespace Proyecto.Design
{
    partial class VistaConsultas
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvResultados = new DataGridView();
            lblSensor = new Label();
            cmbSensor = new ComboBox();
            chkFechas = new CheckBox();
            dtpDesde = new DateTimePicker();
            dtpHasta = new DateTimePicker();
            lblCriterio = new Label();
            cmbCriterio = new ComboBox();
            chkDescendente = new CheckBox();
            btnConsultar = new Button();
            lblId = new Label();
            nudId = new NumericUpDown();
            btnBuscarId = new Button();
            panelFiltros = new Panel();
            lblEstado = new Label();
            lblTitulo = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudId).BeginInit();
            panelFiltros.SuspendLayout();
            SuspendLayout();
            //
            // dgvResultados
            //
            dgvResultados.Dock = DockStyle.Fill;
            dgvResultados.Name = "dgvResultados";
            dgvResultados.Size = new Size(767, 400);
            dgvResultados.TabIndex = 0;
            dgvResultados.CellValueNeeded += dgvResultados_CellValueNeeded;
            //
            // lblSensor
            //
            lblSensor.AutoSize = true;
            lblSensor.Location = new Point(16, 10);
            lblSensor.Name = "lblSensor";
            lblSensor.Size = new Size(150, 23);
            lblSensor.TabIndex = 0;
            lblSensor.Text = "Sensor";
            //
            // cmbSensor
            //
            cmbSensor.BackColor = Color.FromArgb(3, 14, 32);
            cmbSensor.DisplayMember = "Texto";
            cmbSensor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSensor.FlatStyle = FlatStyle.Flat;
            cmbSensor.ForeColor = Color.White;
            cmbSensor.Location = new Point(16, 36);
            cmbSensor.Name = "cmbSensor";
            cmbSensor.Size = new Size(218, 31);
            cmbSensor.TabIndex = 1;
            cmbSensor.ValueMember = "Id";
            //
            // chkFechas
            //
            chkFechas.AutoSize = true;
            chkFechas.Location = new Point(16, 78);
            chkFechas.Name = "chkFechas";
            chkFechas.Size = new Size(150, 27);
            chkFechas.TabIndex = 2;
            chkFechas.Text = "Filtrar por fechas";
            chkFechas.UseVisualStyleBackColor = true;
            chkFechas.CheckedChanged += chkFechas_CheckedChanged;
            //
            // dtpDesde
            //
            dtpDesde.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpDesde.Format = DateTimePickerFormat.Custom;
            dtpDesde.Location = new Point(16, 108);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(218, 30);
            dtpDesde.TabIndex = 3;
            //
            // dtpHasta
            //
            dtpHasta.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpHasta.Format = DateTimePickerFormat.Custom;
            dtpHasta.Location = new Point(16, 144);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(218, 30);
            dtpHasta.TabIndex = 4;
            //
            // lblCriterio
            //
            lblCriterio.AutoSize = true;
            lblCriterio.Location = new Point(16, 186);
            lblCriterio.Name = "lblCriterio";
            lblCriterio.Size = new Size(150, 23);
            lblCriterio.TabIndex = 5;
            lblCriterio.Text = "Ordenar por";
            //
            // cmbCriterio
            //
            cmbCriterio.BackColor = Color.FromArgb(3, 14, 32);
            cmbCriterio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCriterio.FlatStyle = FlatStyle.Flat;
            cmbCriterio.ForeColor = Color.White;
            cmbCriterio.Location = new Point(16, 212);
            cmbCriterio.Name = "cmbCriterio";
            cmbCriterio.Size = new Size(218, 31);
            cmbCriterio.TabIndex = 6;
            //
            // chkDescendente
            //
            chkDescendente.AutoSize = true;
            chkDescendente.Location = new Point(16, 252);
            chkDescendente.Name = "chkDescendente";
            chkDescendente.Size = new Size(150, 27);
            chkDescendente.TabIndex = 7;
            chkDescendente.Text = "Descendente";
            chkDescendente.UseVisualStyleBackColor = true;
            //
            // btnConsultar
            //
            btnConsultar.BackColor = Color.FromArgb(8, 86, 188);
            btnConsultar.FlatAppearance.BorderSize = 0;
            btnConsultar.FlatStyle = FlatStyle.Flat;
            btnConsultar.ForeColor = Color.FromArgb(254, 255, 255);
            btnConsultar.Location = new Point(16, 288);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(218, 40);
            btnConsultar.TabIndex = 8;
            btnConsultar.Text = "Consultar";
            btnConsultar.UseVisualStyleBackColor = false;
            btnConsultar.Click += btnConsultar_Click;
            //
            // lblId
            //
            lblId.AutoSize = true;
            lblId.Location = new Point(16, 346);
            lblId.Name = "lblId";
            lblId.Size = new Size(160, 23);
            lblId.TabIndex = 9;
            lblId.Text = "Buscar lectura por Id";
            //
            // nudId
            //
            nudId.BackColor = Color.FromArgb(3, 14, 32);
            nudId.BorderStyle = BorderStyle.FixedSingle;
            nudId.ForeColor = Color.White;
            nudId.Location = new Point(16, 374);
            nudId.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudId.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudId.Name = "nudId";
            nudId.Size = new Size(110, 30);
            nudId.TabIndex = 10;
            nudId.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // btnBuscarId
            //
            btnBuscarId.BackColor = Color.FromArgb(8, 86, 188);
            btnBuscarId.FlatAppearance.BorderSize = 0;
            btnBuscarId.FlatStyle = FlatStyle.Flat;
            btnBuscarId.ForeColor = Color.FromArgb(254, 255, 255);
            btnBuscarId.Location = new Point(134, 372);
            btnBuscarId.Name = "btnBuscarId";
            btnBuscarId.Size = new Size(100, 34);
            btnBuscarId.TabIndex = 11;
            btnBuscarId.Text = "Buscar";
            btnBuscarId.UseVisualStyleBackColor = false;
            btnBuscarId.Click += btnBuscarId_Click;
            //
            // panelFiltros
            //
            panelFiltros.AutoScroll = true;
            panelFiltros.BackColor = Color.FromArgb(13, 34, 65);
            panelFiltros.Controls.Add(lblSensor);
            panelFiltros.Controls.Add(cmbSensor);
            panelFiltros.Controls.Add(chkFechas);
            panelFiltros.Controls.Add(dtpDesde);
            panelFiltros.Controls.Add(dtpHasta);
            panelFiltros.Controls.Add(lblCriterio);
            panelFiltros.Controls.Add(cmbCriterio);
            panelFiltros.Controls.Add(chkDescendente);
            panelFiltros.Controls.Add(btnConsultar);
            panelFiltros.Controls.Add(lblId);
            panelFiltros.Controls.Add(nudId);
            panelFiltros.Controls.Add(btnBuscarId);
            panelFiltros.Dock = DockStyle.Right;
            panelFiltros.Name = "panelFiltros";
            panelFiltros.Size = new Size(250, 628);
            panelFiltros.TabIndex = 2;
            //
            // lblEstado
            //
            lblEstado.Dock = DockStyle.Bottom;
            lblEstado.ForeColor = Color.FromArgb(174, 180, 194);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(1017, 36);
            lblEstado.TabIndex = 3;
            lblEstado.Text = "Listo";
            lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblTitulo
            //
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Font = new Font("Bahnschrift Condensed", 22.25F);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(767, 72);
            lblTitulo.TabIndex = 4;
            lblTitulo.Text = "Consultas de lecturas";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // VistaConsultas
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(3, 14, 32);
            Controls.Add(dgvResultados);
            Controls.Add(panelFiltros);
            Controls.Add(lblEstado);
            Controls.Add(lblTitulo);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            Name = "VistaConsultas";
            Padding = new Padding(16, 0, 16, 0);
            Size = new Size(1049, 664);
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudId).EndInit();
            panelFiltros.ResumeLayout(false);
            panelFiltros.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvResultados;
        private Label lblSensor;
        private ComboBox cmbSensor;
        private CheckBox chkFechas;
        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private Label lblCriterio;
        private ComboBox cmbCriterio;
        private CheckBox chkDescendente;
        private Button btnConsultar;
        private Label lblId;
        private NumericUpDown nudId;
        private Button btnBuscarId;
        private Panel panelFiltros;
        private Label lblEstado;
        private Label lblTitulo;
    }
}
