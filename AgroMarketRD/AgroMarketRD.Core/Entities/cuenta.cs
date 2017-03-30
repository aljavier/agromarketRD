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
            Usuarios = new HashSet<Usuario>();
        }
        [Column("id")]
        public int Id { get; set; }

        [Column("monto", TypeName = "numeric")]
        public decimal Monto { get; set; }

        [Column("limite_credito", TypeName = "numeric")]
        public decimal LimiteCredito { get; set; }

        [Column("total_consumido", TypeName = "numeric")]
        public decimal TotalConsumido { get; set; }

        [Column("total_disponible", TypeName = "numeric")]
        public decimal TotalDisponible { get; set; }

        [Required]
        [StringLength(250)]
        [Column("descripcion")]
        public string descripcion { get; set; }
        [Column("activo")]
        public bool Activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
