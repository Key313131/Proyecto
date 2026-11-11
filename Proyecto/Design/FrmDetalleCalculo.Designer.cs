namespace Proyecto.Design
{
    partial class FrmDetalleCalculo
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
            // FrmDetalleCalculo
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(3, 14, 32);
            Controls.Add(lstDetalle);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            Name = "FrmDetalleCalculo";
            ClientSize = new Size(1100, 600);
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cálculo detallado del plan de riego";
            Padding = new Padding(12);
            ResumeLayout(false);
        }

        #endregion

        private ListBox lstDetalle;
    }
}
