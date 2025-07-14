namespace TiendaReparacion.UI
{
    partial class OrdenesServicio
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
            label10 = new Label();
            textBoxDescripcion = new TextBox();
            label9 = new Label();
            ButtonGuardar = new Button();
            ButtonEditar = new Button();
            dataGridView1 = new DataGridView();
            ButtonLimpiarCampos = new Button();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            textBoxClienteCedula = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            comboBoxEstado = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            textBoxObservaciones = new TextBox();
            label11 = new Label();
            textBoxDispositivoImei = new TextBox();
            textBoxTecnico = new TextBox();
            ButtonEliminar = new Button();
            ButtonSiguiente = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label10.Location = new Point(14, 296);
            label10.Name = "label10";
            label10.Size = new Size(128, 18);
            label10.TabIndex = 69;
            label10.Text = "Fecha Estimada:";
            // 
            // textBoxDescripcion
            // 
            textBoxDescripcion.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBoxDescripcion.Location = new Point(14, 351);
            textBoxDescripcion.Multiline = true;
            textBoxDescripcion.Name = "textBoxDescripcion";
            textBoxDescripcion.Size = new Size(338, 38);
            textBoxDescripcion.TabIndex = 68;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label9.Location = new Point(14, 330);
            label9.Name = "label9";
            label9.Size = new Size(208, 18);
            label9.TabIndex = 67;
            label9.Text = "Descripción del Problema:";
            // 
            // ButtonGuardar
            // 
            ButtonGuardar.Font = new Font("Comic Sans MS", 11.25F);
            ButtonGuardar.Location = new Point(1290, 544);
            ButtonGuardar.Name = "ButtonGuardar";
            ButtonGuardar.Size = new Size(75, 34);
            ButtonGuardar.TabIndex = 66;
            ButtonGuardar.Text = "Guardar";
            ButtonGuardar.UseVisualStyleBackColor = true;
            ButtonGuardar.Click += ButtonGuardar_Click;
            // 
            // ButtonEditar
            // 
            ButtonEditar.Font = new Font("Comic Sans MS", 11.25F);
            ButtonEditar.Location = new Point(1177, 544);
            ButtonEditar.Name = "ButtonEditar";
            ButtonEditar.Size = new Size(75, 34);
            ButtonEditar.TabIndex = 65;
            ButtonEditar.Text = "Editar";
            ButtonEditar.UseVisualStyleBackColor = true;
            ButtonEditar.Click += ButtonEditar_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(368, 23);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1014, 505);
            dataGridView1.TabIndex = 64;
            dataGridView1.CellClick += DataGridView1_CellClick;
            // 
            // ButtonLimpiarCampos
            // 
            ButtonLimpiarCampos.Font = new Font("Comic Sans MS", 11.25F);
            ButtonLimpiarCampos.Location = new Point(397, 534);
            ButtonLimpiarCampos.Name = "ButtonLimpiarCampos";
            ButtonLimpiarCampos.Size = new Size(115, 34);
            ButtonLimpiarCampos.TabIndex = 63;
            ButtonLimpiarCampos.Text = "Limpiar Campos";
            ButtonLimpiarCampos.UseVisualStyleBackColor = true;
            ButtonLimpiarCampos.Click += ButtonLimpiarCampos_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label8.Location = new Point(14, 260);
            label8.Name = "label8";
            label8.Size = new Size(120, 18);
            label8.TabIndex = 56;
            label8.Text = "Fecha Ingreso:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label7.Location = new Point(14, 225);
            label7.Name = "label7";
            label7.Size = new Size(72, 18);
            label7.TabIndex = 55;
            label7.Text = "Estado: ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label6.Location = new Point(14, 156);
            label6.Name = "label6";
            label6.Size = new Size(112, 18);
            label6.TabIndex = 54;
            label6.Text = "Dispositivos:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label5.Location = new Point(14, 191);
            label5.Name = "label5";
            label5.Size = new Size(144, 18);
            label5.TabIndex = 53;
            label5.Text = "Técnico Asignado:";
            // 
            // textBoxClienteCedula
            // 
            textBoxClienteCedula.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBoxClienteCedula.Location = new Point(155, 120);
            textBoxClienteCedula.Name = "textBoxClienteCedula";
            textBoxClienteCedula.Size = new Size(200, 20);
            textBoxClienteCedula.TabIndex = 52;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label4.Location = new Point(14, 122);
            label4.Name = "label4";
            label4.Size = new Size(72, 18);
            label4.TabIndex = 51;
            label4.Text = "Cliente:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Cascadia Mono", 14.25F, FontStyle.Bold);
            label3.Location = new Point(150, 73);
            label3.Name = "label3";
            label3.Size = new Size(111, 25);
            label3.TabIndex = 50;
            label3.Text = "Servicios";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Cascadia Mono", 14.25F, FontStyle.Bold);
            label2.Location = new Point(150, 48);
            label2.Name = "label2";
            label2.Size = new Size(34, 25);
            label2.TabIndex = 49;
            label2.Text = "De";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cascadia Mono", 14.25F, FontStyle.Bold);
            label1.Location = new Point(150, 23);
            label1.Name = "label1";
            label1.Size = new Size(89, 25);
            label1.TabIndex = 48;
            label1.Text = "Órdenes";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.order;
            pictureBox1.Location = new Point(14, 23);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(130, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 47;
            pictureBox1.TabStop = false;
            // 
            // comboBoxEstado
            // 
            comboBoxEstado.FormattingEnabled = true;
            comboBoxEstado.Location = new Point(155, 220);
            comboBoxEstado.Name = "comboBoxEstado";
            comboBoxEstado.Size = new Size(200, 23);
            comboBoxEstado.TabIndex = 73;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(155, 255);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 74;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(155, 291);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(200, 23);
            dateTimePicker2.TabIndex = 75;
            // 
            // textBoxObservaciones
            // 
            textBoxObservaciones.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBoxObservaciones.Location = new Point(14, 419);
            textBoxObservaciones.Multiline = true;
            textBoxObservaciones.Name = "textBoxObservaciones";
            textBoxObservaciones.Size = new Size(338, 38);
            textBoxObservaciones.TabIndex = 77;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label11.Location = new Point(14, 398);
            label11.Name = "label11";
            label11.Size = new Size(120, 18);
            label11.TabIndex = 76;
            label11.Text = "Observaciones:";
            // 
            // textBoxDispositivoImei
            // 
            textBoxDispositivoImei.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBoxDispositivoImei.Location = new Point(155, 154);
            textBoxDispositivoImei.Name = "textBoxDispositivoImei";
            textBoxDispositivoImei.Size = new Size(200, 20);
            textBoxDispositivoImei.TabIndex = 78;
            // 
            // textBoxTecnico
            // 
            textBoxTecnico.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBoxTecnico.Location = new Point(155, 191);
            textBoxTecnico.Name = "textBoxTecnico";
            textBoxTecnico.Size = new Size(200, 20);
            textBoxTecnico.TabIndex = 79;
            // 
            // ButtonEliminar
            // 
            ButtonEliminar.Font = new Font("Comic Sans MS", 11.25F);
            ButtonEliminar.Location = new Point(397, 574);
            ButtonEliminar.Name = "ButtonEliminar";
            ButtonEliminar.Size = new Size(75, 34);
            ButtonEliminar.TabIndex = 80;
            ButtonEliminar.Text = "Eliminar";
            ButtonEliminar.UseVisualStyleBackColor = true;
            ButtonEliminar.Click += ButtonEliminar_Click;
            // 
            // ButtonSiguiente
            // 
            ButtonSiguiente.Font = new Font("Comic Sans MS", 11.25F);
            ButtonSiguiente.Location = new Point(1200, 600);
            ButtonSiguiente.Name = "ButtonSiguiente";
            ButtonSiguiente.Size = new Size(165, 34);
            ButtonSiguiente.TabIndex = 81;
            ButtonSiguiente.Text = "Siguiente";
            ButtonSiguiente.UseVisualStyleBackColor = true;
            ButtonSiguiente.Click += ButtonSiguiente_Click;
            // 
            // OrdenesServicio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(1394, 646);
            Controls.Add(ButtonSiguiente);
            Controls.Add(ButtonEliminar);
            Controls.Add(textBoxTecnico);
            Controls.Add(textBoxDispositivoImei);
            Controls.Add(textBoxObservaciones);
            Controls.Add(label11);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            Controls.Add(comboBoxEstado);
            Controls.Add(label10);
            Controls.Add(textBoxDescripcion);
            Controls.Add(label9);
            Controls.Add(ButtonGuardar);
            Controls.Add(ButtonEditar);
            Controls.Add(dataGridView1);
            Controls.Add(ButtonLimpiarCampos);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(textBoxClienteCedula);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "OrdenesServicio";
            Text = "OrdenesServicio";
            Load += OrdenesServicio_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label10;
        private TextBox textBoxDescripcion;
        private Label label9;
        private Button ButtonGuardar;
        private Button ButtonEditar;
        private DataGridView dataGridView1;
        private Button ButtonLimpiarCampos;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox textBoxClienteCedula;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private PictureBox pictureBox1;
        private ComboBox comboBoxEstado;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private TextBox textBoxObservaciones;
        private Label label11;
        private TextBox textBoxDispositivoImei;
        private TextBox textBoxTecnico;
        private Button ButtonEliminar;
        private Button ButtonSiguiente;
    }
}