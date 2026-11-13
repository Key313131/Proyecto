namespace Proyecto.Design
{
    partial class VistaTablero
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
            dgvTablero = new DataGridView();
            grafico = new GraficoBarras();
            lblConsumo = new Label();
            lblCosto = new Label();
            lblEficiencia = new Label();
            lblAhorro = new Label();
            panelTarjetas = new Panel();
            lblPeriodo = new Label();
            cmbPeriodo = new ComboBox();
            lblTarifa = new Label();
            nudTarifa = new NumericUpDown();
            lblLamina = new Label();
            nudLamina = new NumericUpDown();
            lblMeta = new Label();
            nudMeta = new NumericUpDown();
            lblOrden = new Label();
            cmbOrden = new ComboBox();
            chkDescendente = new CheckBox();
            btnActualizar = new Button();
            panelDatos = new Panel();
            lblEstado = new Label();
            lblTitulo = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTablero).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTarifa).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLamina).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMeta).BeginInit();
            panelTarjetas.SuspendLayout();
            panelDatos.SuspendLayout();
            SuspendLayout();
            //
            // dgvTablero
            //
            dgvTablero.Dock = DockStyle.Fill;
            dgvTablero.Name = "dgvTablero";
            dgvTablero.Size = new Size(767, 400);
            dgvTablero.TabIndex = 0;
            //
            // grafico
            //
            grafico.BackColor = Color.FromArgb(3, 14, 32);
            grafico.Dock = DockStyle.Top;
            grafico.ForeColor = Color.FromArgb(254, 255, 255);
            grafico.Name = "grafico";
            grafico.Size = new Size(767, 220);
            grafico.TabIndex = 1;
            //
            // lblConsumo
            //
            lblConsumo.BackColor = Color.FromArgb(13, 34, 65);
            lblConsumo.Font = new Font("Bahnschrift Condensed", 15.75F);
            lblConsumo.Location = new Point(0, 6);
            lblConsumo.Name = "lblConsumo";
            lblConsumo.Size = new Size(180, 84);
            lblConsumo.TabIndex = 0;
            lblConsumo.Text = "Consumo";
            lblConsumo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblCosto
            //
            lblCosto.BackColor = Color.FromArgb(13, 34, 65);
            lblCosto.Font = new Font("Bahnschrift Condensed", 15.75F);
            lblCosto.Location = new Point(190, 6);
            lblCosto.Name = "lblCosto";
            lblCosto.Size = new Size(180, 84);
            lblCosto.TabIndex = 1;
            lblCosto.Text = "Costo";
            lblCosto.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblEficiencia
            //
            lblEficiencia.BackColor = Color.FromArgb(13, 34, 65);
            lblEficiencia.Font = new Font("Bahnschrift Condensed", 15.75F);
            lblEficiencia.Location = new Point(380, 6);
            lblEficiencia.Name = "lblEficiencia";
            lblEficiencia.Size = new Size(180, 84);
            lblEficiencia.TabIndex = 2;
            lblEficiencia.Text = "Eficiencia";
            lblEficiencia.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblAhorro
            //
            lblAhorro.BackColor = Color.FromArgb(13, 34, 65);
            lblAhorro.Font = new Font("Bahnschrift Condensed", 15.75F);
            lblAhorro.Location = new Point(570, 6);
            lblAhorro.Name = "lblAhorro";
            lblAhorro.Size = new Size(180, 84);
            lblAhorro.TabIndex = 3;
            lblAhorro.Text = "Ahorro";
            lblAhorro.TextAlign = ContentAlignment.MiddleCenter;
            //
            // panelTarjetas
            //
            panelTarjetas.Controls.Add(lblConsumo);
            panelTarjetas.Controls.Add(lblCosto);
            panelTarjetas.Controls.Add(lblEficiencia);
            panelTarjetas.Controls.Add(lblAhorro);
            panelTarjetas.Dock = DockStyle.Top;
            panelTarjetas.Name = "panelTarjetas";
            panelTarjetas.Size = new Size(767, 96);
            panelTarjetas.TabIndex = 2;
            //
            // lblPeriodo
            //
            lblPeriodo.AutoSize = true;
            lblPeriodo.Location = new Point(16, 10);
            lblPeriodo.Name = "lblPeriodo";
            lblPeriodo.Size = new Size(150, 23);
            lblPeriodo.TabIndex = 0;
            lblPeriodo.Text = "Periodo";
            //
            // cmbPeriodo
            //
            cmbPeriodo.BackColor = Color.FromArgb(3, 14, 32);
            cmbPeriodo.DisplayMember = "Texto";
            cmbPeriodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPeriodo.FlatStyle = FlatStyle.Flat;
            cmbPeriodo.ForeColor = Color.White;
            cmbPeriodo.Location = new Point(16, 36);
            cmbPeriodo.Name = "cmbPeriodo";
            cmbPeriodo.Size = new Size(218, 31);
            cmbPeriodo.TabIndex = 1;
            cmbPeriodo.ValueMember = "Valor";
            cmbPeriodo.SelectedIndexChanged += cmbPeriodo_SelectedIndexChanged;
            //
            // lblTarifa
            //
            lblTarifa.AutoSize = true;
            lblTarifa.Location = new Point(16, 78);
            lblTarifa.Name = "lblTarifa";
            lblTarifa.Size = new Size(200, 23);
            lblTarifa.TabIndex = 2;
            lblTarifa.Text = "Tarifa del agua ($/m³)";
            //
            // nudTarifa
            //
            nudTarifa.BackColor = Color.FromArgb(3, 14, 32);
            nudTarifa.BorderStyle = BorderStyle.FixedSingle;
            nudTarifa.ForeColor = Color.White;
            nudTarifa.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            nudTarifa.Location = new Point(16, 104);
            nudTarifa.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudTarifa.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTarifa.Name = "nudTarifa";
            nudTarifa.Size = new Size(218, 30);
            nudTarifa.TabIndex = 3;
            nudTarifa.ThousandsSeparator = true;
            nudTarifa.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            //
            // lblLamina
            //
            lblLamina.AutoSize = true;
            lblLamina.Location = new Point(16, 146);
            lblLamina.Name = "lblLamina";
            lblLamina.Size = new Size(210, 23);
            lblLamina.TabIndex = 4;
            lblLamina.Text = "Lámina tradicional (mm/día)";
            //
            // nudLamina
            //
            nudLamina.BackColor = Color.FromArgb(3, 14, 32);
            nudLamina.BorderStyle = BorderStyle.FixedSingle;
            nudLamina.ForeColor = Color.White;
            nudLamina.DecimalPlaces = 1;
            nudLamina.Location = new Point(16, 172);
            nudLamina.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nudLamina.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudLamina.Name = "nudLamina";
            nudLamina.Size = new Size(218, 30);
            nudLamina.TabIndex = 5;
            nudLamina.Value = new decimal(new int[] { 5, 0, 0, 0 });
            //
            // lblMeta
            //
            lblMeta.AutoSize = true;
            lblMeta.Location = new Point(16, 214);
            lblMeta.Name = "lblMeta";
            lblMeta.Size = new Size(150, 23);
            lblMeta.TabIndex = 6;
            lblMeta.Text = "Meta de ahorro (%)";
            //
            // nudMeta
            //
            nudMeta.BackColor = Color.FromArgb(3, 14, 32);
            nudMeta.BorderStyle = BorderStyle.FixedSingle;
            nudMeta.ForeColor = Color.White;
            nudMeta.Location = new Point(16, 240);
            nudMeta.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            nudMeta.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudMeta.Name = "nudMeta";
            nudMeta.Size = new Size(218, 30);
            nudMeta.TabIndex = 7;
            nudMeta.Value = new decimal(new int[] { 20, 0, 0, 0 });
            //
            // lblOrden
            //
            lblOrden.AutoSize = true;
            lblOrden.Location = new Point(16, 282);
            lblOrden.Name = "lblOrden";
            lblOrden.Size = new Size(150, 23);
            lblOrden.TabIndex = 8;
            lblOrden.Text = "Ordenar zonas por";
            //
            // cmbOrden
            //
            cmbOrden.BackColor = Color.FromArgb(3, 14, 32);
            cmbOrden.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrden.FlatStyle = FlatStyle.Flat;
            cmbOrden.ForeColor = Color.White;
            cmbOrden.Location = new Point(16, 308);
            cmbOrden.Name = "cmbOrden";
            cmbOrden.Size = new Size(218, 31);
            cmbOrden.TabIndex = 9;
            //
            // chkDescendente
            //
            chkDescendente.AutoSize = true;
            chkDescendente.Location = new Point(16, 348);
            chkDescendente.Name = "chkDescendente";
            chkDescendente.Size = new Size(150, 27);
            chkDescendente.TabIndex = 10;
            chkDescendente.Text = "Descendente";
            chkDescendente.UseVisualStyleBackColor = true;
            //
            // btnActualizar
            //
            btnActualizar.BackColor = Color.FromArgb(8, 86, 188);
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.ForeColor = Color.FromArgb(254, 255, 255);
            btnActualizar.Location = new Point(16, 384);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(218, 40);
            btnActualizar.TabIndex = 11;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            //
            // panelDatos
            //
            panelDatos.AutoScroll = true;
            panelDatos.BackColor = Color.FromArgb(13, 34, 65);
            panelDatos.Controls.Add(lblPeriodo);
            panelDatos.Controls.Add(cmbPeriodo);
            panelDatos.Controls.Add(lblTarifa);
            panelDatos.Controls.Add(nudTarifa);
            panelDatos.Controls.Add(lblLamina);
            panelDatos.Controls.Add(nudLamina);
            panelDatos.Controls.Add(lblMeta);
            panelDatos.Controls.Add(nudMeta);
            panelDatos.Controls.Add(lblOrden);
            panelDatos.Controls.Add(cmbOrden);
            panelDatos.Controls.Add(chkDescendente);
            panelDatos.Controls.Add(btnActualizar);
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
            lblTitulo.Text = "Dashboard de consumo";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // VistaTablero
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(3, 14, 32);
            Controls.Add(dgvTablero);
            Controls.Add(grafico);
            Controls.Add(panelTarjetas);
            Controls.Add(panelDatos);
            Controls.Add(lblEstado);
            Controls.Add(lblTitulo);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            Name = "VistaTablero";
            Padding = new Padding(16, 0, 16, 0);
            Size = new Size(1049, 664);
            ((System.ComponentModel.ISupportInitialize)dgvTablero).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTarifa).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLamina).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMeta).EndInit();
            panelTarjetas.ResumeLayout(false);
            panelTarjetas.PerformLayout();
            panelDatos.ResumeLayout(false);
            panelDatos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvTablero;
        private GraficoBarras grafico;
        private Label lblConsumo;
        private Label lblCosto;
        private Label lblEficiencia;
        private Label lblAhorro;
        private Panel panelTarjetas;
        private Label lblPeriodo;
        private ComboBox cmbPeriodo;
        private Label lblTarifa;
        private NumericUpDown nudTarifa;
        private Label lblLamina;
        private NumericUpDown nudLamina;
        private Label lblMeta;
        private NumericUpDown nudMeta;
        private Label lblOrden;
        private ComboBox cmbOrden;
        private CheckBox chkDescendente;
        private Button btnActualizar;
        private Panel panelDatos;
        private Label lblEstado;
        private Label lblTitulo;
    }
}
