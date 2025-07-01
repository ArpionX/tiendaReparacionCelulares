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
        Task<int> SaveChangesAsync();
    }
    public class DispositivoRepository: IDispositivoRepository
    {
        private readonly TiendaDbContext _context;

        public DispositivoRepository(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dispositivo>> GetAll()
        {
            return await _context.Dispositivos.ToListAsync();
        }

        public async Task<Dispositivo?> GetById(int id)
        {
            return await _context.Dispositivos.FindAsync(id);
        }

        public async Task<Dispositivo?> GetByImei(string imei)
        {
            return await _context.Dispositivos.FirstOrDefaultAsync(dispositivo => dispositivo.Imei.ToUpper().Equals(imei.ToUpper()));
        }

        public async Task Insert(Dispositivo dispositivo)
        {
            await _context.Dispositivos.AddAsync(dispositivo);
        }

        public void Update(Dispositivo dispositivo)
        {
            _context.Dispositivos.Update(dispositivo);
        }
        public void Delete(Dispositivo dispositivo)
        {
            _context.Dispositivos.Remove(dispositivo);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
