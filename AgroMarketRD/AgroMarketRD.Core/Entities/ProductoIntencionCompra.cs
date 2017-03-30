namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("producto_intencion_compra")]
    public partial class ProductoIntencionCompra
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("producto_id")]
        public int ProductoId { get; set; }
        [Column("tipo_unidad_id")]
        public int TipoUnidadId { get; set; }
        [Column("id_intencion_compra")]
        public int IntencionCompraId { get; set; }
        [Column("cantidad")]
        public int cantidad { get; set; }

        [Column("precio_unidad", TypeName = "numeric")]
        public decimal PrecioUnidad { get; set; }

        public virtual IntencionCompra IntencionCompra { get; set; }
    }
}
