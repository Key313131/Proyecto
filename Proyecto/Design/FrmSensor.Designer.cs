namespace Proyecto.Design
{
    partial class FrmSensor
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
            label1 = new Label();
            txtId = new TextBox();
            label2 = new Label();
            cmbZona = new ComboBox();
            label3 = new Label();
            cmbTipo = new ComboBox();
            label4 = new Label();
            nudX = new NumericUpDown();
            label5 = new Label();
            nudY = new NumericUpDown();
            chkActivo = new CheckBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)nudX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudY).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 21);
            label1.Name = "label1";
            label1.Size = new Size(20, 23);
            label1.TabIndex = 30;
            label1.Text = "Id";
            // 
            // txtId
            // 
            txtId.BackColor = Color.FromArgb(3, 14, 32);
            txtId.BorderStyle = BorderStyle.FixedSingle;
            txtId.ForeColor = Color.White;
            txtId.Location = new Point(170, 18);
            txtId.Name = "txtId";
            txtId.Size = new Size(200, 30);
            txtId.TabIndex = 0;
            txtId.ReadOnly = true;
            txtId.TabStop = false;
            txtId.ForeColor = Color.FromArgb(174, 180, 194);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 65);
            label2.Name = "label2";
            label2.Size = new Size(40, 23);
            label2.TabIndex = 31;
            label2.Text = "Zona";
            // 
            // cmbZona
            // 
            cmbZona.BackColor = Color.FromArgb(3, 14, 32);
            cmbZona.DisplayMember = "Nombre";
            cmbZona.ValueMember = "Id";
            cmbZona.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbZona.FlatStyle = FlatStyle.Flat;
            cmbZona.ForeColor = Color.White;
            cmbZona.Location = new Point(170, 62);
            cmbZona.Name = "cmbZona";
            cmbZona.Size = new Size(200, 31);
            cmbZona.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 109);
            label3.Name = "label3";
            label3.Size = new Size(36, 23);
            label3.TabIndex = 32;
            label3.Text = "Tipo";
            // 
            // cmbTipo
            // 
            cmbTipo.BackColor = Color.FromArgb(3, 14, 32);
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.FlatStyle = FlatStyle.Flat;
            cmbTipo.ForeColor = Color.White;
            cmbTipo.Location = new Point(170, 106);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(200, 31);
            cmbTipo.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 153);
            label4.Name = "label4";
            label4.Size = new Size(140, 23);
            label4.TabIndex = 33;
            label4.Text = "X desde la esquina (m)";
            // 
            // nudX
            // 
            nudX.BackColor = Color.FromArgb(3, 14, 32);
            nudX.BorderStyle = BorderStyle.FixedSingle;
            nudX.ForeColor = Color.White;
            nudX.Location = new Point(170, 150);
            nudX.Name = "nudX";
            nudX.Size = new Size(200, 30);
            nudX.TabIndex = 3;
            nudX.DecimalPlaces = 2;
            nudX.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 197);
            label5.Name = "label5";
            label5.Size = new Size(140, 23);
            label5.TabIndex = 34;
            label5.Text = "Y desde la esquina (m)";
            // 
            // nudY
            // 
            nudY.BackColor = Color.FromArgb(3, 14, 32);
            nudY.BorderStyle = BorderStyle.FixedSingle;
            nudY.ForeColor = Color.White;
            nudY.Location = new Point(170, 194);
            nudY.Name = "nudY";
            nudY.Size = new Size(200, 30);
            nudY.TabIndex = 4;
            nudY.DecimalPlaces = 2;
            nudY.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Location = new Point(170, 238);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(70, 27);
            chkActivo.TabIndex = 5;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.LimeGreen;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.FromArgb(3, 14, 32);
            btnGuardar.Location = new Point(275, 282);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(95, 44);
            btnGuardar.TabIndex = 20;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.CausesValidation = false;
            btnCancelar.BackColor = Color.FromArgb(13, 34, 65);
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.ForeColor = Color.FromArgb(254, 255, 255);
            btnCancelar.Location = new Point(170, 282);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(95, 44);
            btnCancelar.TabIndex = 21;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FrmSensor
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(3, 14, 32);
            Controls.Add(label1);
            Controls.Add(txtId);
            Controls.Add(label2);
            Controls.Add(cmbZona);
            Controls.Add(label3);
            Controls.Add(cmbTipo);
            Controls.Add(label4);
            Controls.Add(nudX);
            Controls.Add(label5);
            Controls.Add(nudY);
            Controls.Add(chkActivo);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            Name = "FrmSensor";
            ClientSize = new Size(400, 340);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sensor";
            Load += FrmSensor_Load;
            ((System.ComponentModel.ISupportInitialize)nudX).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudY).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtId;
        private Label label2;
        private ComboBox cmbZona;
        private Label label3;
        private ComboBox cmbTipo;
        private Label label4;
        private NumericUpDown nudX;
        private Label label5;
        private NumericUpDown nudY;
        private CheckBox chkActivo;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}
