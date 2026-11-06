namespace Proyecto.Design
{
    partial class FrmListaZonas
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
            dgvZonas = new DataGridView();
            lblTitulo = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvZonas).BeginInit();
            SuspendLayout();
            // 
            // dgvZonas
            // 
            dgvZonas.Dock = DockStyle.Fill;
            dgvZonas.Location = new Point(16, 72);
            dgvZonas.Name = "dgvZonas";
            dgvZonas.Size = new Size(868, 412);
            dgvZonas.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Font = new Font("Bahnschrift Condensed", 22.25F);
            lblTitulo.Location = new Point(16, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(868, 72);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Zonas registradas";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FrmListaZonas
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(3, 14, 32);
            Controls.Add(dgvZonas);
            Controls.Add(lblTitulo);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            Name = "FrmListaZonas";
            ClientSize = new Size(900, 500);
            MinimumSize = new Size(700, 400);
            MinimizeBox = false;
            Padding = new Padding(16, 0, 16, 16);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Zonas registradas";
            ((System.ComponentModel.ISupportInitialize)dgvZonas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvZonas;
        private Label lblTitulo;
    }
}
