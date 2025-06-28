using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaReparacion.Data.Entities
{
    public enum EstadoOrden
    {
        recibido,
        diagnosticando,
        cotizado,
        aprobado,
        en_reparacion,
        terminado,
        entregado,
        cancelado
    }

    // Representa la tabla 'ordenes_servicio' en la base de datos.
    [Table("ordenes_servicio")]
    public class OrdenServicio
    {
        [Key]
        [Column("id_orden")]
        public int IdOrden { get; set; }

        [Required]
        [StringLength(20)]
        [Column("numero_orden")]
        public string NumeroOrden { get; set; }

        [Column("id_cliente")]
        public int IdCliente { get; set; } // Foreign Key

        [Column("id_dispositivo")]
        public int IdDispositivo { get; set; } // Foreign Key

        [Column("id_tecnico")]
        public int? IdTecnico { get; set; } // Foreign Key, puede ser nulo al inicio

        [Required]
        [Column("descripcion_problema", TypeName = "TEXT")]
        public string DescripcionProblema { get; set; }

        [Column("fecha_ingreso")]
        public DateTime FechaIngreso { get; set; }

        [Column("fecha_entrega_estimada", TypeName = "DATE")]
        public DateTime? FechaEntregaEstimada { get; set; }

        [Column("fecha_entrega_real", TypeName = "DATE")]
        public DateTime? FechaEntregaReal { get; set; }

        [Column("estado_orden")]
        public EstadoOrden EstadoOrden { get; set; }

        [Column("observaciones", TypeName = "TEXT")]
        public string Observaciones { get; set; }

        // Propiedades de navegación a las entidades relacionadas.
        public Cliente Cliente { get; set; }
        public Dispositivo Dispositivo { get; set; }
        public Tecnico Tecnico { get; set; } // Puede ser nulo

        // Propiedades de navegación a las colecciones relacionadas.
        public ICollection<Diagnostico> Diagnosticos { get; set; }
        public ICollection<Cotizacion> Cotizaciones { get; set; }
        public ICollection<Factura> Facturas { get; set; }
        public ICollection<Garantia> Garantias { get; set; }
        public ICollection<RepuestoUtilizado> RepuestosUtilizados { get; set; }

        public OrdenServicio()
        {
            Diagnosticos = new HashSet<Diagnostico>();
            Cotizaciones = new HashSet<Cotizacion>();
            Facturas = new HashSet<Factura>();
            Garantias = new HashSet<Garantia>();
            RepuestosUtilizados = new HashSet<RepuestoUtilizado>();
        }
    }
}
