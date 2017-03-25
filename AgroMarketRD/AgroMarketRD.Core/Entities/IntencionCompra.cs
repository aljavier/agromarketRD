namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("intencion_compra")]
    public partial class IntencionCompra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IntencionCompra()
        {
            ventas = new HashSet<Venta>();
        }

        public int id { get; set; }

        public int demanda_id { get; set; }

        public int oferta_id { get; set; }

        public int cantidad { get; set; }

        public DateTime fecha_creacion { get; set; }

        public DateTime fecha_expiracion { get; set; }

        public bool activo { get; set; }

        public virtual Demanda demanda { get; set; }

        public virtual Oferta oferta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Venta> ventas { get; set; }
    }
}
