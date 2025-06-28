using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    // Enumeración para el tipo de pago.
    public enum TipoPago
    {
        anticipo,
        saldo_final,
        pago_completo
    }

    // Representa la tabla 'pagos' en la base de datos.
    [Table("pagos")]
    public class Pago
    {
        [Key]
        [Column("id_pago")]
        public int IdPago { get; set; }

        [Column("id_factura")]
        public int IdFactura { get; set; } // Foreign Key

        [Required]
        [Column("monto", TypeName = "DECIMAL(10,2)")]
        public decimal Monto { get; set; }

        [Column("fecha_pago")]
        public DateTime FechaPago { get; set; }

        [Required]
        [Column("metodo_pago")]
        public MetodoPago MetodoPago { get; set; } // Reutilizamos el enum de Factura

        [Required]
        [Column("tipo_pago")]
        public TipoPago TipoPago { get; set; }

        [StringLength(50)]
        [Column("numero_transaccion")]
        public string NumeroTransaccion { get; set; }

        [Column("observaciones", TypeName = "TEXT")]
        public string Observaciones { get; set; }

        // Propiedad de navegación a la factura.
        public Factura Factura { get; set; }
    }
}
