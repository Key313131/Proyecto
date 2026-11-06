namespace Proyecto.Design
{
    partial class FrmZona
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            txtId = new TextBox();
            label2 = new Label();
            txtNombre = new TextBox();
            label3 = new Label();
            cmbVegetacion = new ComboBox();
            label4 = new Label();
            nudAncho = new NumericUpDown();
            label5 = new Label();
            nudLargo = new NumericUpDown();
            label6 = new Label();
            nudX = new NumericUpDown();
            label7 = new Label();
            nudY = new NumericUpDown();
            label8 = new Label();
            nudKc = new NumericUpDown();
            label9 = new Label();
            nudHumedad = new NumericUpDown();
            label10 = new Label();
            nudRaiz = new NumericUpDown();
            chkActivo = new CheckBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)nudAncho).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLargo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudKc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHumedad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRaiz).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
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
            label2.Size = new Size(60, 23);
            label2.TabIndex = 31;
            label2.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(3, 14, 32);
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.ForeColor = Color.White;
            txtNombre.Location = new Point(170, 62);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(200, 30);
            txtNombre.TabIndex = 1;
            txtNombre.MaxLength = 60;
            txtNombre.Validating += txtNombre_Validating;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 109);
            label3.Name = "label3";
            label3.Size = new Size(76, 23);
            label3.TabIndex = 32;
            label3.Text = "Vegetación";
            // 
            // cmbVegetacion
            // 
            cmbVegetacion.BackColor = Color.FromArgb(3, 14, 32);
            cmbVegetacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVegetacion.FlatStyle = FlatStyle.Flat;
            cmbVegetacion.ForeColor = Color.White;
            cmbVegetacion.Location = new Point(170, 106);
            cmbVegetacion.Name = "cmbVegetacion";
            cmbVegetacion.Size = new Size(200, 31);
            cmbVegetacion.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 153);
            label4.Name = "label4";
            label4.Size = new Size(70, 23);
            label4.TabIndex = 33;
            label4.Text = "Ancho (m)";
            // 
            // nudAncho
            // 
            nudAncho.BackColor = Color.FromArgb(3, 14, 32);
            nudAncho.BorderStyle = BorderStyle.FixedSingle;
            nudAncho.ForeColor = Color.White;
            nudAncho.Location = new Point(170, 150);
            nudAncho.Name = "nudAncho";
            nudAncho.Size = new Size(200, 30);
            nudAncho.TabIndex = 3;
            nudAncho.DecimalPlaces = 2;
            nudAncho.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudAncho.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            nudAncho.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 197);
            label5.Name = "label5";
            label5.Size = new Size(70, 23);
            label5.TabIndex = 34;
            label5.Text = "Largo (m)";
            // 
            // nudLargo
            // 
            nudLargo.BackColor = Color.FromArgb(3, 14, 32);
            nudLargo.BorderStyle = BorderStyle.FixedSingle;
            nudLargo.ForeColor = Color.White;
            nudLargo.Location = new Point(170, 194);
            nudLargo.Name = "nudLargo";
            nudLargo.Size = new Size(200, 30);
            nudLargo.TabIndex = 4;
            nudLargo.DecimalPlaces = 2;
            nudLargo.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudLargo.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            nudLargo.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 241);
            label6.Name = "label6";
            label6.Size = new Size(50, 23);
            label6.TabIndex = 35;
            label6.Text = "X (m)";
            // 
            // nudX
            // 
            nudX.BackColor = Color.FromArgb(3, 14, 32);
            nudX.BorderStyle = BorderStyle.FixedSingle;
            nudX.ForeColor = Color.White;
            nudX.Location = new Point(170, 238);
            nudX.Name = "nudX";
            nudX.Size = new Size(200, 30);
            nudX.TabIndex = 5;
            nudX.DecimalPlaces = 2;
            nudX.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudX.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(20, 285);
            label7.Name = "label7";
            label7.Size = new Size(50, 23);
            label7.TabIndex = 36;
            label7.Text = "Y (m)";
            // 
            // nudY
            // 
            nudY.BackColor = Color.FromArgb(3, 14, 32);
            nudY.BorderStyle = BorderStyle.FixedSingle;
            nudY.ForeColor = Color.White;
            nudY.Location = new Point(170, 282);
            nudY.Name = "nudY";
            nudY.Size = new Size(200, 30);
            nudY.TabIndex = 6;
            nudY.DecimalPlaces = 2;
            nudY.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudY.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(20, 329);
            label8.Name = "label8";
            label8.Size = new Size(99, 23);
            label8.TabIndex = 37;
            label8.Text = "Coeficiente Kc";
            // 
            // nudKc
            // 
            nudKc.BackColor = Color.FromArgb(3, 14, 32);
            nudKc.BorderStyle = BorderStyle.FixedSingle;
            nudKc.ForeColor = Color.White;
            nudKc.Location = new Point(170, 326);
            nudKc.Name = "nudKc";
            nudKc.Size = new Size(200, 30);
            nudKc.TabIndex = 7;
            nudKc.DecimalPlaces = 2;
            nudKc.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            nudKc.Maximum = new decimal(new int[] { 15, 0, 0, 65536 });
            nudKc.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            nudKc.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(20, 373);
            label9.Name = "label9";
            label9.Size = new Size(109, 23);
            label9.TabIndex = 38;
            label9.Text = "Humedad mín (%)";
            // 
            // nudHumedad
            // 
            nudHumedad.BackColor = Color.FromArgb(3, 14, 32);
            nudHumedad.BorderStyle = BorderStyle.FixedSingle;
            nudHumedad.ForeColor = Color.White;
            nudHumedad.Location = new Point(170, 370);
            nudHumedad.Name = "nudHumedad";
            nudHumedad.Size = new Size(200, 30);
            nudHumedad.TabIndex = 8;
            nudHumedad.DecimalPlaces = 2;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(20, 417);
            label10.Name = "label10";
            label10.Size = new Size(106, 23);
            label10.TabIndex = 39;
            label10.Text = "Prof. de raíz (m)";
            // 
            // nudRaiz
            // 
            nudRaiz.BackColor = Color.FromArgb(3, 14, 32);
            nudRaiz.BorderStyle = BorderStyle.FixedSingle;
            nudRaiz.ForeColor = Color.White;
            nudRaiz.Location = new Point(170, 414);
            nudRaiz.Name = "nudRaiz";
            nudRaiz.Size = new Size(200, 30);
            nudRaiz.TabIndex = 9;
            nudRaiz.DecimalPlaces = 2;
            nudRaiz.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            nudRaiz.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            nudRaiz.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            nudRaiz.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Location = new Point(170, 458);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(70, 27);
            chkActivo.TabIndex = 10;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.LimeGreen;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.FromArgb(3, 14, 32);
            btnGuardar.Location = new Point(275, 502);
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
            btnCancelar.Location = new Point(170, 502);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(95, 44);
            btnCancelar.TabIndex = 21;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // FrmZona
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(3, 14, 32);
            Controls.Add(label1);
            Controls.Add(txtId);
            Controls.Add(label2);
            Controls.Add(txtNombre);
            Controls.Add(label3);
            Controls.Add(cmbVegetacion);
            Controls.Add(label4);
            Controls.Add(nudAncho);
            Controls.Add(label5);
            Controls.Add(nudLargo);
            Controls.Add(label6);
            Controls.Add(nudX);
            Controls.Add(label7);
            Controls.Add(nudY);
            Controls.Add(label8);
            Controls.Add(nudKc);
            Controls.Add(label9);
            Controls.Add(nudHumedad);
            Controls.Add(label10);
            Controls.Add(nudRaiz);
            Controls.Add(chkActivo);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            Name = "FrmZona";
            ClientSize = new Size(400, 560);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Zona verde";
            Load += FrmZona_Load;
            ((System.ComponentModel.ISupportInitialize)nudAncho).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLargo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudX).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudKc).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHumedad).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRaiz).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtId;
        private Label label2;
        private TextBox txtNombre;
        private Label label3;
        private ComboBox cmbVegetacion;
        private Label label4;
        private NumericUpDown nudAncho;
        private Label label5;
        private NumericUpDown nudLargo;
        private Label label6;
        private NumericUpDown nudX;
        private Label label7;
        private NumericUpDown nudY;
        private Label label8;
        private NumericUpDown nudKc;
        private Label label9;
        private NumericUpDown nudHumedad;
        private Label label10;
        private NumericUpDown nudRaiz;
        private CheckBox chkActivo;
        private Button btnGuardar;
        private Button btnCancelar;
        private ErrorProvider errorProvider1;
    }
}
