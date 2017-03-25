using AgroMarketRD.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AgroMarket.Service
{
    [ServiceContract]
    public interface IAgroMarketService
    {
        [OperationContract]
        ProductResponse GetProducts(string user, string token);
    }
}
