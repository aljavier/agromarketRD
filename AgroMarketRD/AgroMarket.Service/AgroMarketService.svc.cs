using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AgroMarketRD.Service.Contracts;
using AgroMarketRD.Core.Entities;

namespace AgroMarket.Service
{
    public class AgroMarketService : IAgroMarketService
    {
        public LoginResponse SignIn(string user, string password)
        {
            return new LoginResponse { Token = Guid.NewGuid().ToString(),
                Error = new ErrorResponse { code = "AG000", description = "OK" } };
        }

        public ProductResponse GetProducts(string userId, string token)
        {
            var productos = new List<Product>();
            productos = new List<Product> {
                new Product { Code = "XXX", Description = "bla bla"},
                new Product { Code = "YYYY", Description = "WHATEVER"}
            };

            return new ProductResponse { Products = productos };
        }

        public ProductUnitResponse GetUnitTypes(string userId, string token)
        {
            return new ProductUnitResponse();
        }
    }
}
