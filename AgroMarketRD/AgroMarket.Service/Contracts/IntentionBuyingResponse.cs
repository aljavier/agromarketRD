using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AgroMarketRD.Service.Contracts
{
    public class IntentionBuyingResponse : BaseAgroContract
    {
        [DataMember]
        public List<IntentionBuying> Intentions { get; set; }

        public IntentionBuyingResponse()
        {
            Intentions = new List<Contracts.IntentionBuying>();
        }
    }

    [DataContract]
    public class IntentionBuying
    {
        public IntentionBuying()
        {
            ProductList = new List<ProductIntention>();
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int BuyerId { get; set; }
        [DataMember]
        public string Buyer { get; set; }
        [DataMember]
        public List<ProductIntention> ProductList { get; set; }
        [DataMember]
        public DateTime DateCreation { get; set; }
        [DataMember]
        public DateTime ExpirationDate { get; set; }
    }
    [DataContract]
    public class ProductIntention
    {
        [DataMember]
        public string ProductCode { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public int ProductUnitId { get; set; }
        [DataMember]
        public string ProductUnit { get; set; }
        [DataMember]
        public decimal Quantity { get; set; }
        [DataMember]
        public decimal PriceUnit { get; set; }
    }
}