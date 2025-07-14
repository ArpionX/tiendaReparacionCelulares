namespace TiendaReparacion.UI
{
    partial class GestionDiagnostico
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
            dataGridView1 = new DataGridView();
            label3 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            textBoxTiempoEstimado = new TextBox();
            label10 = new Label();
            textBoxCausaRaiz = new TextBox();
            textBoxSolucionPropuesta = new TextBox();
            textBoxDescripcionProblema = new TextBox();
            textBoxTecnico = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            textBoxOrden = new TextBox();
            label4 = new Label();
            ButtonSiguiente = new Button();
            textBox8 = new TextBox();
            ButtonEliminar = new Button();
            ButtonBuscar = new Button();
            ButtonEditar = new Button();
            ButtonGuardar = new Button();
            ButtonLimpiar = new Button();
            comboBoxEstado = new ComboBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ActiveCaption;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(376, 28);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(722, 366);
            dataGridView1.TabIndex = 41;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellClick += DataGridView1_CellClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Cascadia Mono", 14.25F, FontStyle.Bold);
            label3.Location = new Point(163, 52);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.No;
            label3.Size = new Size(133, 25);
            label3.TabIndex = 45;
            label3.Text = "Diagnostico";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cascadia Mono", 14.25F, FontStyle.Bold);
            label1.Location = new Point(148, 28);
            label1.Name = "label1";
            label1.Size = new Size(0, 25);
            label1.TabIndex = 43;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.gD;
            pictureBox1.Location = new Point(18, 28);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(130, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 42;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // textBoxTiempoEstimado
            // 
            textBoxTiempoEstimado.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBoxTiempoEstimado.Location = new Point(174, 374);
            textBoxTiempoEstimado.Name = "textBoxTiempoEstimado";
            textBoxTiempoEstimado.Size = new Size(186, 20);
            textBoxTiempoEstimado.TabIndex = 61;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label10.Location = new Point(18, 376);
            label10.Name = "label10";
            label10.Size = new Size(136, 18);
            label10.TabIndex = 60;
            label10.Text = "Tiempo Estimado:";
            // 
            // textBoxCausaRaiz
            // 
            textBoxCausaRaiz.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBoxCausaRaiz.Location = new Point(174, 293);
            textBoxCausaRaiz.Name = "textBoxCausaRaiz";
            textBoxCausaRaiz.Size = new Size(186, 20);
            textBoxCausaRaiz.TabIndex = 59;
            // 
            // textBoxSolucionPropuesta
            // 
            textBoxSolucionPropuesta.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBoxSolucionPropuesta.Location = new Point(174, 328);
            textBoxSolucionPropuesta.Name = "textBoxSolucionPropuesta";
            textBoxSolucionPropuesta.Size = new Size(186, 20);
            textBoxSolucionPropuesta.TabIndex = 58;
            // 
            // textBoxDescripcionProblema
            // 
            textBoxDescripcionProblema.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBoxDescripcionProblema.Location = new Point(174, 204);
            textBoxDescripcionProblema.Multiline = true;
            textBoxDescripcionProblema.Name = "textBoxDescripcionProblema";
            textBoxDescripcionProblema.Size = new Size(186, 83);
            textBoxDescripcionProblema.TabIndex = 57;
            textBoxDescripcionProblema.Text = "\r\n\r\n\r\n";
            textBoxDescripcionProblema.TextChanged += textBox2_TextChanged;
            // 
            // textBoxTecnico
            // 
            textBoxTecnico.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBoxTecnico.Location = new Point(174, 176);
            textBoxTecnico.Name = "textBoxTecnico";
            textBoxTecnico.Size = new Size(186, 20);
            textBoxTecnico.TabIndex = 56;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label8.Location = new Point(18, 328);
            label8.Name = "label8";
            label8.Size = new Size(88, 36);
            label8.TabIndex = 55;
            label8.Text = "Solucion \r\nPropuesta:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label7.Location = new Point(18, 293);
            label7.Name = "label7";
            label7.Size = new Size(96, 18);
            label7.TabIndex = 54;
            label7.Text = "Causa Raiz:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label6.Location = new Point(18, 176);
            label6.Name = "label6";
            label6.Size = new Size(72, 18);
            label6.TabIndex = 53;
            label6.Text = "Tecnico:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label5.Location = new Point(18, 204);
            label5.Name = "label5";
            label5.RightToLeft = RightToLeft.No;
            label5.Size = new Size(112, 36);
            label5.TabIndex = 52;
            label5.Text = "Descripcion \r\ndel Problema:\r\n";
            // 
            // textBoxOrden
            // 
            textBoxOrden.Font = new Font("Microsoft Sans Serif", 8.25F);
            textBoxOrden.Location = new Point(174, 143);
            textBoxOrden.Name = "textBoxOrden";
            textBoxOrden.Size = new Size(186, 20);
            textBoxOrden.TabIndex = 51;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label4.Location = new Point(18, 143);
            label4.Name = "label4";
            label4.Size = new Size(80, 18);
            label4.TabIndex = 50;
            label4.Text = "Orden N°:";
            // 
            // ButtonSiguiente
            // 
            ButtonSiguiente.Font = new Font("Comic Sans MS", 11.25F);
            ButtonSiguiente.Location = new Point(978, 453);
            ButtonSiguiente.Name = "ButtonSiguiente";
            ButtonSiguiente.Size = new Size(102, 34);
            ButtonSiguiente.TabIndex = 68;
            ButtonSiguiente.Text = "Siguiente";
            ButtonSiguiente.UseVisualStyleBackColor = true;
            ButtonSiguiente.Click += ButtonSiguiente_Click;
            // 
            // textBox8
            // 
            textBox8.Location = new Point(556, 421);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(306, 23);
            textBox8.TabIndex = 67;
            // 
            // ButtonEliminar
            // 
            ButtonEliminar.Font = new Font("Comic Sans MS", 11.25F);
            ButtonEliminar.Location = new Point(985, 413);
            ButtonEliminar.Name = "ButtonEliminar";
            ButtonEliminar.Size = new Size(75, 34);
            ButtonEliminar.TabIndex = 66;
            ButtonEliminar.Text = "Eliminar";
            ButtonEliminar.UseVisualStyleBackColor = true;
            ButtonEliminar.Click += ButtonEliminar_Click;
            // 
            // ButtonBuscar
            // 
            ButtonBuscar.Font = new Font("Comic Sans MS", 11.25F);
            ButtonBuscar.Location = new Point(877, 413);
            ButtonBuscar.Name = "ButtonBuscar";
            ButtonBuscar.Size = new Size(75, 34);
            ButtonBuscar.TabIndex = 65;
            ButtonBuscar.Text = "Buscar";
            ButtonBuscar.UseVisualStyleBackColor = true;
            ButtonBuscar.Click += ButtonBuscar_Click;
            // 
            // ButtonEditar
            // 
            ButtonEditar.Font = new Font("Comic Sans MS", 11.25F);
            ButtonEditar.Location = new Point(221, 453);
            ButtonEditar.Name = "ButtonEditar";
            ButtonEditar.Size = new Size(75, 34);
            ButtonEditar.TabIndex = 64;
            ButtonEditar.Text = "Editar";
            ButtonEditar.UseVisualStyleBackColor = true;
            ButtonEditar.Click += ButtonEditar_Click;
            // 
            // ButtonGuardar
            // 
            ButtonGuardar.Font = new Font("Comic Sans MS", 11.25F);
            ButtonGuardar.Location = new Point(141, 453);
            ButtonGuardar.Name = "ButtonGuardar";
            ButtonGuardar.Size = new Size(75, 34);
            ButtonGuardar.TabIndex = 63;
            ButtonGuardar.Text = "Guardar";
            ButtonGuardar.UseVisualStyleBackColor = true;
            ButtonGuardar.Click += ButtonGuardar_Click;
            // 
            // ButtonLimpiar
            // 
            ButtonLimpiar.Font = new Font("Comic Sans MS", 11.25F);
            ButtonLimpiar.Location = new Point(60, 453);
            ButtonLimpiar.Name = "ButtonLimpiar";
            ButtonLimpiar.Size = new Size(75, 34);
            ButtonLimpiar.TabIndex = 62;
            ButtonLimpiar.Text = "Limpiar";
            ButtonLimpiar.UseVisualStyleBackColor = true;
            ButtonLimpiar.Click += ButtonLimpiar_Click;
            // 
            // comboBoxEstado
            // 
            comboBoxEstado.FormattingEnabled = true;
            comboBoxEstado.Location = new Point(174, 412);
            comboBoxEstado.Name = "comboBoxEstado";
            comboBoxEstado.Size = new Size(186, 23);
            comboBoxEstado.TabIndex = 75;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Cascadia Mono", 10.1F, FontStyle.Bold);
            label2.Location = new Point(18, 413);
            label2.Name = "label2";
            label2.Size = new Size(72, 18);
            label2.TabIndex = 74;
            label2.Text = "Estado: ";
            // 
            // GestionDiagnostico
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(1151, 538);
            Controls.Add(comboBoxEstado);
            Controls.Add(label2);
            Controls.Add(ButtonSiguiente);
            Controls.Add(textBox8);
            Controls.Add(ButtonEliminar);
            Controls.Add(ButtonBuscar);
            Controls.Add(ButtonEditar);
            Controls.Add(ButtonGuardar);
            Controls.Add(ButtonLimpiar);
            Controls.Add(textBoxTiempoEstimado);
            Controls.Add(label10);
            Controls.Add(textBoxCausaRaiz);
            Controls.Add(textBoxSolucionPropuesta);
            Controls.Add(textBoxDescripcionProblema);
            Controls.Add(textBoxTecnico);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(textBoxOrden);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(dataGridView1);
            Name = "GestionDiagnostico";
            Text = "GestionDiagnostico";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label3;
        private Label label1;
        private PictureBox pictureBox1;
        private TextBox textBoxTiempoEstimado;
        private Label label10;
        private TextBox textBoxCausaRaiz;
        private TextBox textBoxSolucionPropuesta;
        private TextBox textBoxDescripcionProblema;
        private TextBox textBoxTecnico;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox textBoxOrden;
        private Label label4;
        private Button ButtonSiguiente;
        private TextBox textBox8;
        private Button ButtonEliminar;
        private Button ButtonBuscar;
        private Button ButtonEditar;
        private Button ButtonGuardar;
        private Button ButtonLimpiar;
        private ComboBox comboBoxEstado;
        private Label label2;
    }
}