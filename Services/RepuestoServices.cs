using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface IRepuestoServices
    {
        Task<List<Repuesto>> GetByCompatibleModel(string modelo);
        Task<List<Repuesto>> GetLowStock(int threshold = 5);
        Task<Repuesto?> GetByName(string nombre);
        Task<Repuesto?> GetById(int id);
        Task<List<Repuesto>> GetAll();
        Task<int> Insert(Repuesto repuesto);
        Task<int> Update(Repuesto repuesto);
        Task<int> Delete(int id);
    }

    public class RepuestoServices : IRepuestoServices
    {
        private readonly IRepuestoRepository _repuestoRepository;

        public RepuestoServices(IRepuestoRepository repuestoRepository)
        {
            _repuestoRepository = repuestoRepository;
        }

        public async Task<List<Repuesto>> GetAll()
        {
            try
            {
                return await _repuestoRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Repuesto?> GetById(int id)
        {
            try
            {
                return await _repuestoRepository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Repuesto?> GetByName(string nombre)
        {
            try
            {
                return await _repuestoRepository.GetByName(nombre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Repuesto>> GetByCompatibleModel(string modelo)
        {
            try
            {
                return await _repuestoRepository.GetByCompatibleModel(modelo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Repuesto>> GetLowStock(int threshold = 5)
        {
            try
            {
                return await _repuestoRepository.GetLowStock(threshold);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Insert(Repuesto repuesto)
        {
            int result = 0;
            try
            {
                var existingRepuesto = await _repuestoRepository.GetByName(repuesto.Nombre);
                if (existingRepuesto != null)
                    throw new Exception($"El repuesto {repuesto.Nombre} ya está registrado");

                await _repuestoRepository.Insert(repuesto);
                result = await _repuestoRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<int> Update(Repuesto repuesto)
        {
            int result = 0;
            try
            {
                var existingRepuesto = await _repuestoRepository.GetByName(repuesto.Nombre);
                if (existingRepuesto != null && existingRepuesto.IdRepuesto != repuesto.IdRepuesto)
                    throw new Exception($"El repuesto {repuesto.Nombre} ya está registrado");

                _repuestoRepository.Update(repuesto);
                result = await _repuestoRepository.SaveChangesAsync();
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
                var repuesto = await _repuestoRepository.GetById(id);
                if (repuesto == null)
                    throw new Exception("Repuesto no encontrado");

                _repuestoRepository.Delete(repuesto);
                result = await _repuestoRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}