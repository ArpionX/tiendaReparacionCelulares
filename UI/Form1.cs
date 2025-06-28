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
            this.Load += Form1_Load; // Añade un evento Load para configurar la UI.
        }

        // Constructor por defecto, necesario si el diseñador de WinForms lo requiere,
        // pero preferimos el constructor con inyección de dependencias.
        // public Form1()
        // {
        //     InitializeComponent();
        // }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Configuración básica del formulario.
            this.Text = "Gestión de Técnicos - Tienda Reparación";
            this.Size = new Size(500, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Crea un botón para insertar y mostrar técnicos.
            Button btnAccion = new Button();
            btnAccion.Text = "Insertar y Mostrar Técnicos";
            btnAccion.Location = new Point(150, 100);
            btnAccion.Size = new Size(200, 50);
            btnAccion.Click += BtnAccion_Click; // Asocia el evento click.
            this.Controls.Add(btnAccion); // Añade el botón al formulario.

            // Puedes añadir un Label o TextBox para mostrar resultados directamente en el formulario si quieres.
            Label lblOutput = new Label();
            lblOutput.Name = "lblOutput";
            lblOutput.Text = "Presiona el botón para ver la acción...";
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point(50, 180);
            this.Controls.Add(lblOutput);
        }

        // Manejador del evento click del botón.
        private async void BtnAccion_Click(object sender, EventArgs e)
        {
            Label lblOutput = this.Controls.Find("lblOutput", true).FirstOrDefault() as Label;
            if (lblOutput != null)
            {
                lblOutput.Text = "Realizando operación...";
            }

            try
            {
                // 1. Insertar un nuevo técnico
                Tecnico nuevoTecnico = new Tecnico
                {
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Especialidad = "Hardware Móvil",
                    Telefono = "0987654321",
                    Email = "juan.perez@example.com",
                    FechaIngreso = DateTime.Now.Date,
                    Estado = EstadoTecnico.activo // Usando el enum EstadoTecnico
                };

                await _tecnicoRepository.AddTecnicoAsync(nuevoTecnico);
                Debug.WriteLine($"Tecnico insertado: {nuevoTecnico.Nombre} {nuevoTecnico.Apellido} (ID: {nuevoTecnico.IdTecnico})");
                MessageBox.Show($"Tecnico '{nuevoTecnico.Nombre} {nuevoTecnico.Apellido}' insertado con éxito!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 2. Traer y mostrar todos los técnicos
                IEnumerable<Tecnico> tecnicos = await _tecnicoRepository.GetAllTecnicosAsync();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Lista de Técnicos:");
                foreach (var tecnico in tecnicos)
                {
                    sb.AppendLine($"- ID: {tecnico.IdTecnico}, Nombre: {tecnico.Nombre} {tecnico.Apellido}, Especialidad: {tecnico.Especialidad}, Estado: {tecnico.Estado}");
                }

                // Mostrar en la consola de depuración (Output window en Visual Studio)
                Debug.WriteLine(sb.ToString());

                // Mostrar en un MessageBox para la UI
                MessageBox.Show(sb.ToString(), "Técnicos de la Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (lblOutput != null)
                {
                    lblOutput.Text = "Operación completada. Revisa la ventana de Salida (Output) y el MessageBox.";
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
