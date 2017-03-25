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
    /// <summary>
    /// Servicio web de la bolsa de productos agropecuarios de la Republica Dominicana.
    /// </summary>
    public class AgroMarketService : IAgroMarketService
    {
        /// <summary>
        /// Autentificacion de compradores y productores (y auditores)
        /// </summary>
        /// <param name="user">Nombre de usuario</param>
        /// <param name="password">Contraseña</param>
        /// <returns>Token de acceso y id del usuario si fue exitoso.</returns>
        public LoginResponse SignIn(string user, string password)
        {
            return new LoginResponse { Token = Guid.NewGuid().ToString(),
                Error = new ErrorResponse { code = "AG000", description = "OK" } };
        }

        /// <summary>
        /// Catologo de los productos que se pueden vender y comprar en la bolsa.
        /// </summary>
        /// <param name="userId">Usuario id</param>
        /// <param name="token">Token de acceso</param>
        /// <returns>Lista de productos del mercado</returns>
        public ProductResponse GetProducts(string userId, string token)
        {
            var productos = new List<Product>();
            productos = new List<Product> {
                new Product { Code = "XXX", Description = "bla bla"},
                new Product { Code = "YYYY", Description = "WHATEVER"}
            };

            return new ProductResponse { Products = productos };
        }

        /// <summary>
        /// Tipo de unidades de los productos. e.g. libras, sacos, etc.
        /// </summary>
        /// <param name="userId">Usuario id</param>
        /// <param name="token">Token de acceso</param>
        /// <returns>Los diferentes tipo de unidades de los productos del catalogo</returns>
        public ProductUnitResponse GetUnitTypes(string userId, string token)
        {
            return new ProductUnitResponse();
        }

        
    }
}
