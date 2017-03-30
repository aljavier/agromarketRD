namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sesion")]
    public partial class Sesion
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(350)]
        [Column("token")]
        public string Token { get; set; }
        [Column("fecha_expiracion")]
        public DateTime FechaExpiracion { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
