using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaReparacion.Data;
using TiendaReparacion.Data.Entities;

namespace TiendaReparacion.Repositories
{
    public interface IFacturaRepository
    {
        Task<Factura?> GetByNumber(string numeroFactura);
        Task<List<Factura>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<Factura?> GetById(int id);
        Task<List<Factura>> GetAll();
        Task Insert(Factura factura);
        void Update(Factura factura);
        void Delete(Factura factura);
        Task<int> SaveChangesAsync();
    }

    public class FacturaRepository : IFacturaRepository
    {
        private readonly TiendaDbContext _context;

        public FacturaRepository(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Factura>> GetAll()
        {
            return await _context.Facturas.ToListAsync();
        }

        public async Task<Factura?> GetById(int id)
        {
            return await _context.Facturas.FindAsync(id);
        }

        public async Task<Factura?> GetByNumber(string numeroFactura)
        {
            return await _context.Facturas
                .FirstOrDefaultAsync(f => f.NumeroFactura.ToUpper().Equals(numeroFactura.ToUpper()));
        }

        public async Task<List<Factura>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Facturas
                .Where(f => f.FechaFactura >= startDate && f.FechaFactura <= endDate)
                .ToListAsync();
        }

        public async Task Insert(Factura factura)
        {
            await _context.Facturas.AddAsync(factura);
        }

        public void Update(Factura factura)
        {
            _context.Facturas.Update(factura);
        }

        public void Delete(Factura factura)
        {
            _context.Facturas.Remove(factura);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}