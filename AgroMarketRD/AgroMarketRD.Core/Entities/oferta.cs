namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("oferta")]
    public partial class Oferta
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("producto_id")]
        public int ProductoId { get; set; }
        [Column("tipo_unidad_id")]
        public int TipoUnidadId { get; set; }
        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("precio_unidad", TypeName = "numeric")]
        public decimal PrecioUnidad { get; set; }
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }
        [Column("activo")]
        public bool Activo { get; set; }

        public virtual Producto Producto { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual TipoUnidad TipoUnidad { get; set; }
    }
}
