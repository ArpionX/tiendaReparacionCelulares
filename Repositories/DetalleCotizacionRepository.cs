using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaReparacion.Data;
using TiendaReparacion.Data.Entities;

namespace TiendaReparacion.Repositories
{
    public interface IDetalleCotizacionRepository
    {
        Task<List<DetalleCotizacion>> GetByQuoteId(int quoteId);
        Task<DetalleCotizacion?> GetById(int id);
        Task<List<DetalleCotizacion>> GetAll();
        Task Insert(DetalleCotizacion detalle);
        void Update(DetalleCotizacion detalle);
        void Delete(DetalleCotizacion detalle);
        Task<int> SaveChangesAsync();
    }

    public class DetalleCotizacionRepository : IDetalleCotizacionRepository
    {
        private readonly TiendaDbContext _context;

        public DetalleCotizacionRepository(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<List<DetalleCotizacion>> GetAll()
        {
            return await _context.DetallesCotizacion.ToListAsync();
        }

        public async Task<DetalleCotizacion?> GetById(int id)
        {
            return await _context.DetallesCotizacion.FindAsync(id);
        }

        public async Task<List<DetalleCotizacion>> GetByQuoteId(int quoteId)
        {
            return await _context.DetallesCotizacion
                .Where(d => d.IdCotizacion == quoteId)
                .ToListAsync();
        }

        public async Task Insert(DetalleCotizacion detalle)
        {
            await _context.DetallesCotizacion.AddAsync(detalle);
        }

        public void Update(DetalleCotizacion detalle)
        {
            _context.DetallesCotizacion.Update(detalle);
        }

        public void Delete(DetalleCotizacion detalle)
        {
            _context.DetallesCotizacion.Remove(detalle);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}