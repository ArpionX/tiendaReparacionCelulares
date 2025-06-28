using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    // Representa la tabla 'repuestos' en la base de datos.
    [Table("repuestos")]
    public class Repuesto
    {
        [Key]
        [Column("id_repuesto")]
        public int IdRepuesto { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [StringLength(50)]
        [Column("marca_compatible")]
        public string MarcaCompatible { get; set; }

        [StringLength(100)]
        [Column("modelo_compatible")]
        public string ModeloCompatible { get; set; }

        [Required]
        [Column("precio", TypeName = "DECIMAL(10,2)")]
        public decimal Precio { get; set; }

        [Column("stock_disponible")]
        public int StockDisponible { get; set; } = 0;

        [StringLength(100)]
        [Column("proveedor")]
        public string Proveedor { get; set; }

        [Column("fecha_actualizacion")]
        public DateTime FechaActualizacion { get; set; }

        // Propiedad de navegación para la relación muchos a muchos con Servicio.
        public ICollection<ServicioRepuesto> ServicioRepuestos { get; set; }
        // Propiedad de navegación para los repuestos utilizados en reparaciones.
        public ICollection<RepuestoUtilizado> RepuestosUtilizados { get; set; }

        public Repuesto()
        {
            ServicioRepuestos = new HashSet<ServicioRepuesto>();
            RepuestosUtilizados = new HashSet<RepuestoUtilizado>();
        }
    }
    
}
