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
        [DataMember]
        public List<Offer> Offers { get; set; }

        public OfferResponse()
        {
            Offers = new List<Offer>();
        }
    }

    [DataContract]
    public class Offer {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ProductCode { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public int ProductUnitId { get; set; }
        [DataMember]
        public string ProductUnit { get; set; }
        [DataMember]
        public int ProductorId { get; set; }
        [DataMember]
        public string Productor { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public decimal PriceUnit { get; set; }
    }
}