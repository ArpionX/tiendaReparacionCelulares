namespace TiendaReparacion.UI
{
    partial class GestionDispositivos
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
            ButtonEliminar = new Button();
            ButtonBuscar = new Button();
            dataGridView1 = new DataGridView();
            ButtonEditar = new Button();
            ButtonGuardar = new Button();
            button1 = new Button();
            textBox4 = new TextBox();
            textBox2 = new TextBox();
            textBox5 = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            textBox1 = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            textBox6 = new TextBox();
            label9 = new Label();
            label10 = new Label();
            textBox8 = new TextBox();
            textBox3 = new TextBox();
            textBox7 = new TextBox();
            ButtonSiguienteOrdenes = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // ButtonEliminar
            // 
            ButtonEliminar.Font = new Font("Comic Sans MS", 11.25F);
            ButtonEliminar.Location = new Point(938, 404);
            ButtonEliminar.Name = "ButtonEliminar";
            ButtonEliminar.Size = new Size(75, 34);
            ButtonEliminar.TabIndex = 42;
            ButtonEliminar.Text = "Eliminar";
            ButtonEliminar.UseVisualStyleBackColor = true;
            ButtonEliminar.Click += ButtonEliminar_Click;
            // 
            // ButtonBuscar
            // 
            ButtonBuscar.Font = new Font("Comic Sans MS", 11.25F);
            ButtonBuscar.Location = new Point(830, 404);
            ButtonBuscar.Name = "ButtonBuscar";
            ButtonBuscar.Size = new Size(75, 34);
            ButtonBuscar.TabIndex = 41;
            ButtonBuscar.Text = "Buscar";
            ButtonBuscar.UseVisualStyleBackColor = true;
            ButtonBuscar.Click += ButtonBuscar_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(311, 23);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(722, 366);
            dataGridView1.TabIndex = 40;
            dataGridView1.CellClick += DataGridView1_CellClick;
            // 
            // ButtonEditar
            // 
            ButtonEditar.Font = new Font("Comic Sans MS", 11.25F);
            ButtonEditar.Location = new Point(170, 404);
            ButtonEditar.Name = "ButtonEditar";
            ButtonEditar.Size = new Size(75, 34);
            ButtonEditar.TabIndex = 39;
            ButtonEditar.Text = "Editar";
            ButtonEditar.UseVisualStyleBackColor = true;
            ButtonEditar.Click += ButtonEditar_Click;
            // 
            // ButtonGuardar
            // 
            ButtonGuardar.Font = new Font("Comic Sans MS", 11.25F);
            ButtonGuardar.Location = new Point(90, 404);
            ButtonGuardar.Name = "ButtonGuardar";
            ButtonGuardar.Size = new Size(75, 34);
            ButtonGuardar.TabIndex = 38;
            ButtonGuardar.Text = "Guardar";
            ButtonGuardar.UseVisualStyleBackColor = true;
            ButtonGuardar.Click += ButtonGuardar_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Comic Sans MS", 11.25F);
            button1.Location = new Point(9, 404);
            button1.Name = "button1";
            button1.Size = new Size(75, 34);
            button1.TabIndex = 37;
            button1.Text = "Nuevo";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ButtonNuevo_Click;
            // 
            // textBox4
            // 
            textBox4.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBox4.Location = new Point(106, 243);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(186, 20);
            textBox4.TabIndex = 36;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBox2.Location = new Point(106, 209);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(186, 20);
            textBox2.TabIndex = 34;
            // 
            // textBox5
            // 
            textBox5.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBox5.Location = new Point(106, 174);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(186, 20);
            textBox5.TabIndex = 33;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label8.Location = new Point(12, 280);
            label8.Name = "label8";
            label8.Size = new Size(56, 18);
            label8.TabIndex = 32;
            label8.Text = "Color:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label7.Location = new Point(12, 245);
            label7.Name = "label7";
            label7.Size = new Size(48, 18);
            label7.TabIndex = 31;
            label7.Text = "IMEI:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label6.Location = new Point(12, 176);
            label6.Name = "label6";
            label6.Size = new Size(56, 18);
            label6.TabIndex = 30;
            label6.Text = "Marca:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label5.Location = new Point(12, 211);
            label5.Name = "label5";
            label5.Size = new Size(64, 18);
            label5.TabIndex = 29;
            label5.Text = "Modelo:";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBox1.Location = new Point(106, 141);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(186, 20);
            textBox1.TabIndex = 28;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label4.Location = new Point(12, 143);
            label4.Name = "label4";
            label4.Size = new Size(72, 18);
            label4.TabIndex = 27;
            label4.Text = "Cliente:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Cascadia Mono", 14.25F, FontStyle.Bold);
            label3.Location = new Point(148, 72);
            label3.Name = "label3";
            label3.Size = new Size(144, 25);
            label3.TabIndex = 26;
            label3.Text = "Dispositivos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Cascadia Mono", 14.25F, FontStyle.Bold);
            label2.Location = new Point(148, 47);
            label2.Name = "label2";
            label2.Size = new Size(34, 25);
            label2.TabIndex = 25;
            label2.Text = "De";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cascadia Mono", 14.25F, FontStyle.Bold);
            label1.Location = new Point(148, 22);
            label1.Name = "label1";
            label1.Size = new Size(100, 25);
            label1.TabIndex = 24;
            label1.Text = "Gestión ";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.gD;
            pictureBox1.Location = new Point(12, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(130, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 23;
            pictureBox1.TabStop = false;
            // 
            // textBox6
            // 
            textBox6.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBox6.Location = new Point(106, 349);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(186, 20);
            textBox6.TabIndex = 44;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label9.Location = new Point(12, 351);
            label9.Name = "label9";
            label9.Size = new Size(32, 18);
            label9.TabIndex = 43;
            label9.Text = "SO:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label10.Location = new Point(12, 317);
            label10.Name = "label10";
            label10.Size = new Size(40, 18);
            label10.TabIndex = 45;
            label10.Text = "Año:";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(509, 412);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(306, 23);
            textBox8.TabIndex = 47;
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBox3.Location = new Point(106, 278);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(186, 20);
            textBox3.TabIndex = 35;
            // 
            // textBox7
            // 
            textBox7.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBox7.Location = new Point(106, 315);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(186, 20);
            textBox7.TabIndex = 49;
            // 
            // ButtonSiguienteOrdenes
            // 
            ButtonSiguienteOrdenes.Font = new Font("Comic Sans MS", 11.25F);
            ButtonSiguienteOrdenes.Location = new Point(931, 444);
            ButtonSiguienteOrdenes.Name = "ButtonSiguienteOrdenes";
            ButtonSiguienteOrdenes.Size = new Size(102, 34);
            ButtonSiguienteOrdenes.TabIndex = 50;
            ButtonSiguienteOrdenes.Text = "Siguiente";
            ButtonSiguienteOrdenes.UseVisualStyleBackColor = true;
            ButtonSiguienteOrdenes.Click += ButtonSiguienteOrdenes_Click;
            // 
            // GestionDispositivos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(1066, 507);
            Controls.Add(ButtonSiguienteOrdenes);
            Controls.Add(textBox7);
            Controls.Add(textBox8);
            Controls.Add(label10);
            Controls.Add(textBox6);
            Controls.Add(label9);
            Controls.Add(ButtonEliminar);
            Controls.Add(ButtonBuscar);
            Controls.Add(dataGridView1);
            Controls.Add(ButtonEditar);
            Controls.Add(ButtonGuardar);
            Controls.Add(button1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox5);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GestionDispositivos";
            Text = "GestionDispositivos";
            Load += GestionDispositivos_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ButtonEliminar;
        private Button ButtonBuscar;
        private DataGridView dataGridView1;
        private Button ButtonEditar;
        private Button ButtonGuardar;
        private Button button1;
        private TextBox textBox4;
        private TextBox textBox2;
        private TextBox textBox5;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox textBox1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private PictureBox pictureBox1;
        private TextBox textBox6;
        private Label label9;
        private Label label10;
        private TextBox textBox8;
        private TextBox textBox3;
        private TextBox textBox7;
        private Button ButtonSiguienteOrdenes;
    }
}