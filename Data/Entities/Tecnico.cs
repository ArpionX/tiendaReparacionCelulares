using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    public enum EstadoTecnico
    {
        activo,
        inactivo
    }
    [Table("tecnicos")]
    public class Tecnico
    {
        [Key]
        [Column("id_tecnico")]
        public int IdTecnico { get; set; }

        [Required]
        [StringLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        [Column("apellido")]
        public string Apellido { get; set; }

        [StringLength(100)]
        [Column("especialidad")]
        public string Especialidad { get; set; }

        [StringLength(15)]
        [Column("telefono")]
        public string Telefono { get; set; }

        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [Column("fecha_ingreso", TypeName = "DATE")]
        public DateTime? FechaIngreso { get; set; } // Nullable si no siempre se registra

        [Column("estado")]
        public EstadoTecnico Estado { get; set; }

        // Propiedad de navegación para las órdenes de servicio asignadas a este técnico.
        public ICollection<OrdenServicio> OrdenesServicio { get; set; }
        // Propiedad de navegación para los diagnósticos realizados por este técnico.
        public ICollection<Diagnostico> Diagnosticos { get; set; }

        public Tecnico()
        {
            OrdenesServicio = new HashSet<OrdenServicio>();
            Diagnosticos = new HashSet<Diagnostico>();
        }
    }
}
