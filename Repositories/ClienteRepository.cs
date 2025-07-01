using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaReparacion.Data;
using TiendaReparacion.Data.Entities;

namespace TiendaReparacion.Repositories
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetByName(string name);
        Task<Cliente?> GetByEmail(string email);
        Task<Cliente?> GetById(int id);
        Task<List<Cliente>> GetAll();
        Task Insert(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(Cliente cliente);
        Task<int> SaveChangesAsync();
    }
    public class ClienteRepository : IClienteRepository
    {
        private readonly TiendaDbContext _context;

        public ClienteRepository(TiendaDbContext context)
        {
            _context = context;
        }
       
        public async Task<List<Cliente>> GetAll()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetById(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<List<Cliente>> GetByName(string name)
        {
            return await _context.Clientes.Where(client => client.Nombre.ToUpper().Contains(name.ToUpper())).ToListAsync(); 
        }

        public async Task Insert(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
        }

        public void Update(Cliente cliente)
        {
             _context.Clientes.Update(cliente);
        }
        public void  Delete(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Cliente?> GetByEmail(string email)
        {
            return await _context.Clientes.FirstOrDefaultAsync(client => client.Email.ToUpper().Equals(email.ToUpper()));
        }
    }
}
