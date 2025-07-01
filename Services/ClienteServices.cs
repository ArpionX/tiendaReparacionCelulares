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
                result = await _clientRepository.SaveChangesAsync();
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

                _clientRepository.Update(cliente);
                result = await _clientRepository.SaveChangesAsync();
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
                result = await _clientRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
