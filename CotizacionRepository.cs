using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaReparacion.Data;
using TiendaReparacion.Data.Entities;

namespace TiendaReparacion.Repositories
{
    public interface ICotizacionRepository
    {
        Task<List<Cotizacion>> GetPendingApproval();
        Task<List<Cotizacion>> GetExpiringQuotes(DateTime cutoffDate);
        Task<Cotizacion?> GetById(int id);
        Task<List<Cotizacion>> GetAll();
        Task Insert(Cotizacion cotizacion);
        void Update(Cotizacion cotizacion);
        void Delete(Cotizacion cotizacion);
        Task<int> SaveChangesAsync();
    }

    public class CotizacionRepository : ICotizacionRepository
    {
        private readonly TiendaDbContext _context;

        public CotizacionRepository(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cotizacion>> GetAll()
        {
            return await _context.Cotizaciones.ToListAsync();
        }

        public async Task<Cotizacion?> GetById(int id)
        {
            return await _context.Cotizaciones.FindAsync(id);
        }

        public async Task<List<Cotizacion>> GetPendingApproval()
        {
            return await _context.Cotizaciones
                .Where(c => c.EstadoCotizacion == EstadoCotizacion.pendiente)
                .ToListAsync();
        }

        public async Task<List<Cotizacion>> GetExpiringQuotes(DateTime cutoffDate)
        {
            return await _context.Cotizaciones
                .Where(c => c.FechaVencimiento <= cutoffDate &&
                           c.EstadoCotizacion == EstadoCotizacion.pendiente)
                .ToListAsync();
        }

        public async Task Insert(Cotizacion cotizacion)
        {
            await _context.Cotizaciones.AddAsync(cotizacion);
        }

        public void Update(Cotizacion cotizacion)
        {
            _context.Cotizaciones.Update(cotizacion);
        }

        public void Delete(Cotizacion cotizacion)
        {
            _context.Cotizaciones.Remove(cotizacion);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}