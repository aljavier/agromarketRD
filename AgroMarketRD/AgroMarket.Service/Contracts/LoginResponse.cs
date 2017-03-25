using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AgroMarketRD.Service.Contracts
{
    [DataContract]
    public class LoginResponse : BaseAgroContract
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string Token { get; set; }
    }
}