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
            Ofertas = new HashSet<Oferta>();
        }
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(20)]
        [Column("rnc")]
        public string RNC { get; set; }

        [Column("usuario")]
        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(350)]
        [Column("contrasena")]
        public string Contrasena { get; set; }
        [Column("cuenta_id")]
        public int CuentaId { get; set; }
        [Column("tipo_usuario_id")]
        public int TipoUsuarioId { get; set; }
        [Column("activo")]
        public bool Activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oferta> Ofertas { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
        public virtual Cuenta Cuenta { get; set; }
    }
}
