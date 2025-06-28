// Data/AppConstants.cs
namespace TiendaReparacion.Data
{
    // Clase estática para almacenar constantes de la aplicación, como la cadena de conexión.
    public static class AppConstants
    {
        // Define la cadena de conexión a tu base de datos MySQL como una constante.
        // ADVERTENCIA: Para aplicaciones en producción, no se recomienda codificar la cadena de conexión
        // directamente en el código fuente por razones de seguridad y flexibilidad.
        // Utiliza 'appsettings.json' o un sistema de gestión de secretos en un entorno de producción.
        public const string DefaultConnectionString = "Server=localhost;Database=tiendareparacion;User=root;Password=Andres2052xD@;";//CAMBIAR ESTA CADENA DE CONEXIÓN PARA TU BASE DE DATOS
    }
}

