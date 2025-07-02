using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface IDetalleCotizacionServices
    {
        Task<List<DetalleCotizacion>> GetByQuoteId(int quoteId);
        Task<DetalleCotizacion?> GetById(int id);
        Task<List<DetalleCotizacion>> GetAll();
        Task<int> Insert(DetalleCotizacion detalle);
        Task<int> Update(DetalleCotizacion detalle);
        Task<int> Delete(int id);
    }

    public class DetalleCotizacionServices : IDetalleCotizacionServices
    {
        private readonly IDetalleCotizacionRepository _detalleCotizacionRepository;

        public DetalleCotizacionServices(IDetalleCotizacionRepository detalleCotizacionRepository)
        {
            _detalleCotizacionRepository = detalleCotizacionRepository;
        }

        public async Task<List<DetalleCotizacion>> GetAll()
        {
            try
            {
                return await _detalleCotizacionRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DetalleCotizacion?> GetById(int id)
        {
            try
            {
                return await _detalleCotizacionRepository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DetalleCotizacion>> GetByQuoteId(int quoteId)
        {
            try
            {
                return await _detalleCotizacionRepository.GetByQuoteId(quoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Insert(DetalleCotizacion detalle)
        {
            int result = 0;
            try
            {
                await _detalleCotizacionRepository.Insert(detalle);
                result = await _detalleCotizacionRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<int> Update(DetalleCotizacion detalle)
        {
            int result = 0;
            try
            {
                _detalleCotizacionRepository.Update(detalle);
                result = await _detalleCotizacionRepository.SaveChangesAsync();
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
                var detalle = await _detalleCotizacionRepository.GetById(id);
                if (detalle == null)
                    throw new Exception("Detalle de cotización no encontrado");

                _detalleCotizacionRepository.Delete(detalle);
                result = await _detalleCotizacionRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}