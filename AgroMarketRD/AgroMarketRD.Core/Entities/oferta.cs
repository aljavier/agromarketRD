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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Oferta()
        {
            intencion_compra = new HashSet<IntencionCompra>();
        }

        public int id { get; set; }

        public int producto_id { get; set; }

        public int tipo_unidad_id { get; set; }

        public int cantidad { get; set; }

        [Column(TypeName = "numeric")]
        public decimal precio_unidad { get; set; }

        [Column(TypeName = "numeric")]
        public decimal monto_total { get; set; }

        public int usuario_id { get; set; }

        public DateTime fecha_creacion { get; set; }

        public bool activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntencionCompra> intencion_compra { get; set; }

        public virtual Producto producto { get; set; }

        public virtual TipoUnidad tipo_unidad { get; set; }

        public virtual Usuario usuario { get; set; }
    }
}
