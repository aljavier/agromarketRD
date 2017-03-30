namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("errores")]
    public partial class Error
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("codigo")]
        public string Codigo { get; set; }

        [Required]
        [StringLength(250)]
        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
