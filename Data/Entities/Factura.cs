using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TiendaReparacion.Data.Entities
{
    // Enumeración para el método de pago.
    public enum MetodoPago
    {
        efectivo,
        tarjeta_credito,
        tarjeta_debito,
        transferencia,
        mixto
    }

    // Enumeración para el estado de pago.
    public enum EstadoPago
    {
        pendiente,
        pagado_parcial,
        pagado_total
    }

    // Representa la tabla 'facturas' en la base de datos.
    [Table("facturas")]
    public class Factura
    {
        [Key]
        [Column("id_factura")]
        public int IdFactura { get; set; }

        [Column("id_orden")]
        public int IdOrden { get; set; } // Foreign Key

        [Required]
        [StringLength(20)]
        [Column("numero_factura")]
        public string NumeroFactura { get; set; }

        [Column("fecha_factura")]
        public DateTime FechaFactura { get; set; }

        [Required]
        [Column("subtotal", TypeName = "DECIMAL(10,2)")]
        public decimal Subtotal { get; set; }

        [Column("impuestos", TypeName = "DECIMAL(10,2)")]
        public decimal Impuestos { get; set; }

        [Required]
        [Column("total", TypeName = "DECIMAL(10,2)")]
        public decimal Total { get; set; }

        [Column("metodo_pago")]
        public MetodoPago? MetodoPago { get; set; } // Puede ser nulo si se decide después

        [Column("estado_pago")]
        public EstadoPago EstadoPago { get; set; }

        // Propiedad de navegación a la orden de servicio.
        public OrdenServicio OrdenServicio { get; set; }
        // Propiedad de navegación a los pagos asociados a esta factura.
        public ICollection<Pago> Pagos { get; set; }

        public Factura()
        {
            Pagos = new HashSet<Pago>();
        }
    }
}
