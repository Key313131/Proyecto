namespace Proyecto.Presentacion
{
    partial class Principal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            salirToolStripMenuItem = new ToolStripMenuItem();
            gestionToolStripMenuItem = new ToolStripMenuItem();
            zonasVerdesToolStripMenuItem = new ToolStripMenuItem();
            sensoresToolStripMenuItem = new ToolStripMenuItem();
            ventanaToolStripMenuItem = new ToolStripMenuItem();
            cascadaToolStripMenuItem = new ToolStripMenuItem();
            mosaicoToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            lblDatos = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            //
            // menuStrip1
            //
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem, gestionToolStripMenuItem, ventanaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.MdiWindowListItem = ventanaToolStripMenuItem;
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1000, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            //
            // archivoToolStripMenuItem
            //
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { salirToolStripMenuItem });
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(60, 20);
            archivoToolStripMenuItem.Text = "&Archivo";
            //
            // salirToolStripMenuItem
            //
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(96, 22);
            salirToolStripMenuItem.Text = "&Salir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            //
            // gestionToolStripMenuItem
            //
            gestionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { zonasVerdesToolStripMenuItem, sensoresToolStripMenuItem });
            gestionToolStripMenuItem.Name = "gestionToolStripMenuItem";
            gestionToolStripMenuItem.Size = new Size(60, 20);
            gestionToolStripMenuItem.Text = "&Gestión";
            //
            // zonasVerdesToolStripMenuItem
            //
            zonasVerdesToolStripMenuItem.Name = "zonasVerdesToolStripMenuItem";
            zonasVerdesToolStripMenuItem.Size = new Size(143, 22);
            zonasVerdesToolStripMenuItem.Text = "&Zonas verdes";
            zonasVerdesToolStripMenuItem.Click += zonasVerdesToolStripMenuItem_Click;
            //
            // sensoresToolStripMenuItem
            //
            sensoresToolStripMenuItem.Name = "sensoresToolStripMenuItem";
            sensoresToolStripMenuItem.Size = new Size(143, 22);
            sensoresToolStripMenuItem.Text = "&Sensores";
            sensoresToolStripMenuItem.Click += sensoresToolStripMenuItem_Click;
            //
            // ventanaToolStripMenuItem
            //
            ventanaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cascadaToolStripMenuItem, mosaicoToolStripMenuItem });
            ventanaToolStripMenuItem.Name = "ventanaToolStripMenuItem";
            ventanaToolStripMenuItem.Size = new Size(61, 20);
            ventanaToolStripMenuItem.Text = "&Ventana";
            //
            // cascadaToolStripMenuItem
            //
            cascadaToolStripMenuItem.Name = "cascadaToolStripMenuItem";
            cascadaToolStripMenuItem.Size = new Size(120, 22);
            cascadaToolStripMenuItem.Text = "&Cascada";
            cascadaToolStripMenuItem.Click += cascadaToolStripMenuItem_Click;
            //
            // mosaicoToolStripMenuItem
            //
            mosaicoToolStripMenuItem.Name = "mosaicoToolStripMenuItem";
            mosaicoToolStripMenuItem.Size = new Size(120, 22);
            mosaicoToolStripMenuItem.Text = "&Mosaico";
            mosaicoToolStripMenuItem.Click += mosaicoToolStripMenuItem_Click;
            //
            // statusStrip1
            //
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblDatos });
            statusStrip1.Location = new Point(0, 628);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1000, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            //
            // lblDatos
            //
            lblDatos.Name = "lblDatos";
            lblDatos.Size = new Size(31, 17);
            lblDatos.Text = "Listo";
            //
            // Principal
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "Principal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EcoFacility - Gestión hídrica de zonas verdes";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem salirToolStripMenuItem;
        private ToolStripMenuItem gestionToolStripMenuItem;
        private ToolStripMenuItem zonasVerdesToolStripMenuItem;
        private ToolStripMenuItem sensoresToolStripMenuItem;
        private ToolStripMenuItem ventanaToolStripMenuItem;
        private ToolStripMenuItem cascadaToolStripMenuItem;
        private ToolStripMenuItem mosaicoToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblDatos;
    }
}
