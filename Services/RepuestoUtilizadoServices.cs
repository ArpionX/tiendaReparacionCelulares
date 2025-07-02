using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface IRepuestoUtilizadoServices
    {
        Task<List<RepuestoUtilizado>> GetByOrderId(int orderId);
        Task<List<RepuestoUtilizado>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<RepuestoUtilizado?> GetById(int id);
        Task<List<RepuestoUtilizado>> GetAll();
        Task<int> Insert(RepuestoUtilizado repuestoUtilizado);
        Task<int> Update(RepuestoUtilizado repuestoUtilizado);
        Task<int> Delete(int id);
    }

    public class RepuestoUtilizadoServices : IRepuestoUtilizadoServices
    {
        private readonly IRepuestoUtilizadoRepository _repuestoUtilizadoRepository;

        public RepuestoUtilizadoServices(IRepuestoUtilizadoRepository repuestoUtilizadoRepository)
        {
            _repuestoUtilizadoRepository = repuestoUtilizadoRepository;
        }

        public async Task<List<RepuestoUtilizado>> GetAll()
        {
            try
            {
                return await _repuestoUtilizadoRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RepuestoUtilizado?> GetById(int id)
        {
            try
            {
                return await _repuestoUtilizadoRepository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RepuestoUtilizado>> GetByOrderId(int orderId)
        {
            try
            {
                return await _repuestoUtilizadoRepository.GetByOrderId(orderId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RepuestoUtilizado>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _repuestoUtilizadoRepository.GetByDateRange(startDate, endDate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Insert(RepuestoUtilizado repuestoUtilizado)
        {
            int result = 0;
            try
            {
                await _repuestoUtilizadoRepository.Insert(repuestoUtilizado);
                result = await _repuestoUtilizadoRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<int> Update(RepuestoUtilizado repuestoUtilizado)
        {
            int result = 0;
            try
            {
                _repuestoUtilizadoRepository.Update(repuestoUtilizado);
                result = await _repuestoUtilizadoRepository.SaveChangesAsync();
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
                var repuestoUtilizado = await _repuestoUtilizadoRepository.GetById(id);
                if (repuestoUtilizado == null)
                    throw new Exception("Repuesto utilizado no encontrado");

                _repuestoUtilizadoRepository.Delete(repuestoUtilizado);
                result = await _repuestoUtilizadoRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}