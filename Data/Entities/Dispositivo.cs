using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    // Representa la tabla 'dispositivos' en la base de datos.
    [Table("dispositivos")]
    public class Dispositivo
    {
        [Key]
        [Column("id_dispositivo")]
        public int IdDispositivo { get; set; }

        [Column("id_cliente")]
        public int IdCliente { get; set; } // Foreign Key

        [Required]
        [StringLength(50)]
        [Column("marca")]
        public string Marca { get; set; }

        [Required]
        [StringLength(100)]
        [Column("modelo")]
        public string Modelo { get; set; }

        [StringLength(20)]
        [Column("imei")]
        public string Imei { get; set; }

        [StringLength(30)]
        [Column("color")]
        public string Color { get; set; }

        [Column("año_fabricacion", TypeName = "YEAR")]
        public short? AnioFabricacion { get; set; } // Usar short para YEAR

        [StringLength(50)]
        [Column("sistema_operativo")]
        public string SistemaOperativo { get; set; }

        // Propiedad de navegación al cliente al que pertenece el dispositivo.
        public Cliente Cliente { get; set; }
        // Propiedad de navegación para las órdenes de servicio asociadas a este dispositivo.
        public ICollection<OrdenServicio> OrdenesServicio { get; set; }

        public Dispositivo()
        {
            OrdenesServicio = new HashSet<OrdenServicio>();
        }
    }
}
