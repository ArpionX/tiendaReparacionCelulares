using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;

namespace TiendaReparacion.Services
{
    public interface IClienteServices
    {
        Task<Cliente?> GetByEmail(string email);
        Task<Cliente?> GetByCedula(string cedula); // ¡NUEVO!
        Task<List<Cliente>> GetByName(string name);
        Task<Cliente?> GetById(int id);
        Task<List<Cliente>> GetAll();
        Task<int> Insert(Cliente cliente);
        Task<int> Update(Cliente cliente);
        Task<int> Delete(int id);
    }
    public class ClienteServices: IClienteServices
    {
        private readonly IClienteRepository _clientRepository;

        public ClienteServices(IClienteRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

     

        public async Task<List<Cliente>> GetAll()
        {
            List<Cliente> clients = new List<Cliente>();
            try
            {
                clients = await _clientRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
            return clients;
        }

        public async Task<Cliente?> GetById(int id)
        {
            Cliente? client = new Cliente();
            try
            {
                client = await _clientRepository.GetById(id);
            }
            catch (Exception)
            {

                throw;
            }
            return client;
        }
        public async Task<Cliente?> GetByEmail(string email)
        {
            Cliente? client = null;
            try
            {
                client = await _clientRepository.GetByEmail(email);
            }
            catch (Exception)
            {
                throw;
            }
            return client;
        }

        public async Task<Cliente?> GetByCedula(string cedula) // ¡NUEVO!
        {
            Cliente? client = null;
            try
            {
                client = await _clientRepository.GetByCedula(cedula);
            }
            catch (Exception)
            {
                throw;
            }
            return client;
        }
        public async Task<List<Cliente>> GetByName(string name)
        {
            List<Cliente> clients = new List<Cliente>();
            try
            {
                clients = await _clientRepository.GetByName(name);
            }
            catch (Exception)
            {

                throw;
            }
            return clients;
        }

        public async Task<int> Insert(Cliente cliente)
        {
            int result = 0;
            try
            {
                await _clientRepository.Insert(cliente);
                result = 1;
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public async Task<int> Update(Cliente cliente)
        {
            int result = 0;
            try
            {
                Cliente? client = await _clientRepository.GetByEmail(cliente.Email);
                if (client is not null && client.IdCliente != cliente.IdCliente) throw new Exception("Email ya registrado");

                Cliente? existingCliente = await _clientRepository.GetById(cliente.IdCliente);

                if (existingCliente is null)
                {
                    throw new Exception("Cliente no encontrado para actualizar.");
                }

                // 3. Actualizar solo las propiedades necesarias del objeto cargado
                existingCliente.Nombre = cliente.Nombre;
                existingCliente.Apellido = cliente.Apellido;
                existingCliente.Telefono = cliente.Telefono;
                existingCliente.Email = cliente.Email;
                existingCliente.Direccion = cliente.Direccion;
                existingCliente.Cedula = cliente.Cedula; // ¡NUEVO! Incluir la cédula en la actualización

                // FechaRegistro NO se toca, mantiene su valor original de la DB

                // 4. Pasar la entidad cargada y modificada al repositorio para guardar
                _clientRepository.Update(existingCliente); // El repositorio ahora guarda directamente.
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
                Cliente? client = await _clientRepository.GetById(id);

                if (client is null) throw new Exception("Cliente no registrado");

                _clientRepository.Delete(client);
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
