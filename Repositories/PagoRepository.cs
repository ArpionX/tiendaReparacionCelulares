using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaReparacion.Data;
using TiendaReparacion.Data.Entities;

namespace TiendaReparacion.Repositories
{
    public interface IPagoRepository
    {
        Task<List<Pago>> GetByPaymentMethod(MetodoPago metodo);
        Task<List<Pago>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<Pago?> GetById(int id);
        Task<List<Pago>> GetAll();
        Task Insert(Pago pago);
        void Update(Pago pago);
        void Delete(Pago pago);
        Task<int> SaveChangesAsync();
    }

    public class PagoRepository : IPagoRepository
    {
        private readonly TiendaDbContext _context;

        public PagoRepository(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pago>> GetAll()
        {
            return await _context.Pagos.ToListAsync();
        }

        public async Task<Pago?> GetById(int id)
        {
            return await _context.Pagos.FindAsync(id);
        }

        public async Task<List<Pago>> GetByPaymentMethod(MetodoPago metodo)
        {
            return await _context.Pagos
                .Where(p => p.MetodoPago == metodo)
                .ToListAsync();
        }

        public async Task<List<Pago>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Pagos
                .Where(p => p.FechaPago >= startDate && p.FechaPago <= endDate)
                .ToListAsync();
        }

        public async Task Insert(Pago pago)
        {
            await _context.Pagos.AddAsync(pago);
        }

        public void Update(Pago pago)
        {
            _context.Pagos.Update(pago);
        }

        public void Delete(Pago pago)
        {
            _context.Pagos.Remove(pago);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}