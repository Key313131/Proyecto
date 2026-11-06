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
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            textBox1 = new TextBox();
            button1 = new Button();
            label3 = new Label();
            textBox2 = new TextBox();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(13, 34, 65);
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(388, 165);
            panel1.Name = "panel1";
            panel1.Size = new Size(381, 343);
            panel1.TabIndex = 0;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(3, 14, 32);
            pictureBox2.Image = Properties.Resources.visibility_off_24dp_E3E3E3_FILL0_wght400_GRAD0_opsz24;
            pictureBox2.Location = new Point(318, 184);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(45, 34);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(3, 14, 32);
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Font = new Font("Bahnschrift Condensed", 18.25F);
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(15, 83);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "   Usuario";
            textBox1.Size = new Size(351, 43);
            textBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.BackColor = Color.LimeGreen;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Bahnschrift Condensed", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(15, 245);
            button1.Name = "button1";
            button1.Size = new Size(348, 45);
            button1.TabIndex = 0;
            button1.Text = "Ingresar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Bahnschrift Condensed", 18.25F);
            label3.ForeColor = Color.FromArgb(254, 255, 255);
            label3.Location = new Point(15, 140);
            label3.Name = "label3";
            label3.Size = new Size(106, 30);
            label3.TabIndex = 5;
            label3.Text = "Contraseña";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(3, 14, 32);
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Font = new Font("Bahnschrift Condensed", 18.25F);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(15, 180);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "   Contraseña";
            textBox2.Size = new Size(351, 43);
            textBox2.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Bahnschrift Condensed", 18.25F);
            label2.ForeColor = Color.FromArgb(254, 255, 255);
            label2.Location = new Point(15, 35);
            label2.Name = "label2";
            label2.Size = new Size(147, 30);
            label2.TabIndex = 3;
            label2.Text = "Usuario o Correo";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Bahnschrift Condensed", 22.25F);
            label1.ForeColor = Color.FromArgb(254, 255, 255);
            label1.Location = new Point(458, 106);
            label1.Name = "label1";
            label1.Size = new Size(283, 36);
            label1.TabIndex = 1;
            label1.Text = "Inicia Sesion en EcoFacility";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.image_removebg_preview;
            pictureBox1.Location = new Point(493, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(170, 101);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(3, 14, 32);
            ClientSize = new Size(1197, 622);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(panel1);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label3;
        private TextBox textBox2;
        private Button button1;
        private TextBox textBox1;
        private PictureBox pictureBox2;
    }
}
