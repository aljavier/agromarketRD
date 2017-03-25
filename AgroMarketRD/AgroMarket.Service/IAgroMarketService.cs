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
        OfferResponse GetOfferProductor(string userId, string token, int offerId);

        /// <summary>
        /// Get all offers from a productor
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <returns>Offers from productor</returns>
        [OperationContract]
        OfferResponse GetOffersProductor(string userId, string token);

        /// <summary>
        /// Get all offers
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <returns>All offers in the market</returns>
        [OperationContract]
        OfferResponse GetAllOffers(string userId, string token);
    }
}
