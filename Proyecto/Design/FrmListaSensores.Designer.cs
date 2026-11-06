namespace Proyecto.Design
{
    partial class FrmListaSensores
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
            dgvSensores = new DataGridView();
            panelFiltro = new Panel();
            lblTitulo = new Label();
            label1 = new Label();
            cmbFiltro = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvSensores).BeginInit();
            panelFiltro.SuspendLayout();
            SuspendLayout();
            // 
            // dgvSensores
            // 
            dgvSensores.Dock = DockStyle.Fill;
            dgvSensores.Location = new Point(16, 72);
            dgvSensores.Name = "dgvSensores";
            dgvSensores.Size = new Size(868, 412);
            dgvSensores.TabIndex = 0;
            // 
            // panelFiltro
            // 
            panelFiltro.Controls.Add(lblTitulo);
            panelFiltro.Controls.Add(label1);
            panelFiltro.Controls.Add(cmbFiltro);
            panelFiltro.Dock = DockStyle.Top;
            panelFiltro.Location = new Point(16, 0);
            panelFiltro.Name = "panelFiltro";
            panelFiltro.Size = new Size(868, 72);
            panelFiltro.TabIndex = 1;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Bahnschrift Condensed", 22.25F);
            lblTitulo.Location = new Point(0, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(200, 36);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Sensores registrados";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(300, 24);
            label1.Name = "label1";
            label1.Size = new Size(40, 23);
            label1.TabIndex = 3;
            label1.Text = "Zona";
            // 
            // cmbFiltro
            // 
            cmbFiltro.BackColor = Color.FromArgb(3, 14, 32);
            cmbFiltro.DisplayMember = "Texto";
            cmbFiltro.ValueMember = "Id";
            cmbFiltro.SelectedIndexChanged += cmbFiltro_SelectedIndexChanged;
            cmbFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltro.FlatStyle = FlatStyle.Flat;
            cmbFiltro.ForeColor = Color.White;
            cmbFiltro.Location = new Point(350, 21);
            cmbFiltro.Name = "cmbFiltro";
            cmbFiltro.Size = new Size(300, 31);
            cmbFiltro.TabIndex = 0;
            // 
            // FrmListaSensores
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(3, 14, 32);
            Controls.Add(dgvSensores);
            Controls.Add(panelFiltro);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            Name = "FrmListaSensores";
            ClientSize = new Size(900, 500);
            MinimumSize = new Size(700, 400);
            MinimizeBox = false;
            Padding = new Padding(16, 0, 16, 16);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sensores registrados";
            ((System.ComponentModel.ISupportInitialize)dgvSensores).EndInit();
            panelFiltro.ResumeLayout(false);
            panelFiltro.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvSensores;
        private Panel panelFiltro;
        private Label lblTitulo;
        private Label label1;
        private ComboBox cmbFiltro;
    }
}
