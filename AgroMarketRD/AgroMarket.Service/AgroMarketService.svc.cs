using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AgroMarketRD.Service.Contracts;
using AgroMarketRD.Core.Entities;
using AgroMarketRD.Core.Helpers;
using AgroMarketRD.Core;
using static AgroMarketRD.Core.Enums.Enumeradores;
using AgroMarketRD.Service.Helpers;
using System.Configuration;

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
        /// <param name="userName">Nombre de usuario</param>
        /// <param name="password">Contraseña</param>
        /// <returns>Token de acceso y id del usuario si fue exitoso.</returns>
        public LoginResponse SignIn(string userName, string password)
        {
            LoginResponse response = new LoginResponse();

            try
            {
                #region "Sign in, logueo de acceso y limpieza de sesion"

                AccessHelper.Add(userName, OperationContext.Current);

                using (AgroMarketDbContext db = new AgroMarketDbContext())
                {
                    string _passwd = CryptoHelper.Encrypt(password);

                    var _login = db.Usuarios.FirstOrDefault(x => x.NombreUsuario == userName
                                                                && x.Contrasena == _passwd);

                    if (_login != null)
                    {
                        int _id = Errores.AG000.GetHashCode();
                        var _error = db.Errores.Where(x => x.Id == _id).First();

                        response = new LoginResponse
                        {
                            Error = new ErrorResponse
                            {
                                Code = _error.Codigo,
                                Description = _error.Descripcion
                            },
                            Token = string.Empty,
                            UserId = 0
                        };

                        var activeSessions = db.Sesiones.Where(x => x.UsuarioId == _login.Id && x.Activo).ToList();

                        if (activeSessions.Count > 0)
                        {
                            activeSessions.ForEach(x => x.Activo = false);
                        }

                        int _days = Convert.ToInt16(ConfigurationManager.AppSettings["DaysToExpireSession"]);
                        string _token = Guid.NewGuid().ToString();

                        db.Sesiones.Add(
                            new Sesion
                            {
                                UsuarioId = _login.Id,
                                Token = _token,
                                FechaExpiracion = DateTime.Now.AddDays(_days),
                                Activo = true
                            });
                        db.SaveChanges();

                        response.Token = _token;
                        response.UserId = _login.Id;
                        response.UserName = _login.NombreUsuario;
                        response.Error.Code = Errores.AG000.ToString();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                response.Error.Code = Errores.AG002.ToString();
                response.Error.Description = ex.Message;

                LogHelper.AddLog(ex.Message, ex.ToString(), ex.StackTrace.ToString(), userName);
            }

            return response;
        }

        /// <summary>
        /// Cierra la sesón de un usuario.
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="token">Token</param>
        /// <returns>Log off success or not</returns>
        public ErrorResponse LogOff(string userName, string token)
        {
            ErrorResponse response = new ErrorResponse();

            try
            {
                AccessHelper.Add(userName, OperationContext.Current);

                using (AgroMarketDbContext db = new AgroMarketDbContext())
                {
                    var user = db.Usuarios.FirstOrDefault(x => x.NombreUsuario == userName);

                    if (user != null)
                    {
                        if (db.Sesiones.Any(x => x.UsuarioId == user.Id && x.Activo)) // Lo hacemos asi porque el Any es muy eficiente
                        {
                            var _sessions = db.Sesiones.Where(x => x.Id == x.Id && x.Activo).ToList();
                            _sessions.ForEach(x => x.Activo = false);
                            db.SaveChanges();
                        }
                    } // TODO: Mensaje personalizado de usuario no valido (?)
                }
            }
            catch (Exception ex)
            {
                response.Code = Errores.AG002.ToString();
                response.Description = ex.Message;

                LogHelper.AddLog(ex.Message, ex.ToString(), ex.StackTrace.ToString(), userName);
            }

            return response;
        }

        /// <summary>
        /// Catalogo de los productos que se pueden vender y comprar en la bolsa.
        /// </summary>
        /// <param name="userName">Usuario id</param>
        /// <param name="token">Token de acceso</param>
        /// <returns>Lista de productos del mercado</returns>
        public ProductResponse GetProducts(string userName, string token)
        {
            ProductResponse response = new ProductResponse();

            try
            {
                AccessHelper.Add(userName, OperationContext.Current);

                if (!AccessHelper.IsSessionValid(userName, token))
                {
                    response.Error.Code = Errores.AG003.ToString();
                    response.Error.Description = "La sesión no es válida."; // TODO: Tomar error desc de la db

                    return response;
                }

                using (var db = new AgroMarketDbContext())
                {
                    response.Products = db.Productos.Select(x => new Product
                    {
                        Code = x.Codigo,
                        Description = x.Descripcion
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                response.Error.Code = Errores.AG002.ToString();
                response.Error.Description = ex.Message;

                LogHelper.AddLog(ex.Message, ex.ToString(), ex.StackTrace.ToString(), userName);
            }
            return response;
        }

        /// <summary>
        /// Tipo de unidades de los productos. e.g. libras, sacos, etc.
        /// </summary>
        /// <param name="userName">Usuario id</param>
        /// <param name="token">Token de acceso</param>
        /// <returns>Los diferentes tipo de unidades de los productos del catalogo</returns>
        public ProductUnitResponse GetUnitTypes(string userName, string token)
        {
            ProductUnitResponse response = new ProductUnitResponse();

            try
            {
                AccessHelper.Add(userName, OperationContext.Current);

                if (!AccessHelper.IsSessionValid(userName, token))
                {
                    response.Error.Code = Errores.AG003.ToString();
                    response.Error.Description = "La sesión no es válida."; // TODO: Tomar error desc de la db

                    return response;
                }

                using (var db = new AgroMarketDbContext())
                {
                    response.UnitTypes = db.TipoUnidad.Select(x => new ProductUnit
                    {
                        Id = x.Id,
                        Description = x.Descripcion
                    }).ToList();
                }
            }
            catch (Exception ex)
            {

                response.Error.Code = Errores.AG002.ToString();
                response.Error.Description = ex.Message;

                LogHelper.AddLog(ex.Message, ex.ToString(), ex.StackTrace.ToString(), userName);
            }

            return response;
        }

        /// <summary>
        /// Crea una oferta, solo util para productores.
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <param name="cantidad">Cantidad</param>
        /// <param name="codigoProducto">codigo producto</param>
        /// <param name="precioUnidad">precio unidad</param>
        /// <param name="tipoUnidad">tipo unidad</param>
        /// <returns>Id de la oferta creada y error de exitoso o fallido</returns>
        public GeneralResponse CreateOffer(string userName, string token, int cantidad, int tipoUnidad, 
            decimal precioUnidad, string codigoProducto)
        {
            GeneralResponse response = new GeneralResponse();

            try
            {
                AccessHelper.Add(userName, OperationContext.Current);

                if (!AccessHelper.IsSessionValid(userName, token))
                {
                    response.Error.Code = Errores.AG003.ToString();
                    response.Error.Description = "La sesión no es válida."; // TODO: Tomar error desc de la db

                    return response;
                }

                using (var db = new AgroMarketDbContext())
                {
                    if (db.Usuarios.First(x => x.NombreUsuario == userName).TipoUsuarioId != 2) // Solo usuarios Productor. TODO: Quitar magic number
                    {
                        response.Error.Code = Errores.AG004.ToString();
                        response.Error.Description = "No es un productor!";

                        return response;
                    }

                    db.Ofertas.Add(new Oferta {
                        Activo = true,
                        Cantidad = cantidad,
                        FechaCreacion = DateTime.Now,
                        PrecioUnidad = precioUnidad,
                        ProductoId = db.Productos.First(x => x.Codigo == codigoProducto).Id, // TODO: Validar que código producto es válido
                        TipoUnidadId = tipoUnidad,
                        UsuarioId = db.Usuarios.First(x => x.NombreUsuario == userName).Id
                    });
                    response.Id = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                response.Error.Code = Errores.AG002.ToString();
                response.Error.Description = ex.Message;

                LogHelper.AddLog(ex.Message, ex.ToString(), ex.StackTrace.ToString(), userName);
            }

            return response;
        }

        /// <summary>
        /// Borra una oferta, solo puede hacerse si es el propietario de la oferta.
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <param name="offerId">oferta id</param>
        /// <returns></returns>
        public ErrorResponse RemoveOffer(string userName, string token, int offerId)
        {
            ErrorResponse response = new ErrorResponse();

            try
            {
                AccessHelper.Add(userName, OperationContext.Current);

                if (!AccessHelper.IsSessionValid(userName, token))
                {
                    response.Code = Errores.AG003.ToString();
                    response.Description = "La sesión no es válida."; // TODO: Tomar error desc de la db

                    return response;
                }

                using (var db = new AgroMarketDbContext())
                {
                    var _user = db.Usuarios.First(x => x.NombreUsuario == userName);

                    if (_user.TipoUsuarioId != 2) // Solo usuarios Productor. TODO: Quitar magic number
                    {
                        response.Code = Errores.AG004.ToString();
                        response.Description = "No es un productor!";

                        return response;
                    }

                    var _offer = db.Ofertas.FirstOrDefault(x => x.Id == offerId 
                                    && x.UsuarioId == _user.Id);

                    if (_offer != null)
                    {
                        _offer.Activo = false;
                        db.Entry(_offer).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    } // TODO: Devolver mensaje descriptivo de que no es valida la oferta
                }
            }
            catch (Exception ex)
            {

                response.Code = Errores.AG002.ToString();
                response.Description = ex.Message;

                LogHelper.AddLog(ex.Message, ex.ToString(), ex.StackTrace.ToString(), userName);
            }

            return response;
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
