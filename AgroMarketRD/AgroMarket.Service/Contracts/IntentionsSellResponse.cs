using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AgroMarketRD.Service.Contracts
{
    [DataContract]
    public class IntentionsSellResponse: BaseAgroContract
    {
        [DataMember]
        public List<IntentionSell> Intentions { get; set; }

        public IntentionsSellResponse()
        {
            Intentions = new List<IntentionSell>();
        }
    }
    
    [DataContract]
    public class IntentionSell
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int SellerId { get; set; }
        [DataMember]
        public string Seller { get; set; }
        [DataMember]
        public List<ProductIntention> ProductList { get; set; }
        public DateTime ExpirationDate { get; set; }
        [DataMember]
        public DateTime DateCreation { get; set; }

        public IntentionSell()
        {
            ProductList = new List<ProductIntention>();
        }
    }
}