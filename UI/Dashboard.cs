using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiendaReparacion.Data.Entities;

namespace TiendaReparacion.UI
{
    public partial class Dashboard : Form
    {
        public Usuario? AuthenticatedUser { get; set; } // Puede ser nulo si no se establece.

        private readonly IServiceProvider _serviceProvider;
        // ¡NUEVO! Propiedades para almacenar datos compartidos
        public string? SelectedClientCedula { get; set; }
        public string? SelectedDeviceImei { get; set; }
        // ¡NUEVO! Propiedades para datos de la Orden de Servicio
        public int? SelectedOrdenId { get; set; }
        public string? SelectedOrdenNumero { get; set; }
        public string? SelectedOrdenDescripcionProblema { get; set; }

        public Dashboard(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }
        private void OpenChildForm<TForm>() where TForm : Form
        {
            // Cierra cualquier formulario que esté actualmente en el panel.
            if (panel3.Controls.Count > 0)
            {
                panel3.Controls.RemoveAt(0);
            }

            // Resuelve el formulario usando el ServiceProvider
            TForm childForm = _serviceProvider.GetRequiredService<TForm>();

            // Si el formulario es GestionClientes, pasa la referencia del Dashboard
            if (childForm is GestionClientes gestionClientesForm)
            {
                gestionClientesForm.SetDashboardReference(this);
            }
            // Si el formulario es GestionDispositivos, también pasa la referencia del Dashboard
            else if (childForm is GestionDispositivos gestionDispositivosForm)
            {
                gestionDispositivosForm.SetDashboardReference(this);
            }
            // Puedes añadir más 'else if' para otros formularios si necesitan la referencia al Dashboard

            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            childForm.Show();

            panel3.Controls.Add(childForm);
            panel3.Tag = childForm;
        }
        public void OpenGestionDispositivos(string? cedulaCliente = null)
        {
            // Cierra cualquier formulario que esté actualmente en el panel.
            if (panel3.Controls.Count > 0)
            {
                panel3.Controls.RemoveAt(0);
            }

            GestionDispositivos gestionDispositivosForm = _serviceProvider.GetRequiredService<GestionDispositivos>();

            // Pasa la referencia del Dashboard al formulario de dispositivos
            gestionDispositivosForm.SetDashboardReference(this);

            if (!string.IsNullOrEmpty(cedulaCliente))
            {
                gestionDispositivosForm.SetClienteCedula(cedulaCliente);
            }

            gestionDispositivosForm.TopLevel = false;
            gestionDispositivosForm.Dock = DockStyle.Fill;
            gestionDispositivosForm.Show();

            panel3.Controls.Add(gestionDispositivosForm);
            panel3.Tag = gestionDispositivosForm;
        }
        public void OpenGestionOrdenesServicio() // ¡NUEVO MÉTODO!
        {
            // Cierra cualquier formulario que esté actualmente en el panel.
            if (panel3.Controls.Count > 0)
            {
                panel3.Controls.RemoveAt(0);
            }

            OrdenesServicio gestionOrdenesForm = _serviceProvider.GetRequiredService<OrdenesServicio>();

            // Pasa la referencia del Dashboard al formulario de órdenes de servicio
            gestionOrdenesForm.SetDashboardReference(this);

            gestionOrdenesForm.TopLevel = false;
            gestionOrdenesForm.Dock = DockStyle.Fill;
            gestionOrdenesForm.Show();

            panel3.Controls.Add(gestionOrdenesForm);
            panel3.Tag = gestionOrdenesForm;
        }
        /// <summary>
        /// Abre el formulario de Gestión de Diagnóstico, pre-llenando los datos de la orden.
        /// </summary>
        public void OpenGestionDiagnostico(int ordenId, string numeroOrden, string descripcionProblema) // ¡NUEVO MÉTODO!
        {
            // Cierra cualquier formulario que esté actualmente en el panel.
            if (panel3.Controls.Count > 0)
            {
                panel3.Controls.RemoveAt(0);
            }

            // Almacena los datos de la orden en el Dashboard para que GestionDiagnostico los recupere
            SelectedOrdenId = ordenId;
            SelectedOrdenNumero = numeroOrden;
            SelectedOrdenDescripcionProblema = descripcionProblema;

            GestionDiagnostico gestionDiagnosticoForm = _serviceProvider.GetRequiredService<GestionDiagnostico>();

            // Pasa la referencia del Dashboard al formulario de diagnóstico
            gestionDiagnosticoForm.SetDashboardReference(this);

            gestionDiagnosticoForm.TopLevel = false;
            gestionDiagnosticoForm.Dock = DockStyle.Fill;
            gestionDiagnosticoForm.Show();

            panel3.Controls.Add(gestionDiagnosticoForm);
            panel3.Tag = gestionDiagnosticoForm;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm<GestionClientes>();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenGestionDispositivos(null);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenGestionOrdenesServicio();

        }
        private void ButtonDiagnostico_Click(object sender, EventArgs e)
        {
            OpenChildForm<GestionDiagnostico>();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            if (AuthenticatedUser != null)
            {
                NombreUsuario.Text = $"Bienvenido, {AuthenticatedUser.NombreUsuario} ({AuthenticatedUser.Rol})";
            }
            else
            {
                NombreUsuario.Text = "Bienvenido, Invitado"; // En caso de que el usuario no se haya pasado correctamente.
            }

            this.Text = "Dashboard - Tienda de Reparación de Celulares";
            this.StartPosition = FormStartPosition.CenterScreen; // Centra el formulario al iniciar
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
