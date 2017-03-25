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
        LoginResponse SignIn(string userId, string password);

        [OperationContract]
        ProductResponse GetProducts(string userId, string token);

        [OperationContract]
        ProductUnitResponse GetUnitTypes(string userId, string token);
    }
}
