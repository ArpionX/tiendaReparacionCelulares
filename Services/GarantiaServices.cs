using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface IGarantiaServices
    {
        Task<List<Garantia>> GetExpiringWarranties(DateTime cutoffDate);
        Task<List<Garantia>> GetByStatus(EstadoGarantia estado);
        Task<Garantia?> GetById(int id);
        Task<List<Garantia>> GetAll();
        Task<int> Insert(Garantia garantia);
        Task<int> Update(Garantia garantia);
        Task<int> Delete(int id);
    }

    public class GarantiaServices : IGarantiaServices
    {
        private readonly IGarantiaRepository _garantiaRepository;

        public GarantiaServices(IGarantiaRepository garantiaRepository)
        {
            _garantiaRepository = garantiaRepository;
        }

        public async Task<List<Garantia>> GetAll()
        {
            try
            {
                return await _garantiaRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Garantia?> GetById(int id)
        {
            try
            {
                return await _garantiaRepository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Garantia>> GetByStatus(EstadoGarantia estado)
        {
            try
            {
                return await _garantiaRepository.GetByStatus(estado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Garantia>> GetExpiringWarranties(DateTime cutoffDate)
        {
            try
            {
                return await _garantiaRepository.GetExpiringWarranties(cutoffDate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Insert(Garantia garantia)
        {
            int result = 0;
            try
            {
                await _garantiaRepository.Insert(garantia);
                result = await _garantiaRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<int> Update(Garantia garantia)
        {
            int result = 0;
            try
            {
                _garantiaRepository.Update(garantia);
                result = await _garantiaRepository.SaveChangesAsync();
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
                var garantia = await _garantiaRepository.GetById(id);
                if (garantia == null)
                    throw new Exception("Garantía no encontrada");

                _garantiaRepository.Delete(garantia);
                result = await _garantiaRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}