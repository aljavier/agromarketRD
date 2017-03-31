using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AgroMarketRD.Service.Requests
{
    [DataContract]
    public class IntentionToSellRequest: BaseRequest
    {
        [DataMember]
        public List<InnerProductRequest> ProductList { get; set; }
        [DataMember]
        public int IntentionToBuydId { get; set; }

        public IntentionToSellRequest()
        {
            ProductList = new List<InnerProductRequest>();
        }
    }


}