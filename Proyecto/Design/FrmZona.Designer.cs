namespace Proyecto.Presentacion
{
    partial class FrmZona
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
            components = new System.ComponentModel.Container();
            toolStrip1 = new ToolStrip();
            btnNuevo = new ToolStripButton();
            btnGuardar = new ToolStripButton();
            btnEliminar = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            lblEstado = new ToolStripStatusLabel();
            dgvZonas = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colVegetacion = new DataGridViewTextBoxColumn();
            colArea = new DataGridViewTextBoxColumn();
            colKc = new DataGridViewTextBoxColumn();
            colHumedad = new DataGridViewTextBoxColumn();
            panelDatos = new Panel();
            label1 = new Label();
            txtId = new TextBox();
            label2 = new Label();
            txtNombre = new TextBox();
            label3 = new Label();
            cmbVegetacion = new ComboBox();
            label4 = new Label();
            nudArea = new NumericUpDown();
            label5 = new Label();
            nudX = new NumericUpDown();
            label6 = new Label();
            nudY = new NumericUpDown();
            label7 = new Label();
            nudKc = new NumericUpDown();
            label8 = new Label();
            nudHumedad = new NumericUpDown();
            label9 = new Label();
            nudRaiz = new NumericUpDown();
            errorProvider1 = new ErrorProvider(components);
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvZonas).BeginInit();
            panelDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudArea).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudKc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHumedad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRaiz).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            //
            // toolStrip1
            //
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnNuevo, btnGuardar, btnEliminar });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(860, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            //
            // btnNuevo
            //
            btnNuevo.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(46, 22);
            btnNuevo.Text = "Nuevo";
            btnNuevo.Click += btnNuevo_Click;
            //
            // btnGuardar
            //
            btnGuardar.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(53, 22);
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            //
            // btnEliminar
            //
            btnEliminar.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(54, 22);
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;
            //
            // statusStrip1
            //
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblEstado });
            statusStrip1.Location = new Point(0, 418);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(860, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            //
            // lblEstado
            //
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(31, 17);
            lblEstado.Text = "Listo";
            //
            // dgvZonas
            //
            dgvZonas.AllowUserToAddRows = false;
            dgvZonas.AllowUserToDeleteRows = false;
            dgvZonas.AutoGenerateColumns = false;
            dgvZonas.BackgroundColor = SystemColors.Window;
            dgvZonas.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colVegetacion, colArea, colKc, colHumedad });
            dgvZonas.Dock = DockStyle.Fill;
            dgvZonas.Location = new Point(0, 25);
            dgvZonas.MultiSelect = false;
            dgvZonas.Name = "dgvZonas";
            dgvZonas.ReadOnly = true;
            dgvZonas.RowHeadersVisible = false;
            dgvZonas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvZonas.Size = new Size(560, 393);
            dgvZonas.TabIndex = 2;
            dgvZonas.SelectionChanged += dgvZonas_SelectionChanged;
            //
            // colId
            //
            colId.DataPropertyName = "Id";
            colId.HeaderText = "Id";
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Width = 45;
            //
            // colNombre
            //
            colNombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNombre.DataPropertyName = "Nombre";
            colNombre.HeaderText = "Nombre";
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            //
            // colVegetacion
            //
            colVegetacion.DataPropertyName = "Vegetacion";
            colVegetacion.HeaderText = "Vegetación";
            colVegetacion.Name = "colVegetacion";
            colVegetacion.ReadOnly = true;
            colVegetacion.Width = 90;
            //
            // colArea
            //
            colArea.DataPropertyName = "AreaM2";
            colArea.HeaderText = "Área (m²)";
            colArea.Name = "colArea";
            colArea.ReadOnly = true;
            colArea.Width = 80;
            //
            // colKc
            //
            colKc.DataPropertyName = "Kc";
            colKc.HeaderText = "Kc";
            colKc.Name = "colKc";
            colKc.ReadOnly = true;
            colKc.Width = 50;
            //
            // colHumedad
            //
            colHumedad.DataPropertyName = "HumedadMinima";
            colHumedad.HeaderText = "Hum. mín (%)";
            colHumedad.Name = "colHumedad";
            colHumedad.ReadOnly = true;
            colHumedad.Width = 95;
            //
            // panelDatos
            //
            panelDatos.Controls.Add(label1);
            panelDatos.Controls.Add(txtId);
            panelDatos.Controls.Add(label2);
            panelDatos.Controls.Add(txtNombre);
            panelDatos.Controls.Add(label3);
            panelDatos.Controls.Add(cmbVegetacion);
            panelDatos.Controls.Add(label4);
            panelDatos.Controls.Add(nudArea);
            panelDatos.Controls.Add(label5);
            panelDatos.Controls.Add(nudX);
            panelDatos.Controls.Add(label6);
            panelDatos.Controls.Add(nudY);
            panelDatos.Controls.Add(label7);
            panelDatos.Controls.Add(nudKc);
            panelDatos.Controls.Add(label8);
            panelDatos.Controls.Add(nudHumedad);
            panelDatos.Controls.Add(label9);
            panelDatos.Controls.Add(nudRaiz);
            panelDatos.Dock = DockStyle.Right;
            panelDatos.Location = new Point(560, 25);
            panelDatos.Name = "panelDatos";
            panelDatos.Size = new Size(300, 393);
            panelDatos.TabIndex = 1;
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 0;
            label1.Text = "Id";
            //
            // txtId
            //
            txtId.Location = new Point(135, 12);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(140, 23);
            txtId.TabIndex = 1;
            txtId.TabStop = false;
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Location = new Point(12, 47);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 2;
            label2.Text = "Nombre";
            //
            // txtNombre
            //
            txtNombre.Location = new Point(135, 44);
            txtNombre.MaxLength = 60;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(140, 23);
            txtNombre.TabIndex = 3;
            txtNombre.Validating += txtNombre_Validating;
            //
            // label3
            //
            label3.AutoSize = true;
            label3.Location = new Point(12, 79);
            label3.Name = "label3";
            label3.Size = new Size(65, 15);
            label3.TabIndex = 4;
            label3.Text = "Vegetación";
            //
            // cmbVegetacion
            //
            cmbVegetacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVegetacion.Location = new Point(135, 76);
            cmbVegetacion.Name = "cmbVegetacion";
            cmbVegetacion.Size = new Size(140, 23);
            cmbVegetacion.TabIndex = 5;
            //
            // label4
            //
            label4.AutoSize = true;
            label4.Location = new Point(12, 111);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 6;
            label4.Text = "Área (m²)";
            //
            // nudArea
            //
            nudArea.DecimalPlaces = 2;
            nudArea.Location = new Point(135, 108);
            nudArea.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudArea.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            nudArea.Name = "nudArea";
            nudArea.Size = new Size(140, 23);
            nudArea.TabIndex = 7;
            nudArea.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // label5
            //
            label5.AutoSize = true;
            label5.Location = new Point(12, 143);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 8;
            label5.Text = "X (m)";
            //
            // nudX
            //
            nudX.DecimalPlaces = 2;
            nudX.Location = new Point(135, 140);
            nudX.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudX.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            nudX.Name = "nudX";
            nudX.Size = new Size(140, 23);
            nudX.TabIndex = 9;
            //
            // label6
            //
            label6.AutoSize = true;
            label6.Location = new Point(12, 175);
            label6.Name = "label6";
            label6.Size = new Size(36, 15);
            label6.TabIndex = 10;
            label6.Text = "Y (m)";
            //
            // nudY
            //
            nudY.DecimalPlaces = 2;
            nudY.Location = new Point(135, 172);
            nudY.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudY.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            nudY.Name = "nudY";
            nudY.Size = new Size(140, 23);
            nudY.TabIndex = 11;
            //
            // label7
            //
            label7.AutoSize = true;
            label7.Location = new Point(12, 207);
            label7.Name = "label7";
            label7.Size = new Size(108, 15);
            label7.TabIndex = 12;
            label7.Text = "Coeficiente Kc";
            //
            // nudKc
            //
            nudKc.DecimalPlaces = 2;
            nudKc.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            nudKc.Location = new Point(135, 204);
            nudKc.Maximum = new decimal(new int[] { 15, 0, 0, 65536 });
            nudKc.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            nudKc.Name = "nudKc";
            nudKc.Size = new Size(140, 23);
            nudKc.TabIndex = 13;
            nudKc.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // label8
            //
            label8.AutoSize = true;
            label8.Location = new Point(12, 239);
            label8.Name = "label8";
            label8.Size = new Size(100, 15);
            label8.TabIndex = 14;
            label8.Text = "Humedad mín (%)";
            //
            // nudHumedad
            //
            nudHumedad.DecimalPlaces = 2;
            nudHumedad.Location = new Point(135, 236);
            nudHumedad.Name = "nudHumedad";
            nudHumedad.Size = new Size(140, 23);
            nudHumedad.TabIndex = 15;
            nudHumedad.Value = new decimal(new int[] { 30, 0, 0, 0 });
            //
            // label9
            //
            label9.AutoSize = true;
            label9.Location = new Point(12, 271);
            label9.Name = "label9";
            label9.Size = new Size(95, 15);
            label9.TabIndex = 16;
            label9.Text = "Prof. de raíz (m)";
            //
            // nudRaiz
            //
            nudRaiz.DecimalPlaces = 2;
            nudRaiz.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            nudRaiz.Location = new Point(135, 268);
            nudRaiz.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            nudRaiz.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            nudRaiz.Name = "nudRaiz";
            nudRaiz.Size = new Size(140, 23);
            nudRaiz.TabIndex = 17;
            nudRaiz.Value = new decimal(new int[] { 3, 0, 0, 65536 });
            //
            // errorProvider1
            //
            errorProvider1.ContainerControl = this;
            //
            // FrmZona
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 440);
            Controls.Add(dgvZonas);
            Controls.Add(panelDatos);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "FrmZona";
            Text = "Zonas verdes";
            Load += FrmZona_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvZonas).EndInit();
            panelDatos.ResumeLayout(false);
            panelDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudArea).EndInit();
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

        private ToolStrip toolStrip1;
        private ToolStripButton btnNuevo;
        private ToolStripButton btnGuardar;
        private ToolStripButton btnEliminar;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblEstado;
        private DataGridView dgvZonas;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colVegetacion;
        private DataGridViewTextBoxColumn colArea;
        private DataGridViewTextBoxColumn colKc;
        private DataGridViewTextBoxColumn colHumedad;
        private Panel panelDatos;
        private Label label1;
        private TextBox txtId;
        private Label label2;
        private TextBox txtNombre;
        private Label label3;
        private ComboBox cmbVegetacion;
        private Label label4;
        private NumericUpDown nudArea;
        private Label label5;
        private NumericUpDown nudX;
        private Label label6;
        private NumericUpDown nudY;
        private Label label7;
        private NumericUpDown nudKc;
        private Label label8;
        private NumericUpDown nudHumedad;
        private Label label9;
        private NumericUpDown nudRaiz;
        private ErrorProvider errorProvider1;
    }
}
