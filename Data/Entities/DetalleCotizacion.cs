using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    [Table("detalle_cotizacion")]
    public class DetalleCotizacion
    {
        [Key]
        [Column("id_detalle")]
        public int IdDetalle { get; set; }

        [Column("id_cotizacion")]
        public int IdCotizacion { get; set; } // Foreign Key

        [Column("id_servicio")]
        public int? IdServicio { get; set; } // Foreign Key, puede ser nulo si es una descripción libre

        [Required]
        [StringLength(200)]
        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; } = 1;

        [Required]
        [Column("precio_unitario", TypeName = "DECIMAL(10,2)")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Column("subtotal", TypeName = "DECIMAL(10,2)")]
        public decimal Subtotal { get; set; }

        // Propiedades de navegación a las entidades relacionadas.
        public Cotizacion Cotizacion { get; set; }
        public Servicio Servicio { get; set; } // Puede ser nulo
    }
}
