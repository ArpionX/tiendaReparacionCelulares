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
using TiendaReparacion.Services;

namespace TiendaReparacion.UI
{
    public partial class OrdenesServicio : Form
    {
        private readonly IOrdenServicioServices _ordenServicioServices;
        private readonly IClienteServices _clienteServices;
        private readonly IDispositivoServices _dispositivoServices;
        private readonly ITecnicoServices _tecnicoServices; // ¡NUEVO! Para buscar técnicos
        private Dashboard _dashboardParent; // Campo para almacenar la referencia al Dashboard
        private int _selectedOrdenId = 0; // Para almacenar el ID de la orden seleccionada

        public OrdenesServicio(IOrdenServicioServices ordenServicioServices,
            IClienteServices clienteServices,
            IDispositivoServices dispositivoServices,
            ITecnicoServices tecnicoServices, // ¡NUEVO! Inyecta ITecnicoServices
            Dashboard dashboard)
        {
            InitializeComponent();
            _ordenServicioServices = ordenServicioServices;
            _clienteServices = clienteServices;
            _dispositivoServices = dispositivoServices;
            _tecnicoServices = tecnicoServices; // Asigna el servicio de técnicos
            _dashboardParent = dashboard;
        }
        public void SetDashboardReference(Dashboard dashboard)
        {
            _dashboardParent = dashboard;
        }

        private async void OrdenesServicio_Load(object sender, EventArgs e)
        {
            this.Text = "Gestión de Órdenes de Servicio";
            await LoadOrdenesIntoDataGridView(); // Carga las órdenes existentes

            // Inicializar ComboBox de Estado
            comboBoxEstado.DataSource = Enum.GetValues(typeof(EstadoOrden));
            comboBoxEstado.SelectedIndex = -1; // No seleccionar nada por defecto

            // Inicializar DateTimePickers para que solo muestren la fecha
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker2.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Value = DateTime.Now.Date; // Fecha de ingreso por defecto hoy
            dateTimePicker2.Value = DateTime.Now.Date; // Fecha estimada por defecto hoy

            // Recuperar la cédula del cliente y el IMEI del dispositivo del Dashboard
            // y mostrar la información en los campos correspondientes.
            if (_dashboardParent != null)
            {
                // Cédula del Cliente
                textBoxClienteCedula.Text = _dashboardParent.SelectedClientCedula ?? string.Empty;
                textBoxClienteCedula.ReadOnly = true; // Hacerlo de solo lectura

                // Modelo del Dispositivo (buscado por IMEI)
                if (!string.IsNullOrEmpty(_dashboardParent.SelectedDeviceImei))
                {
                    Dispositivo? dispositivo = await _dispositivoServices.GetByImei(_dashboardParent.SelectedDeviceImei);
                    textBoxDispositivoImei.Text = dispositivo?.Modelo ?? "No encontrado";
                }
                else
                {
                    textBoxDispositivoImei.Text = string.Empty;
                }
                textBoxDispositivoImei.ReadOnly = true; // Hacerlo de solo lectura

                // Técnico Asignado
                if (_dashboardParent.AuthenticatedUser != null && _dashboardParent.AuthenticatedUser.IdTecnico.HasValue)
                {
                    Tecnico? tecnicoAsignado = await _tecnicoServices.GetById(_dashboardParent.AuthenticatedUser.IdTecnico.Value);
                    textBoxTecnico.Text = tecnicoAsignado?.Nombre ?? "N/A";
                }
                else
                {
                    textBoxTecnico.Text = "No asignado";
                }
                textBoxTecnico.ReadOnly = true;

                System.Diagnostics.Debug.WriteLine($"Ordenes de Servicio cargadas. Cédula: {textBoxClienteCedula.Text}, Modelo: {textBoxDispositivoImei.Text}, Técnico: {textBoxTecnico.Text}");
            }
            else
            {
                MessageBox.Show("No se pudo acceder al Dashboard principal para obtener la información del cliente/dispositivo/técnico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ClearFormFields(); // Limpia los campos adicionales después de cargar los datos iniciales
        }

        /// <summary>
        /// Carga las órdenes de servicio desde la base de datos y las muestra en el DataGridView.
        /// </summary>
        private async Task LoadOrdenesIntoDataGridView()
        {
            try
            {
                List<OrdenServicio> ordenes = await _ordenServicioServices.GetAll();

                var ordenesParaVista = new List<object>();
                foreach (var orden in ordenes)
                {
                    Cliente? cliente = await _clienteServices.GetById(orden.IdCliente);
                    Dispositivo? dispositivo = await _dispositivoServices.GetById(orden.IdDispositivo);
                    Tecnico? tecnico = null;
                    if (orden.IdTecnico.HasValue)
                    {
                        tecnico = await _tecnicoServices.GetById(orden.IdTecnico.Value);
                    }

                    ordenesParaVista.Add(new
                    {
                        orden.IdOrden,
                        orden.NumeroOrden,
                        ClienteCedula = cliente?.Cedula ?? "N/A",
                        DispositivoModelo = dispositivo?.Modelo ?? "N/A",
                        TecnicoAsignado = tecnico?.Nombre ?? "N/A",
                        orden.DescripcionProblema,
                        orden.FechaIngreso,
                        orden.FechaEntregaEstimada,
                        orden.EstadoOrden,
                        orden.Observaciones
                    });
                }

                dataGridView1.DataSource = ordenesParaVista;

                // Ocultar columnas de ID si es necesario
                if (dataGridView1.Columns.Contains("IdOrden"))
                {
                    dataGridView1.Columns["IdOrden"].Visible = false;
                }
                // Ajustar el tamaño de las columnas
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar órdenes de servicio en la tabla: {ex.Message}", "Error de Carga de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Limpia los campos del formulario y restablece la selección del DataGridView.
        /// </summary>
        private void ClearFormFields()
        {
            // No limpiar textBoxClienteCedula, textBoxDispositivoImei, textBoxTecnico aquí
            // ya que estos se cargan desde el Dashboard o se establecen al seleccionar.
            textBoxDescripcion.Text = string.Empty;
            textBoxObservaciones.Text = string.Empty;
            comboBoxEstado.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now.Date;
            dateTimePicker2.Value = DateTime.Now.Date;

            dataGridView1.ClearSelection();
            _selectedOrdenId = 0;
        }

        /// <summary>
        /// Maneja el clic en una celda del DataGridView para cargar los datos de la orden en los campos del formulario.
        /// </summary>
        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                _selectedOrdenId = (int)row.Cells["IdOrden"].Value;

                // Cargar los datos de la orden seleccionada
                OrdenServicio? orden = await _ordenServicioServices.GetById(_selectedOrdenId);
                if (orden != null)
                {
                    // Cargar Cédula del Cliente
                    Cliente? cliente = await _clienteServices.GetById(orden.IdCliente);
                    textBoxClienteCedula.Text = cliente?.Cedula ?? "N/A";

                    // Cargar Modelo del Dispositivo
                    Dispositivo? dispositivo = await _dispositivoServices.GetById(orden.IdDispositivo);
                    textBoxDispositivoImei.Text = dispositivo?.Modelo ?? "N/A";

                    // Cargar Técnico Asignado
                    if (orden.IdTecnico.HasValue)
                    {
                        Tecnico? tecnico = await _tecnicoServices.GetById(orden.IdTecnico.Value);
                        textBoxTecnico.Text = tecnico?.Nombre ?? "N/A";
                    }
                    else
                    {
                        textBoxTecnico.Text = "No asignado";
                    }

                    textBoxDescripcion.Text = orden.DescripcionProblema;
                    comboBoxEstado.SelectedItem = orden.EstadoOrden;
                    dateTimePicker1.Value = orden.FechaIngreso.Date; // Solo la fecha
                    dateTimePicker2.Value = orden.FechaEntregaEstimada?.Date ?? DateTime.Now.Date; // Solo la fecha
                    textBoxObservaciones.Text = orden.Observaciones;
                }
            }
        }

        /// <summary>
        /// Maneja el clic del botón "Limpiar Campos".
        /// </summary>
        private async void ButtonLimpiarCampos_Click(object sender, EventArgs e)
        {
            ClearFormFields();
            // Restablecer los campos de cliente/dispositivo/técnico a los valores del Dashboard
            if (_dashboardParent != null)
            {
                textBoxClienteCedula.Text = _dashboardParent.SelectedClientCedula ?? string.Empty;
                if (!string.IsNullOrEmpty(_dashboardParent.SelectedDeviceImei))
                {
                    // Al limpiar, si hay un IMEI seleccionado en Dashboard, intenta mostrar su modelo
                    Dispositivo? dispositivo = _dispositivoServices.GetByImei(_dashboardParent.SelectedDeviceImei).Result; // Usar .Result para evitar async void aquí
                    textBoxDispositivoImei.Text = dispositivo?.Modelo ?? "No encontrado";
                }
                else
                {
                    textBoxDispositivoImei.Text = string.Empty;
                }
                // ¡CAMBIO CLAVE AQUÍ! Obtener el técnico por IdTecnico del usuario autenticado
                if (_dashboardParent.AuthenticatedUser != null && _dashboardParent.AuthenticatedUser.IdTecnico.HasValue)
                {
                    Tecnico? tecnicoAsignado = await _tecnicoServices.GetById(_dashboardParent.AuthenticatedUser.IdTecnico.Value);
                    textBoxTecnico.Text = tecnicoAsignado?.Nombre ?? "N/A";
                }
                else
                {
                    textBoxTecnico.Text = "No asignado";
                }
            }
        }

        /// <summary>
        /// Maneja el clic del botón "Guardar" para insertar una nueva orden de servicio.
        /// </summary>
        private async void ButtonGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrEmpty(textBoxClienteCedula.Text) || string.IsNullOrEmpty(textBoxDispositivoImei.Text) ||
                string.IsNullOrEmpty(textBoxDescripcion.Text) || comboBoxEstado.SelectedItem == null)
            {
                MessageBox.Show("Cédula de Cliente, Modelo de Dispositivo, Descripción del Problema y Estado son campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener IDs de Cliente, Dispositivo y Técnico
            Cliente? cliente = await _clienteServices.GetByCedula(textBoxClienteCedula.Text.Trim());
            if (cliente == null)
            {
                MessageBox.Show("La Cédula de Cliente no es válida o no existe.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Para el dispositivo, usamos el IMEI que se pasó al Dashboard
            Dispositivo? dispositivo = await _dispositivoServices.GetByImei(_dashboardParent.SelectedDeviceImei);
            if (dispositivo == null)
            {
                MessageBox.Show("El IMEI del dispositivo no es válido o no existe.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int? idTecnico = null;
            if (_dashboardParent.AuthenticatedUser != null && _dashboardParent.AuthenticatedUser.IdTecnico.HasValue)
            {
                idTecnico = _dashboardParent.AuthenticatedUser.IdTecnico.Value;
            }
            else
            {
                MessageBox.Show("No hay un técnico asignado al usuario autenticado. No se puede guardar la orden.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear un número de orden simple (puedes implementar una lógica más robusta)
            string numeroOrden = $"ORD-{DateTime.Now.ToString("yyyyMMddHHmmss")}";

            OrdenServicio newOrden = new OrdenServicio
            {
                NumeroOrden = numeroOrden,
                IdCliente = cliente.IdCliente,
                IdDispositivo = dispositivo.IdDispositivo,
                IdTecnico = idTecnico,
                DescripcionProblema = textBoxDescripcion.Text.Trim(),
                FechaIngreso = dateTimePicker1.Value.Date, // Solo la fecha
                FechaEntregaEstimada = dateTimePicker2.Value.Date, // Solo la fecha
                EstadoOrden = (EstadoOrden)comboBoxEstado.SelectedItem,
                Observaciones = textBoxObservaciones.Text.Trim()
            };

            try
            {
                int result = await _ordenServicioServices.Insert(newOrden);
                if (result > 0)
                {
                    MessageBox.Show("Orden de servicio guardada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadOrdenesIntoDataGridView();
                    ClearFormFields();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la orden de servicio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la orden de servicio: {ex.Message}", "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el clic del botón "Editar" para actualizar una orden de servicio existente.
        /// </summary>
        private async void ButtonEditar_Click(object sender, EventArgs e)
        {
            if (_selectedOrdenId == 0)
            {
                MessageBox.Show("Seleccione una orden de servicio de la tabla para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validaciones
            if (string.IsNullOrEmpty(textBoxClienteCedula.Text) || string.IsNullOrEmpty(textBoxDispositivoImei.Text) ||
                string.IsNullOrEmpty(textBoxDescripcion.Text) || comboBoxEstado.SelectedItem == null)
            {
                MessageBox.Show("Cédula de Cliente, Modelo de Dispositivo, Descripción del Problema y Estado son campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener IDs de Cliente, Dispositivo y Técnico
            Cliente? cliente = await _clienteServices.GetByCedula(textBoxClienteCedula.Text.Trim());
            if (cliente == null)
            {
                MessageBox.Show("La Cédula de Cliente no es válida o no existe.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Para el dispositivo, usamos el IMEI que se pasó al Dashboard
            Dispositivo? dispositivo = await _dispositivoServices.GetByImei(_dashboardParent.SelectedDeviceImei);
            if (dispositivo == null)
            {
                MessageBox.Show("El IMEI del dispositivo no es válido o no existe.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int? idTecnico = null;
            if (_dashboardParent.AuthenticatedUser != null && _dashboardParent.AuthenticatedUser.IdTecnico.HasValue)
            {
                idTecnico = _dashboardParent.AuthenticatedUser.IdTecnico.Value;
            }
            else
            {
                MessageBox.Show("No hay un técnico asignado al usuario autenticado. No se puede actualizar la orden.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OrdenServicio? ordenToUpdate = await _ordenServicioServices.GetById(_selectedOrdenId);

            if (ordenToUpdate == null)
            {
                MessageBox.Show("Orden de servicio no encontrada para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ordenToUpdate.IdCliente = cliente.IdCliente;
            ordenToUpdate.IdDispositivo = dispositivo.IdDispositivo;
            ordenToUpdate.IdTecnico = idTecnico;
            ordenToUpdate.DescripcionProblema = textBoxDescripcion.Text.Trim();
            ordenToUpdate.FechaIngreso = dateTimePicker1.Value.Date;
            ordenToUpdate.FechaEntregaEstimada = dateTimePicker2.Value.Date;
            ordenToUpdate.EstadoOrden = (EstadoOrden)comboBoxEstado.SelectedItem;
            ordenToUpdate.Observaciones = textBoxObservaciones.Text.Trim();

            try
            {
                int result = await _ordenServicioServices.Update(ordenToUpdate);
                if (result > 0)
                {
                    MessageBox.Show("Orden de servicio actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadOrdenesIntoDataGridView();
                    ClearFormFields();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la orden de servicio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la orden de servicio: {ex.Message}", "Error de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el clic del botón "Eliminar" para borrar una orden de servicio.
        /// </summary>
        private async void ButtonEliminar_Click(object sender, EventArgs e)
        {
            if (_selectedOrdenId == 0)
            {
                MessageBox.Show("Seleccione una orden de servicio de la tabla para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar esta orden de servicio? Esta acción es irreversible.", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int result = await _ordenServicioServices.Delete(_selectedOrdenId);
                    if (result > 0)
                    {
                        MessageBox.Show("Orden de servicio eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadOrdenesIntoDataGridView();
                        ClearFormFields();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la orden de servicio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar la orden de servicio: {ex.Message}", "Error de Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async void ButtonSiguiente_Click(object sender, EventArgs e)
        {
            
        }


    }
}
