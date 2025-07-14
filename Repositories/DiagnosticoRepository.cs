using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        Task<List<Diagnostico>> GetByOrdenId(int idOrden);
        Task Insert(Diagnostico orden);
        void Update(Diagnostico orden);
        void Delete(Diagnostico orden);
    }
    public class DiagnosticoRepository: IDiagnosticoRepository
    {
        private readonly IDbContextFactory<TiendaDbContext> _contextFactory;

        public DiagnosticoRepository(IDbContextFactory<TiendaDbContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<Diagnostico>> GetAll()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Diagnosticos.ToListAsync();
            }
        }

        public async Task<Diagnostico?> GetById(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Diagnosticos.FindAsync(id);
            }
        }
        public async Task<List<Diagnostico>> GetByOrdenId(int idOrden)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Diagnosticos
                                    .Where(d => d.IdOrden == idOrden)
                                    .ToListAsync();
            }
        }
        public async Task Insert(Diagnostico diagnostico)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                await context.Diagnosticos.AddAsync(diagnostico);
                await context.SaveChangesAsync();
            }
        }

        public void Update(Diagnostico diagnostico)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Diagnosticos.Update(diagnostico);
                context.SaveChanges();
            }
        }

        public void Delete(Diagnostico diagnostico)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Diagnosticos.Remove(diagnostico);
                context.SaveChanges();
            }
        }

        
    }
}
