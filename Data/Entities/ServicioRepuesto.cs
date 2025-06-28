using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    // Representa la tabla de unión 'servicio_repuesto' para la relación muchos a muchos.
    [Table("servicio_repuesto")]
    public class ServicioRepuesto
    {
        [Column("id_servicio")]
        public int IdServicio { get; set; } // Foreign Key Parte de la PK compuesta

        [Column("id_repuesto")]
        public int IdRepuesto { get; set; } // Foreign Key Parte de la PK compuesta

        [Column("cantidad_necesaria")]
        public int CantidadNecesaria { get; set; } = 1;

        // Propiedades de navegación a las entidades relacionadas.
        public Servicio Servicio { get; set; }
        public Repuesto Repuesto { get; set; }
    }
}
