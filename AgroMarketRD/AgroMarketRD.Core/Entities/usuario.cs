namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            demandas = new HashSet<Demanda>();
            ofertas = new HashSet<Oferta>();
            sesions = new HashSet<Sesion>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(150)]
        public string nombre { get; set; }

        [Required]
        [StringLength(20)]
        public string rnc { get; set; }

        [Column("usuario")]
        [Required]
        [StringLength(50)]
        public string usuario1 { get; set; }

        [Required]
        [StringLength(350)]
        public string contrasena { get; set; }

        public int cuenta_id { get; set; }

        public int tipo_usuario_id { get; set; }

        public bool? activo { get; set; }

        public virtual Cuenta cuenta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Demanda> demandas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oferta> ofertas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sesion> sesions { get; set; }

        public virtual TipoUsuario tipo_usuario { get; set; }
    }
}
