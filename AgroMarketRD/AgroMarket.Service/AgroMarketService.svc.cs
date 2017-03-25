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
        /// Catalogo de los productos que se pueden vender y comprar en la bolsa.
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

        /// <summary>
        /// Crea una oferta, solo util para productores.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <returns>Id de la oferta creada y error de exitoso o fallido</returns>
        public GeneralResponse CreateOffer(string userId, string token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Borra una oferta, solo puede hacerse si es el propietario de la oferta.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="offerId">oferta id</param>
        /// <returns></returns>
        public ErrorResponse RemoveOffer(string userId, string token, int offerId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene una oferta por el id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="offerId">offer id</param>
        /// <returns>Oferta solicitada si existe</returns>
        public OfferResponse GetOffer(string userId, string token, int offerId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene todas las ofertas de un productor
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="productorId">productor id</param>
        /// <returns>Todas las ofertas del productor</returns>
        public OfferResponse GetOffersProductor(string userId, string token, int productorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene todas las ofertas, de todos los productores
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <returns>Todas las ofertas disponibles en el mercado</returns>
        public OfferResponse GetAllOffers(string userId, string token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Crea una solicitud de un producto
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="productId">productor id</param>
        /// <param name="quantity">quantity</param>
        /// <returns>Id del request creado</returns>
        public GeneralResponse CreateRequest(string userId, string token, int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Borra una solicitud creada, solo puede hacerlo quien la creó.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="requestId">rquest id</param>
        /// <returns>Error de exitosos o fallido</returns>
        public ErrorResponse RemoveRequest(string userId, string token, int requestId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene una solicitud de un producto por el id.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="requestId">request id</param>
        /// <returns>Solicitud del id si existe</returns>
        public RequestResponse GetRequest(string userId, string token, int requestId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene todas las solicitudes de un vendedor
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="buyerId">buyer id</param>
        /// <returns>Todas las solicitudes del vendedor</returns>
        public RequestResponse GetRequestsBuyer(string userId, string token, int buyerId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene todas las solicitudes hechas en el mercado que aun esten activas
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <returns>Lista de solicitudes disponibles</returns>
        public RequestResponse GetAllRequests(string userId, string token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene una intencion de compra por id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="intentionId">intention id</param>
        /// <returns>Intention de compra del id si existe</returns>
        public IntentionBuyingResponse GetIntentionBuying(string userId, string token, int intentionId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cancela una intencion de compra, solo pueden hacerla las partes involucradas
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="intentionId">intention id</param>
        /// <returns>Error de exitosos o fallido segun sea</returns>
        public ErrorResponse CancelIntentionBuying(string userId, string token, int intentionId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene todas las intenciones de compra disponibles en el mercado. Para uso de la Auditoria.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <returns>Todas las intenciones de compras activas o no</returns>
        public IntentionBuyingResponse GetAllIntentionsBuying(string userId, string token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Crea una intencion de compra.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="offerId">offer id</param>
        /// <param name="requestId">request id</param>
        /// <param name="quantity">quantity, optional</param>
        /// <returns>Informacion d ela intencion de compra con id y demas.</returns>
        public IntentionBuyingResponse MakeDeal(string userId, string token, int offerId, int requestId, int quantity = 0)
        {
            throw new NotImplementedException();
        }
    }
}
