using System.Diagnostics;
using System.Text;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion
{
    public partial class Form1 : Form
    {
        private readonly ITecnicoRepository _tecnicoRepository;

        public Form1(ITecnicoRepository tecnicoRepository)
        {
            InitializeComponent();
            _tecnicoRepository = tecnicoRepository; // Asigna la instancia inyectada.
            this.Load += Form1_Load; // A�ade un evento Load para configurar la UI.
        }

        // Constructor por defecto, necesario si el dise�ador de WinForms lo requiere,
        // pero preferimos el constructor con inyecci�n de dependencias.
        // public Form1()
        // {
        //     InitializeComponent();
        // }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Configuraci�n b�sica del formulario.
            this.Text = "Gesti�n de T�cnicos - Tienda Reparaci�n";
            this.Size = new Size(500, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Crea un bot�n para insertar y mostrar t�cnicos.
            Button btnAccion = new Button();
            btnAccion.Text = "Insertar y Mostrar T�cnicos";
            btnAccion.Location = new Point(150, 100);
            btnAccion.Size = new Size(200, 50);
            btnAccion.Click += BtnAccion_Click; // Asocia el evento click.
            this.Controls.Add(btnAccion); // A�ade el bot�n al formulario.

            // Puedes a�adir un Label o TextBox para mostrar resultados directamente en el formulario si quieres.
            Label lblOutput = new Label();
            lblOutput.Name = "lblOutput";
            lblOutput.Text = "Presiona el bot�n para ver la acci�n...";
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point(50, 180);
            this.Controls.Add(lblOutput);
        }

        // Manejador del evento click del bot�n.
        private async void BtnAccion_Click(object sender, EventArgs e)
        {
            Label lblOutput = this.Controls.Find("lblOutput", true).FirstOrDefault() as Label;
            if (lblOutput != null)
            {
                lblOutput.Text = "Realizando operaci�n...";
            }

            try
            {
                // 1. Insertar un nuevo t�cnico
                Tecnico nuevoTecnico = new Tecnico
                {
                    Nombre = "Juan",
                    Apellido = "P�rez",
                    Especialidad = "Hardware M�vil",
                    Telefono = "0987654321",
                    Email = "juan.perez@example.com",
                    FechaIngreso = DateTime.Now.Date,
                    Estado = EstadoTecnico.activo // Usando el enum EstadoTecnico
                };

                await _tecnicoRepository.AddTecnicoAsync(nuevoTecnico);
                Debug.WriteLine($"Tecnico insertado: {nuevoTecnico.Nombre} {nuevoTecnico.Apellido} (ID: {nuevoTecnico.IdTecnico})");
                MessageBox.Show($"Tecnico '{nuevoTecnico.Nombre} {nuevoTecnico.Apellido}' insertado con �xito!", "Operaci�n Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 2. Traer y mostrar todos los t�cnicos
                IEnumerable<Tecnico> tecnicos = await _tecnicoRepository.GetAllTecnicosAsync();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Lista de T�cnicos:");
                foreach (var tecnico in tecnicos)
                {
                    sb.AppendLine($"- ID: {tecnico.IdTecnico}, Nombre: {tecnico.Nombre} {tecnico.Apellido}, Especialidad: {tecnico.Especialidad}, Estado: {tecnico.Estado}");
                }

                // Mostrar en la consola de depuraci�n (Output window en Visual Studio)
                Debug.WriteLine(sb.ToString());

                // Mostrar en un MessageBox para la UI
                MessageBox.Show(sb.ToString(), "T�cnicos de la Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (lblOutput != null)
                {
                    lblOutput.Text = "Operaci�n completada. Revisa la ventana de Salida (Output) y el MessageBox.";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al interactuar con la base de datos: {ex.Message}");
                MessageBox.Show($"Error al interactuar con la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (lblOutput != null)
                {
                    lblOutput.Text = $"Error: {ex.Message}";
                }
            }
        }
    }
}
