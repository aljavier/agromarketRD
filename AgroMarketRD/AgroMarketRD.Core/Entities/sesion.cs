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
        public int id { get; set; }

        public int usuario_id { get; set; }

        [Required]
        [StringLength(350)]
        public string token { get; set; }

        public DateTime fecha_expiracion { get; set; }

        public virtual Usuario usuario { get; set; }
    }
}
