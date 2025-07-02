using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface IPagoServices
    {
        Task<List<Pago>> GetByPaymentMethod(MetodoPago metodo);
        Task<List<Pago>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<Pago?> GetById(int id);
        Task<List<Pago>> GetAll();
        Task<int> Insert(Pago pago);
        Task<int> Update(Pago pago);
        Task<int> Delete(int id);
    }

    public class PagoServices : IPagoServices
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoServices(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        public async Task<List<Pago>> GetAll()
        {
            try
            {
                return await _pagoRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Pago?> GetById(int id)
        {
            try
            {
                return await _pagoRepository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Pago>> GetByPaymentMethod(MetodoPago metodo)
        {
            try
            {
                return await _pagoRepository.GetByPaymentMethod(metodo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Pago>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _pagoRepository.GetByDateRange(startDate, endDate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Insert(Pago pago)
        {
            int result = 0;
            try
            {
                await _pagoRepository.Insert(pago);
                result = await _pagoRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<int> Update(Pago pago)
        {
            int result = 0;
            try
            {
                _pagoRepository.Update(pago);
                result = await _pagoRepository.SaveChangesAsync();
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
                var pago = await _pagoRepository.GetById(id);
                if (pago == null)
                    throw new Exception("Pago no encontrado");

                _pagoRepository.Delete(pago);
                result = await _pagoRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
