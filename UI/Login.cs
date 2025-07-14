using System;
using System.Drawing;
using System.Windows.Forms;
using TiendaReparacion.Services; // Importa el namespace de tus servicios
using TiendaReparacion.Data.Entities; // Necesario para la entidad Usuario
using Microsoft.Extensions.DependencyInjection; // Necesario para IServiceProvider

namespace TiendaReparacion.UI
{
    public partial class Login : Form
    {
        private readonly IAuthServices _authServices;
        private readonly IServiceProvider _serviceProvider;
        public Login(IAuthServices authServices, IServiceProvider serviceProvider)
        {

            InitializeComponent();
            _authServices = authServices;
            _serviceProvider = serviceProvider;
            this.Load += Login_Load; // Suscribe el evento Load del formulario.
            button1.Click += button1_Click; // Asegura que el evento Click del bot�n Ingresar est� suscrito.
            button2.Click += button2_Click; // Asegura que el evento Click del bot�n Salir est� suscrito.
        }
        private void Login_Load(object sender, EventArgs e)
        {
            // Configura el campo de contrase�a para ocultar los caracteres.
            textBox2.PasswordChar = '*';
            // Opcional: Centra el formulario en la pantalla al iniciar.
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, ingrese su usuario y contrase�a.", "Campos Vac�os", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Usuario? authenticatedUser = await _authServices.Login(username, password);

                if (authenticatedUser != null)
                {
                    MessageBox.Show($"�Bienvenido, {authenticatedUser.NombreUsuario}! Rol: {authenticatedUser.Rol}", "Inicio de Sesi�n Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Oculta el formulario de login.
                    this.Hide();

                    // �CAMBIO CLAVE AQU�!
                    // 1. Resuelve el Dashboard usando el serviceProvider.
                    Dashboard mainDashboard = _serviceProvider.GetRequiredService<Dashboard>();
                    // 2. Pasa el usuario autenticado a una propiedad p�blica del Dashboard.
                    mainDashboard.AuthenticatedUser = authenticatedUser;
                    // 3. Muestra el Dashboard.
                    mainDashboard.ShowDialog();

                    // Cierra la aplicaci�n cuando el formulario principal se cierre.
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de inicio de sesi�n: {ex.Message}", "Error de Autenticaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Cierra la aplicaci�n.

        }
    }
}
