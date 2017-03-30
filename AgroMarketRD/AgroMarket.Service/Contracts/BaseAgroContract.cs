using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using static AgroMarketRD.Core.Enums.Enumeradores;

namespace AgroMarketRD.Service.Contracts
{
    [DataContract]
    public class BaseAgroContract
    {
        [DataMember]
        public ErrorResponse Error { get; set; }

        public BaseAgroContract()
        {
            Error = new ErrorResponse { Code = Errores.AG000.ToString(), Description = "Respuesta exitosa!" };
        }
    }

    [DataContract]
    public class ErrorResponse {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Description { get; set; }

        public ErrorResponse()
        {
            Code = Errores.AG000.ToString();
            Description = "Respuesta exitosa!";
        }
    }
}