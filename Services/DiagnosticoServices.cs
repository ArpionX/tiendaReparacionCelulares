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

        public async Task<int> Insert(Diagnostico diagnostico)
        {
            int result = 0;
            try
            {
                await _diagnosticoRepository.Insert(diagnostico);
                result = await _diagnosticoRepository.SaveChangesAsync();
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
                _diagnosticoRepository.Update(diagnostico);
                result = await _diagnosticoRepository.SaveChangesAsync();
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
                result = await _diagnosticoRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
