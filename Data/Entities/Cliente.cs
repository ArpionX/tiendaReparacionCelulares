using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    [Table("clientes")]
    public class Cliente
    {
        [Key]
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        [Column("apellido")]
        public string Apellido { get; set; }

        [Required]
        [StringLength(15)]
        [Column("telefono")]
        public string Telefono { get; set; }

        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [Column("direccion", TypeName = "TEXT")]
        public string Direccion { get; set; }

        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; }

        // Propiedad de navegación para las órdenes de servicio asociadas a este cliente.
        public ICollection<OrdenServicio> OrdenesServicio { get; set; }
        // Propiedad de navegación para los dispositivos asociados a este cliente.
        public ICollection<Dispositivo> Dispositivos { get; set; }

        public Cliente()
        {
            OrdenesServicio = new HashSet<OrdenServicio>();
            Dispositivos = new HashSet<Dispositivo>();
        }
    }
}
