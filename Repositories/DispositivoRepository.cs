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
    public interface IDispositivoRepository
    {
        Task<Dispositivo?> GetByImei(string imei);
        Task<Dispositivo?> GetById(int id);
        Task<List<Dispositivo>> GetAll();
        Task Insert(Dispositivo dispositivo);
        void Update(Dispositivo dispositivo);
        void Delete(Dispositivo dispositivo);
        Task<List<Dispositivo>> GetByClienteId(int idCliente);
    }
    public class DispositivoRepository: IDispositivoRepository
    {
        private readonly IDbContextFactory<TiendaDbContext> _contextFactory;

        public DispositivoRepository(IDbContextFactory<TiendaDbContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<Dispositivo>> GetAll()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Dispositivos.ToListAsync();
            }
        }

        public async Task<Dispositivo?> GetById(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Dispositivos.FindAsync(id);
            }
        }
        public async Task<List<Dispositivo>> GetByClienteId(int idCliente) // ¡NUEVO!
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Dispositivos
                                    .Where(d => d.IdCliente == idCliente)
                                    .ToListAsync();
            }
        }
        public async Task<Dispositivo?> GetByImei(string imei)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Dispositivos.FirstOrDefaultAsync(d => d.Imei.ToUpper().Equals(imei.ToUpper()));
            }
        }

        public async Task Insert(Dispositivo dispositivo)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                await context.Dispositivos.AddAsync(dispositivo);
                await context.SaveChangesAsync();
            }
        }

        public void Update(Dispositivo dispositivo)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Dispositivos.Update(dispositivo);
                context.SaveChanges();
            }
        }

        public void Delete(Dispositivo dispositivo)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Dispositivos.Remove(dispositivo);
                context.SaveChanges();
            }
        }
    }
}
