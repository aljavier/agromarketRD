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
    }

    [DataContract]
    public class IntentionBuying
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int OfferId { get; set; }
        [DataMember]
        public int RequestId { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public DateTime ExpirationDate { get; set; }
    }
}