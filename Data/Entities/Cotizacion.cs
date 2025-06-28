using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    // Enumeración para el estado de la cotización.
    public enum EstadoCotizacion
    {
        pendiente,
        aprobada,
        rechazada
    }

    // Representa la tabla 'cotizaciones' en la base de datos.
    [Table("cotizaciones")]
    public class Cotizacion
    {
        [Key]
        [Column("id_cotizacion")]
        public int IdCotizacion { get; set; }

        [Column("id_orden")]
        public int IdOrden { get; set; } // Foreign Key

        [Column("fecha_cotizacion")]
        public DateTime FechaCotizacion { get; set; }

        [Required]
        [Column("subtotal", TypeName = "DECIMAL(10,2)")]
        public decimal Subtotal { get; set; }

        [Column("impuestos", TypeName = "DECIMAL(10,2)")]
        public decimal Impuestos { get; set; }

        [Required]
        [Column("total", TypeName = "DECIMAL(10,2)")]
        public decimal Total { get; set; }

        [Column("estado_cotizacion")]
        public EstadoCotizacion EstadoCotizacion { get; set; }

        [Column("vigencia_dias")]
        public int VigenciaDias { get; set; } = 30;

        [Column("fecha_vencimiento", TypeName = "DATE")]
        public DateTime? FechaVencimiento { get; set; }

        // Propiedad de navegación a la orden de servicio.
        public OrdenServicio OrdenServicio { get; set; }
        // Propiedad de navegación a los detalles de cotización.
        public ICollection<DetalleCotizacion> DetallesCotizacion { get; set; }

        public Cotizacion()
        {
            DetallesCotizacion = new HashSet<DetalleCotizacion>();
        }
    }
}
