namespace AgroMarketRD.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Venta")]
    public partial class Venta
    {
        public int VentaId { get; set; }

        public int FirmaId { get; set; }

        public int IntencionVentaId { get; set; }

        public int IntencionCompraId { get; set; }
    }
}
