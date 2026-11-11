namespace Proyecto.Design
{
    partial class VistaCalculo
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
            lblZona = new Label();
            cmbZona = new ComboBox();
            lblTemp = new Label();
            nudTemp = new NumericUpDown();
            lblHum = new Label();
            nudHum = new NumericUpDown();
            lblSol = new Label();
            nudSol = new NumericUpDown();
            btnTomar = new Button();
            btnCalcular = new Button();
            lblPresupuesto = new Label();
            nudPresupuesto = new NumericUpDown();
            lblTarifa = new Label();
            nudTarifa = new NumericUpDown();
            btnRepartir = new Button();
            lienzo = new LienzoCampo();
            lstZona = new ListBox();
            panelZona = new Panel();
            dgvReparto = new DataGridView();
            lstReparto = new ListBox();
            panelReparto = new Panel();
            panelDatos = new Panel();
            lblEstado = new Label();
            lblTitulo = new Label();
            panelZona.SuspendLayout();
            panelReparto.SuspendLayout();
            panelDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTemp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSol).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPresupuesto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTarifa).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvReparto).BeginInit();
            SuspendLayout();
            //
            // lblZona
            //
            lblZona.AutoSize = true;
            lblZona.Location = new Point(16, 10);
            lblZona.Name = "lblZona";
            lblZona.Size = new Size(150, 23);
            lblZona.TabIndex = 0;
            lblZona.Text = "Zona";
            //
            // cmbZona
            //
            cmbZona.BackColor = Color.FromArgb(3, 14, 32);
            cmbZona.DisplayMember = "Texto";
            cmbZona.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbZona.FlatStyle = FlatStyle.Flat;
            cmbZona.ForeColor = Color.White;
            cmbZona.Location = new Point(16, 36);
            cmbZona.Name = "cmbZona";
            cmbZona.Size = new Size(218, 31);
            cmbZona.TabIndex = 1;
            cmbZona.ValueMember = "Id";
            cmbZona.SelectedIndexChanged += cmbZona_SelectedIndexChanged;
            //
            // lblTemp
            //
            lblTemp.AutoSize = true;
            lblTemp.Location = new Point(16, 78);
            lblTemp.Name = "lblTemp";
            lblTemp.Size = new Size(150, 23);
            lblTemp.TabIndex = 2;
            lblTemp.Text = "Temperatura (°C)";
            //
            // nudTemp
            //
            nudTemp.BackColor = Color.FromArgb(3, 14, 32);
            nudTemp.BorderStyle = BorderStyle.FixedSingle;
            nudTemp.ForeColor = Color.White;
            nudTemp.DecimalPlaces = 1;
            nudTemp.Location = new Point(16, 104);
            nudTemp.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nudTemp.Minimum = new decimal(new int[] { 10, 0, 0, -2147483648 });
            nudTemp.Name = "nudTemp";
            nudTemp.Size = new Size(218, 30);
            nudTemp.TabIndex = 3;
            nudTemp.Value = new decimal(new int[] { 22, 0, 0, 0 });
            //
            // lblHum
            //
            lblHum.AutoSize = true;
            lblHum.Location = new Point(16, 146);
            lblHum.Name = "lblHum";
            lblHum.Size = new Size(150, 23);
            lblHum.TabIndex = 4;
            lblHum.Text = "Humedad del suelo (%)";
            //
            // nudHum
            //
            nudHum.BackColor = Color.FromArgb(3, 14, 32);
            nudHum.BorderStyle = BorderStyle.FixedSingle;
            nudHum.ForeColor = Color.White;
            nudHum.DecimalPlaces = 1;
            nudHum.Location = new Point(16, 172);
            nudHum.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            nudHum.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudHum.Name = "nudHum";
            nudHum.Size = new Size(218, 30);
            nudHum.TabIndex = 5;
            nudHum.Value = new decimal(new int[] { 40, 0, 0, 0 });
            //
            // lblSol
            //
            lblSol.AutoSize = true;
            lblSol.Location = new Point(16, 214);
            lblSol.Name = "lblSol";
            lblSol.Size = new Size(150, 23);
            lblSol.TabIndex = 6;
            lblSol.Text = "Horas de luz del día";
            //
            // nudSol
            //
            nudSol.BackColor = Color.FromArgb(3, 14, 32);
            nudSol.BorderStyle = BorderStyle.FixedSingle;
            nudSol.ForeColor = Color.White;
            nudSol.DecimalPlaces = 1;
            nudSol.Location = new Point(16, 240);
            nudSol.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            nudSol.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudSol.Name = "nudSol";
            nudSol.Size = new Size(218, 30);
            nudSol.TabIndex = 7;
            nudSol.Value = new decimal(new int[] { 12, 0, 0, 0 });
            //
            // btnTomar
            //
            btnTomar.BackColor = Color.FromArgb(38, 62, 100);
            btnTomar.FlatAppearance.BorderSize = 0;
            btnTomar.FlatStyle = FlatStyle.Flat;
            btnTomar.ForeColor = Color.FromArgb(254, 255, 255);
            btnTomar.Location = new Point(16, 282);
            btnTomar.Name = "btnTomar";
            btnTomar.Size = new Size(218, 36);
            btnTomar.TabIndex = 8;
            btnTomar.Text = "Usar últimas lecturas";
            btnTomar.UseVisualStyleBackColor = false;
            btnTomar.Click += btnTomar_Click;
            //
            // btnCalcular
            //
            btnCalcular.BackColor = Color.FromArgb(8, 86, 188);
            btnCalcular.FlatAppearance.BorderSize = 0;
            btnCalcular.FlatStyle = FlatStyle.Flat;
            btnCalcular.ForeColor = Color.FromArgb(254, 255, 255);
            btnCalcular.Location = new Point(16, 326);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(218, 40);
            btnCalcular.TabIndex = 9;
            btnCalcular.Text = "Calcular zona";
            btnCalcular.UseVisualStyleBackColor = false;
            btnCalcular.Click += btnCalcular_Click;
            //
            // lblPresupuesto
            //
            lblPresupuesto.AutoSize = true;
            lblPresupuesto.Location = new Point(16, 392);
            lblPresupuesto.Name = "lblPresupuesto";
            lblPresupuesto.Size = new Size(150, 23);
            lblPresupuesto.TabIndex = 10;
            lblPresupuesto.Text = "Presupuesto diario ($)";
            //
            // nudPresupuesto
            //
            nudPresupuesto.BackColor = Color.FromArgb(3, 14, 32);
            nudPresupuesto.BorderStyle = BorderStyle.FixedSingle;
            nudPresupuesto.ForeColor = Color.White;
            nudPresupuesto.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudPresupuesto.Location = new Point(16, 418);
            nudPresupuesto.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudPresupuesto.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudPresupuesto.Name = "nudPresupuesto";
            nudPresupuesto.Size = new Size(218, 30);
            nudPresupuesto.TabIndex = 11;
            nudPresupuesto.ThousandsSeparator = true;
            nudPresupuesto.Value = new decimal(new int[] { 10000, 0, 0, 0 });
            //
            // lblTarifa
            //
            lblTarifa.AutoSize = true;
            lblTarifa.Location = new Point(16, 460);
            lblTarifa.Name = "lblTarifa";
            lblTarifa.Size = new Size(150, 23);
            lblTarifa.TabIndex = 12;
            lblTarifa.Text = "Tarifa del agua ($/m³)";
            //
            // nudTarifa
            //
            nudTarifa.BackColor = Color.FromArgb(3, 14, 32);
            nudTarifa.BorderStyle = BorderStyle.FixedSingle;
            nudTarifa.ForeColor = Color.White;
            nudTarifa.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            nudTarifa.Location = new Point(16, 486);
            nudTarifa.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudTarifa.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTarifa.Name = "nudTarifa";
            nudTarifa.Size = new Size(218, 30);
            nudTarifa.TabIndex = 13;
            nudTarifa.ThousandsSeparator = true;
            nudTarifa.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            //
            // btnRepartir
            //
            btnRepartir.BackColor = Color.FromArgb(8, 86, 188);
            btnRepartir.FlatAppearance.BorderSize = 0;
            btnRepartir.FlatStyle = FlatStyle.Flat;
            btnRepartir.ForeColor = Color.FromArgb(254, 255, 255);
            btnRepartir.Location = new Point(16, 528);
            btnRepartir.Name = "btnRepartir";
            btnRepartir.Size = new Size(218, 40);
            btnRepartir.TabIndex = 14;
            btnRepartir.Text = "Repartir con Lagrange";
            btnRepartir.UseVisualStyleBackColor = false;
            btnRepartir.Click += btnRepartir_Click;
            //
            // lienzo
            //
            lienzo.BackColor = Color.FromArgb(3, 14, 32);
            lienzo.Dock = DockStyle.Fill;
            lienzo.ForeColor = Color.FromArgb(254, 255, 255);
            lienzo.Location = new Point(0, 0);
            lienzo.Name = "lienzo";
            lienzo.Size = new Size(767, 346);
            lienzo.TabIndex = 0;
            //
            // lstZona
            //
            lstZona.BackColor = Color.FromArgb(13, 34, 65);
            lstZona.BorderStyle = BorderStyle.None;
            lstZona.Dock = DockStyle.Bottom;
            lstZona.ForeColor = Color.FromArgb(254, 255, 255);
            lstZona.HorizontalScrollbar = true;
            lstZona.IntegralHeight = false;
            lstZona.Location = new Point(0, 346);
            lstZona.Name = "lstZona";
            lstZona.Size = new Size(767, 210);
            lstZona.TabIndex = 1;
            //
            // panelZona
            //
            panelZona.Controls.Add(lienzo);
            panelZona.Controls.Add(lstZona);
            panelZona.Dock = DockStyle.Fill;
            panelZona.Location = new Point(16, 72);
            panelZona.Name = "panelZona";
            panelZona.Size = new Size(767, 556);
            panelZona.TabIndex = 0;
            //
            // dgvReparto
            //
            dgvReparto.Dock = DockStyle.Fill;
            dgvReparto.Location = new Point(0, 0);
            dgvReparto.Name = "dgvReparto";
            dgvReparto.Size = new Size(767, 366);
            dgvReparto.TabIndex = 0;
            //
            // lstReparto
            //
            lstReparto.BackColor = Color.FromArgb(13, 34, 65);
            lstReparto.BorderStyle = BorderStyle.None;
            lstReparto.Dock = DockStyle.Bottom;
            lstReparto.ForeColor = Color.FromArgb(254, 255, 255);
            lstReparto.HorizontalScrollbar = true;
            lstReparto.IntegralHeight = false;
            lstReparto.Location = new Point(0, 366);
            lstReparto.Name = "lstReparto";
            lstReparto.Size = new Size(767, 190);
            lstReparto.TabIndex = 1;
            //
            // panelReparto
            //
            panelReparto.Controls.Add(dgvReparto);
            panelReparto.Controls.Add(lstReparto);
            panelReparto.Dock = DockStyle.Fill;
            panelReparto.Location = new Point(16, 72);
            panelReparto.Name = "panelReparto";
            panelReparto.Size = new Size(767, 556);
            panelReparto.TabIndex = 1;
            panelReparto.Visible = false;
            //
            // panelDatos
            //
            panelDatos.AutoScroll = true;
            panelDatos.BackColor = Color.FromArgb(13, 34, 65);
            panelDatos.Controls.Add(lblZona);
            panelDatos.Controls.Add(cmbZona);
            panelDatos.Controls.Add(lblTemp);
            panelDatos.Controls.Add(nudTemp);
            panelDatos.Controls.Add(lblHum);
            panelDatos.Controls.Add(nudHum);
            panelDatos.Controls.Add(lblSol);
            panelDatos.Controls.Add(nudSol);
            panelDatos.Controls.Add(btnTomar);
            panelDatos.Controls.Add(btnCalcular);
            panelDatos.Controls.Add(lblPresupuesto);
            panelDatos.Controls.Add(nudPresupuesto);
            panelDatos.Controls.Add(lblTarifa);
            panelDatos.Controls.Add(nudTarifa);
            panelDatos.Controls.Add(btnRepartir);
            panelDatos.Dock = DockStyle.Right;
            panelDatos.Location = new Point(783, 0);
            panelDatos.Name = "panelDatos";
            panelDatos.Size = new Size(250, 628);
            panelDatos.TabIndex = 2;
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
            lblTitulo.Text = "Cálculo multivariable";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // VistaCalculo
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(3, 14, 32);
            Controls.Add(panelZona);
            Controls.Add(panelReparto);
            Controls.Add(panelDatos);
            Controls.Add(lblEstado);
            Controls.Add(lblTitulo);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            Name = "VistaCalculo";
            Padding = new Padding(16, 0, 16, 0);
            Size = new Size(1049, 664);
            ((System.ComponentModel.ISupportInitialize)nudTemp).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHum).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSol).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPresupuesto).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTarifa).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvReparto).EndInit();
            panelZona.ResumeLayout(false);
            panelReparto.ResumeLayout(false);
            panelDatos.ResumeLayout(false);
            panelDatos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblZona;
        private ComboBox cmbZona;
        private Label lblTemp;
        private NumericUpDown nudTemp;
        private Label lblHum;
        private NumericUpDown nudHum;
        private Label lblSol;
        private NumericUpDown nudSol;
        private Button btnTomar;
        private Button btnCalcular;
        private Label lblPresupuesto;
        private NumericUpDown nudPresupuesto;
        private Label lblTarifa;
        private NumericUpDown nudTarifa;
        private Button btnRepartir;
        private LienzoCampo lienzo;
        private ListBox lstZona;
        private Panel panelZona;
        private DataGridView dgvReparto;
        private ListBox lstReparto;
        private Panel panelReparto;
        private Panel panelDatos;
        private Label lblEstado;
        private Label lblTitulo;
    }
}
