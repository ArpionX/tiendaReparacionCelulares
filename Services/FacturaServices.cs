using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface IFacturaServices
    {
        Task<Factura?> GetByNumber(string numeroFactura);
        Task<List<Factura>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<Factura?> GetById(int id);
        Task<List<Factura>> GetAll();
        Task<int> Insert(Factura factura);
        Task<int> Update(Factura factura);
        Task<int> Delete(int id);
    }

    public class FacturaServices : IFacturaServices
    {
        private readonly IFacturaRepository _facturaRepository;

        public FacturaServices(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public async Task<List<Factura>> GetAll()
        {
            try
            {
                return await _facturaRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Factura?> GetById(int id)
        {
            try
            {
                return await _facturaRepository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Factura?> GetByNumber(string numeroFactura)
        {
            try
            {
                return await _facturaRepository.GetByNumber(numeroFactura);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Factura>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _facturaRepository.GetByDateRange(startDate, endDate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Insert(Factura factura)
        {
            int result = 0;
            try
            {
                var existingFactura = await _facturaRepository.GetByNumber(factura.NumeroFactura);
                if (existingFactura != null)
                    throw new Exception($"La factura {factura.NumeroFactura} ya está registrada");

                await _facturaRepository.Insert(factura);
                result = await _facturaRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<int> Update(Factura factura)
        {
            int result = 0;
            try
            {
                var existingFactura = await _facturaRepository.GetByNumber(factura.NumeroFactura);
                if (existingFactura != null && existingFactura.IdFactura != factura.IdFactura)
                    throw new Exception($"La factura {factura.NumeroFactura} ya está registrada");

                _facturaRepository.Update(factura);
                result = await _facturaRepository.SaveChangesAsync();
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
                var factura = await _facturaRepository.GetById(id);
                if (factura == null)
                    throw new Exception("Factura no encontrada");

                _facturaRepository.Delete(factura);
                result = await _facturaRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}