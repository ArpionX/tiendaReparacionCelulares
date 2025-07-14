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
    public interface ITecnicoRepository
    {
        Task<List<Tecnico>> GetAll();
        Task<Tecnico?> GetById(int id);
        Task<Tecnico?> GetByName(string nombre); // ¡NUEVO!
        Task Insert(Tecnico tecnico);
        void Update(Tecnico tecnico);
        void Delete(Tecnico tecnico);
    }
    // Implementación del repositorio de Técnicos.
    public class TecnicoRepository : ITecnicoRepository
    {
        private readonly IDbContextFactory<TiendaDbContext> _contextFactory;

        public TecnicoRepository(IDbContextFactory<TiendaDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Tecnico>> GetAll()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Tecnicos.ToListAsync();
            }
        }

        public async Task<Tecnico?> GetById(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Tecnicos.FindAsync(id);
            }
        }

        /// <summary>
        /// Obtiene un técnico por su nombre.
        /// </summary>
        /// <param name="nombre">El nombre del técnico.</param>
        /// <returns>La instancia del técnico si se encuentra, de lo contrario null.</returns>
        public async Task<Tecnico?> GetByName(string nombre) // ¡NUEVO!
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Tecnicos
                                    .FirstOrDefaultAsync(t => t.Nombre != null && t.Nombre.ToUpper().Equals(nombre.ToUpper()));
            }
        }

        public async Task Insert(Tecnico tecnico)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                await context.Tecnicos.AddAsync(tecnico);
                await context.SaveChangesAsync();
            }
        }

        public void Update(Tecnico tecnico)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Tecnicos.Update(tecnico);
                context.SaveChanges();
            }
        }

        public void Delete(Tecnico tecnico)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Tecnicos.Remove(tecnico);
                context.SaveChanges();
            }
        }
    }
}
