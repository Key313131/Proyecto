namespace Proyecto
{
    partial class Sesion
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelLogin = new Panel();
            picVerContrasena = new PictureBox();
            txtUsuario = new TextBox();
            btnIngresar = new Button();
            lblContrasena = new Label();
            txtContrasena = new TextBox();
            lblUsuario = new Label();
            lblTitulo = new Label();
            picLogo = new PictureBox();
            panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picVerContrasena).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // panelLogin
            // 
            panelLogin.BackColor = Color.FromArgb(13, 34, 65);
            panelLogin.BorderStyle = BorderStyle.FixedSingle;
            panelLogin.Controls.Add(picVerContrasena);
            panelLogin.Controls.Add(txtUsuario);
            panelLogin.Controls.Add(btnIngresar);
            panelLogin.Controls.Add(lblContrasena);
            panelLogin.Controls.Add(txtContrasena);
            panelLogin.Controls.Add(lblUsuario);
            panelLogin.Location = new Point(388, 165);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(381, 343);
            panelLogin.TabIndex = 0;
            // 
            // picVerContrasena
            // 
            picVerContrasena.BackColor = Color.FromArgb(3, 14, 32);
            picVerContrasena.Image = Properties.Resources.visibility_off_24dp_E3E3E3_FILL0_wght400_GRAD0_opsz24;
            picVerContrasena.Location = new Point(318, 184);
            picVerContrasena.Name = "picVerContrasena";
            picVerContrasena.Size = new Size(45, 34);
            picVerContrasena.SizeMode = PictureBoxSizeMode.Zoom;
            picVerContrasena.TabIndex = 6;
            picVerContrasena.TabStop = false;
            picVerContrasena.Cursor = Cursors.Hand;
            picVerContrasena.Click += picVerContrasena_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = Color.FromArgb(3, 14, 32);
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.Font = new Font("Bahnschrift Condensed", 18.25F);
            txtUsuario.ForeColor = Color.White;
            txtUsuario.Location = new Point(15, 83);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "   Usuario";
            txtUsuario.Size = new Size(351, 43);
            txtUsuario.TabIndex = 1;
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = Color.LimeGreen;
            btnIngresar.FlatStyle = FlatStyle.Popup;
            btnIngresar.Font = new Font("Bahnschrift Condensed", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnIngresar.ForeColor = Color.Transparent;
            btnIngresar.Location = new Point(15, 245);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(348, 45);
            btnIngresar.TabIndex = 3;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // lblContrasena
            // 
            lblContrasena.AutoSize = true;
            lblContrasena.Font = new Font("Bahnschrift Condensed", 18.25F);
            lblContrasena.ForeColor = Color.FromArgb(254, 255, 255);
            lblContrasena.Location = new Point(15, 140);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(106, 30);
            lblContrasena.TabIndex = 5;
            lblContrasena.Text = "Contraseña";
            // 
            // txtContrasena
            // 
            txtContrasena.BackColor = Color.FromArgb(3, 14, 32);
            txtContrasena.BorderStyle = BorderStyle.FixedSingle;
            txtContrasena.Font = new Font("Bahnschrift Condensed", 18.25F);
            txtContrasena.ForeColor = Color.White;
            txtContrasena.Location = new Point(15, 180);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PlaceholderText = "   Contraseña";
            txtContrasena.Size = new Size(351, 43);
            txtContrasena.TabIndex = 2;
            txtContrasena.UseSystemPasswordChar = true;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Bahnschrift Condensed", 18.25F);
            lblUsuario.ForeColor = Color.FromArgb(254, 255, 255);
            lblUsuario.Location = new Point(15, 35);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(147, 30);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "Usuario";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Bahnschrift Condensed", 22.25F);
            lblTitulo.ForeColor = Color.FromArgb(254, 255, 255);
            lblTitulo.Location = new Point(458, 106);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(283, 36);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Inicia sesión en EcoFacility";
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Image = Properties.Resources.image_removebg_preview;
            picLogo.Location = new Point(493, 12);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(170, 101);
            picLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            picLogo.TabIndex = 2;
            picLogo.TabStop = false;
            // 
            // Sesion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(3, 14, 32);
            ClientSize = new Size(1197, 622);
            Controls.Add(picLogo);
            Controls.Add(lblTitulo);
            Controls.Add(panelLogin);
            MaximizeBox = false;
            AcceptButton = btnIngresar;
            Name = "Sesion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EcoFacility - Iniciar sesión";
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picVerContrasena).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelLogin;
        private Label lblTitulo;
        private PictureBox picLogo;
        private Label lblUsuario;
        private Label lblContrasena;
        private TextBox txtContrasena;
        private Button btnIngresar;
        private TextBox txtUsuario;
        private PictureBox picVerContrasena;
    }
}
