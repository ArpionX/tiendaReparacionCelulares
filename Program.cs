using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TiendaReparacion.Data;
using TiendaReparacion.Repositories;

namespace TiendaReparacion
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configura el host de la aplicación. Esto es lo que permite la inyección de dependencias
            // y la gestión de la configuración en aplicaciones .NET 8.0 WinForms.
            var host = CreateHostBuilder().Build();

            // Resuelve el servicio de DbContext y lo usa para asegurar que la base de datos se cree/migre.
            // Esto es opcional, pero útil para el desarrollo inicial.
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TiendaDbContext>();
                    // Opcional: Aplica migraciones pendientes al iniciar la aplicación.
                    // context.Database.Migrate(); // Descomentar si usas migraciones.
                }
                catch (Exception ex)
                {
                    // Manejo de errores si la conexión a la BD falla al inicio.
                    MessageBox.Show($"Error al conectar o migrar la base de datos: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Termina la aplicación si hay un error crítico al inicio de la BD.
                }
            }

            // Para configurar la alta DPI (píxeles por pulgada) para la aplicación.
            ApplicationConfiguration.Initialize();

            // Ejecuta la aplicación WinForms.
            Application.Run(host.Services.GetRequiredService<Form1>()); // Asume que tienes un Form1
        }

        // Crea y configura el IHostBuilder para la aplicación.
        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    // En este enfoque "más sencillo", no necesitamos cargar appsettings.json.
                    // Puedes dejar esta sección vacía o eliminarla si no planeas usar ninguna otra configuración.
                    // La dejaremos con el SetBasePath por si en el futuro se quiere añadir algo.
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                })
                .ConfigureServices((context, services) =>
                {
                    // Obtiene la cadena de conexión de la clase estática AppConstants.
                    var connectionString = AppConstants.DefaultConnectionString;

                    // Registra el TiendaDbContext con la inyección de dependencias.
                    services.AddDbContext<TiendaDbContext>(options =>
                        options.UseMySql(connectionString,
                            ServerVersion.AutoDetect(connectionString), // Auto-detecta la versión de tu servidor MySQL.
                            mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 10,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null)
                        )
                    );
                    // ¡NUEVO! Registra el repositorio de técnicos para la inyección de dependencias.
                    // Usamos AddTransient porque los repositorios a menudo tienen un ciclo de vida corto.
                    services.AddTransient<ITecnicoRepository, TecnicoRepository>();
                    // Registra tus formularios principales o servicios de UI que necesiten inyección.
                    services.AddTransient<Form1>(); // Asegúrate de que Form1 pueda recibir TiendaDbContext en su constructor
                });
    }
}