namespace TiendaReparacion.UI
{
    partial class Dashboard
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
            pictureBox1 = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            ButtonDiagnostico = new Button();
            button5 = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            NombreUsuario = new Label();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            panel3 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.logo;
            pictureBox1.Location = new Point(3, 14);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(130, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft Sans Serif", 8.25F);
            button1.Location = new Point(3, 112);
            button1.Name = "button1";
            button1.Size = new Size(130, 40);
            button1.TabIndex = 1;
            button1.Text = "Clientes";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft Sans Serif", 8.25F);
            button2.Location = new Point(3, 158);
            button2.Name = "button2";
            button2.Size = new Size(130, 40);
            button2.TabIndex = 2;
            button2.Text = "Dispositivos";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Microsoft Sans Serif", 8.25F);
            button3.Location = new Point(3, 204);
            button3.Name = "button3";
            button3.Size = new Size(130, 40);
            button3.TabIndex = 3;
            button3.Text = "Órdenes";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // ButtonDiagnostico
            // 
            ButtonDiagnostico.Font = new Font("Microsoft Sans Serif", 8.25F);
            ButtonDiagnostico.Location = new Point(3, 250);
            ButtonDiagnostico.Name = "ButtonDiagnostico";
            ButtonDiagnostico.Size = new Size(130, 40);
            ButtonDiagnostico.TabIndex = 4;
            ButtonDiagnostico.Text = "Diagnosticos";
            ButtonDiagnostico.UseVisualStyleBackColor = true;
            ButtonDiagnostico.Click += ButtonDiagnostico_Click;
            // 
            // button5
            // 
            button5.Font = new Font("Microsoft Sans Serif", 8.25F);
            button5.Location = new Point(3, 296);
            button5.Name = "button5";
            button5.Size = new Size(130, 40);
            button5.TabIndex = 5;
            button5.Text = "Servicios";
            button5.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(button5);
            panel1.Controls.Add(ButtonDiagnostico);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(136, 604);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.Controls.Add(NombreUsuario);
            panel2.Controls.Add(pictureBox4);
            panel2.Controls.Add(pictureBox3);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(154, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(1280, 80);
            panel2.TabIndex = 2;
            // 
            // NombreUsuario
            // 
            NombreUsuario.AutoSize = true;
            NombreUsuario.Location = new Point(540, 28);
            NombreUsuario.Name = "NombreUsuario";
            NombreUsuario.Size = new Size(91, 15);
            NombreUsuario.TabIndex = 5;
            NombreUsuario.Text = "NombreUsuario";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.user;
            pictureBox4.Location = new Point(1075, 15);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(28, 28);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 3;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.noti;
            pictureBox3.Location = new Point(1239, 15);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(28, 28);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 3;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.conf;
            pictureBox2.Location = new Point(1159, 15);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 28);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cascadia Mono", 14.25F, FontStyle.Bold);
            label1.Location = new Point(3, 15);
            label1.Name = "label1";
            label1.Size = new Size(276, 25);
            label1.TabIndex = 0;
            label1.Text = "Dashboard/Menú Principal";
            // 
            // panel3
            // 
            panel3.Location = new Point(157, 98);
            panel3.Name = "panel3";
            panel3.Size = new Size(1361, 806);
            panel3.TabIndex = 3;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(1530, 938);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Dashboard";
            Text = "Dashboard";
            WindowState = FormWindowState.Maximized;
            Load += Dashboard_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button ButtonDiagnostico;
        private Button button5;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private Label label1;
        private Label NombreUsuario;
        private Panel panel3;
    }
}