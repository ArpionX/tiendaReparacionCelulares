using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    public enum RolUsuario
    {
        administrador,
        tecnico,
        ventas,
        gerente
    }

    // Representa la tabla 'usuarios' en la base de datos (sugerencia adicional).
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        [Column("nombre_usuario")]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(255)] // Suficientemente grande para un hash de contraseña
        [Column("contrasena_hash")]
        public string ContrasenaHash { get; set; }

        [Required]
        [Column("rol")]
        public RolUsuario Rol { get; set; }

        [Column("id_tecnico")]
        public int? IdTecnico { get; set; } // Foreign Key opcional

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

        [Column("ultimo_login")]
        public DateTime? UltimoLogin { get; set; }

        // Propiedad de navegación opcional al técnico si existe.
        public Tecnico Tecnico { get; set; }
    }
}
