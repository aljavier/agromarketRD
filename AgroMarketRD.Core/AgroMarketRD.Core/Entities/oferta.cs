namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("oferta")]
    public partial class oferta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public oferta()
        {
            intencion_compra = new HashSet<intencion_compra>();
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
        public virtual ICollection<intencion_compra> intencion_compra { get; set; }

        public virtual producto producto { get; set; }

        public virtual tipo_unidad tipo_unidad { get; set; }

        public virtual usuario usuario { get; set; }
    }
}
