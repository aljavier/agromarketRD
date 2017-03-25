namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("demanda")]
    public partial class Demanda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Demanda()
        {
            intencion_compra = new HashSet<IntencionCompra>();
        }

        public int id { get; set; }

        public int producto_id { get; set; }

        public int tipo_unidad_id { get; set; }

        public int usuario_id { get; set; }

        [Required]
        [StringLength(250)]
        public string comentario { get; set; }

        [StringLength(10)]
        public string activo { get; set; }

        public virtual Producto producto { get; set; }

        public virtual TipoUnidad tipo_unidad { get; set; }

        public virtual Usuario usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntencionCompra> intencion_compra { get; set; }
    }
}
