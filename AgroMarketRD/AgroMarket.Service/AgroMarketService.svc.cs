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
        public ProductResponse GetProducts(string user, string token)
        {
            var productos = new List<Product>();
            productos = new List<Product> {
                new Product { code = "XXX", description = "bla bla"},
                new Product { code = "YYYY", description = "WHATEVER"}
            };

            return new ProductResponse { Products = productos };
        }
    }
}
