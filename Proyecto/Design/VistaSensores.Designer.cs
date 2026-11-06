namespace Proyecto.Design
{
    partial class VistaSensores
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
            lienzo = new LienzoZona();
            panelAcciones = new Panel();
            label1 = new Label();
            cmbZona = new ComboBox();
            label3 = new Label();
            cmbSensor = new ComboBox();
            label2 = new Label();
            nudId = new NumericUpDown();
            btnAgregar = new Button();
            btnActualizar = new Button();
            btnDesactivar = new Button();
            btnListar = new Button();
            lblEstado = new Label();
            lblTitulo = new Label();
            ((System.ComponentModel.ISupportInitialize)nudId).BeginInit();
            panelAcciones.SuspendLayout();
            SuspendLayout();
            // 
            // lienzo
            // 
            lienzo.BackColor = Color.FromArgb(3, 14, 32);
            lienzo.Dock = DockStyle.Fill;
            lienzo.Location = new Point(16, 72);
            lienzo.Name = "lienzo";
            lienzo.Size = new Size(787, 556);
            lienzo.TabIndex = 2;
            lienzo.TabStop = false;
            // 
            // panelAcciones
            // 
            panelAcciones.Controls.Add(label1);
            panelAcciones.Controls.Add(cmbZona);
            panelAcciones.Controls.Add(label3);
            panelAcciones.Controls.Add(cmbSensor);
            panelAcciones.Controls.Add(label2);
            panelAcciones.Controls.Add(nudId);
            panelAcciones.Controls.Add(btnAgregar);
            panelAcciones.Controls.Add(btnActualizar);
            panelAcciones.Controls.Add(btnDesactivar);
            panelAcciones.Controls.Add(btnListar);
            panelAcciones.BackColor = Color.FromArgb(13, 34, 65);
            panelAcciones.Dock = DockStyle.Right;
            panelAcciones.Location = new Point(803, 0);
            panelAcciones.Name = "panelAcciones";
            panelAcciones.Size = new Size(230, 628);
            panelAcciones.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 18);
            label1.Name = "label1";
            label1.Size = new Size(40, 23);
            label1.TabIndex = 8;
            label1.Text = "Zona";
            // 
            // cmbZona
            // 
            cmbZona.BackColor = Color.FromArgb(3, 14, 32);
            cmbZona.DisplayMember = "Texto";
            cmbZona.ValueMember = "Id";
            cmbZona.SelectedIndexChanged += cmbZona_SelectedIndexChanged;
            cmbZona.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbZona.FlatStyle = FlatStyle.Flat;
            cmbZona.ForeColor = Color.White;
            cmbZona.Location = new Point(18, 44);
            cmbZona.Name = "cmbZona";
            cmbZona.Size = new Size(194, 31);
            cmbZona.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 90);
            label3.Name = "label3";
            label3.Size = new Size(50, 23);
            label3.TabIndex = 10;
            label3.Text = "Sensor";
            // 
            // cmbSensor
            // 
            cmbSensor.BackColor = Color.FromArgb(3, 14, 32);
            cmbSensor.DisplayMember = "Texto";
            cmbSensor.ValueMember = "Id";
            cmbSensor.SelectedIndexChanged += cmbSensor_SelectedIndexChanged;
            cmbSensor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSensor.FlatStyle = FlatStyle.Flat;
            cmbSensor.ForeColor = Color.White;
            cmbSensor.Location = new Point(18, 116);
            cmbSensor.Name = "cmbSensor";
            cmbSensor.Size = new Size(194, 31);
            cmbSensor.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 162);
            label2.Name = "label2";
            label2.Size = new Size(20, 23);
            label2.TabIndex = 9;
            label2.Text = "Id";
            // 
            // nudId
            // 
            nudId.BackColor = Color.FromArgb(3, 14, 32);
            nudId.BorderStyle = BorderStyle.FixedSingle;
            nudId.ForeColor = Color.White;
            nudId.Location = new Point(18, 188);
            nudId.Name = "nudId";
            nudId.Size = new Size(194, 30);
            nudId.TabIndex = 3;
            nudId.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            nudId.ValueChanged += nudId_ValueChanged;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.LimeGreen;
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.FromArgb(3, 14, 32);
            btnAgregar.Location = new Point(18, 248);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(194, 44);
            btnAgregar.TabIndex = 4;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.FromArgb(8, 86, 188);
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.ForeColor = Color.FromArgb(254, 255, 255);
            btnActualizar.Location = new Point(18, 302);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(194, 44);
            btnActualizar.TabIndex = 5;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnDesactivar
            // 
            btnDesactivar.BackColor = Color.FromArgb(214, 69, 69);
            btnDesactivar.FlatAppearance.BorderSize = 0;
            btnDesactivar.FlatStyle = FlatStyle.Flat;
            btnDesactivar.ForeColor = Color.FromArgb(254, 255, 255);
            btnDesactivar.Location = new Point(18, 356);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new Size(194, 44);
            btnDesactivar.TabIndex = 6;
            btnDesactivar.Text = "Desactivar";
            btnDesactivar.UseVisualStyleBackColor = false;
            btnDesactivar.Click += btnDesactivar_Click;
            // 
            // btnListar
            // 
            btnListar.BackColor = Color.FromArgb(3, 14, 32);
            btnListar.FlatAppearance.BorderSize = 0;
            btnListar.FlatStyle = FlatStyle.Flat;
            btnListar.ForeColor = Color.FromArgb(254, 255, 255);
            btnListar.Location = new Point(18, 410);
            btnListar.Name = "btnListar";
            btnListar.Size = new Size(194, 44);
            btnListar.TabIndex = 7;
            btnListar.Text = "Listar";
            btnListar.UseVisualStyleBackColor = false;
            btnListar.Click += btnListar_Click;
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
            lblTitulo.Size = new Size(787, 72);
            lblTitulo.TabIndex = 3;
            lblTitulo.Text = "Sensores";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // VistaSensores
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(3, 14, 32);
            Controls.Add(lienzo);
            Controls.Add(panelAcciones);
            Controls.Add(lblEstado);
            Controls.Add(lblTitulo);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            Name = "VistaSensores";
            Padding = new Padding(16, 0, 16, 0);
            Size = new Size(1049, 664);
            Load += VistaSensores_Load;
            ((System.ComponentModel.ISupportInitialize)nudId).EndInit();
            panelAcciones.ResumeLayout(false);
            panelAcciones.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private LienzoZona lienzo;
        private Panel panelAcciones;
        private Label label1;
        private ComboBox cmbZona;
        private Label label3;
        private ComboBox cmbSensor;
        private Label label2;
        private NumericUpDown nudId;
        private Button btnAgregar;
        private Button btnActualizar;
        private Button btnDesactivar;
        private Button btnListar;
        private Label lblEstado;
        private Label lblTitulo;
    }
}
