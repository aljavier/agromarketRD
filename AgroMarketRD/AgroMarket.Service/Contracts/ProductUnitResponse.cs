using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AgroMarketRD.Service.Contracts
{
    [DataContract]
    public class ProductUnitResponse : BaseAgroContract
    {
        [DataMember]
        public List<ProductUnit> UnitTypes { get; set; }
    }

    [DataContract]
    public class ProductUnit {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}