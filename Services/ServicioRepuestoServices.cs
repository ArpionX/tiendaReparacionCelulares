using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface IServicioRepuestoServices
    {
        Task<List<ServicioRepuesto>> GetByServiceId(int serviceId);
        Task<List<ServicioRepuesto>> GetByPartId(int partId);
        Task<ServicioRepuesto?> GetByIds(int serviceId, int partId);
        Task<List<ServicioRepuesto>> GetAll();
        Task<int> Insert(ServicioRepuesto servicioRepuesto);
        Task<int> Update(ServicioRepuesto servicioRepuesto);
        Task<int> Delete(int serviceId, int partId);
    }

    public class ServicioRepuestoServices : IServicioRepuestoServices
    {
        private readonly IServicioRepuestoRepository _servicioRepuestoRepository;

        public ServicioRepuestoServices(IServicioRepuestoRepository servicioRepuestoRepository)
        {
            _servicioRepuestoRepository = servicioRepuestoRepository;
        }

        public async Task<List<ServicioRepuesto>> GetAll()
        {
            try
            {
                return await _servicioRepuestoRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ServicioRepuesto?> GetByIds(int serviceId, int partId)
        {
            try
            {
                return await _servicioRepuestoRepository.GetByIds(serviceId, partId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ServicioRepuesto>> GetByServiceId(int serviceId)
        {
            try
            {
                return await _servicioRepuestoRepository.GetByServiceId(serviceId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ServicioRepuesto>> GetByPartId(int partId)
        {
            try
            {
                return await _servicioRepuestoRepository.GetByPartId(partId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Insert(ServicioRepuesto servicioRepuesto)
        {
            int result = 0;
            try
            {
                var existing = await _servicioRepuestoRepository.GetByIds(
                    servicioRepuesto.IdServicio, servicioRepuesto.IdRepuesto);

                if (existing != null)
                    throw new Exception("Esta relación servicio-repuesto ya existe");

                await _servicioRepuestoRepository.Insert(servicioRepuesto);
                result = await _servicioRepuestoRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<int> Update(ServicioRepuesto servicioRepuesto)
        {
            int result = 0;
            try
            {
                _servicioRepuestoRepository.Update(servicioRepuesto);
                result = await _servicioRepuestoRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<int> Delete(int serviceId, int partId)
        {
            int result = 0;
            try
            {
                var servicioRepuesto = await _servicioRepuestoRepository.GetByIds(serviceId, partId);
                if (servicioRepuesto == null)
                    throw new Exception("Relación servicio-repuesto no encontrada");

                _servicioRepuestoRepository.Delete(servicioRepuesto);
                result = await _servicioRepuestoRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}