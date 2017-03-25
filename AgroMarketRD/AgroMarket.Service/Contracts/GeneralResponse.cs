using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AgroMarketRD.Service.Contracts
{
    [DataContract]
    public class GeneralResponse : BaseAgroContract
    {
        [DataMember]
        public int Id { get; set; }
    }
}