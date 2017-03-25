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
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string codigo { get; set; }

        [Required]
        [StringLength(250)]
        public string descripcion { get; set; }
    }
}
