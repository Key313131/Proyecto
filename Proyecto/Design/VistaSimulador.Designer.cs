namespace Proyecto.Design
{
    partial class VistaSimulador
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
            dgvLecturas = new DataGridView();
            lstEventos = new ListBox();
            panelAcciones = new Panel();
            btnIniciar = new Button();
            btnDetener = new Button();
            label1 = new Label();
            nudIntervalo = new NumericUpDown();
            lblReloj = new Label();
            lblClima = new Label();
            lblTotal = new Label();
            lblCola = new Label();
            lblEstado = new Label();
            lblTitulo = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvLecturas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudIntervalo).BeginInit();
            panelAcciones.SuspendLayout();
            SuspendLayout();
            //
            // dgvLecturas
            //
            dgvLecturas.Dock = DockStyle.Fill;
            dgvLecturas.Location = new Point(16, 72);
            dgvLecturas.Name = "dgvLecturas";
            dgvLecturas.Size = new Size(787, 400);
            dgvLecturas.TabIndex = 0;
            //
            // lstEventos
            //
            lstEventos.BackColor = Color.FromArgb(13, 34, 65);
            lstEventos.BorderStyle = BorderStyle.None;
            lstEventos.Dock = DockStyle.Bottom;
            lstEventos.ForeColor = Color.FromArgb(254, 255, 255);
            lstEventos.IntegralHeight = false;
            lstEventos.Location = new Point(16, 472);
            lstEventos.Name = "lstEventos";
            lstEventos.Size = new Size(787, 156);
            lstEventos.TabIndex = 1;
            //
            // panelAcciones
            //
            panelAcciones.BackColor = Color.FromArgb(13, 34, 65);
            panelAcciones.Controls.Add(btnIniciar);
            panelAcciones.Controls.Add(btnDetener);
            panelAcciones.Controls.Add(label1);
            panelAcciones.Controls.Add(nudIntervalo);
            panelAcciones.Controls.Add(lblReloj);
            panelAcciones.Controls.Add(lblClima);
            panelAcciones.Controls.Add(lblTotal);
            panelAcciones.Controls.Add(lblCola);
            panelAcciones.Dock = DockStyle.Right;
            panelAcciones.Location = new Point(803, 0);
            panelAcciones.Name = "panelAcciones";
            panelAcciones.Size = new Size(230, 628);
            panelAcciones.TabIndex = 2;
            //
            // btnIniciar
            //
            btnIniciar.BackColor = Color.LimeGreen;
            btnIniciar.FlatAppearance.BorderSize = 0;
            btnIniciar.FlatStyle = FlatStyle.Flat;
            btnIniciar.ForeColor = Color.FromArgb(3, 14, 32);
            btnIniciar.Location = new Point(18, 18);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(194, 44);
            btnIniciar.TabIndex = 0;
            btnIniciar.Text = "Iniciar";
            btnIniciar.UseVisualStyleBackColor = false;
            btnIniciar.Click += btnIniciar_Click;
            //
            // btnDetener
            //
            btnDetener.BackColor = Color.FromArgb(214, 69, 69);
            btnDetener.FlatAppearance.BorderSize = 0;
            btnDetener.FlatStyle = FlatStyle.Flat;
            btnDetener.ForeColor = Color.FromArgb(254, 255, 255);
            btnDetener.Location = new Point(18, 72);
            btnDetener.Name = "btnDetener";
            btnDetener.Size = new Size(194, 44);
            btnDetener.TabIndex = 1;
            btnDetener.Text = "Detener";
            btnDetener.UseVisualStyleBackColor = false;
            btnDetener.Click += btnDetener_Click;
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Location = new Point(18, 132);
            label1.Name = "label1";
            label1.Size = new Size(160, 23);
            label1.TabIndex = 2;
            label1.Text = "Intervalo real (ms)";
            //
            // nudIntervalo
            //
            nudIntervalo.BackColor = Color.FromArgb(3, 14, 32);
            nudIntervalo.BorderStyle = BorderStyle.FixedSingle;
            nudIntervalo.ForeColor = Color.White;
            nudIntervalo.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            nudIntervalo.Location = new Point(18, 158);
            nudIntervalo.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudIntervalo.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            nudIntervalo.Name = "nudIntervalo";
            nudIntervalo.Size = new Size(194, 30);
            nudIntervalo.TabIndex = 3;
            nudIntervalo.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            nudIntervalo.ValueChanged += nudIntervalo_ValueChanged;
            //
            // lblReloj
            //
            lblReloj.Location = new Point(18, 210);
            lblReloj.Name = "lblReloj";
            lblReloj.Size = new Size(194, 52);
            lblReloj.TabIndex = 4;
            lblReloj.Text = "Hora simulada";
            //
            // lblClima
            //
            lblClima.Location = new Point(18, 266);
            lblClima.Name = "lblClima";
            lblClima.Size = new Size(194, 28);
            lblClima.TabIndex = 5;
            lblClima.Text = "Clima";
            //
            // lblTotal
            //
            lblTotal.Location = new Point(18, 298);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(194, 28);
            lblTotal.TabIndex = 6;
            lblTotal.Text = "Lecturas";
            //
            // lblCola
            //
            lblCola.Location = new Point(18, 330);
            lblCola.Name = "lblCola";
            lblCola.Size = new Size(194, 28);
            lblCola.TabIndex = 7;
            lblCola.Text = "En cola";
            //
            // lblEstado
            //
            lblEstado.Dock = DockStyle.Bottom;
            lblEstado.ForeColor = Color.FromArgb(174, 180, 194);
            lblEstado.Location = new Point(16, 628);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(1017, 36);
            lblEstado.TabIndex = 3;
            lblEstado.Text = "Detenido";
            lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblTitulo
            //
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Font = new Font("Bahnschrift Condensed", 22.25F);
            lblTitulo.Location = new Point(16, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(787, 72);
            lblTitulo.TabIndex = 4;
            lblTitulo.Text = "Simulador de sensores";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // VistaSimulador
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(3, 14, 32);
            Controls.Add(dgvLecturas);
            Controls.Add(lstEventos);
            Controls.Add(panelAcciones);
            Controls.Add(lblEstado);
            Controls.Add(lblTitulo);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            Name = "VistaSimulador";
            Padding = new Padding(16, 0, 16, 0);
            Size = new Size(1049, 664);
            ((System.ComponentModel.ISupportInitialize)dgvLecturas).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudIntervalo).EndInit();
            panelAcciones.ResumeLayout(false);
            panelAcciones.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvLecturas;
        private ListBox lstEventos;
        private Panel panelAcciones;
        private Button btnIniciar;
        private Button btnDetener;
        private Label label1;
        private NumericUpDown nudIntervalo;
        private Label lblReloj;
        private Label lblClima;
        private Label lblTotal;
        private Label lblCola;
        private Label lblEstado;
        private Label lblTitulo;
    }
}
