using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface ITecnicoServices
    {
        Task<List<Tecnico>> GetAll();
        Task<Tecnico?> GetById(int id);
        Task<Tecnico?> GetByName(string nombre); // ¡NUEVO!
        Task<int> Insert(Tecnico tecnico);
        Task<int> Update(Tecnico tecnico);
        Task<int> Delete(int id);
    }
    public class TecnicoServices : ITecnicoServices
    {
        private readonly ITecnicoRepository _tecnicoRepository;

        public TecnicoServices(ITecnicoRepository tecnicoRepository)
        {
            _tecnicoRepository = tecnicoRepository;
        }

        public async Task<List<Tecnico>> GetAll()
        {
            List<Tecnico> tecnicos = new List<Tecnico>();
            try
            {
                tecnicos = await _tecnicoRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
            return tecnicos;
        }

        public async Task<Tecnico?> GetById(int id)
        {
            Tecnico? tecnico = null;
            try
            {
                tecnico = await _tecnicoRepository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
            return tecnico;
        }

        /// <summary>
        /// Obtiene un técnico por su nombre.
        /// </summary>
        /// <param name="nombre">El nombre del técnico.</param>
        /// <returns>La instancia del técnico si se encuentra, de lo contrario null.</returns>
        public async Task<Tecnico?> GetByName(string nombre) // ¡NUEVO!
        {
            Tecnico? tecnico = null;
            try
            {
                tecnico = await _tecnicoRepository.GetByName(nombre);
            }
            catch (Exception)
            {
                throw;
            }
            return tecnico;
        }

        public async Task<int> Insert(Tecnico tecnico)
        {
            int result = 0;
            try
            {
                await _tecnicoRepository.Insert(tecnico);
                result = 1;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<int> Update(Tecnico tecnico)
        {
            int result = 0;
            try
            {
                Tecnico? existingTecnico = await _tecnicoRepository.GetById(tecnico.IdTecnico);

                if (existingTecnico is null)
                {
                    throw new Exception("Técnico no encontrado para actualizar.");
                }

                existingTecnico.Nombre = tecnico.Nombre;
                existingTecnico.Apellido = tecnico.Apellido;
                existingTecnico.Especialidad = tecnico.Especialidad;
                existingTecnico.Telefono = tecnico.Telefono;
                existingTecnico.Email = tecnico.Email;
                existingTecnico.FechaIngreso = tecnico.FechaIngreso;
                existingTecnico.Estado = tecnico.Estado;

                _tecnicoRepository.Update(existingTecnico);
                result = 1;
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
                Tecnico? tecnico = await _tecnicoRepository.GetById(id);

                if (tecnico is null) throw new Exception("Técnico no registrado");

                _tecnicoRepository.Delete(tecnico);
                result = 1;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
