using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        Task<Cliente?> GetByCedula(string cedula); // ¡NUEVO!
        Task<List<Cliente>> GetByName(string name);
        Task<Cliente?> GetByEmail(string email);
        Task<Cliente?> GetById(int id);
        Task<List<Cliente>> GetAll();
        Task Insert(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(Cliente cliente);
    }
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDbContextFactory<TiendaDbContext> _contextFactory;

        public ClienteRepository(IDbContextFactory<TiendaDbContext> context)
        {
            _contextFactory = context;
        }
       
        public async Task<List<Cliente>> GetAll()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Clientes.ToListAsync();
            }
        }
        public async Task<Cliente?> GetByCedula(string cedula) // ¡NUEVO!
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Clientes.FirstOrDefaultAsync(client => client.Cedula != null && client.Cedula.ToUpper().Equals(cedula.ToUpper()));
            }
        }
        public async Task<Cliente?> GetById(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Clientes.FindAsync(id);
            }
        }

        public async Task<List<Cliente>> GetByName(string name)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Clientes.Where(client => client.Nombre.ToUpper().Contains(name.ToUpper())).ToListAsync();
            }
        }

        public async Task Insert(Cliente cliente)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                await context.Clientes.AddAsync(cliente);
                await context.SaveChangesAsync();
            }
        }

        public void Update(Cliente cliente)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                // Adjuntar la entidad si no está siendo rastreada
                context.Clientes.Update(cliente); // Update ya maneja el estado
                context.SaveChanges();
            }
        }

        public void Delete(Cliente cliente)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                // Adjuntar la entidad si no está siendo rastreada
                context.Clientes.Remove(cliente);
                context.SaveChanges();
            }
        }
        public async Task<Cliente?> GetByEmail(string email)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Clientes.FirstOrDefaultAsync(client => client.Email.ToUpper().Equals(email.ToUpper()));
            }
        }
    }
}
