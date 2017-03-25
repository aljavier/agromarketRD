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
    /// <summary>
    /// Interfaz WCF Service AgroMarketRD
    /// </summary>
    [ServiceContract]
    public interface IAgroMarketService
    {
        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="password">password</param>
        /// <returns>Login response</returns>
        [OperationContract]
        LoginResponse SignIn(string userId, string password);

        /// <summary>
        /// Get products
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <returns>Products response</returns>
        [OperationContract]
        ProductResponse GetProducts(string userId, string token);

        /// <summary>
        /// Get unit types
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <returns>Unit types response</returns>
        [OperationContract]
        ProductUnitResponse GetUnitTypes(string userId, string token);

        /// <summary>
        /// Create offer
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <returns>Create offer response</returns>
        [OperationContract]
        GeneralResponse CreateOffer(string userId, string token);

        /// <summary>
        /// Remove an offer
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="offerId">offer id</param>
        /// <returns>Remove an offer</returns>
        [OperationContract]
        ErrorResponse RemoveOffer(string userId, string token, int offerId);

        /// <summary>
        /// Get offer productor
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="offerId">offer id</param>
        /// <returns>An offer of the productor</returns>
        [OperationContract]
        OfferResponse GetOffer(string userId, string token, int offerId);

        /// <summary>
        /// Get all offers from a productor
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="productorId">productor id</param>
        /// <returns>Offers from productor</returns>
        [OperationContract]
        OfferResponse GetOffersProductor(string userId, string token, int productorId);

        /// <summary>
        /// Get all offers
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <returns>All offers in the market</returns>
        [OperationContract]
        OfferResponse GetAllOffers(string userId, string token);

        /// <summary>
        /// Create a request of a product
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="productId">product id</param>
        /// <param name="quantity">quantity</param>
        /// <returns>Create request response</returns>
        [OperationContract]
        GeneralResponse CreateRequest(string userId, string token, int productId, int quantity);

        /// <summary>
        /// Remove a request
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="requestId">request id</param>
        /// <returns>Remove request response</returns>
        [OperationContract]
        ErrorResponse RemoveRequest(string userId, string token, int requestId);

        /// <summary>
        /// Get a request
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="requestId">request id</param>
        /// <returns>Request response</returns>
        [OperationContract]
        RequestResponse GetRequest(string userId, string token, int requestId);

        /// <summary>
        /// Get requests from buyer
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="buyerId">buyer id</param>
        /// <returns>Requests from a buyer</returns>
        [OperationContract]
        RequestResponse GetRequestsBuyer(string userId, string token, int buyerId);

        /// <summary>
        /// Get all requests
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <returns>All requests in the market</returns>
        [OperationContract]
        RequestResponse GetAllRequests(string userId, string token);

        /// <summary>
        /// Get a intention to buy
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="intentionId">intention id</param>
        /// <returns>Intention buying response</returns>
        [OperationContract]
        IntentionBuyingResponse GetIntentionBuying(string userId, string token, int intentionId);

        /// <summary>
        /// Cancel intention buying
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="intentionId">intention id</param>
        /// <returns>Cancel intention response</returns>
        [OperationContract]
        ErrorResponse CancelIntentionBuying(string userId, string token, int intentionId);

        /// <summary>
        /// Get all intentions to buy
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <returns>Get intentions buying response</returns>
        [OperationContract]
        IntentionBuyingResponse GetAllIntentionsBuying(string userId, string token);

        /// <summary>
        /// Make a intention to buy/sell
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="offerId">offer id</param>
        /// <param name="requestId">request id</param>
        /// <param name="quantity">quantity</param>
        /// <returns>Intention buying response</returns>
        [OperationContract]
        IntentionBuyingResponse MakeDeal(string userId, string token, int offerId, int requestId, int quantity = 0);


    }
}
