using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AgroMarketRD.Service.Requests
{
    [DataContract]
    public class IntentionToBuyFromProducts : BaseRequest
    {
        [DataMember]
        public List<InnerProductRequest> ProductList { get; set; }

        public IntentionToBuyFromProducts()
        {
            ProductList = new List<InnerProductRequest>();
        }
    }

    [DataContract]
    public class IntentionToBuyFromOffers: BaseRequest
    {
        [DataMember]
        public List<int> OffersId { get; set; }

        public IntentionToBuyFromOffers()
        {
            OffersId = new List<int>();
        }

    }

    [DataContract]
    public class InnerProductRequest {
        [DataMember]
        public string ProductCode { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public int ProductUnit { get; set; }
        [DataMember]
        public decimal PriceUnit { get; set; }
    }
}