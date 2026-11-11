namespace Proyecto.Design
{
    partial class FrmAnalisisAlgoritmos
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
            lstDetalle = new ListBox();
            lblN = new Label();
            nudN = new NumericUpDown();
            btnComparar = new Button();
            panelAcciones = new Panel();
            lblEstado = new Label();
            ((System.ComponentModel.ISupportInitialize)nudN).BeginInit();
            panelAcciones.SuspendLayout();
            SuspendLayout();
            //
            // lstDetalle
            //
            lstDetalle.BackColor = Color.FromArgb(13, 34, 65);
            lstDetalle.BorderStyle = BorderStyle.None;
            lstDetalle.Dock = DockStyle.Fill;
            lstDetalle.ForeColor = Color.FromArgb(254, 255, 255);
            lstDetalle.HorizontalScrollbar = true;
            lstDetalle.IntegralHeight = false;
            lstDetalle.Name = "lstDetalle";
            lstDetalle.Size = new Size(767, 400);
            lstDetalle.TabIndex = 0;
            //
            // lblN
            //
            lblN.AutoSize = true;
            lblN.Location = new Point(16, 10);
            lblN.Name = "lblN";
            lblN.Size = new Size(180, 23);
            lblN.TabIndex = 0;
            lblN.Text = "Comparar con n datos";
            //
            // nudN
            //
            nudN.BackColor = Color.FromArgb(3, 14, 32);
            nudN.BorderStyle = BorderStyle.FixedSingle;
            nudN.ForeColor = Color.White;
            nudN.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudN.Location = new Point(16, 36);
            nudN.Maximum = new decimal(new int[] { 200000, 0, 0, 0 });
            nudN.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            nudN.Name = "nudN";
            nudN.Size = new Size(218, 30);
            nudN.TabIndex = 1;
            nudN.ThousandsSeparator = true;
            nudN.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            //
            // btnComparar
            //
            btnComparar.BackColor = Color.FromArgb(8, 86, 188);
            btnComparar.FlatAppearance.BorderSize = 0;
            btnComparar.FlatStyle = FlatStyle.Flat;
            btnComparar.ForeColor = Color.FromArgb(254, 255, 255);
            btnComparar.Location = new Point(16, 80);
            btnComparar.Name = "btnComparar";
            btnComparar.Size = new Size(218, 40);
            btnComparar.TabIndex = 2;
            btnComparar.Text = "Comparar algoritmos";
            btnComparar.UseVisualStyleBackColor = false;
            btnComparar.Click += btnComparar_Click;
            //
            // panelAcciones
            //
            panelAcciones.AutoScroll = true;
            panelAcciones.BackColor = Color.FromArgb(13, 34, 65);
            panelAcciones.Controls.Add(lblN);
            panelAcciones.Controls.Add(nudN);
            panelAcciones.Controls.Add(btnComparar);
            panelAcciones.Dock = DockStyle.Right;
            panelAcciones.Name = "panelAcciones";
            panelAcciones.Size = new Size(250, 628);
            panelAcciones.TabIndex = 2;
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
            // FrmAnalisisAlgoritmos
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(3, 14, 32);
            Controls.Add(lstDetalle);
            Controls.Add(panelAcciones);
            Controls.Add(lblEstado);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            Name = "FrmAnalisisAlgoritmos";
            ClientSize = new Size(1000, 560);
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Análisis de algoritmos";
            Padding = new Padding(12);
            ((System.ComponentModel.ISupportInitialize)nudN).EndInit();
            panelAcciones.ResumeLayout(false);
            panelAcciones.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListBox lstDetalle;
        private Label lblN;
        private NumericUpDown nudN;
        private Button btnComparar;
        private Panel panelAcciones;
        private Label lblEstado;
    }
}
