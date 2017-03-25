namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cuenta")]
    public partial class Cuenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cuenta()
        {
            usuarios = new HashSet<Usuario>();
        }

        public int id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal monto { get; set; }

        [Column(TypeName = "numeric")]
        public decimal limite_credito { get; set; }

        [Column(TypeName = "numeric")]
        public decimal total_consumido { get; set; }

        [Column(TypeName = "numeric")]
        public decimal total_disponible { get; set; }

        [Required]
        [StringLength(250)]
        public string descripcion { get; set; }

        public bool activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> usuarios { get; set; }
    }
}
