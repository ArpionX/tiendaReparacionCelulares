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
    public interface IDiagnosticoRepository
    {
        Task<Diagnostico?> GetById(int id);
        Task<List<Diagnostico>> GetAll();
        Task Insert(Diagnostico orden);
        void Update(Diagnostico orden);
        void Delete(Diagnostico orden);
        Task<int> SaveChangesAsync();
    }
    public class DiagnosticoRepository: IDiagnosticoRepository
    {
        private readonly TiendaDbContext _context;

        public DiagnosticoRepository(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Diagnostico>> GetAll()
        {
            return await _context.Diagnosticos.ToListAsync();
        }

        public async Task<Diagnostico?> GetById(int id)
        {
            return await _context.Diagnosticos.FindAsync(id);
        }
    
        public async Task Insert(Diagnostico cliente)
        {
            await _context.Diagnosticos.AddAsync(cliente);
        }

        public void Update(Diagnostico cliente)
        {
            _context.Diagnosticos.Update(cliente);
        }
        public void Delete(Diagnostico cliente)
        {
            _context.Diagnosticos.Remove(cliente);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
