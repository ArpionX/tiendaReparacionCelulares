using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface IServicioServices
    {
        Task<List<Servicio>> GetByCategory(CategoriaServicio categoria);
        Task<Servicio?> GetByName(string nombre);
        Task<Servicio?> GetById(int id);
        Task<List<Servicio>> GetAll();
        Task<int> Insert(Servicio servicio);
        Task<int> Update(Servicio servicio);
        Task<int> Delete(int id);
    }

    public class ServicioServices : IServicioServices
    {
        private readonly IServicioRepository _servicioRepository;

        public ServicioServices(IServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        public async Task<List<Servicio>> GetAll()
        {
            try
            {
                return await _servicioRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Servicio?> GetById(int id)
        {
            try
            {
                return await _servicioRepository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Servicio?> GetByName(string nombre)
        {
            try
            {
                return await _servicioRepository.GetByName(nombre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Servicio>> GetByCategory(CategoriaServicio categoria)
        {
            try
            {
                return await _servicioRepository.GetByCategory(categoria);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Insert(Servicio servicio)
        {
            int result = 0;
            try
            {
                var existingServicio = await _servicioRepository.GetByName(servicio.NombreServicio);
                if (existingServicio != null)
                    throw new Exception($"El servicio {servicio.NombreServicio} ya está registrado");

                await _servicioRepository.Insert(servicio);
                result = await _servicioRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<int> Update(Servicio servicio)
        {
            int result = 0;
            try
            {
                var existingServicio = await _servicioRepository.GetByName(servicio.NombreServicio);
                if (existingServicio != null && existingServicio.IdServicio != servicio.IdServicio)
                    throw new Exception($"El servicio {servicio.NombreServicio} ya está registrado");

                _servicioRepository.Update(servicio);
                result = await _servicioRepository.SaveChangesAsync();
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
                var servicio = await _servicioRepository.GetById(id);
                if (servicio == null)
                    throw new Exception("Servicio no encontrado");

                _servicioRepository.Delete(servicio);
                result = await _servicioRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}