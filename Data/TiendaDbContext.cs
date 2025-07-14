using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaReparacion.Data.Entities;

namespace TiendaReparacion.Data
{
    // DbContext principal para la aplicación de la tienda de reparación de celulares.
    public class TiendaDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Repuesto> Repuestos { get; set; }
        public DbSet<OrdenServicio> OrdenesServicio { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        public DbSet<DetalleCotizacion> DetallesCotizacion { get; set; }
        public DbSet<ServicioRepuesto> ServicioRepuestos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Garantia> Garantias { get; set; }
        public DbSet<RepuestoUtilizado> RepuestosUtilizados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; } // Sugerencia de tabla de Usuarios

        // Constructor que acepta DbContextOptions.
        // Este es el constructor preferido y será usado cuando se registra con la inyección de dependencias (DI).
        public TiendaDbContext(DbContextOptions<TiendaDbContext> options) : base(options) { }

        // El método OnConfiguring se llama si el DbContext no se ha configurado externamente
        // (es decir, el constructor con DbContextOptions no fue usado, o si se llama directamente 'new TiendaDbContext()').
        // Es útil para herramientas como las migraciones de EF Core cuando no se ejecuta toda la aplicación.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Solo configura la conexión si las opciones no han sido ya configuradas.
            // Esto evita que sobrescriba la configuración que viene de Program.cs (DI).
            if (!optionsBuilder.IsConfigured)
            {
                // Usa la cadena de conexión definida en la clase AppConstants.
                string connectionString = AppConstants.DefaultConnectionString;

                // Configura el uso de MySQL con la cadena de conexión.
                optionsBuilder.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString), // Auto-detecta la versión de tu servidor MySQL.
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null)
                );
            }
        }

        // Este método configura el modelo de la base de datos (relaciones, tipos de datos, etc.).
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla 'servicio_repuesto' para la clave primaria compuesta.
            modelBuilder.Entity<ServicioRepuesto>()
                .HasKey(sr => new { sr.IdServicio, sr.IdRepuesto });

            // Definición de las relaciones para 'ServicioRepuesto'.
            modelBuilder.Entity<ServicioRepuesto>()
                .HasOne(sr => sr.Servicio)
                .WithMany(s => s.ServicioRepuestos)
                .HasForeignKey(sr => sr.IdServicio);

            modelBuilder.Entity<ServicioRepuesto>()
                .HasOne(sr => sr.Repuesto)
                .WithMany(r => r.ServicioRepuestos)
                .HasForeignKey(sr => sr.IdRepuesto);

            // Configuración de las enumeraciones para que se mapeen como cadenas de texto en la BD.
            modelBuilder.Entity<Tecnico>()
                .Property(t => t.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<Servicio>()
                .Property(s => s.Categoria)
                .HasConversion<string>();

            modelBuilder.Entity<OrdenServicio>()
                .Property(o => o.EstadoOrden)
                .HasConversion<string>();

            modelBuilder.Entity<Cotizacion>()
                .Property(c => c.EstadoCotizacion)
                .HasConversion<string>();

            modelBuilder.Entity<Factura>()
                .Property(f => f.MetodoPago)
                .HasConversion<string>();

            modelBuilder.Entity<Factura>()
                .Property(f => f.EstadoPago)
                .HasConversion<string>();

            modelBuilder.Entity<Pago>()
                .Property(p => p.MetodoPago)
                .HasConversion<string>();

            modelBuilder.Entity<Pago>()
                .Property(p => p.TipoPago)
                .HasConversion<string>();

            modelBuilder.Entity<Garantia>()
                .Property(g => g.EstadoGarantia)
                .HasConversion<string>();

            // Configuración para la entidad Usuario (sugerencia)
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Rol)
                .HasConversion<string>();

            // Configuración de índices únicos
            modelBuilder.Entity<OrdenServicio>()
                .HasIndex(o => o.NumeroOrden)
                .IsUnique();

            modelBuilder.Entity<Factura>()
                .HasIndex(f => f.NumeroFactura)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NombreUsuario)
                .IsUnique();

            // ¡NUEVA CONFIGURACIÓN PARA RESOLVER EL ERROR 'TecnicoIdTecnico'!
            // Define explícitamente la relación entre Usuario y Tecnico.
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Tecnico) // Un Usuario tiene un Tecnico (opcional)
                .WithMany() // Un Tecnico puede estar asociado con muchos Usuarios (no hay propiedad de navegación inversa en Tecnico)
                .HasForeignKey(u => u.IdTecnico) // La clave foránea en Usuario es 'IdTecnico'
                .IsRequired(false); // La relación es opcional porque 'IdTecnico' es nullable.
            modelBuilder.Entity<Dispositivo>()
                .HasOne(d => d.Cliente) // Un Dispositivo pertenece a un Cliente
                .WithMany(c => c.Dispositivos) // Un Cliente puede tener muchos Dispositivos
                .HasForeignKey(d => d.IdCliente) // La clave foránea en Dispositivo es 'IdCliente'
                .OnDelete(DeleteBehavior.Cascade); // Elimina los Dispositivos si se elimina el Cliente
            modelBuilder.Entity<OrdenServicio>()
                .HasOne(o => o.Cliente) // Una Orden de Servicio pertenece a un Cliente
                .WithMany(c => c.OrdenesServicio) // Un Cliente puede tener muchas Órdenes de Servicio
                .HasForeignKey(o => o.IdCliente) // La clave foránea en OrdenServicio es 'IdCliente'
                .OnDelete(DeleteBehavior.Cascade); // Elimina las Órdenes de Servicio si se elimina el Cliente
            modelBuilder.Entity<OrdenServicio>()
                .HasOne(o => o.Dispositivo) // Una Orden de Servicio pertenece a un Dispositivo
                .WithMany(d => d.OrdenesServicio) // Un Dispositivo puede tener muchas Órdenes de Servicio
                .HasForeignKey(o => o.IdDispositivo) // La clave foránea en OrdenServicio es 'IdDispositivo'
                .OnDelete(DeleteBehavior.Cascade); // Elimina las Órdenes de Servicio si se elimina el Dispositivo
            modelBuilder.Entity<OrdenServicio>()
                .HasOne(o => o.Tecnico) // Una Orden de Servicio puede tener un Técnico asignado
                .WithMany(t => t.OrdenesServicio) // Un Técnico puede estar asociado con muchas Órdenes de Servicio
                .HasForeignKey(o => o.IdTecnico) // La clave foránea en OrdenServicio es 'IdTecnico'
                .IsRequired(false); // La relación es opcional porque 'IdTecnico' es nullable.
        }
    }
}

