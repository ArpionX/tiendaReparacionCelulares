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
            // Configura el host de la aplicaci�n. Esto es lo que permite la inyecci�n de dependencias
            // y la gesti�n de la configuraci�n en aplicaciones .NET 8.0 WinForms.
            var host = CreateHostBuilder().Build();

            // Resuelve el servicio de DbContext y lo usa para asegurar que la base de datos se cree/migre.
            // Esto es opcional, pero �til para el desarrollo inicial.
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TiendaDbContext>();
                    // Opcional: Aplica migraciones pendientes al iniciar la aplicaci�n.
                    // context.Database.Migrate(); // Descomentar si usas migraciones.
                }
                catch (Exception ex)
                {
                    // Manejo de errores si la conexi�n a la BD falla al inicio.
                    MessageBox.Show($"Error al conectar o migrar la base de datos: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Termina la aplicaci�n si hay un error cr�tico al inicio de la BD.
                }
            }

            // Para configurar la alta DPI (p�xeles por pulgada) para la aplicaci�n.
            ApplicationConfiguration.Initialize();

            // Ejecuta la aplicaci�n WinForms.
            Application.Run(host.Services.GetRequiredService<Form1>()); // Asume que tienes un Form1
        }

        // Crea y configura el IHostBuilder para la aplicaci�n.
        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    // En este enfoque "m�s sencillo", no necesitamos cargar appsettings.json.
                    // Puedes dejar esta secci�n vac�a o eliminarla si no planeas usar ninguna otra configuraci�n.
                    // La dejaremos con el SetBasePath por si en el futuro se quiere a�adir algo.
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                })
                .ConfigureServices((context, services) =>
                {
                    // Obtiene la cadena de conexi�n de la clase est�tica AppConstants.
                    var connectionString = AppConstants.DefaultConnectionString;

                    // Registra el TiendaDbContext con la inyecci�n de dependencias.
                    services.AddDbContext<TiendaDbContext>(options =>
                        options.UseMySql(connectionString,
                            ServerVersion.AutoDetect(connectionString), // Auto-detecta la versi�n de tu servidor MySQL.
                            mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 10,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null)
                        )
                    );
                    // �NUEVO! Registra el repositorio de t�cnicos para la inyecci�n de dependencias.
                    // Usamos AddTransient porque los repositorios a menudo tienen un ciclo de vida corto.
                    services.AddTransient<ITecnicoRepository, TecnicoRepository>();
                    // Registra tus formularios principales o servicios de UI que necesiten inyecci�n.
                    services.AddTransient<Form1>(); // Aseg�rate de que Form1 pueda recibir TiendaDbContext en su constructor
                });
    }
}