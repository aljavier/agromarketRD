namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("firma")]
    public partial class Firma
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("vendedor")]
        public int? Vendedor { get; set; }
        [Column("comprador")]
        public int? Comprador { get; set; }
        [Column("intencion_compra_id")]
        public int IntencionCompraId { get; set; }
        [Column("intencion_venta_id")]
        public int IntencionVentaId { get; set; }
        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }
        [Column("fecha_final")]
        public DateTime? FechaFinal { get; set; }

        public virtual IntencionCompra IntencionCompra { get; set; }
        public virtual IntencionVenta IntencionVenta { get; set; }
    }
}
