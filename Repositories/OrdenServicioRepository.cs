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
    public interface IOrdenServicioRepository
    {
        Task<OrdenServicio?> GetByNumberOrden(string numberOrden);
        Task<OrdenServicio?> GetById(int id);
        Task<List<OrdenServicio>> GetAll();
        Task Insert(OrdenServicio orden);
        void Update(OrdenServicio orden);
        void Delete(OrdenServicio orden);
        Task<int> SaveChangesAsync();
    }
    public class OrdenServicioRepository : IOrdenServicioRepository
    {
        private readonly TiendaDbContext _context;

        public OrdenServicioRepository(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrdenServicio>> GetAll()
        {
            return await _context.OrdenesServicio.ToListAsync();
        }

        public async Task<OrdenServicio?> GetById(int id)
        {
            return await _context.OrdenesServicio.FindAsync(id);
        }

        public async Task<OrdenServicio?> GetByNumberOrden(string numberOrden)
        {
            return await _context.OrdenesServicio.FirstOrDefaultAsync(orden => orden.NumeroOrden.ToUpper().Equals(numberOrden.ToUpper()));
        }

        public async Task Insert(OrdenServicio orden)
        {
            await _context.OrdenesServicio.AddAsync(orden);
        }

        public void Update(OrdenServicio orden)
        {
            _context.OrdenesServicio.Update(orden);
        }
        public void Delete(OrdenServicio orden)
        {
            _context.OrdenesServicio.Remove(orden);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
