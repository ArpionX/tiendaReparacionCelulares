using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    // Representa la tabla 'diagnosticos' en la base de datos.
    [Table("diagnosticos")]
    public class Diagnostico
    {
        [Key]
        [Column("id_diagnostico")]
        public int IdDiagnostico { get; set; }

        [Column("id_orden")]
        public int IdOrden { get; set; } // Foreign Key

        [Column("id_tecnico")]
        public int IdTecnico { get; set; } // Foreign Key

        [Required]
        [Column("descripcion_problema", TypeName = "TEXT")]
        public string DescripcionProblema { get; set; }

        [Column("causa_raiz", TypeName = "TEXT")]
        public string CausaRaiz { get; set; }

        [Column("solucion_propuesta", TypeName = "TEXT")]
        public string SolucionPropuesta { get; set; }

        [Column("tiempo_estimado_horas")]
        public int? TiempoEstimadoHoras { get; set; }

        [Column("fecha_diagnostico")]
        public DateTime FechaDiagnostico { get; set; }

        // Propiedades de navegación a las entidades relacionadas.
        public OrdenServicio OrdenServicio { get; set; }
        public Tecnico Tecnico { get; set; }
    }
}
