using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AgroMarketRD.Service.Requests
{
    [DataContract]
    public class BaseRequest
    {
        [DataMember]
        public string userName { get; set; } 
        [DataMember]
        public string token { get; set; }
    }
}