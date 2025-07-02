using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaReparacion.Data;
using TiendaReparacion.Data.Entities;

namespace TiendaReparacion.Repositories
{
    public interface IServicioRepuestoRepository
    {
        Task<List<ServicioRepuesto>> GetByServiceId(int serviceId);
        Task<List<ServicioRepuesto>> GetByPartId(int partId);
        Task<ServicioRepuesto?> GetByIds(int serviceId, int partId);
        Task<List<ServicioRepuesto>> GetAll();
        Task Insert(ServicioRepuesto servicioRepuesto);
        void Update(ServicioRepuesto servicioRepuesto);
        void Delete(ServicioRepuesto servicioRepuesto);
        Task<int> SaveChangesAsync();
    }

    public class ServicioRepuestoRepository : IServicioRepuestoRepository
    {
        private readonly TiendaDbContext _context;

        public ServicioRepuestoRepository(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServicioRepuesto>> GetAll()
        {
            return await _context.ServicioRepuestos.ToListAsync();
        }

        public async Task<ServicioRepuesto?> GetByIds(int serviceId, int partId)
        {
            return await _context.ServicioRepuestos
                .FirstOrDefaultAsync(sr => sr.IdServicio == serviceId && sr.IdRepuesto == partId);
        }

        public async Task<List<ServicioRepuesto>> GetByServiceId(int serviceId)
        {
            return await _context.ServicioRepuestos
                .Where(sr => sr.IdServicio == serviceId)
                .ToListAsync();
        }

        public async Task<List<ServicioRepuesto>> GetByPartId(int partId)
        {
            return await _context.ServicioRepuestos
                .Where(sr => sr.IdRepuesto == partId)
                .ToListAsync();
        }

        public async Task Insert(ServicioRepuesto servicioRepuesto)
        {
            await _context.ServicioRepuestos.AddAsync(servicioRepuesto);
        }

        public void Update(ServicioRepuesto servicioRepuesto)
        {
            _context.ServicioRepuestos.Update(servicioRepuesto);
        }

        public void Delete(ServicioRepuesto servicioRepuesto)
        {
            _context.ServicioRepuestos.Remove(servicioRepuesto);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}