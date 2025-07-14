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
    public interface IUsuarioRepository
    {
        Task<Usuario?> Login(string nameUser);
        Task<Usuario?> GetById(int id); 
        Task Insert(Usuario user);
        Task Update(Usuario user);
        Task Delete(Usuario user);
    }
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbContextFactory<TiendaDbContext> _contextFactory;

        public UsuarioRepository(IDbContextFactory<TiendaDbContext> context)
        {
            _contextFactory = context;
        }

        public async Task<Usuario?> GetById(int id)
        {
            using (var context = _contextFactory.CreateDbContext()) // <-- ¡Se crea una nueva instancia aquí!
            {
                return await context.Usuarios.FindAsync(id);
            } // <-- La instancia se desecha automáticamente aquí        
        }
        public async Task<Usuario?> Login(string nameUser)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Usuarios.FirstOrDefaultAsync(user => user.NombreUsuario.ToUpper().Equals(nameUser.ToUpper()));
            }
        }
        public async Task Insert(Usuario user)
        {
            using (var context = _contextFactory.CreateDbContext()) // <-- Otra nueva instancia
            {
                await context.Usuarios.AddAsync(user);
                await context.SaveChangesAsync(); // Guarda los cambios de esta operación
            } // <-- La instancia se desecha
        }

        public async Task Update(Usuario user)
        {
            using(var context = _contextFactory.CreateDbContext())
            {
                await context.Usuarios
                    .Where(u => u.IdUsuario == user.IdUsuario)
                    .ExecuteUpdateAsync(u => u.SetProperty(p => p.NombreUsuario, user.NombreUsuario)
                                               .SetProperty(p => p.ContrasenaHash, user.ContrasenaHash)
                                               .SetProperty(p => p.Rol, user.Rol)
                                               .SetProperty(p => p.IdTecnico, user.IdTecnico));
                await context.SaveChangesAsync();
            }

        }
        public async Task Delete(Usuario user)
        {

            using (var context = _contextFactory.CreateDbContext())
            {
                await context.Usuarios
                    .Where(u => u.IdUsuario == user.IdUsuario)
                    .ExecuteDeleteAsync();
                await context.SaveChangesAsync(); // Guarda los cambios de esta operación
            }
        }
        
    }
}
