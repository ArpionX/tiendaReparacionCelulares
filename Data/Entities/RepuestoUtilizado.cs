using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    // Representa la tabla 'repuestos_utilizados' en la base de datos.
    [Table("repuestos_utilizados")]
    public class RepuestoUtilizado
    {
        [Key]
        [Column("id_uso")]
        public int IdUso { get; set; }

        [Column("id_orden")]
        public int IdOrden { get; set; } // Foreign Key

        [Column("id_repuesto")]
        public int IdRepuesto { get; set; } // Foreign Key

        [Required]
        [Column("cantidad_utilizada")]
        public int CantidadUtilizada { get; set; }

        [Required]
        [Column("precio_unitario", TypeName = "DECIMAL(10,2)")]
        public decimal PrecioUnitario { get; set; }

        [Column("fecha_uso")]
        public DateTime FechaUso { get; set; }

        // Propiedades de navegación a las entidades relacionadas.
        public OrdenServicio OrdenServicio { get; set; }
        public Repuesto Repuesto { get; set; }
    }
}
