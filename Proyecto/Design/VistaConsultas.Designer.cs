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
            lstDetalle = new ListBox();
            panelFiltros = new Panel();
            lblSensor = new Label();
            cmbSensor = new ComboBox();
            chkFechas = new CheckBox();
            dtpDesde = new DateTimePicker();
            dtpHasta = new DateTimePicker();
            lblCriterio = new Label();
            cmbCriterio = new ComboBox();
            lblAlgoritmo = new Label();
            cmbAlgoritmo = new ComboBox();
            chkDescendente = new CheckBox();
            btnConsultar = new Button();
            lblId = new Label();
            nudId = new NumericUpDown();
            btnBuscarId = new Button();
            lblN = new Label();
            nudN = new NumericUpDown();
            btnComparar = new Button();
            lblEstado = new Label();
            lblTitulo = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            panelFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudN).BeginInit();
            SuspendLayout();
            //
            // dgvResultados
            //
            dgvResultados.Dock = DockStyle.Fill;
            dgvResultados.Location = new Point(16, 72);
            dgvResultados.Name = "dgvResultados";
            dgvResultados.Size = new Size(767, 386);
            dgvResultados.TabIndex = 0;
            dgvResultados.CellValueNeeded += dgvResultados_CellValueNeeded;
            //
            // lstDetalle
            //
            lstDetalle.BackColor = Color.FromArgb(13, 34, 65);
            lstDetalle.BorderStyle = BorderStyle.None;
            lstDetalle.Dock = DockStyle.Bottom;
            lstDetalle.ForeColor = Color.FromArgb(254, 255, 255);
            lstDetalle.HorizontalScrollbar = true;
            lstDetalle.IntegralHeight = false;
            lstDetalle.Location = new Point(16, 458);
            lstDetalle.Name = "lstDetalle";
            lstDetalle.Size = new Size(767, 170);
            lstDetalle.TabIndex = 1;
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
            panelFiltros.Controls.Add(lblAlgoritmo);
            panelFiltros.Controls.Add(cmbAlgoritmo);
            panelFiltros.Controls.Add(chkDescendente);
            panelFiltros.Controls.Add(btnConsultar);
            panelFiltros.Controls.Add(lblId);
            panelFiltros.Controls.Add(nudId);
            panelFiltros.Controls.Add(btnBuscarId);
            panelFiltros.Controls.Add(lblN);
            panelFiltros.Controls.Add(nudN);
            panelFiltros.Controls.Add(btnComparar);
            panelFiltros.Dock = DockStyle.Right;
            panelFiltros.Location = new Point(783, 0);
            panelFiltros.Name = "panelFiltros";
            panelFiltros.Size = new Size(250, 628);
            panelFiltros.TabIndex = 2;
            //
            // lblSensor
            //
            lblSensor.AutoSize = true;
            lblSensor.Location = new Point(16, 10);
            lblSensor.Name = "lblSensor";
            lblSensor.Size = new Size(58, 23);
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
            chkFechas.Size = new Size(147, 27);
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
            lblCriterio.Size = new Size(95, 23);
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
            // lblAlgoritmo
            //
            lblAlgoritmo.AutoSize = true;
            lblAlgoritmo.Location = new Point(16, 254);
            lblAlgoritmo.Name = "lblAlgoritmo";
            lblAlgoritmo.Size = new Size(83, 23);
            lblAlgoritmo.TabIndex = 7;
            lblAlgoritmo.Text = "Algoritmo";
            //
            // cmbAlgoritmo
            //
            cmbAlgoritmo.BackColor = Color.FromArgb(3, 14, 32);
            cmbAlgoritmo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAlgoritmo.FlatStyle = FlatStyle.Flat;
            cmbAlgoritmo.ForeColor = Color.White;
            cmbAlgoritmo.Location = new Point(16, 280);
            cmbAlgoritmo.Name = "cmbAlgoritmo";
            cmbAlgoritmo.Size = new Size(218, 31);
            cmbAlgoritmo.TabIndex = 8;
            //
            // chkDescendente
            //
            chkDescendente.AutoSize = true;
            chkDescendente.Location = new Point(16, 320);
            chkDescendente.Name = "chkDescendente";
            chkDescendente.Size = new Size(116, 27);
            chkDescendente.TabIndex = 9;
            chkDescendente.Text = "Descendente";
            chkDescendente.UseVisualStyleBackColor = true;
            //
            // btnConsultar
            //
            btnConsultar.BackColor = Color.FromArgb(8, 86, 188);
            btnConsultar.FlatAppearance.BorderSize = 0;
            btnConsultar.FlatStyle = FlatStyle.Flat;
            btnConsultar.ForeColor = Color.FromArgb(254, 255, 255);
            btnConsultar.Location = new Point(16, 356);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(218, 40);
            btnConsultar.TabIndex = 10;
            btnConsultar.Text = "Consultar";
            btnConsultar.UseVisualStyleBackColor = false;
            btnConsultar.Click += btnConsultar_Click;
            //
            // lblId
            //
            lblId.AutoSize = true;
            lblId.Location = new Point(16, 414);
            lblId.Name = "lblId";
            lblId.Size = new Size(160, 23);
            lblId.TabIndex = 11;
            lblId.Text = "Buscar lectura por Id";
            //
            // nudId
            //
            nudId.BackColor = Color.FromArgb(3, 14, 32);
            nudId.BorderStyle = BorderStyle.FixedSingle;
            nudId.ForeColor = Color.White;
            nudId.Location = new Point(16, 442);
            nudId.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudId.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudId.Name = "nudId";
            nudId.Size = new Size(110, 30);
            nudId.TabIndex = 12;
            nudId.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // btnBuscarId
            //
            btnBuscarId.BackColor = Color.FromArgb(8, 86, 188);
            btnBuscarId.FlatAppearance.BorderSize = 0;
            btnBuscarId.FlatStyle = FlatStyle.Flat;
            btnBuscarId.ForeColor = Color.FromArgb(254, 255, 255);
            btnBuscarId.Location = new Point(134, 440);
            btnBuscarId.Name = "btnBuscarId";
            btnBuscarId.Size = new Size(100, 34);
            btnBuscarId.TabIndex = 13;
            btnBuscarId.Text = "Buscar";
            btnBuscarId.UseVisualStyleBackColor = false;
            btnBuscarId.Click += btnBuscarId_Click;
            //
            // lblN
            //
            lblN.AutoSize = true;
            lblN.Location = new Point(16, 490);
            lblN.Name = "lblN";
            lblN.Size = new Size(190, 23);
            lblN.TabIndex = 14;
            lblN.Text = "Comparar algoritmos (n)";
            //
            // nudN
            //
            nudN.BackColor = Color.FromArgb(3, 14, 32);
            nudN.BorderStyle = BorderStyle.FixedSingle;
            nudN.ForeColor = Color.White;
            nudN.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudN.Location = new Point(16, 518);
            nudN.Maximum = new decimal(new int[] { 200000, 0, 0, 0 });
            nudN.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            nudN.Name = "nudN";
            nudN.Size = new Size(110, 30);
            nudN.TabIndex = 15;
            nudN.ThousandsSeparator = true;
            nudN.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            //
            // btnComparar
            //
            btnComparar.BackColor = Color.FromArgb(8, 86, 188);
            btnComparar.FlatAppearance.BorderSize = 0;
            btnComparar.FlatStyle = FlatStyle.Flat;
            btnComparar.ForeColor = Color.FromArgb(254, 255, 255);
            btnComparar.Location = new Point(134, 516);
            btnComparar.Name = "btnComparar";
            btnComparar.Size = new Size(100, 34);
            btnComparar.TabIndex = 16;
            btnComparar.Text = "Comparar";
            btnComparar.UseVisualStyleBackColor = false;
            btnComparar.Click += btnComparar_Click;
            //
            // lblEstado
            //
            lblEstado.Dock = DockStyle.Bottom;
            lblEstado.ForeColor = Color.FromArgb(174, 180, 194);
            lblEstado.Location = new Point(16, 628);
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
            lblTitulo.Location = new Point(16, 0);
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
            Controls.Add(lstDetalle);
            Controls.Add(panelFiltros);
            Controls.Add(lblEstado);
            Controls.Add(lblTitulo);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            Name = "VistaConsultas";
            Padding = new Padding(16, 0, 16, 0);
            Size = new Size(1049, 664);
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            panelFiltros.ResumeLayout(false);
            panelFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudId).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudN).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvResultados;
        private ListBox lstDetalle;
        private Panel panelFiltros;
        private Label lblSensor;
        private ComboBox cmbSensor;
        private CheckBox chkFechas;
        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private Label lblCriterio;
        private ComboBox cmbCriterio;
        private Label lblAlgoritmo;
        private ComboBox cmbAlgoritmo;
        private CheckBox chkDescendente;
        private Button btnConsultar;
        private Label lblId;
        private NumericUpDown nudId;
        private Button btnBuscarId;
        private Label lblN;
        private NumericUpDown nudN;
        private Button btnComparar;
        private Label lblEstado;
        private Label lblTitulo;
    }
}
