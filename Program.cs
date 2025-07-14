using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TiendaReparacion.Data;
using TiendaReparacion.Data.Entities;
using TiendaReparacion.Repositories;
using TiendaReparacion.Services;
using TiendaReparacion.UI;

namespace TiendaReparacion
{
    internal static class Program
    {
        public static IHost? AppHost { get; private set; }

        [STAThread]
        static async Task Main()
        {
            AppHost = CreateHostBuilder().Build();

            using (var scope = AppHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TiendaDbContext>();
                    // await context.Database.MigrateAsync();

                    var authService = services.GetRequiredService<IAuthServices>();
                    var testUser = await authService.GetById(1);
                    if (testUser == null)
                    {
                        Usuario adminUser = new Usuario
                        {
                            NombreUsuario = "admin",
                            ContrasenaHash = "password123",
                            Rol = RolUsuario.administrador,
                            FechaCreacion = DateTime.Now
                        };
                        await authService.Insert(adminUser);
                        MessageBox.Show("Usuario 'admin' creado con contraseña 'password123'. ¡Cámbiala en producción!", "Usuario de Prueba Creado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al conectar o migrar la base de datos: {ex.Message}\nDetalles: {ex.InnerException?.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ApplicationConfiguration.Initialize();

            // Ejecuta el formulario de Login al inicio.
            Application.Run(AppHost.Services.GetRequiredService<Login>());
        }

        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                })
                .ConfigureServices((context, services) =>
                {
                    var connectionString = AppConstants.DefaultConnectionString;

                    services.AddDbContextFactory<TiendaDbContext>(options =>
                        options.UseMySql(connectionString,
                            ServerVersion.AutoDetect(connectionString),
                            mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 10,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null)
                        )
                    );


                    // Registro de Repositorios
                    services.AddTransient<IUsuarioRepository, UsuarioRepository>();
                    services.AddTransient<ITecnicoRepository, TecnicoRepository>();
                    services.AddTransient<IClienteRepository, ClienteRepository>(); // ¡NUEVO!
                    services.AddTransient<IDispositivoRepository, DispositivoRepository>(); // ¡NUEVO!
                    services.AddTransient<IOrdenServicioRepository, OrdenServicioRepository>(); // ¡NUEVO!


                    // Registro de Servicios
                    services.AddTransient<IAuthServices, AuthServices>();
                    services.AddTransient<IClienteServices, ClienteServices>(); // ¡NUEVO!
                    services.AddTransient<IDispositivoServices, DispositivoServices>(); // ¡NUEVO!
                    services.AddTransient<IOrdenServicioServices, OrdenServicioServices>(); // ¡NUEVO!
                    services.AddTransient<ITecnicoServices, TecnicoServices>(); // ¡NUEVO! Para buscar técnicos


                    // Registro de Formularios de UI para que puedan ser inyectados.
                    services.AddTransient<Login>();
                    // El Dashboard ahora recibe el Usuario y el IServiceProvider
                    services.AddTransient<Dashboard>(); // Registra el Dashboard
                    services.AddTransient<GestionClientes>(); // ¡NUEVO!
                    services.AddTransient<GestionDispositivos>(); // ¡NUEVO!
                    services.AddTransient<OrdenesServicio>(); // ¡NUEVO!
                });
    }
}