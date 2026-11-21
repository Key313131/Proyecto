namespace Proyecto.Design
{
    partial class FrmUsuario
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
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblContrasena = new Label();
            txtContrasena = new TextBox();
            lblConfirmar = new Label();
            txtConfirmar = new TextBox();
            lblAyuda = new Label();
            btnGuardar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            //
            // lblUsuario
            //
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(12, 20);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(58, 23);
            lblUsuario.TabIndex = 10;
            lblUsuario.Text = "Usuario";
            //
            // txtUsuario
            //
            txtUsuario.BackColor = Color.FromArgb(3, 14, 32);
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.ForeColor = Color.White;
            txtUsuario.Location = new Point(170, 18);
            txtUsuario.MaxLength = 30;
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(200, 30);
            txtUsuario.TabIndex = 0;
            //
            // lblContrasena
            //
            lblContrasena.AutoSize = true;
            lblContrasena.Location = new Point(12, 65);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(82, 23);
            lblContrasena.TabIndex = 11;
            lblContrasena.Text = "Contraseña";
            //
            // txtContrasena
            //
            txtContrasena.BackColor = Color.FromArgb(3, 14, 32);
            txtContrasena.BorderStyle = BorderStyle.FixedSingle;
            txtContrasena.ForeColor = Color.White;
            txtContrasena.Location = new Point(170, 63);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(200, 30);
            txtContrasena.TabIndex = 1;
            txtContrasena.UseSystemPasswordChar = true;
            //
            // lblConfirmar
            //
            lblConfirmar.AutoSize = true;
            lblConfirmar.Location = new Point(12, 110);
            lblConfirmar.Name = "lblConfirmar";
            lblConfirmar.Size = new Size(147, 23);
            lblConfirmar.TabIndex = 12;
            lblConfirmar.Text = "Confirmar contraseña";
            //
            // txtConfirmar
            //
            txtConfirmar.BackColor = Color.FromArgb(3, 14, 32);
            txtConfirmar.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmar.ForeColor = Color.White;
            txtConfirmar.Location = new Point(170, 108);
            txtConfirmar.Name = "txtConfirmar";
            txtConfirmar.Size = new Size(200, 30);
            txtConfirmar.TabIndex = 2;
            txtConfirmar.UseSystemPasswordChar = true;
            //
            // lblAyuda
            //
            lblAyuda.Font = new Font("Bahnschrift Condensed", 11.25F);
            lblAyuda.ForeColor = Color.FromArgb(174, 180, 194);
            lblAyuda.Location = new Point(12, 150);
            lblAyuda.Name = "lblAyuda";
            lblAyuda.Size = new Size(358, 22);
            lblAyuda.TabIndex = 13;
            lblAyuda.Text = "La contraseña debe tener al menos 6 caracteres.";
            //
            // btnGuardar
            //
            btnGuardar.BackColor = Color.LimeGreen;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.FromArgb(3, 14, 32);
            btnGuardar.Location = new Point(275, 186);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(95, 44);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "Registrar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            //
            // btnCancelar
            //
            btnCancelar.BackColor = Color.FromArgb(13, 34, 65);
            btnCancelar.CausesValidation = false;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.ForeColor = Color.FromArgb(254, 255, 255);
            btnCancelar.Location = new Point(170, 186);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(95, 44);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            //
            // FrmUsuario
            //
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(3, 14, 32);
            CancelButton = btnCancelar;
            ClientSize = new Size(400, 245);
            Controls.Add(lblUsuario);
            Controls.Add(txtUsuario);
            Controls.Add(lblContrasena);
            Controls.Add(txtContrasena);
            Controls.Add(lblConfirmar);
            Controls.Add(txtConfirmar);
            Controls.Add(lblAyuda);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            Font = new Font("Bahnschrift Condensed", 14.25F);
            ForeColor = Color.FromArgb(254, 255, 255);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmUsuario";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar usuario";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblContrasena;
        private TextBox txtContrasena;
        private Label lblConfirmar;
        private TextBox txtConfirmar;
        private Label lblAyuda;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}
