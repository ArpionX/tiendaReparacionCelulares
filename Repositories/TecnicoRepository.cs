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
    // Interfaz para el repositorio de Técnicos.
    // Define las operaciones CRUD (Crear, Leer, Actualizar, Borrar) básicas para la entidad Tecnico.
    public interface ITecnicoRepository
    {
        // Inserta un nuevo técnico en la base de datos.
        Task AddTecnicoAsync(Tecnico tecnico);

        // Obtiene una lista de todos los técnicos de la base de datos.
        Task<IEnumerable<Tecnico>> GetAllTecnicosAsync();

        // Puedes añadir más métodos aquí según las necesidades de tu aplicación, por ejemplo:
        // Task<Tecnico> GetTecnicoByIdAsync(int id);
        // Task UpdateTecnicoAsync(Tecnico tecnico);
        // Task DeleteTecnicoAsync(int id);
    }
    // Implementación del repositorio de Técnicos.
    public class TecnicoRepository : ITecnicoRepository
    {
        // Campo privado para la instancia del DbContext.
        private readonly TiendaDbContext _context;

        // Constructor que recibe una instancia de TiendaDbContext a través de la inyección de dependencias.
        public TecnicoRepository(TiendaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Añade un nuevo técnico a la base de datos de forma asíncrona.
        /// </summary>
        /// <param name="tecnico">Objeto Tecnico a añadir.</param>
        public async Task AddTecnicoAsync(Tecnico tecnico)
        {
            // Añade el técnico al DbSet.
            _context.Tecnicos.Add(tecnico);
            // Guarda los cambios en la base de datos.
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Obtiene todos los técnicos de la base de datos de forma asíncrona.
        /// </summary>
        /// <returns>Una colección de objetos Tecnico.</returns>
        public async Task<IEnumerable<Tecnico>> GetAllTecnicosAsync()
        {
            // Retorna todos los técnicos desde el DbSet.
            return await _context.Tecnicos.ToListAsync();
        }
    }
}
