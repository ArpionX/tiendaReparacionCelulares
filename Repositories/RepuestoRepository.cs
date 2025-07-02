using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaReparacion.Data;
using TiendaReparacion.Data.Entities;

namespace TiendaReparacion.Repositories
{
    public interface IRepuestoRepository
    {
        Task<List<Repuesto>> GetByCompatibleModel(string modelo);
        Task<List<Repuesto>> GetLowStock(int threshold = 5);
        Task<Repuesto?> GetByName(string nombre);
        Task<Repuesto?> GetById(int id);
        Task<List<Repuesto>> GetAll();
        Task Insert(Repuesto repuesto);
        void Update(Repuesto repuesto);
        void Delete(Repuesto repuesto);
        Task<int> SaveChangesAsync();
    }

    public class RepuestoRepository : IRepuestoRepository
    {
        private readonly TiendaDbContext _context;

        public RepuestoRepository(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Repuesto>> GetAll()
        {
            return await _context.Repuestos.ToListAsync();
        }

        public async Task<Repuesto?> GetById(int id)
        {
            return await _context.Repuestos.FindAsync(id);
        }

        public async Task<Repuesto?> GetByName(string nombre)
        {
            return await _context.Repuestos
                .FirstOrDefaultAsync(r => r.Nombre.ToUpper().Equals(nombre.ToUpper()));
        }

        public async Task<List<Repuesto>> GetByCompatibleModel(string modelo)
        {
            return await _context.Repuestos
                .Where(r => r.ModeloCompatible.ToUpper().Contains(modelo.ToUpper()))
                .ToListAsync();
        }

        public async Task<List<Repuesto>> GetLowStock(int threshold = 5)
        {
            return await _context.Repuestos
                .Where(r => r.StockDisponible <= threshold)
                .ToListAsync();
        }

        public async Task Insert(Repuesto repuesto)
        {
            await _context.Repuestos.AddAsync(repuesto);
        }

        public void Update(Repuesto repuesto)
        {
            _context.Repuestos.Update(repuesto);
        }

        public void Delete(Repuesto repuesto)
        {
            _context.Repuestos.Remove(repuesto);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}