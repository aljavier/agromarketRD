using AgroMarketRD.Service.Contracts;
using AgroMarketRD.Service.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AgroMarket.Service
{
    /// <summary>
    /// Interfaz WCF Service AgroMarketRD
    /// </summary>
    [ServiceContract]
    public interface IAgroMarketService
    {
        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        /// <returns>Login response</returns>
        [OperationContract]
        LoginResponse SignIn(string userName, string password);

        /// <summary>
        /// Log ogg
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="token">Token</param>
        /// <returns>Log off success or not</returns>
        [OperationContract]
        ErrorResponse LogOff(string userName, string token);

        /// <summary>
        /// Get products
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <returns>Products response</returns>
        [OperationContract]
        ProductResponse GetProducts(string userName, string token);

        /// <summary>
        /// Get unit types
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <returns>Unit types response</returns>
        [OperationContract]
        ProductUnitResponse GetUnitTypes(string userName, string token);

        /// <summary>
        /// Create offer
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <param name="cantidad">Cantidad</param>
        /// <param name="codigoProducto">codigo producto</param>
        /// <param name="precioUnidad">precio unidad</param>
        /// <param name="tipoUnidad">tipo unidad</param>
        /// <returns>Create offer response</returns>
        [OperationContract]
        GeneralResponse CreateOffer(string userName, string token, int cantidad, int tipoUnidad, decimal precioUnidad, string codigoProducto);

        /// <summary>
        /// Remove an offer
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <param name="offerId">offer id</param>
        /// <returns>Remove an offer</returns>
        [OperationContract]
        ErrorResponse RemoveOffer(string userName, string token, int offerId);

        /// <summary>
        /// Get offer productor
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <param name="offerId">offer id</param>
        /// <returns>An offer of the productor</returns>
        [OperationContract]
        OfferResponse GetOffer(string userName, string token, int offerId);

        /// <summary>
        /// Get all offers on the market
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <returns>Offers from productor</returns>
        [OperationContract]
        OfferResponse GetAllOffers(string userName, string token);

        /// <summary>
        /// Create a request of a product
        /// </summary>
        /// <param name="request">request</param>
        /// <returns>Create request response</returns>
        [OperationContract]
        GeneralResponse CreateIntentionToBuy(IntentionToBuyFromProducts request);

        /// <summary>
        /// Create a request of a product
        /// </summary>
        /// <param name="request">request</param>
        /// <returns>Create request response</returns>
        [OperationContract]
        GeneralResponse CreateIntentionToBuyFromOffers(IntentionToBuyFromOffers request);

        /// <summary>
        /// Remove a request
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <param name="intentionId">request id</param>
        /// <returns>Remove request response</returns>
        [OperationContract]
        ErrorResponse RemoveIntentionToBuy(string userName, string token, int intentionId);

        /// <summary>
        /// Get a request
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <param name="intentionId">request id</param>
        /// <returns>Request response</returns>
        [OperationContract]
        IntentionBuyingResponse GetIntentionToBuy(string userName, string token, int intentionId);

        /// <summary>
        /// Get all requests
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <returns>All requests in the market</returns>
        [OperationContract]
        IntentionBuyingResponse GetAllIntentionsToBuy(string userName, string token);

        /// <summary>
        /// Create intention to sell
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Response with id of request created</returns>
        [OperationContract]
        GeneralResponse CreateIntentionToSell(IntentionToSellRequest request);

        /// <summary>
        /// Cancel an intention to sell
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="token">Token</param>
        /// <param name="intentionId">Intention Id</param>
        /// <returns>Success or not</returns>
        [OperationContract]
        ErrorResponse RemoveIntentionToSell(string userName, string token, int intentionId);

        /// <summary>
        /// Get an intention to sell
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="token">Token</param>
        /// <param name="intentionId">intention id</param>
        /// <returns>Intention with the id sended</returns>
        [OperationContract]
        IntentionsSellResponse GetIntentionToSell(string userName, string token, int intentionId);

        /// <summary>
        /// Get intentions to sell
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="token">Token</param>
        /// <returns>Intentions to sell</returns>
        [OperationContract]
        IntentionsSellResponse GetIntentionsToSell(string userName, string token);

        /// <summary>
        /// Make a intention to buy/sell
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <param name="intentionSellId">offer id</param>
        /// <param name="intentionBuyId">request id</param>
        /// <returns>Intention buying response</returns>
        [OperationContract]
        ErrorResponse MakeDeal(string userName, string token, int intentionSellId, int intentionBuyId);

        /// <summary>
        /// Get all finalized sells.
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="token">token</param>
        /// <returns>All sells in the market</returns>
        [OperationContract]
        SellsResponse GetAllSells(string userName, string token);
    }
}
