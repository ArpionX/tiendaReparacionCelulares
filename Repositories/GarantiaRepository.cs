using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaReparacion.Data;
using TiendaReparacion.Data.Entities;

namespace TiendaReparacion.Repositories
{
    public interface IGarantiaRepository
    {
        Task<List<Garantia>> GetExpiringWarranties(DateTime cutoffDate);
        Task<List<Garantia>> GetByStatus(EstadoGarantia estado);
        Task<Garantia?> GetById(int id);
        Task<List<Garantia>> GetAll();
        Task Insert(Garantia garantia);
        void Update(Garantia garantia);
        void Delete(Garantia garantia);
        Task<int> SaveChangesAsync();
    }

    public class GarantiaRepository : IGarantiaRepository
    {
        private readonly TiendaDbContext _context;

        public GarantiaRepository(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Garantia>> GetAll()
        {
            return await _context.Garantias.ToListAsync();
        }

        public async Task<Garantia?> GetById(int id)
        {
            return await _context.Garantias.FindAsync(id);
        }

        public async Task<List<Garantia>> GetByStatus(EstadoGarantia estado)
        {
            return await _context.Garantias
                .Where(g => g.EstadoGarantia == estado)
                .ToListAsync();
        }

        public async Task<List<Garantia>> GetExpiringWarranties(DateTime cutoffDate)
        {
            return await _context.Garantias
                .Where(g => g.FechaVencimiento <= cutoffDate && g.EstadoGarantia == EstadoGarantia.vigente)
                .ToListAsync();
        }

        public async Task Insert(Garantia garantia)
        {
            await _context.Garantias.AddAsync(garantia);
        }

        public void Update(Garantia garantia)
        {
            _context.Garantias.Update(garantia);
        }

        public void Delete(Garantia garantia)
        {
            _context.Garantias.Remove(garantia);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}