using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface IDiagnosticoServices
    {
        Task<Diagnostico?> GetById(int id);
        Task<List<Diagnostico>> GetAll();
        Task<List<Diagnostico>> GetByOrdenId(int idOrden); // Nuevo método para buscar por ID de Orden

        Task<int> Insert(Diagnostico orden);
        Task<int> Update(Diagnostico orden);
        Task<int> Delete(int id);
    }
    public class DiagnosticoServices : IDiagnosticoServices
    {
        private readonly IDiagnosticoRepository _diagnosticoRepository;

        public DiagnosticoServices(IDiagnosticoRepository diagnosticoRepository)
        {
            _diagnosticoRepository = diagnosticoRepository;
        }       

        public async Task<List<Diagnostico>> GetAll()
        {
            List<Diagnostico> diagnosticos = new List<Diagnostico>();
            try
            {
                diagnosticos = await _diagnosticoRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
            return diagnosticos;
        }

        public async Task<Diagnostico?> GetById(int id)
        {
            Diagnostico? diagnostico = new Diagnostico();
            try
            {
                diagnostico = await _diagnosticoRepository.GetById(id);
            }
            catch (Exception)
            {

                throw;
            }
            return diagnostico;
        }
        public async Task<List<Diagnostico>> GetByOrdenId(int idOrden)
        {
            List<Diagnostico> diagnosticos = new List<Diagnostico>();
            try
            {
                diagnosticos = await _diagnosticoRepository.GetByOrdenId(idOrden);
            }
            catch (Exception)
            {
                throw;
            }
            return diagnosticos;
        }
        public async Task<int> Insert(Diagnostico diagnostico)
        {
            int result = 0;
            try
            {
                await _diagnosticoRepository.Insert(diagnostico);
                result = 1; // Indica que la inserción fue exitosa
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public async Task<int> Update(Diagnostico diagnostico)
        {
            int result = 0;
            try
            {
                Diagnostico? existingDiagnostico = await _diagnosticoRepository.GetById(diagnostico.IdDiagnostico);

                if (existingDiagnostico is null)
                {
                    throw new Exception("Diagnóstico no encontrado para actualizar.");
                }

                existingDiagnostico.IdOrden = diagnostico.IdOrden;
                existingDiagnostico.IdTecnico = diagnostico.IdTecnico;
                existingDiagnostico.DescripcionProblema = diagnostico.DescripcionProblema;
                existingDiagnostico.CausaRaiz = diagnostico.CausaRaiz;
                existingDiagnostico.SolucionPropuesta = diagnostico.SolucionPropuesta;
                existingDiagnostico.TiempoEstimadoHoras = diagnostico.TiempoEstimadoHoras;
                existingDiagnostico.FechaDiagnostico = diagnostico.FechaDiagnostico;

                _diagnosticoRepository.Update(existingDiagnostico);
                result = 1;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public async Task<int> Delete(int id)
        {
            int result = 0;
            try
            {
                Diagnostico? diagnostico = await _diagnosticoRepository.GetById(id);

                if (diagnostico is null) throw new Exception("Diagnostico no registrado");

                _diagnosticoRepository.Delete(diagnostico);
                result = 1; // Indica que la eliminación fue exitosa
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
