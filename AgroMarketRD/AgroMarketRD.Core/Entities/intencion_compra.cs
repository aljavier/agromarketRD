namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class intencion_compra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public intencion_compra()
        {
            ventas = new HashSet<venta>();
        }

        public int id { get; set; }

        public int demanda_id { get; set; }

        public int oferta_id { get; set; }

        public int cantidad { get; set; }

        public DateTime fecha_creacion { get; set; }

        public DateTime fecha_expiracion { get; set; }

        public bool activo { get; set; }

        public virtual demanda demanda { get; set; }

        public virtual oferta oferta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<venta> ventas { get; set; }
    }
}
