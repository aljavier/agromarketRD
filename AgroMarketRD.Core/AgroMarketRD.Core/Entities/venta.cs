namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("venta")]
    public partial class venta
    {
        public int id { get; set; }

        public int intencion_compra_id { get; set; }

        public DateTime fecha_creacion { get; set; }

        public virtual intencion_compra intencion_compra { get; set; }
    }
}
