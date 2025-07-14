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
    public interface IOrdenServicioRepository
    {
        Task<OrdenServicio?> GetByNumberOrden(string numberOrden);
        Task<OrdenServicio?> GetById(int id);
        Task<List<OrdenServicio>> GetAll();
        Task Insert(OrdenServicio orden);
        void Update(OrdenServicio orden);
        void Delete(OrdenServicio orden);
    }
    public class OrdenServicioRepository : IOrdenServicioRepository
    {
        private readonly IDbContextFactory<TiendaDbContext> _contextFactory;

        public OrdenServicioRepository(IDbContextFactory<TiendaDbContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<OrdenServicio>> GetAll()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.OrdenesServicio.ToListAsync();
            }

        }

        public async Task<OrdenServicio?> GetById(int id)
        {

            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.OrdenesServicio.FindAsync(id);
            }
        }

        public async Task<OrdenServicio?> GetByNumberOrden(string numberOrden)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.OrdenesServicio.FirstOrDefaultAsync(orden => orden.NumeroOrden.ToUpper().Equals(numberOrden.ToUpper()));
            }
        }

        public async Task Insert(OrdenServicio orden)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                await context.OrdenesServicio.AddAsync(orden);
                await context.SaveChangesAsync();
            }
        }

        public void Update(OrdenServicio orden)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.OrdenesServicio.Update(orden);
                context.SaveChanges();
            }
        }
        public void Delete(OrdenServicio orden)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.OrdenesServicio.Remove(orden);
                context.SaveChanges();
            }
        }

        

    }
}
