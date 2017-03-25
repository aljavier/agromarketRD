using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AgroMarketRD.Service.Contracts
{
    [DataContract]
    public class OfferResponse : BaseAgroContract
    {
        public List<Offer> Offers { get; set; }
    }

    [DataContract]
    public class Offer {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ProductId { get; set; }
        [DataMember]
        public int ProductUnitId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public decimal PriceUnit { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }
    }
}