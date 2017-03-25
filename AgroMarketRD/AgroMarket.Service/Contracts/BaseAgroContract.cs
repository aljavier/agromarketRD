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
            Error = new ErrorResponse { Code = "AG000", Description = "Respuesta exitosa!" };
        }
    }

    [DataContract]
    public class ErrorResponse {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}