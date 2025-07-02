using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaReparacion.Data;
using TiendaReparacion.Data.Entities;

namespace TiendaReparacion.Repositories
{
    public interface IServicioRepository
    {
        Task<List<Servicio>> GetByCategory(CategoriaServicio categoria);
        Task<Servicio?> GetByName(string nombre);
        Task<Servicio?> GetById(int id);
        Task<List<Servicio>> GetAll();
        Task Insert(Servicio servicio);
        void Update(Servicio servicio);
        void Delete(Servicio servicio);
        Task<int> SaveChangesAsync();
    }

    public class ServicioRepository : IServicioRepository
    {
        private readonly TiendaDbContext _context;

        public ServicioRepository(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Servicio>> GetAll()
        {
            return await _context.Servicios.ToListAsync();
        }

        public async Task<Servicio?> GetById(int id)
        {
            return await _context.Servicios.FindAsync(id);
        }

        public async Task<Servicio?> GetByName(string nombre)
        {
            return await _context.Servicios
                .FirstOrDefaultAsync(s => s.NombreServicio.ToUpper().Equals(nombre.ToUpper()));
        }

        public async Task<List<Servicio>> GetByCategory(CategoriaServicio categoria)
        {
            return await _context.Servicios
                .Where(s => s.Categoria == categoria)
                .ToListAsync();
        }

        public async Task Insert(Servicio servicio)
        {
            await _context.Servicios.AddAsync(servicio);
        }

        public void Update(Servicio servicio)
        {
            _context.Servicios.Update(servicio);
        }

        public void Delete(Servicio servicio)
        {
            _context.Servicios.Remove(servicio);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}