using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AgroMarketRD.Service.Contracts
{
    public class RequestResponse : BaseAgroContract
    {
        [DataMember]
        public List<Request> Requests { get; set; }
    }

    [DataContract]
    public class Request {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ProductId { get; set; }
        [DataMember]
        public int ProductUnitId { get; set; }
        [DataMember]
        public int BuyerId { get; set; }
        [DataMember]
        public string Comment { get; set; }
    }
}