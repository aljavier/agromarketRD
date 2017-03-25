namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("demanda")]
    public partial class demanda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public demanda()
        {
            intencion_compra = new HashSet<intencion_compra>();
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

        public virtual producto producto { get; set; }

        public virtual tipo_unidad tipo_unidad { get; set; }

        public virtual usuario usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<intencion_compra> intencion_compra { get; set; }
    }
}
