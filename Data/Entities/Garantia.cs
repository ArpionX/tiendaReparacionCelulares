using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    public enum EstadoGarantia
    {
        vigente,
        vencida,
        utilizada
    }

    // Representa la tabla 'garantias' en la base de datos.
    [Table("garantias")]
    public class Garantia
    {
        [Key]
        [Column("id_garantia")]
        public int IdGarantia { get; set; }

        [Column("id_orden")]
        public int IdOrden { get; set; } // Foreign Key

        [Required]
        [Column("fecha_inicio", TypeName = "DATE")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Column("fecha_vencimiento", TypeName = "DATE")]
        public DateTime FechaVencimiento { get; set; }

        [Column("descripcion_cobertura", TypeName = "TEXT")]
        public string DescripcionCobertura { get; set; }

        [Column("estado_garantia")]
        public EstadoGarantia EstadoGarantia { get; set; }

        [Column("terminos_condiciones", TypeName = "TEXT")]
        public string TerminosCondiciones { get; set; }

        // Propiedad de navegación a la orden de servicio.
        public OrdenServicio OrdenServicio { get; set; }
    }
}
