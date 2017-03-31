using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AgroMarketRD.Service.Contracts
{
    [DataContract]
    public class SellsResponse : BaseAgroContract
    {
        [DataMember]
        public List<Sell> SellList { get; set; }

        public SellsResponse()
        {
            SellList = new List<Sell>();
        }
    }
    [DataContract]
    public class Sell
    {
        [DataMember]
        public List<ProductIntention> ProductList { get; set; }
        [DataMember]
        public int IntentionSellId { get; set; }
        [DataMember]
        public int SellerId { get; set; }
        [DataMember]
        public string Seller { get; set; }
        [DataMember]
        public int IntentionBuyId { get; set; }
        [DataMember]
        public int BuyerId { get; set; }
        [DataMember]
        public string Buyer { get; set; }
        [DataMember]
        public DateTime? CreationDate { get; set; }

        public Sell()
        {
            ProductList = new List<ProductIntention>();
        }
    }
}