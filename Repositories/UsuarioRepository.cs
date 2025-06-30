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
        void Update(Usuario user);
        void Delete(Usuario user);
        Task<int> SaveChangesAsync();
    }
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TiendaDbContext _context;

        public UsuarioRepository(TiendaDbContext context)
        {
            _context = context;
        }
       
        public async Task<Usuario?> GetById(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }
        public async Task<Usuario?> Login(string nameUser)
        {
           return await _context.Usuarios.FirstOrDefaultAsync(user => user.NombreUsuario.ToUpper().Equals(nameUser.ToUpper()));
        }
        public Task Insert(Usuario user)
        {
            throw new NotImplementedException();
        }
       
        public void Update(Usuario user)
        {
            _context.Usuarios.Update(user);
        }
        public void Delete(Usuario user)
        {
            _context.Usuarios.Remove(user);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
