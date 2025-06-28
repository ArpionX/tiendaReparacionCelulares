using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    // Enumeración para la categoría de servicio.
    public enum CategoriaServicio
    {
        pantalla,
        bateria,
        software,
        audio,
        camara,
        conectores,
        otro
    }

    // Representa la tabla 'servicios' en la base de datos.
    [Table("servicios")]
    public class Servicio
    {
        [Key]
        [Column("id_servicio")]
        public int IdServicio { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombre_servicio")]
        public string NombreServicio { get; set; }

        [Column("descripcion", TypeName = "TEXT")]
        public string Descripcion { get; set; }

        [Required]
        [Column("precio_base", TypeName = "DECIMAL(10,2)")]
        public decimal PrecioBase { get; set; }

        [Column("tiempo_estimado_horas")]
        public int? TiempoEstimadoHoras { get; set; }

        [Required]
        [Column("categoria")]
        public CategoriaServicio Categoria { get; set; }

        // Propiedad de navegación para los detalles de cotización que incluyen este servicio.
        public ICollection<DetalleCotizacion> DetallesCotizacion { get; set; }
        // Propiedad de navegación para la relación muchos a muchos con Repuesto.
        public ICollection<ServicioRepuesto> ServicioRepuestos { get; set; }

        public Servicio()
        {
            DetallesCotizacion = new HashSet<DetalleCotizacion>();
            ServicioRepuestos = new HashSet<ServicioRepuesto>();
        }
    }
}
