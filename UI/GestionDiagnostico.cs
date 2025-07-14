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
    public partial class GestionDiagnostico : Form
    {
        private readonly IDiagnosticoServices _diagnosticoServices;
        private readonly IOrdenServicioServices _ordenServicioServices; // Para actualizar el estado de la orden
        private readonly ITecnicoServices _tecnicoServices; // Para obtener el nombre del técnico
        private Dashboard _dashboardParent; // Referencia al Dashboard

        private int _selectedDiagnosticoId = 0; // Para almacenar el ID del diagnóstico seleccionado
        private int _currentOrdenId = 0;
        public GestionDiagnostico(IDiagnosticoServices diagnosticoServices,
            IOrdenServicioServices ordenServicioServices,
            ITecnicoServices tecnicoServices,
            Dashboard dashboard)
        {
            InitializeComponent();
            _diagnosticoServices = diagnosticoServices;
            _ordenServicioServices = ordenServicioServices;
            _tecnicoServices = tecnicoServices;
            _dashboardParent = dashboard;
        }
        public void SetDashboardReference(Dashboard dashboard)
        {
            _dashboardParent = dashboard;
        }
        private async void GestionDiagnostico_Load(object sender, EventArgs e)
        {
            this.Text = "Gestión de Diagnóstico";

            // Recuperar datos de la orden de servicio del Dashboard
            if (_dashboardParent != null && _dashboardParent.SelectedOrdenId.HasValue)
            {
                _currentOrdenId = _dashboardParent.SelectedOrdenId.Value;
                textBoxOrden.Text = _dashboardParent.SelectedOrdenNumero ?? "N/A";
                textBoxDescripcionProblema.Text = _dashboardParent.SelectedOrdenDescripcionProblema ?? string.Empty;

                // Cargar el nombre del técnico autenticado
                if (_dashboardParent.AuthenticatedUser != null && _dashboardParent.AuthenticatedUser.IdTecnico.HasValue)
                {
                    Tecnico? tecnicoAsignado = await _tecnicoServices.GetById(_dashboardParent.AuthenticatedUser.IdTecnico.Value);
                    textBoxTecnico.Text = tecnicoAsignado?.Nombre ?? "N/A";
                }
                else
                {
                    textBoxTecnico.Text = "No asignado";
                }

                // Hacer los campos de solo lectura
                textBoxOrden.ReadOnly = true;
                textBoxDescripcionProblema.ReadOnly = true;
                textBoxTecnico.ReadOnly = true;

                Debug.WriteLine($"Diagnóstico cargado para Orden N°: {textBoxOrden.Text}, Descripción: {textBoxDescripcionProblema.Text}, Técnico: {textBoxTecnico.Text}");
            }
            else
            {
                MessageBox.Show("No se pudo obtener la información de la orden de servicio del Dashboard.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxOrden.Text = "Error";
                textBoxDescripcionProblema.Text = "Error al cargar";
                textBoxTecnico.Text = "Error";
            }

            // Inicializar ComboBox de Estado de la ORDEN
            comboBoxEstado.DataSource = Enum.GetValues(typeof(EstadoOrden));
            comboBoxEstado.SelectedIndex = -1; // No seleccionar nada por defecto

            await LoadDiagnosticosIntoDataGridView(_currentOrdenId); // Carga diagnósticos para la orden actual
            ClearFormFields(); // Limpia los campos de entrada del diagnóstico
        }
        private async Task LoadDiagnosticosIntoDataGridView(int idOrden)
        {
            try
            {
                List<Diagnostico> diagnosticos = await _diagnosticoServices.GetAll();

                var diagnosticosParaVista = new List<object>();
                foreach (var diag in diagnosticos)
                {
                    OrdenServicio? orden = await _ordenServicioServices.GetById(diag.IdOrden);
                    Tecnico? tecnico = null;
                    if (diag.IdTecnico.HasValue)
                    {
                        tecnico = await _tecnicoServices.GetById(diag.IdTecnico.Value);
                    }

                    diagnosticosParaVista.Add(new
                    {
                        diag.IdDiagnostico,
                        OrdenNumero = orden?.NumeroOrden ?? "N/A",
                        TecnicoAsignado = tecnico?.Nombre ?? "N/A",
                        diag.DescripcionProblema,
                        diag.CausaRaiz,
                        diag.SolucionPropuesta,
                        diag.TiempoEstimadoHoras,
                        diag.FechaDiagnostico
                    });
                }

                dataGridView1.DataSource = diagnosticosParaVista;

                if (dataGridView1.Columns.Contains("IdDiagnostico"))
                {
                    dataGridView1.Columns["IdDiagnostico"].Visible = false;
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar diagnósticos en la tabla: {ex.Message}", "Error de Carga de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearFormFields()
        {
            textBoxCausaRaiz.Text = string.Empty;
            textBoxSolucionPropuesta.Text = string.Empty;
            textBoxTiempoEstimado.Text = string.Empty;
            // comboBoxEstado.SelectedIndex = -1; // No limpiar el estado de la orden al limpiar campos de diagnóstico
            _selectedDiagnosticoId = 0;
            dataGridView1.ClearSelection();
        }
        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                _selectedDiagnosticoId = (int)row.Cells["IdDiagnostico"].Value;

                Diagnostico? diagnostico = await _diagnosticoServices.GetById(_selectedDiagnosticoId);
                if (diagnostico != null)
                {
                    // Cargar los campos de la orden (ya están en solo lectura, solo se actualiza si cambian)
                    OrdenServicio? orden = await _ordenServicioServices.GetById(diagnostico.IdOrden);
                    textBoxOrden.Text = orden?.NumeroOrden ?? "N/A";
                    textBoxDescripcionProblema.Text = orden?.DescripcionProblema ?? string.Empty;

                    // Cargar el técnico
                    if (diagnostico.IdTecnico.HasValue)
                    {
                        Tecnico? tecnico = await _tecnicoServices.GetById(diagnostico.IdTecnico.Value);
                        textBoxTecnico.Text = tecnico?.Nombre ?? "N/A";
                    }
                    else
                    {
                        textBoxTecnico.Text = "No asignado";
                    }

                    textBoxCausaRaiz.Text = diagnostico.CausaRaiz;
                    textBoxSolucionPropuesta.Text = diagnostico.SolucionPropuesta;
                    textBoxTiempoEstimado.Text = diagnostico.TiempoEstimadoHoras?.ToString() ?? string.Empty;

                    // Cargar el estado de la orden (del objeto OrdenServicio)
                    if (orden != null)
                    {
                        comboBoxEstado.SelectedItem = orden.EstadoOrden;
                    }
                }
            }
        }

        /// <summary>
        /// Maneja el clic del botón "Limpiar".
        /// </summary>
        private void ButtonLimpiar_Click(object sender, EventArgs e)
        {
            ClearFormFields();
            // Restablecer los campos de la orden y técnico desde el Dashboard
            if (_dashboardParent != null)
            {
                textBoxOrden.Text = _dashboardParent.SelectedOrdenNumero ?? "N/A";
                textBoxDescripcionProblema.Text = _dashboardParent.SelectedOrdenDescripcionProblema ?? string.Empty;
                if (_dashboardParent.AuthenticatedUser != null && _dashboardParent.AuthenticatedUser.IdTecnico.HasValue)
                {
                    Tecnico? tecnicoAsignado = _tecnicoServices.GetById(_dashboardParent.AuthenticatedUser.IdTecnico.Value).Result; // Usar .Result para async void
                    textBoxTecnico.Text = tecnicoAsignado?.Nombre ?? "N/A";
                }
                else
                {
                    textBoxTecnico.Text = "No asignado";
                }
            }
        }
        /// <summary>
        /// Maneja el clic del botón "Editar" para actualizar un diagnóstico existente.
        /// </summary>
        private async void ButtonEditar_Click(object sender, EventArgs e)
        {
            if (_selectedDiagnosticoId == 0)
            {
                MessageBox.Show("Seleccione un diagnóstico de la tabla para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_currentOrdenId == 0 || string.IsNullOrEmpty(textBoxDescripcionProblema.Text) ||
                string.IsNullOrEmpty(textBoxCausaRaiz.Text) || string.IsNullOrEmpty(textBoxSolucionPropuesta.Text) ||
                string.IsNullOrEmpty(textBoxTiempoEstimado.Text) || comboBoxEstado.SelectedItem == null)
            {
                MessageBox.Show("Todos los campos de diagnóstico (Causa Raíz, Solución Propuesta, Tiempo Estimado, Estado) son obligatorios, y debe haber una orden seleccionada.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBoxTiempoEstimado.Text, out int tiempoEstimadoHoras))
            {
                MessageBox.Show("El Tiempo Estimado debe ser un número entero válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? idTecnico = null;
            if (_dashboardParent.AuthenticatedUser != null && _dashboardParent.AuthenticatedUser.IdTecnico.HasValue)
            {
                idTecnico = _dashboardParent.AuthenticatedUser.IdTecnico.Value;
            }
            else
            {
                MessageBox.Show("No hay un técnico asignado al usuario autenticado. No se puede actualizar el diagnóstico.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Diagnostico? diagnosticoToUpdate = await _diagnosticoServices.GetById(_selectedDiagnosticoId);

            if (diagnosticoToUpdate == null)
            {
                MessageBox.Show("Diagnóstico no encontrado para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            diagnosticoToUpdate.IdOrden = _currentOrdenId;
            diagnosticoToUpdate.IdTecnico = idTecnico;
            diagnosticoToUpdate.DescripcionProblema = textBoxDescripcionProblema.Text.Trim();
            diagnosticoToUpdate.CausaRaiz = textBoxCausaRaiz.Text.Trim();
            diagnosticoToUpdate.SolucionPropuesta = textBoxSolucionPropuesta.Text.Trim();
            diagnosticoToUpdate.TiempoEstimadoHoras = tiempoEstimadoHoras;
            diagnosticoToUpdate.FechaDiagnostico = DateTime.Now; // Actualiza la fecha de diagnóstico

            try
            {
                int result = await _diagnosticoServices.Update(diagnosticoToUpdate);
                if (result > 0)
                {
                    MessageBox.Show("Diagnóstico actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDiagnosticosIntoDataGridView(_currentOrdenId);
                    ClearFormFields();

                    // ¡NUEVO! Actualizar el estado de la orden de servicio
                    OrdenServicio? ordenToUpdate = await _ordenServicioServices.GetById(_currentOrdenId);
                    if (ordenToUpdate != null)
                    {
                        ordenToUpdate.EstadoOrden = (EstadoOrden)comboBoxEstado.SelectedItem;
                        await _ordenServicioServices.Update(ordenToUpdate);
                        MessageBox.Show($"Estado de la orden N° {ordenToUpdate.NumeroOrden} actualizado a: {ordenToUpdate.EstadoOrden}.", "Estado de Orden", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el diagnóstico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar diagnóstico: {ex.Message}", "Error de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el clic del botón "Buscar" para filtrar diagnósticos por número de orden.
        /// </summary>
        private async void ButtonBuscar_Click(object sender, EventArgs e)
        {
            string searchText = textBox8.Text.Trim(); // Asumo que textBox8 es el campo para buscar por número de orden
            if (string.IsNullOrEmpty(searchText))
            {
                await LoadDiagnosticosIntoDataGridView(_currentOrdenId); // Si está vacío, recarga los de la orden actual
                return;
            }

            try
            {
                OrdenServicio? orden = await _ordenServicioServices.GetByNumberOrden(searchText);
                List<Diagnostico> resultados = new List<Diagnostico>();

                if (orden != null)
                {
                    resultados = await _diagnosticoServices.GetByOrdenId(orden.IdOrden);
                }

                var resultadosParaVista = new List<object>();
                foreach (var diag in resultados)
                {
                    Tecnico? tecnico = null;
                    if (diag.IdTecnico.HasValue)
                    {
                        tecnico = await _tecnicoServices.GetById(diag.IdTecnico.Value);
                    }

                    resultadosParaVista.Add(new
                    {
                        diag.IdDiagnostico,
                        OrdenNumero = orden?.NumeroOrden ?? "N/A",
                        TecnicoAsignado = tecnico?.Nombre ?? "N/A",
                        diag.DescripcionProblema,
                        diag.CausaRaiz,
                        diag.SolucionPropuesta,
                        diag.TiempoEstimadoHoras,
                        diag.FechaDiagnostico
                    });
                }

                dataGridView1.DataSource = resultadosParaVista;
                if (resultadosParaVista.Count == 0)
                {
                    MessageBox.Show($"No se encontraron diagnósticos para la orden N° '{searchText}'.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // No limpiar los campos de entrada aquí para ver los resultados de la búsqueda
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar diagnóstico: {ex.Message}", "Error de Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el clic del botón "Eliminar" para borrar un diagnóstico.
        /// </summary>
        private async void ButtonEliminar_Click(object sender, EventArgs e)
        {
            if (_selectedDiagnosticoId == 0)
            {
                MessageBox.Show("Seleccione un diagnóstico de la tabla para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar este diagnóstico? Esta acción es irreversible.", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int result = await _diagnosticoServices.Delete(_selectedDiagnosticoId);
                    if (result > 0)
                    {
                        MessageBox.Show("Diagnóstico eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDiagnosticosIntoDataGridView(_currentOrdenId);
                        ClearFormFields();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el diagnóstico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar diagnóstico: {ex.Message}", "Error de Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Maneja el clic del botón "Siguiente" (hacia Gestión de Cotización).
        /// </summary>
        private void ButtonSiguiente_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad para ir a Gestión de Cotización aún no implementada.", "Próximamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Aquí iría la lógica para abrir GestionCotizacion, similar a cómo se abren otros formularios
            // _dashboardParent.OpenGestionCotizacion(...);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// Maneja el clic del botón "Guardar" para insertar un nuevo diagnóstico.
        /// </summary>
        private async void ButtonGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (_currentOrdenId == 0 || string.IsNullOrEmpty(textBoxDescripcionProblema.Text) ||
                string.IsNullOrEmpty(textBoxCausaRaiz.Text) || string.IsNullOrEmpty(textBoxSolucionPropuesta.Text) ||
                string.IsNullOrEmpty(textBoxTiempoEstimado.Text) || comboBoxEstado.SelectedItem == null)
            {
                MessageBox.Show("Todos los campos de diagnóstico (Causa Raíz, Solución Propuesta, Tiempo Estimado, Estado) son obligatorios, y debe haber una orden seleccionada.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBoxTiempoEstimado.Text, out int tiempoEstimadoHoras))
            {
                MessageBox.Show("El Tiempo Estimado debe ser un número entero válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? idTecnico = null;
            if (_dashboardParent.AuthenticatedUser != null && _dashboardParent.AuthenticatedUser.IdTecnico.HasValue)
            {
                idTecnico = _dashboardParent.AuthenticatedUser.IdTecnico.Value;
            }
            else
            {
                MessageBox.Show("No hay un técnico asignado al usuario autenticado. No se puede guardar el diagnóstico.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Diagnostico newDiagnostico = new Diagnostico
            {
                IdOrden = _currentOrdenId,
                IdTecnico = idTecnico,
                DescripcionProblema = textBoxDescripcionProblema.Text.Trim(), // Usa la descripción de la orden
                CausaRaiz = textBoxCausaRaiz.Text.Trim(),
                SolucionPropuesta = textBoxSolucionPropuesta.Text.Trim(),
                TiempoEstimadoHoras = tiempoEstimadoHoras,
                FechaDiagnostico = DateTime.Now // Fecha y hora actual del diagnóstico
            };

            try
            {
                int result = await _diagnosticoServices.Insert(newDiagnostico);
                if (result > 0)
                {
                    MessageBox.Show("Diagnóstico guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDiagnosticosIntoDataGridView(_currentOrdenId);
                    ClearFormFields();

                    // ¡NUEVO! Actualizar el estado de la orden de servicio
                    OrdenServicio? ordenToUpdate = await _ordenServicioServices.GetById(_currentOrdenId);
                    if (ordenToUpdate != null)
                    {
                        ordenToUpdate.EstadoOrden = (EstadoOrden)comboBoxEstado.SelectedItem;
                        await _ordenServicioServices.Update(ordenToUpdate);
                        MessageBox.Show($"Estado de la orden N° {ordenToUpdate.NumeroOrden} actualizado a: {ordenToUpdate.EstadoOrden}.", "Estado de Orden", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el diagnóstico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar diagnóstico: {ex.Message}", "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
