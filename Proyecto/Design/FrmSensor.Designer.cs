namespace Proyecto.Presentacion
{
    partial class FrmSensor
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
            toolStrip1 = new ToolStrip();
            btnNuevo = new ToolStripButton();
            btnGuardar = new ToolStripButton();
            btnEliminar = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            lblEstado = new ToolStripStatusLabel();
            dgvSensores = new DataGridView();
            panelDatos = new Panel();
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
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSensores).BeginInit();
            panelDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudY).BeginInit();
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
            statusStrip1.Location = new Point(0, 318);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(860, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblEstado
            // 
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(32, 17);
            lblEstado.Text = "Listo";
            // 
            // dgvSensores
            // 
            dgvSensores.AllowUserToAddRows = false;
            dgvSensores.AllowUserToDeleteRows = false;
            dgvSensores.BackgroundColor = SystemColors.Window;
            dgvSensores.Dock = DockStyle.Fill;
            dgvSensores.Location = new Point(0, 25);
            dgvSensores.MultiSelect = false;
            dgvSensores.Name = "dgvSensores";
            dgvSensores.ReadOnly = true;
            dgvSensores.RowHeadersVisible = false;
            dgvSensores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSensores.Size = new Size(560, 293);
            dgvSensores.TabIndex = 2;
            dgvSensores.SelectionChanged += dgvSensores_SelectionChanged;
            // 
            // panelDatos
            // 
            panelDatos.Controls.Add(label1);
            panelDatos.Controls.Add(txtId);
            panelDatos.Controls.Add(label2);
            panelDatos.Controls.Add(cmbZona);
            panelDatos.Controls.Add(label3);
            panelDatos.Controls.Add(cmbTipo);
            panelDatos.Controls.Add(label4);
            panelDatos.Controls.Add(nudX);
            panelDatos.Controls.Add(label5);
            panelDatos.Controls.Add(nudY);
            panelDatos.Controls.Add(chkActivo);
            panelDatos.Dock = DockStyle.Right;
            panelDatos.Location = new Point(560, 25);
            panelDatos.Name = "panelDatos";
            panelDatos.Size = new Size(300, 293);
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
            label2.Size = new Size(34, 15);
            label2.TabIndex = 2;
            label2.Text = "Zona";
            // 
            // cmbZona
            // 
            cmbZona.DisplayMember = "Nombre";
            cmbZona.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbZona.Location = new Point(135, 44);
            cmbZona.Name = "cmbZona";
            cmbZona.Size = new Size(140, 23);
            cmbZona.TabIndex = 3;
            cmbZona.ValueMember = "Id";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 79);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 4;
            label3.Text = "Tipo";
            // 
            // cmbTipo
            // 
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Location = new Point(135, 76);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(140, 23);
            cmbTipo.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 111);
            label4.Name = "label4";
            label4.Size = new Size(36, 15);
            label4.TabIndex = 6;
            label4.Text = "X (m)";
            // 
            // nudX
            // 
            nudX.DecimalPlaces = 2;
            nudX.Location = new Point(135, 108);
            nudX.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudX.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            nudX.Name = "nudX";
            nudX.Size = new Size(140, 23);
            nudX.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 143);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 8;
            label5.Text = "Y (m)";
            // 
            // nudY
            // 
            nudY.DecimalPlaces = 2;
            nudY.Location = new Point(135, 140);
            nudY.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudY.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            nudY.Name = "nudY";
            nudY.Size = new Size(140, 23);
            nudY.TabIndex = 9;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.Location = new Point(135, 174);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(60, 19);
            chkActivo.TabIndex = 10;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = true;
            // 
            // FrmSensor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 340);
            Controls.Add(dgvSensores);
            Controls.Add(panelDatos);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "FrmSensor";
            Text = "Sensores";
            Activated += FrmSensor_Activated;
            Load += FrmSensor_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSensores).EndInit();
            panelDatos.ResumeLayout(false);
            panelDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudX).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudY).EndInit();
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
        private DataGridView dgvSensores;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colZona;
        private DataGridViewTextBoxColumn colTipo;
        private DataGridViewTextBoxColumn colUnidad;
        private DataGridViewTextBoxColumn colX;
        private DataGridViewTextBoxColumn colY;
        private DataGridViewCheckBoxColumn colActivo;
        private Panel panelDatos;
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
    }
}
