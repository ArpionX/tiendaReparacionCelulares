using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Services;

namespace TiendaReparacion.UI
{
    public partial class GestionClientes : Form
    {
        private readonly IClienteServices _clienteServices;
        private readonly IServiceProvider _serviceProvider; // Para resolver GestionDispositivos
        private Dashboard? _dashboardParent;
        public GestionClientes(IClienteServices clienteServices, IServiceProvider serviceProvider, Dashboard dashboard)
        {
            InitializeComponent();
            _clienteServices = clienteServices;
            _serviceProvider = serviceProvider; // Guardar el ServiceProvider para resolver otros formularios
            _dashboardParent = dashboard; // Guardar la referencia al Dashboard
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                _selectedClienteId = (int)row.Cells["IdCliente"].Value;
                textBox1.Text = row.Cells["Nombre"].Value.ToString();
                textBox5.Text = row.Cells["Apellido"].Value.ToString();
                textBox2.Text = row.Cells["Telefono"].Value.ToString();
                textBox4.Text = row.Cells["Email"].Value.ToString();
                textBox3.Text = row.Cells["Direccion"].Value.ToString();
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                _selectedClienteId = (int)row.Cells["IdCliente"].Value;
                textBox1.Text = row.Cells["Nombre"].Value.ToString();
                textBox5.Text = row.Cells["Apellido"].Value.ToString();
                textBox2.Text = row.Cells["Telefono"].Value.ToString();
                textBox4.Text = row.Cells["Email"].Value.ToString();
                textBox3.Text = row.Cells["Direccion"].Value.ToString();
                textBox7.Text = row.Cells["Cedula"].Value.ToString(); // Asumiendo que este es el campo para la cédula
            }
        }
        public void SetDashboardReference(Dashboard dashboard)
        {
            _dashboardParent = dashboard;
        }
        private async void GestionClientes_Load(object sender, EventArgs e)
        {
            this.Text = "Gestión de Clientes";
            // Cargar datos iniciales en el DataGridView
            await LoadClientesIntoDataGridView();
        }
        private async Task LoadClientesIntoDataGridView()
        {
            try
            {
                List<Cliente> clientes = await _clienteServices.GetAll();
                dataGridView1.DataSource = clientes;
                // Opcional: Ajustar columnas
                dataGridView1.Columns["IdCliente"].Visible = false; // Ocultar ID si no es relevante para el usuario
                dataGridView1.Columns["OrdenesServicio"].Visible = false; // Ocultar la colección de órdenes de servicio
                dataGridView1.Columns["FechaRegistro"].DefaultCellStyle.Format = "dd/MM/yyyy"; // Formatear fecha
                dataGridView1.Columns["Dispositivos"].Visible = false; // Ocultar la colección de dispositivos
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                ClearFormFields(); // Limpiar campos después de recargar

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int _selectedClienteId = 0;
        private void ClearFormFields()
        {
            textBox1.Text = string.Empty; // Nombre
            textBox5.Text = string.Empty; // Apellido
            textBox2.Text = string.Empty; // Teléfono
            textBox4.Text = string.Empty; // Email
            textBox3.Text = string.Empty; // Dirección
            textBox7.Text = string.Empty; // Cédula (asumiendo que este es el campo para la cédula)
            // Limpiar selección del DataGridView
            dataGridView1.ClearSelection();
            // Restablecer el ID del cliente seleccionado (si lo tienes en una variable de clase)
            _selectedClienteId = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearFormFields();
            MessageBox.Show("Campos listos para un nuevo cliente.", "Nuevo Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Nombre, Apellido y Teléfono son campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cliente newCliente = new Cliente
            {
                Nombre = textBox1.Text.Trim(),
                Apellido = textBox5.Text.Trim(),
                Telefono = textBox2.Text.Trim(),
                Email = textBox4.Text.Trim(),
                Direccion = textBox3.Text.Trim(),
                Cedula = textBox7.Text.Trim(), // Asumiendo que textBox6 es para la cédula
                FechaRegistro = DateTime.Now // Se establece al guardar
            };

            try
            {
                int result = await _clienteServices.Insert(newCliente);
                if (result > 0)
                {
                    MessageBox.Show("Cliente guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFormFields();
                    await LoadClientesIntoDataGridView(); // Recargar la tabla
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (_selectedClienteId == 0)
            {
                MessageBox.Show("Seleccione un cliente de la tabla para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validaciones
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Nombre, Apellido y Teléfono son campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cliente clienteToUpdate = new Cliente
            {
                IdCliente = _selectedClienteId,
                Nombre = textBox1.Text.Trim(),
                Apellido = textBox5.Text.Trim(),
                Telefono = textBox2.Text.Trim(),
                Email = textBox4.Text.Trim(),
                Direccion = textBox3.Text.Trim(),
                Cedula = textBox7.Text.Trim(), // Asumiendo que textBox7 es para la cédula
                // FechaRegistro no se actualiza aquí, se mantiene la original
            };

            try
            {
                int result = await _clienteServices.Update(clienteToUpdate);
                if (result > 0)
                {
                    MessageBox.Show("Cliente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFormFields();
                    await LoadClientesIntoDataGridView();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (_selectedClienteId == 0)
            {
                MessageBox.Show("Seleccione un cliente de la tabla para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar este cliente? Esta acción es irreversible.", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int result = await _clienteServices.Delete(_selectedClienteId);
                    if (result > 0)
                    {
                        MessageBox.Show("Cliente eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFormFields();
                        await LoadClientesIntoDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            string searchText = textBox6.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                await LoadClientesIntoDataGridView(); // Si el campo de búsqueda está vacío, muestra todos
                return;
            }

            try
            {
                List<Cliente> clientes = await _clienteServices.GetByName(searchText);
                dataGridView1.DataSource = clientes;
                if (clientes.Count == 0)
                {
                    MessageBox.Show("No se encontraron clientes con ese nombre.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar clientes: {ex.Message}", "Error de Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private async void ButtonSiguiente_Click(object sender, EventArgs e)
        {
            if (_selectedClienteId == 0)
            {
                MessageBox.Show("Por favor, seleccione un cliente de la tabla o guarde uno nuevo antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el cliente seleccionado para pasar su cédula
            Cliente? clienteSeleccionado = await _clienteServices.GetById(_selectedClienteId);

            if (clienteSeleccionado == null || string.IsNullOrEmpty(clienteSeleccionado.Cedula))
            {
                MessageBox.Show("No se pudo obtener la cédula del cliente seleccionado. Asegúrese de que el cliente tenga una cédula registrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cerrar este formulario (GestionClientes)
            if (_dashboardParent != null)
            {
                _dashboardParent.SelectedClientCedula = clienteSeleccionado.Cedula; // Guardar la cédula del cliente seleccionado en el Dashboard
                Debug.WriteLine($"Cédula del cliente seleccionada: {clienteSeleccionado.Cedula}"); // ¡CAMBIO! Usando Debug.WriteLine para depuración
                // Cierra este formulario (GestionClientes)
                this.Close(); // Cierra el formulario actual

                // Llama al método del Dashboard para abrir GestionDispositivos con la cédula
                _dashboardParent.OpenGestionDispositivos(clienteSeleccionado.Cedula);
            }
            else
            {
                MessageBox.Show("No se pudo acceder al Dashboard principal para abrir la gestión de dispositivos.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Dashboard? FindDashboardParent()
        {
            Control? currentParent = this.Parent;
            int level = 0;
            while (currentParent != null)
            {
                level++;
                Debug.WriteLine($"Nivel {level}: Parent es de tipo {currentParent.GetType().Name}"); // ¡CAMBIO! Usando Debug.WriteLine

                if (currentParent is Dashboard dashboard)
                {
                    Debug.WriteLine($"Dashboard encontrado en nivel {level}."); // ¡CAMBIO! Usando Debug.WriteLine
                    return dashboard;
                }
                currentParent = currentParent.Parent;
            }
            Debug.WriteLine("Dashboard no encontrado en la jerarquía de padres."); // ¡CAMBIO! Usando Debug.WriteLine
            return null;
        }
    }
}
