namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("producto_intencion_venta")]
    public partial class ProductoIntencionVenta
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("producto_id")]
        public int ProductoId { get; set; }
        [Column("tipo_unidad_id")]
        public int TipoUnidadId { get; set; }
        [Column("id_intencion_venta")]
        public int IntencionVentaId { get; set; }
        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("precio_unidad", TypeName = "numeric")]
        public decimal PrecioUnidad { get; set; }

        public virtual IntencionVenta IntencionVenta { get; set; }
    }
}
