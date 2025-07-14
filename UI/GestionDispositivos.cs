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
    public partial class GestionDispositivos : Form
    {
        private readonly IDispositivoServices _dispositivoServices;
        private readonly IClienteServices _clienteServices;
        private int _selectedDispositivoId = 0;
        private string? _initialClienteCedula;
        private Dashboard? _dashboardParent; // ¡NUEVO! Campo para almacenar la referencia al Dashboard

        public GestionDispositivos(IDispositivoServices dispositivoServices, IClienteServices clienteServices, Dashboard dashboard)
        {
            InitializeComponent();
            _dispositivoServices = dispositivoServices;
            _clienteServices = clienteServices;
            _dashboardParent = dashboard; // Asigna la referencia del Dashboard directamente

        }
        public void SetDashboardReference(Dashboard dashboard)
        {
            _dashboardParent = dashboard;
        }
        public void SetClienteCedula(string cedula)
        {
            _initialClienteCedula = cedula;
            if (this.IsHandleCreated)
            {
                textBox1.Text = cedula;
            }
        }

        private async void GestionDispositivos_Load(object sender, EventArgs e)
        {
            this.Text = "Gestión de Dispositivos";
            await LoadDispositivosIntoDataGridView();            // Si se ha pasado una cédula de cliente inicial, la muestra en el campo correspondiente
            if (!string.IsNullOrEmpty(_initialClienteCedula))
            {
                textBox1.Text = _initialClienteCedula;
            }

        }

        /// <summary>
        /// Carga los dispositivos desde la base de datos y los muestra en el DataGridView.
        /// </summary>
        private async Task LoadDispositivosIntoDataGridView()
        {
            try
            {
                List<Dispositivo> dispositivos = await _dispositivoServices.GetAll();

                var dispositivosConCedula = new List<object>();
                foreach (var disp in dispositivos)
                {
                    Cliente? clienteAsociado = await _clienteServices.GetById(disp.IdCliente);
                    dispositivosConCedula.Add(new
                    {
                        disp.IdDispositivo,
                        ClienteCedula = clienteAsociado?.Cedula ?? "N/A",
                        disp.Marca,
                        disp.Modelo,
                        disp.Imei,
                        disp.Color,
                        disp.AnioFabricacion,
                        disp.SistemaOperativo
                    });
                }

                dataGridView1.DataSource = dispositivosConCedula;

                if (dataGridView1.Columns.Contains("IdDispositivo"))
                {
                    dataGridView1.Columns["IdDispositivo"].Visible = false;
                }

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                ClearFormFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar dispositivos en la tabla: {ex.Message}", "Error de Carga de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Limpia los campos del formulario y restablece la selección del DataGridView.
        /// </summary>
        private void ClearFormFields()
        {
            //textBox1.Text = string.Empty; // Cédula Cliente
            textBox5.Text = string.Empty; // Marca
            textBox2.Text = string.Empty; // Modelo
            textBox4.Text = string.Empty; // IMEI
            textBox3.Text = string.Empty; // Color
            textBox7.Text = string.Empty; // Año Fabricación
            textBox6.Text = string.Empty; // Sistema Operativo

            dataGridView1.ClearSelection();
            _selectedDispositivoId = 0;
        }

        /// <summary>
        /// Maneja el clic en una celda del DataGridView para cargar los datos del dispositivo en los campos del formulario.
        /// </summary>
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                _selectedDispositivoId = (int)row.Cells["IdDispositivo"].Value;

                textBox1.Text = row.Cells["ClienteCedula"].Value?.ToString() ?? string.Empty;
                textBox5.Text = row.Cells["Marca"].Value?.ToString() ?? string.Empty;
                textBox2.Text = row.Cells["Modelo"].Value?.ToString() ?? string.Empty;
                textBox4.Text = row.Cells["Imei"].Value?.ToString() ?? string.Empty;
                textBox3.Text = row.Cells["Color"].Value?.ToString() ?? string.Empty;
                textBox7.Text = row.Cells["AnioFabricacion"].Value?.ToString() ?? string.Empty;
                textBox6.Text = row.Cells["SistemaOperativo"].Value?.ToString() ?? string.Empty;

                // ¡NUEVO! Guardar el IMEI del dispositivo seleccionado en el Dashboard
                if (_dashboardParent != null)
                {
                    _dashboardParent.SelectedDeviceImei = textBox4.Text.Trim(); // IMEI está en textBox4
                    Debug.WriteLine($"IMEI de dispositivo guardado en Dashboard: {_dashboardParent.SelectedDeviceImei}");
                }
            }
        }

        /// <summary>
        /// Maneja el clic del botón "Nuevo" para preparar el formulario para un nuevo registro.
        /// </summary>
        private void ButtonNuevo_Click(object sender, EventArgs e)
        {
            ClearFormFields();
            textBox1.Text = string.Empty; // ¡NUEVO! Limpiar explícitamente la cédula del cliente para una nueva entrada

            MessageBox.Show("Campos listos para un nuevo dispositivo.", "Nuevo Dispositivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Maneja el clic del botón "Guardar" para insertar un nuevo dispositivo.
        /// </summary>
        private async void ButtonGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones básicas
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox5.Text) ||
                string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Cédula de Cliente, Marca, Modelo e IMEI son campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar Cédula de Cliente y obtener IdCliente
            string clienteCedula = textBox1.Text.Trim();
            Cliente? clienteExistente = await _clienteServices.GetByCedula(clienteCedula);
            if (clienteExistente == null)
            {
                MessageBox.Show("La Cédula de Cliente ingresada no existe.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idCliente = clienteExistente.IdCliente;

            // Validar Año de Fabricación (si se ingresa)
            short? anioFabricacion = null;
            if (!string.IsNullOrEmpty(textBox7.Text))
            {
                if (short.TryParse(textBox7.Text, out short parsedAnio))
                {
                    anioFabricacion = parsedAnio;
                }
                else
                {
                    MessageBox.Show("El Año de Fabricación debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            Dispositivo newDispositivo = new Dispositivo
            {
                IdCliente = idCliente,
                Marca = textBox5.Text.Trim(),
                Modelo = textBox2.Text.Trim(),
                Imei = textBox4.Text.Trim(),
                Color = textBox3.Text.Trim(),
                AnioFabricacion = anioFabricacion,
                SistemaOperativo = textBox6.Text.Trim()
            };

            try
            {
                int result = await _dispositivoServices.Insert(newDispositivo);
                if (result > 0)
                {
                    MessageBox.Show("Dispositivo guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDispositivosIntoDataGridView();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el dispositivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar dispositivo: {ex.Message}", "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el clic del botón "Editar" para actualizar un dispositivo existente.
        /// </summary>
        private async void ButtonEditar_Click(object sender, EventArgs e)
        {
            if (_selectedDispositivoId == 0)
            {
                MessageBox.Show("Seleccione un dispositivo de la tabla para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validaciones básicas
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox5.Text) ||
                string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Cédula de Cliente, Marca, Modelo e IMEI son campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar Cédula de Cliente y obtener IdCliente
            string clienteCedula = textBox1.Text.Trim();
            Cliente? clienteExistente = await _clienteServices.GetByCedula(clienteCedula);
            if (clienteExistente == null)
            {
                MessageBox.Show("La Cédula de Cliente ingresada no existe.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idCliente = clienteExistente.IdCliente;

            // Validar Año de Fabricación (si se ingresa)
            short? anioFabricacion = null;
            if (!string.IsNullOrEmpty(textBox7.Text))
            {
                if (short.TryParse(textBox7.Text, out short parsedAnio))
                {
                    anioFabricacion = parsedAnio;
                }
                else
                {
                    MessageBox.Show("El Año de Fabricación debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Cargar el dispositivo existente para actualizar sus propiedades
            Dispositivo? dispositivoToUpdate = await _dispositivoServices.GetById(_selectedDispositivoId);

            if (dispositivoToUpdate == null)
            {
                MessageBox.Show("Dispositivo no encontrado para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Actualizar propiedades
            dispositivoToUpdate.IdCliente = idCliente;
            dispositivoToUpdate.Marca = textBox5.Text.Trim();
            dispositivoToUpdate.Modelo = textBox2.Text.Trim();
            dispositivoToUpdate.Imei = textBox4.Text.Trim();
            dispositivoToUpdate.Color = textBox3.Text.Trim();
            dispositivoToUpdate.AnioFabricacion = anioFabricacion;
            dispositivoToUpdate.SistemaOperativo = textBox6.Text.Trim();

            try
            {
                int result = await _dispositivoServices.Update(dispositivoToUpdate);
                if (result > 0)
                {
                    MessageBox.Show("Dispositivo actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDispositivosIntoDataGridView();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el dispositivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar dispositivo: {ex.Message}", "Error de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el clic del botón "Eliminar" para borrar un dispositivo.
        /// </summary>
        private async void ButtonEliminar_Click(object sender, EventArgs e)
        {
            if (_selectedDispositivoId == 0)
            {
                MessageBox.Show("Seleccione un dispositivo de la tabla para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar este dispositivo? Esta acción es irreversible.", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int result = await _dispositivoServices.Delete(_selectedDispositivoId);
                    if (result > 0)
                    {
                        MessageBox.Show("Dispositivo eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDispositivosIntoDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el dispositivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar dispositivo: {ex.Message}", "Error de Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Maneja el clic del botón "Buscar" para filtrar dispositivos por IMEI.
        /// </summary>
        private async void ButtonBuscar_Click(object sender, EventArgs e)
        {
            string searchText = textBox8.Text.Trim(); // ¡CAMBIO! Ahora se busca por Cédula en textBox8
            if (string.IsNullOrEmpty(searchText))
            {
                await LoadDispositivosIntoDataGridView(); // Si el campo de búsqueda está vacío, muestra todos
                return;
            }

            try
            {
                // 1. Buscar el cliente por la cédula
                Cliente? cliente = await _clienteServices.GetByCedula(searchText);
                List<Dispositivo> resultados = new List<Dispositivo>();

                if (cliente != null)
                {
                    // 2. Si se encuentra el cliente, obtener los dispositivos asociados a su IdCliente
                    resultados = await _dispositivoServices.GetByClienteId(cliente.IdCliente);
                }

                // Convertir los resultados para mostrar la cédula del cliente
                var resultadosConCedula = new List<object>();
                foreach (var disp in resultados)
                {
                    // No necesitamos buscar el cliente de nuevo, ya lo tenemos
                    resultadosConCedula.Add(new
                    {
                        disp.IdDispositivo,
                        ClienteCedula = cliente?.Cedula ?? "N/A", // Usar la cédula del cliente encontrado
                        disp.Marca,
                        disp.Modelo,
                        disp.Imei,
                        disp.Color,
                        disp.AnioFabricacion,
                        disp.SistemaOperativo
                    });
                }

                dataGridView1.DataSource = resultadosConCedula;
                if (resultadosConCedula.Count == 0)
                {
                    MessageBox.Show($"No se encontraron dispositivos para la cédula '{searchText}'.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ClearFormFields(); // Limpiar campos después de la búsqueda
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar dispositivo por cédula: {ex.Message}", "Error de Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ButtonSiguienteOrdenes_Click(object sender, EventArgs e)
        {
            if (_selectedDispositivoId == 0)
            {
                MessageBox.Show("Por favor, seleccione un dispositivo de la tabla antes de continuar a Órdenes de Servicio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el dispositivo seleccionado para guardar su IMEI
            Dispositivo? dispositivoSeleccionado = await _dispositivoServices.GetById(_selectedDispositivoId);
            Cliente? clienteSeleccionado = await _clienteServices.GetByCedula(textBox1.Text.Trim());

            if (dispositivoSeleccionado == null || string.IsNullOrEmpty(dispositivoSeleccionado.Imei))
            {
                MessageBox.Show("No se pudo obtener el IMEI del dispositivo seleccionado. Asegúrese de que el dispositivo tenga un IMEI registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(clienteSeleccionado == null)
            {
                MessageBox.Show("No se pudo obtener el cliente asociado al dispositivo seleccionado. Asegúrese de que el cliente tenga una cédula registrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_dashboardParent != null)
            {
                // ¡NUEVO! Guardar el IMEI del dispositivo en el Dashboard
                _dashboardParent.SelectedDeviceImei = dispositivoSeleccionado.Imei;
                _dashboardParent.SelectedClientCedula = clienteSeleccionado?.Cedula; // También guardar la cédula del cliente
                Debug.WriteLine($"IMEI de dispositivo guardado en Dashboard: {_dashboardParent.SelectedDeviceImei}");

                this.Close(); // Cierra el formulario actual

                // ¡NUEVO! Abrir el formulario de Gestión de Órdenes de Servicio
                _dashboardParent.OpenGestionOrdenesServicio();
            }
            else
            {
                MessageBox.Show("La referencia al Dashboard principal no se ha establecido correctamente. No se puede abrir la gestión de órdenes de servicio.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
