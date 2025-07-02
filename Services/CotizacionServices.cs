using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface ICotizacionServices
    {
        Task<List<Cotizacion>> GetPendingApproval();
        Task<List<Cotizacion>> GetExpiringQuotes(DateTime cutoffDate);
        Task<Cotizacion?> GetById(int id);
        Task<List<Cotizacion>> GetAll();
        Task<int> Insert(Cotizacion cotizacion);
        Task<int> Update(Cotizacion cotizacion);
        Task<int> Delete(int id);
    }

    public class CotizacionServices : ICotizacionServices
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public CotizacionServices(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<List<Cotizacion>> GetAll()
        {
            try
            {
                return await _cotizacionRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Cotizacion?> GetById(int id)
        {
            try
            {
                return await _cotizacionRepository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Cotizacion>> GetPendingApproval()
        {
            try
            {
                return await _cotizacionRepository.GetPendingApproval();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Cotizacion>> GetExpiringQuotes(DateTime cutoffDate)
        {
            try
            {
                return await _cotizacionRepository.GetExpiringQuotes(cutoffDate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Insert(Cotizacion cotizacion)
        {
            int result = 0;
            try
            {
                await _cotizacionRepository.Insert(cotizacion);
                result = await _cotizacionRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<int> Update(Cotizacion cotizacion)
        {
            int result = 0;
            try
            {
                _cotizacionRepository.Update(cotizacion);
                result = await _cotizacionRepository.SaveChangesAsync();
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
                var cotizacion = await _cotizacionRepository.GetById(id);
                if (cotizacion == null)
                    throw new Exception("Cotización no encontrada");

                _cotizacionRepository.Delete(cotizacion);
                result = await _cotizacionRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}