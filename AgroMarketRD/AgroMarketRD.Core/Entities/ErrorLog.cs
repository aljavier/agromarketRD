namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("error_log")]
    public partial class ErrorLog
    {
        [Key]
        [Column("mensaje", Order = 0)]
        [StringLength(250)]
        public string Mensaje { get; set; }

        [Column("excepcion", TypeName = "text")]
        public string Excepcion { get; set; }

        [Column("stacktrace", TypeName = "text")]
        public string StackTrace { get; set; }

        [Column("usuario")]
        public string Usuario { get; set; }

        [Key]
        [Column("fecha_creacion", Order = 1)]
        public DateTime FechaCreacion { get; set; }
    }
}
