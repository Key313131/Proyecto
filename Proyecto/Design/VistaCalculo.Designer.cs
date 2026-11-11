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
            dgvPlan = new DataGridView();
            lblResumen = new Label();
            lblTemp = new Label();
            nudTemp = new NumericUpDown();
            lblSol = new Label();
            nudSol = new NumericUpDown();
            lblInicio = new Label();
            dtpInicio = new DateTimePicker();
            lblHoras = new Label();
            nudHoras = new NumericUpDown();
            lblPresupuesto = new Label();
            nudPresupuesto = new NumericUpDown();
            lblTarifa = new Label();
            nudTarifa = new NumericUpDown();
            btnCalcular = new Button();
            btnDetalle = new Button();
            panelDatos = new Panel();
            lblEstado = new Label();
            lblTitulo = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPlan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTemp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSol).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHoras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPresupuesto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTarifa).BeginInit();
            panelDatos.SuspendLayout();
            SuspendLayout();
            //
            // dgvPlan
            //
            dgvPlan.Dock = DockStyle.Fill;
            dgvPlan.Name = "dgvPlan";
            dgvPlan.Size = new Size(767, 400);
            dgvPlan.TabIndex = 0;
            //
            // lblResumen
            //
            lblResumen.BackColor = Color.FromArgb(13, 34, 65);
            lblResumen.Dock = DockStyle.Bottom;
            lblResumen.ForeColor = Color.FromArgb(254, 255, 255);
            lblResumen.Name = "lblResumen";
            lblResumen.Padding = new Padding(10, 8, 10, 8);
            lblResumen.Size = new Size(767, 110);
            lblResumen.TabIndex = 1;
            lblResumen.Text = "";
            //
            // lblTemp
            //
            lblTemp.AutoSize = true;
            lblTemp.Location = new Point(16, 10);
            lblTemp.Name = "lblTemp";
            lblTemp.Size = new Size(200, 23);
            lblTemp.TabIndex = 0;
            lblTemp.Text = "Temperatura prevista (°C)";
            //
            // nudTemp
            //
            nudTemp.BackColor = Color.FromArgb(3, 14, 32);
            nudTemp.BorderStyle = BorderStyle.FixedSingle;
            nudTemp.ForeColor = Color.White;
            nudTemp.DecimalPlaces = 1;
            nudTemp.Location = new Point(16, 36);
            nudTemp.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nudTemp.Minimum = new decimal(new int[] { 10, 0, 0, -2147483648 });
            nudTemp.Name = "nudTemp";
            nudTemp.Size = new Size(218, 30);
            nudTemp.TabIndex = 1;
            nudTemp.Value = new decimal(new int[] { 22, 0, 0, 0 });
            //
            // lblSol
            //
            lblSol.AutoSize = true;
            lblSol.Location = new Point(16, 78);
            lblSol.Name = "lblSol";
            lblSol.Size = new Size(150, 23);
            lblSol.TabIndex = 2;
            lblSol.Text = "Horas de luz del día";
            //
            // nudSol
            //
            nudSol.BackColor = Color.FromArgb(3, 14, 32);
            nudSol.BorderStyle = BorderStyle.FixedSingle;
            nudSol.ForeColor = Color.White;
            nudSol.DecimalPlaces = 1;
            nudSol.Location = new Point(16, 104);
            nudSol.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            nudSol.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudSol.Name = "nudSol";
            nudSol.Size = new Size(218, 30);
            nudSol.TabIndex = 3;
            nudSol.Value = new decimal(new int[] { 12, 0, 0, 0 });
            //
            // lblInicio
            //
            lblInicio.AutoSize = true;
            lblInicio.Location = new Point(16, 146);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(200, 23);
            lblInicio.TabIndex = 4;
            lblInicio.Text = "Hora de inicio del riego";
            //
            // dtpInicio
            //
            dtpInicio.CustomFormat = "HH:mm";
            dtpInicio.Format = DateTimePickerFormat.Custom;
            dtpInicio.Location = new Point(16, 172);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.ShowUpDown = true;
            dtpInicio.Size = new Size(218, 30);
            dtpInicio.TabIndex = 5;
            //
            // lblHoras
            //
            lblHoras.AutoSize = true;
            lblHoras.Location = new Point(16, 214);
            lblHoras.Name = "lblHoras";
            lblHoras.Size = new Size(210, 23);
            lblHoras.TabIndex = 6;
            lblHoras.Text = "Horas disponibles para regar";
            //
            // nudHoras
            //
            nudHoras.BackColor = Color.FromArgb(3, 14, 32);
            nudHoras.BorderStyle = BorderStyle.FixedSingle;
            nudHoras.ForeColor = Color.White;
            nudHoras.DecimalPlaces = 1;
            nudHoras.Location = new Point(16, 240);
            nudHoras.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            nudHoras.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudHoras.Name = "nudHoras";
            nudHoras.Size = new Size(218, 30);
            nudHoras.TabIndex = 7;
            nudHoras.Value = new decimal(new int[] { 3, 0, 0, 0 });
            //
            // lblPresupuesto
            //
            lblPresupuesto.AutoSize = true;
            lblPresupuesto.Location = new Point(16, 282);
            lblPresupuesto.Name = "lblPresupuesto";
            lblPresupuesto.Size = new Size(200, 23);
            lblPresupuesto.TabIndex = 8;
            lblPresupuesto.Text = "Presupuesto diario ($)";
            //
            // nudPresupuesto
            //
            nudPresupuesto.BackColor = Color.FromArgb(3, 14, 32);
            nudPresupuesto.BorderStyle = BorderStyle.FixedSingle;
            nudPresupuesto.ForeColor = Color.White;
            nudPresupuesto.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudPresupuesto.Location = new Point(16, 308);
            nudPresupuesto.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudPresupuesto.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudPresupuesto.Name = "nudPresupuesto";
            nudPresupuesto.Size = new Size(218, 30);
            nudPresupuesto.TabIndex = 9;
            nudPresupuesto.ThousandsSeparator = true;
            nudPresupuesto.Value = new decimal(new int[] { 10000, 0, 0, 0 });
            //
            // lblTarifa
            //
            lblTarifa.AutoSize = true;
            lblTarifa.Location = new Point(16, 350);
            lblTarifa.Name = "lblTarifa";
            lblTarifa.Size = new Size(200, 23);
            lblTarifa.TabIndex = 10;
            lblTarifa.Text = "Tarifa del agua ($/m³)";
            //
            // nudTarifa
            //
            nudTarifa.BackColor = Color.FromArgb(3, 14, 32);
            nudTarifa.BorderStyle = BorderStyle.FixedSingle;
            nudTarifa.ForeColor = Color.White;
            nudTarifa.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            nudTarifa.Location = new Point(16, 376);
            nudTarifa.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudTarifa.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTarifa.Name = "nudTarifa";
            nudTarifa.Size = new Size(218, 30);
            nudTarifa.TabIndex = 11;
            nudTarifa.ThousandsSeparator = true;
            nudTarifa.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            //
            // btnCalcular
            //
            btnCalcular.BackColor = Color.FromArgb(8, 86, 188);
            btnCalcular.FlatAppearance.BorderSize = 0;
            btnCalcular.FlatStyle = FlatStyle.Flat;
            btnCalcular.ForeColor = Color.FromArgb(254, 255, 255);
            btnCalcular.Location = new Point(16, 420);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(218, 40);
            btnCalcular.TabIndex = 12;
            btnCalcular.Text = "Calcular plan";
            btnCalcular.UseVisualStyleBackColor = false;
            btnCalcular.Click += btnCalcular_Click;
            //
            // btnDetalle
            //
            btnDetalle.BackColor = Color.FromArgb(38, 62, 100);
            btnDetalle.FlatAppearance.BorderSize = 0;
            btnDetalle.FlatStyle = FlatStyle.Flat;
            btnDetalle.ForeColor = Color.FromArgb(254, 255, 255);
            btnDetalle.Location = new Point(16, 470);
            btnDetalle.Name = "btnDetalle";
            btnDetalle.Size = new Size(218, 36);
            btnDetalle.TabIndex = 13;
            btnDetalle.Text = "Ver cálculo detallado";
            btnDetalle.UseVisualStyleBackColor = false;
            btnDetalle.Click += btnDetalle_Click;
            //
            // panelDatos
            //
            panelDatos.AutoScroll = true;
            panelDatos.BackColor = Color.FromArgb(13, 34, 65);
            panelDatos.Controls.Add(lblTemp);
            panelDatos.Controls.Add(nudTemp);
            panelDatos.Controls.Add(lblSol);
            panelDatos.Controls.Add(nudSol);
            panelDatos.Controls.Add(lblInicio);
            panelDatos.Controls.Add(dtpInicio);
            panelDatos.Controls.Add(lblHoras);
            panelDatos.Controls.Add(nudHoras);
            panelDatos.Controls.Add(lblPresupuesto);
            panelDatos.Controls.Add(nudPresupuesto);
            panelDatos.Controls.Add(lblTarifa);
            panelDatos.Controls.Add(nudTarifa);
            panelDatos.Controls.Add(btnCalcular);
            panelDatos.Controls.Add(btnDetalle);
            panelDatos.Dock = DockStyle.Right;
            panelDatos.Name = "panelDatos";
            panelDatos.Size = new Size(250, 628);
            panelDatos.TabIndex = 2;
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
            lblTitulo.Text = "Plan de riego";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // VistaCalculo
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(3, 14, 32);
            Controls.Add(dgvPlan);
            Controls.Add(lblResumen);
            Controls.Add(panelDatos);
            Controls.Add(lblEstado);
            Controls.Add(lblTitulo);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            Name = "VistaCalculo";
            Padding = new Padding(16, 0, 16, 0);
            Size = new Size(1049, 664);
            ((System.ComponentModel.ISupportInitialize)dgvPlan).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTemp).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSol).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHoras).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPresupuesto).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTarifa).EndInit();
            panelDatos.ResumeLayout(false);
            panelDatos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvPlan;
        private Label lblResumen;
        private Label lblTemp;
        private NumericUpDown nudTemp;
        private Label lblSol;
        private NumericUpDown nudSol;
        private Label lblInicio;
        private DateTimePicker dtpInicio;
        private Label lblHoras;
        private NumericUpDown nudHoras;
        private Label lblPresupuesto;
        private NumericUpDown nudPresupuesto;
        private Label lblTarifa;
        private NumericUpDown nudTarifa;
        private Button btnCalcular;
        private Button btnDetalle;
        private Panel panelDatos;
        private Label lblEstado;
        private Label lblTitulo;
    }
}
