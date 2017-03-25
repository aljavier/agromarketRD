using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AgroMarketRD.Service.Contracts
{
    [DataContract]
    public class BaseAgroContract
    {
        [DataMember]
        public ErrorResponse Error { get; set; }

        public BaseAgroContract()
        {
            Error = new ErrorResponse { code = "AG000", description = "Respuesta exitosa!" };
        }
    }

    [DataContract]
    public class ErrorResponse {
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string description { get; set; }
    }
}