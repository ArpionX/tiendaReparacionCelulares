using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaReparacion.Data;
using TiendaReparacion.Data.Entities;

namespace TiendaReparacion.Repositories
{
    public interface IRepuestoUtilizadoRepository
    {
        Task<List<RepuestoUtilizado>> GetByOrderId(int orderId);
        Task<List<RepuestoUtilizado>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<RepuestoUtilizado?> GetById(int id);
        Task<List<RepuestoUtilizado>> GetAll();
        Task Insert(RepuestoUtilizado repuestoUtilizado);
        void Update(RepuestoUtilizado repuestoUtilizado);
        void Delete(RepuestoUtilizado repuestoUtilizado);
        Task<int> SaveChangesAsync();
    }

    public class RepuestoUtilizadoRepository : IRepuestoUtilizadoRepository
    {
        private readonly TiendaDbContext _context;

        public RepuestoUtilizadoRepository(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<List<RepuestoUtilizado>> GetAll()
        {
            return await _context.RepuestosUtilizados.ToListAsync();
        }

        public async Task<RepuestoUtilizado?> GetById(int id)
        {
            return await _context.RepuestosUtilizados.FindAsync(id);
        }

        public async Task<List<RepuestoUtilizado>> GetByOrderId(int orderId)
        {
            return await _context.RepuestosUtilizados
                .Where(ru => ru.IdOrden == orderId)
                .ToListAsync();
        }

        public async Task<List<RepuestoUtilizado>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.RepuestosUtilizados
                .Where(ru => ru.FechaUso >= startDate && ru.FechaUso <= endDate)
                .ToListAsync();
        }

        public async Task Insert(RepuestoUtilizado repuestoUtilizado)
        {
            await _context.RepuestosUtilizados.AddAsync(repuestoUtilizado);
        }

        public void Update(RepuestoUtilizado repuestoUtilizado)
        {
            _context.RepuestosUtilizados.Update(repuestoUtilizado);
        }

        public void Delete(RepuestoUtilizado repuestoUtilizado)
        {
            _context.RepuestosUtilizados.Remove(repuestoUtilizado);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}