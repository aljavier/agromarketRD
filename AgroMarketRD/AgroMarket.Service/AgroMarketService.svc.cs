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
using AgroMarketRD.Service.Requests;

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

                    if (_login == null)
                    {

                        response = new LoginResponse
                        {
                            Error = new ErrorResponse
                            {
                                Code = Errores.AG001.ToString(),
                                Description = "Falló autentificación."
                            },
                            Token = string.Empty,
                            UserId = 0
                        };

                        return response;
                    }

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
        /// <returns>Id de la oferta creada y error de exitoso o fallid</returns>
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

                    var _offer = new Oferta
                    {
                        Activo = true,
                        Cantidad = cantidad,
                        FechaCreacion = DateTime.Now,
                        PrecioUnidad = precioUnidad,
                        ProductoId = db.Productos.First(x => x.Codigo == codigoProducto).Id, // TODO: Validar que código producto es válido
                        TipoUnidadId = tipoUnidad,
                        UsuarioId = db.Usuarios.First(x => x.NombreUsuario == userName).Id
                    };


                    db.Ofertas.Add(_offer);
                    db.SaveChanges();

                    response.Id = _offer.Id;
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
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <param name="offerId">offer id</param>
        /// <returns>Oferta solicitada si existe</returns>
        public OfferResponse GetOffer(string userName, string token, int offerId)
        {
            OfferResponse response = new OfferResponse();

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
                    var _offer = db.Ofertas.FirstOrDefault(x => x.Id == offerId && x.Activo); // Solo activas

                    if (_offer != null)
                    {
                        response.Offers.Add(new Offer
                        {
                            Id = _offer.Id,
                            PriceUnit = _offer.PrecioUnidad,
                            ProductCode = _offer.Producto.Codigo,
                            ProductName = _offer.Producto.Descripcion,
                            Quantity = _offer.Cantidad,
                            ProductUnitId = _offer.TipoUnidadId,
                            ProductUnit = _offer.TipoUnidad.Descripcion,
                            ProductorId = _offer.ProductoId,
                            Productor = _offer.Usuario.Nombre
                        });
                    }
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
        /// Obtiene todas las ofertas de un productor
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="token">token</param>
        /// <returns>Todas las ofertas del productor</returns>
        public OfferResponse GetAllOffers(string userName, string token)
        {
            OfferResponse response = new OfferResponse();

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
                    response.Offers = db.Ofertas.Where(x => x.Activo).Select(x => new Offer
                    {
                        Id = x.Id,
                        PriceUnit = x.PrecioUnidad,
                        ProductCode = x.Producto.Codigo,
                        ProductName = x.Producto.Descripcion,
                        Quantity = x.Cantidad,
                        ProductUnitId = x.TipoUnidadId,
                        ProductUnit = x.TipoUnidad.Descripcion,
                        ProductorId = x.ProductoId,
                        Productor = x.Usuario.Nombre
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
        /// Crea una intencion de compra desde productos.
        /// </summary>
        /// <returns>Id del request creado</returns>
        public GeneralResponse CreateIntentionToBuy(IntentionToBuyFromProducts request)
        {
            GeneralResponse response = new GeneralResponse();

            try
            {
                AccessHelper.Add(request.userName, OperationContext.Current);

                if (!AccessHelper.IsSessionValid(request.userName, request.token))
                {
                    response.Error.Code = Errores.AG003.ToString();
                    response.Error.Description = "La sesión no es válida."; // TODO: Tomar error desc de la db

                    return response;
                }

                using (var db = new AgroMarketDbContext())
                {
                    var _user = db.Usuarios.First(x => x.NombreUsuario == request.userName);

                    if (_user.TipoUsuarioId != 1) // Solo usuarios Compradores. TODO: Quitar magic number
                    {
                        response.Error.Code = Errores.AG004.ToString();
                        response.Error.Description = "No es un comprador!";

                        return response;
                    }

                    if (request.ProductList.Count == 0)
                    {
                        response.Error.Code = Errores.AG004.ToString();
                        response.Error.Description = "Tiene que enviar productos, socio!"; // TODO: Tomar de db, etc.

                        return response;
                    }

                    var _intention = new IntencionCompra
                    {
                        Activo = true,
                        FechaCreacion = DateTime.Now,
                        FechaExpiracion = DateTime.Now.AddDays(30), // TODO: Quitar magic number
                        UsuarioId = _user.Id
                    };

                    db.IntencionCompra.Add(_intention);

                    db.SaveChanges();

                    response.Id = _intention.Id;

                    #region "agregar productos a la intencion de compra"

                    foreach (var prod in request.ProductList)
                    {
                        db.ProductoIntencionCompra.Add(new ProductoIntencionCompra
                        {
                            cantidad = prod.Quantity,
                            IntencionCompraId = _intention.Id,
                            PrecioUnidad = prod.PriceUnit,
                            TipoUnidadId = prod.ProductUnit,
                            ProductoId = db.Productos.First(x => x.Codigo == prod.ProductCode).Id // TODO: Validar existencias de productos, manejar errores acorde
                        });
                    }
                    db.SaveChanges();
                    #endregion
                }
            }
            catch (Exception ex)
            {


                response.Error.Code = Errores.AG002.ToString();
                response.Error.Description = ex.Message;

                LogHelper.AddLog(ex.Message, ex.ToString(), ex.StackTrace.ToString(), request.userName);
            }

            return response;
        }

        /// <summary>
        /// Crea una intencion de compra a partir de una o mas ofertas del mercado
        /// </summary>
        /// <param name="request">request</param>
        /// <returns>Id d ela intencion creada</returns>
        public GeneralResponse CreateIntentionToBuyFromOffers(IntentionToBuyFromOffers request)
        {
            GeneralResponse response = new GeneralResponse();

            try
            {
                AccessHelper.Add(request.userName, OperationContext.Current);

                if (!AccessHelper.IsSessionValid(request.userName, request.token))
                {
                    response.Error.Code = Errores.AG003.ToString();
                    response.Error.Description = "La sesión no es válida."; // TODO: Tomar error desc de la db

                    return response;
                }

                using (var db = new AgroMarketDbContext())
                {
                    var _user = db.Usuarios.First(x => x.NombreUsuario == request.userName);

                    if (_user.TipoUsuarioId != 1) // Solo usuarios Compradores. TODO: Quitar magic number
                    {
                        response.Error.Code = Errores.AG004.ToString();
                        response.Error.Description = "No es un comprador!";

                        return response;
                    }

                    if (request.OffersId.Count == 0)
                    {
                        response.Error.Code = Errores.AG004.ToString();
                        response.Error.Description = "Tiene que enviar las ofertas, socio!"; // TODO: Tomar de db, etc.

                        return response;
                    }

                    var _intention = new IntencionCompra
                    {
                        Activo = true,
                        FechaCreacion = DateTime.Now,
                        FechaExpiracion = DateTime.Now.AddDays(30), // TODO: Quitar magic number
                        UsuarioId = _user.Id
                    };

                    db.IntencionCompra.Add(_intention);

                    db.SaveChanges();

                    response.Id = _intention.Id;

                    #region "agregar productos a la intencion de compra"

                    foreach (var offerId in request.OffersId)
                    {
                        var _offer = db.Ofertas.FirstOrDefault(x => x.Id == offerId);

                        if (_offer == null)
                        {
                            // TODO: Manejar casos de ofertas no existentes enviadas
                            continue;
                        }

                        db.ProductoIntencionCompra.Add(new ProductoIntencionCompra
                        {
                            cantidad = _offer.Cantidad,
                            IntencionCompraId = _intention.Id,
                            PrecioUnidad = _offer.PrecioUnidad,
                            TipoUnidadId = _offer.TipoUnidadId,
                            ProductoId = _offer.ProductoId // TODO: Validar existencias de productos, manejar errores acorde
                        });
                    }
                    db.SaveChanges();
                    #endregion
                }

            }
            catch (Exception ex)
            {


                response.Error.Code = Errores.AG002.ToString();
                response.Error.Description = ex.Message;

                LogHelper.AddLog(ex.Message, ex.ToString(), ex.StackTrace.ToString(), request.userName);
            }

            return response;
        }

        /// <summary>
        /// Borra una solicitud creada, solo puede hacerlo quien la creó.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="token">token</param>
        /// <param name="requestId">rquest id</param>
        /// <returns>Error de exitosos o fallido</returns>
        public ErrorResponse RemoveIntentionToBuy(string userName, string token, int intentionId)
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

                    var _intention = db.IntencionCompra.FirstOrDefault(x => x.Id == intentionId && x.UsuarioId == _user.Id);

                    if (_intention != null)
                    {
                        _intention.Activo = false;
                        db.Entry(_intention).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }// TODO: Devolver error acorde

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
        /// Obtiene una solicitud de un producto por el id.
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <param name="intentionId">request id</param>
        /// <returns>Solicitud del id si existe</returns>
        public IntentionBuyingResponse GetIntentionToBuy(string userName, string token, int intentionId)
        {
            IntentionBuyingResponse response = new IntentionBuyingResponse();

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
                    var _intention = db.IntencionCompra.FirstOrDefault(x => x.Id == intentionId && x.Activo);

                    if (_intention != null)
                    {
                        var _new = new IntentionBuying
                        {
                            Id = _intention.Id,
                            BuyerId = _intention.UsuarioId,
                            Buyer = db.Usuarios.First(x => x.NombreUsuario == userName).Nombre,
                            DateCreation = _intention.FechaCreacion,
                            IntentionsToSellId = db.IntencionVenta.Where(x => x.IntencionCompraId == _intention.Id && x.Activo).Select(x => x.Id).ToList(),
                            ExpirationDate = _intention.FechaExpiracion,
                        };

                        foreach (var prod in db.ProductoIntencionCompra.Where(x => x.IntencionCompraId == _intention.Id))
                        {
                            var _producto = db.Productos.FirstOrDefault(x => x.Id == prod.ProductoId);
                            var _tipoUnidad = db.TipoUnidad.FirstOrDefault(x => x.Id == prod.TipoUnidadId);

                            if ((_producto == null) || (_tipoUnidad == null))
                            {
                                continue; // TODO: Manejar errores
                            }

                            _new.ProductList.Add(new ProductIntention
                            {
                                PriceUnit = prod.PrecioUnidad,
                                ProductCode = _producto.Codigo,
                                ProductName = _producto.Descripcion,
                                ProductUnit = _tipoUnidad.Descripcion,
                                ProductUnitId = prod.TipoUnidadId,
                                Quantity = prod.cantidad
                            });

                            response.Intentions.Add(_new);
                        }
                    } // TODO: Manejar error de respuesta de que no existe 
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
        /// Obtiene todas las solicitudes hechas en el mercado que aun esten activas
        /// </summary>
        /// <param name="userName">user id</param>
        /// <param name="token">token</param>
        /// <returns>Lista de solicitudes disponibles</returns>
        public IntentionBuyingResponse GetAllIntentionsToBuy(string userName, string token)
        {
            IntentionBuyingResponse response = new IntentionBuyingResponse();

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

                    foreach (var _intention in db.IntencionCompra.Where(x => x.Activo).ToList())
                    {
                        var _new = new IntentionBuying
                        {
                            Id = _intention.Id,
                            BuyerId = _intention.UsuarioId,
                            Buyer = db.Usuarios.First(x => x.NombreUsuario == userName).Nombre,
                            DateCreation = _intention.FechaCreacion,
                            IntentionsToSellId = db.IntencionVenta.Where(x => x.IntencionCompraId == _intention.Id && x.Activo).Select(x => x.Id).ToList(),
                            ExpirationDate = _intention.FechaExpiracion,
                        };

                        foreach (var prod in db.ProductoIntencionCompra.Where(x => x.IntencionCompraId == _intention.Id))
                        {
                            var _producto = db.Productos.FirstOrDefault(x => x.Id == prod.ProductoId);
                            var _tipoUnidad = db.TipoUnidad.FirstOrDefault(x => x.Id == prod.TipoUnidadId);

                            if ((_producto == null) || (_tipoUnidad == null))
                            {
                                continue; // TODO: Manejar errores
                            }

                            _new.ProductList.Add(new ProductIntention
                            {
                                PriceUnit = prod.PrecioUnidad,
                                ProductCode = _producto.Codigo,
                                ProductName = _producto.Descripcion,
                                ProductUnit = _tipoUnidad.Descripcion,
                                ProductUnitId = prod.TipoUnidadId,
                                Quantity = prod.cantidad
                            });

                            response.Intentions.Add(_new);
                        }
                    }
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
        /// Create una intencion de venta
        /// </summary>
        /// <param name="request">request ( ver las propiedades del objeto)</param>
        /// <returns>Id de la intencion creada</returns>
        public GeneralResponse CreateIntentionToSell(IntentionToSellRequest request)
        {
            GeneralResponse response = new GeneralResponse();

            try
            {
                AccessHelper.Add(request.userName, OperationContext.Current);

                if (!AccessHelper.IsSessionValid(request.userName, request.token))
                {
                    response.Error.Code = Errores.AG003.ToString();
                    response.Error.Description = "La sesión no es válida."; // TODO: Tomar error desc de la db

                    return response;
                }

                if (request.ProductList.Count == 0)
                {
                    response.Error.Code = Errores.AG003.ToString();
                    response.Error.Description = "Tiene que enviar los productos!."; // TODO: Tomar error desc de la db

                    return response;
                }

                using (var db = new AgroMarketDbContext())
                {
                    var _user = db.Usuarios.First(x => x.NombreUsuario == request.userName);

                    if (_user.TipoUsuarioId != 2) // TODO: Qitar magic number
                    {
                        response.Error.Code = Errores.AG003.ToString();
                        response.Error.Description = "Usted no es vendedor!"; // TODO: Sacar mensaje de db
                    }

                    var _intention = new IntencionVenta
                    {
                        Activo = true,
                        FechaCreacion = DateTime.Now,
                        FechaExpiracion = DateTime.Now.AddDays(30), // TODO: Manejar expiracion quitar magic number
                        IntencionCompraId = request.IntentionToBuydId,
                        UsuarioId = _user.Id
                    };

                    db.IntencionVenta.Add(_intention);
                    db.SaveChanges();

                    response.Id = _intention.Id;
                    // TODO: Validar que NO sea mayor la cantidad a la solicitada
                    foreach (var prod in request.ProductList)
                    {
                        var _producto = db.Productos.FirstOrDefault(x => x.Codigo == prod.ProductCode);

                        if (_producto == null)
                        {
                            continue; // TODO: Manejar en caso no existe producto enviado
                        }

                        db.ProductoIntencionVenta.Add(new ProductoIntencionVenta
                        {
                            Cantidad = prod.Quantity,
                            IntencionVentaId = _intention.Id,
                            PrecioUnidad = prod.PriceUnit,
                            ProductoId = _producto.Id,
                            TipoUnidadId = prod.ProductUnit
                        });
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {


                response.Error.Code = Errores.AG002.ToString();
                response.Error.Description = ex.Message;

                LogHelper.AddLog(ex.Message, ex.ToString(), ex.StackTrace.ToString(), request.userName);
            }

            return response;
        }

        /// <summary>
        /// Cancela una intencion de venta
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="token">Token</param>
        /// <param name="intentionId">Intention Id</param>
        /// <returns>Success or not</returns>
        public ErrorResponse RemoveIntentionToSell(string userName, string token, int intentionId)
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

                    if (_user.TipoUsuarioId != 2) // TODO: Quitar magic number
                    {
                        response.Code = Errores.AG003.ToString();
                        response.Description = "El usuario enviado no es un vendedor/productor!"; // TODO: Tomar error desc de la db

                        return response;
                    }

                    var _intention = db.IntencionVenta.FirstOrDefault(x => x.Id == intentionId);

                    if (_intention != null)
                    {
                        _intention.Activo = false;
                        db.Entry(_intention).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    } // TODO: Manejar error de no existencia
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
        /// Obtiene los detalles de una intencion.
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="token">token</param>
        /// <param name="intentionId">intention id</param>
        /// <returns>Intention del id si existe</returns>
        public IntentionsSellResponse GetIntentionToSell(string userName, string token, int intentionId)
        {
            IntentionsSellResponse response = new IntentionsSellResponse();

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
                    var _intention = db.IntencionVenta.FirstOrDefault(x => x.Id == intentionId);

                    if (_intention != null)
                    {
                        var _new = new IntentionSell
                        {
                            DateCreation = _intention.FechaCreacion,
                            ExpirationDate = _intention.FechaExpiracion,
                            Id = _intention.Id,
                            SellerId = _intention.Id,
                            Seller = db.Usuarios.First(x => x.NombreUsuario == userName).Nombre
                        };

                        foreach (var prod in db.ProductoIntencionVenta.Where(x => x.IntencionVentaId == _intention.Id).ToList())
                        {
                            var _producto = db.Productos.FirstOrDefault(x => x.Id == prod.ProductoId);
                            var _tipoUnidad = db.TipoUnidad.FirstOrDefault(x => x.Id == prod.TipoUnidadId);

                            if ((_producto == null) || (_tipoUnidad == null))
                            {
                                continue; // TODO: Manejar error de no existencia
                            }

                            _new.ProductList.Add(new ProductIntention
                            {
                                PriceUnit = prod.PrecioUnidad,
                                ProductCode = _producto.Codigo,
                                ProductName = _producto.Descripcion,
                                ProductUnitId = prod.TipoUnidadId,
                                ProductUnit = _tipoUnidad.Descripcion,
                                Quantity = prod.Cantidad
                            });
                        }
                        response.Intentions.Add(_new);
                    } // TODO: Manejar error de que no existe
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
        /// Obtiene todas las intenciones ventas activas.
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="token">Token</param>
        /// <returns>Intentions to sell</returns>
        public IntentionsSellResponse GetIntentionsToSell(string userName, string token)
        {
            IntentionsSellResponse response = new IntentionsSellResponse();

            try
            {
                AccessHelper.Add(userName, OperationContext.Current);

                if (!AccessHelper.IsSessionValid(userName, token))
                {
                    response.Error.Code = Errores.AG003.ToString();
                    response.Error.Description = "La sesión no es válida."; // TODO: Tomar error desc de la db

                    return response;
                }

                #region "obtener todas las intenciones activas y parsear a response"

                using (var db = new AgroMarketDbContext())
                {
                    foreach (var _intention in db.IntencionVenta.Where(x => x.Activo).ToList())
                    {
                        var _new = new IntentionSell
                        {
                            DateCreation = _intention.FechaCreacion,
                            ExpirationDate = _intention.FechaExpiracion,
                            Id = _intention.Id,
                            SellerId = _intention.Id,
                            Seller = db.Usuarios.First(x => x.NombreUsuario == userName).Nombre
                        };

                        foreach (var prod in db.ProductoIntencionVenta.Where(x => x.IntencionVentaId == _intention.Id).ToList())
                        {
                            var _producto = db.Productos.FirstOrDefault(x => x.Id == prod.ProductoId);
                            var _tipoUnidad = db.TipoUnidad.FirstOrDefault(x => x.Id == prod.TipoUnidadId);

                            if ((_producto == null) || (_tipoUnidad == null))
                            {
                                continue; // TODO: Manejar error de no existencia
                            }

                            _new.ProductList.Add(new ProductIntention
                            {
                                PriceUnit = prod.PrecioUnidad,
                                ProductCode = _producto.Codigo,
                                ProductName = _producto.Descripcion,
                                ProductUnitId = prod.TipoUnidadId,
                                ProductUnit = _tipoUnidad.Descripcion,
                                Quantity = prod.Cantidad
                            });
                        }
                        response.Intentions.Add(_new);
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
        /// Sella un trato de venta y compra. Tanto vendedor como comprador deben sellar
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="token">token</param>
        /// <param name="intentionSellId">itencion compra id</param>
        /// <param name="intentionBuyId">intecion venta id</param>
        /// <returns>Repuesta exitoso o no</returns>
        public ErrorResponse MakeDeal(string userName, string token, int intentionSellId, int intentionBuyId)
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

                    if ((_user.TipoUsuarioId != 2) && (_user.TipoUsuarioId != 1)) // TODO: Quitar magic numbers
                    {
                        response.Code = Errores.AG003.ToString();
                        response.Description = "Tipo de usuario no válida para esta acción."; // TODO: Poner mensage de la db

                        return response;
                    }

                    if (db.IntencionCompra.Any(x => x.UsuarioId == _user.Id)
                        && db.IntencionVenta.Any(x => x.UsuarioId == _user.Id))
                    {
                        response.Code = Errores.AG003.ToString();
                        response.Description = "Esta intención no es propieda del usuario que consulta!"; // TODO: Poner de db

                        return response;
                    }

                    #region "firmar transacción"

                    var _firma = db.Firmas.FirstOrDefault(x => x.IntencionCompraId == intentionBuyId
                            && x.IntencionVentaId == intentionSellId);
                    bool isUpdate = (_firma != null);

                    if (_firma == null)
                    {
                        _firma = new Firma();
                        _firma.FechaCreacion = DateTime.Now;
                    } else {
                        _firma.FechaFinal = DateTime.Now;
                    }

                    if (_user.TipoUsuarioId == 2) // Productor - TODO: Quitar magic number
                    {
                        _firma.Vendedor = _user.Id;
                    }
                    else
                    {
                        _firma.Comprador = _user.Id;
                    }

                    _firma.IntencionCompraId = intentionBuyId;
                    _firma.IntencionVentaId = intentionSellId;

                    if (isUpdate)
                    {
                        db.Entry(_firma).State = System.Data.Entity.EntityState.Modified;
                    }
                    else {
                        db.Firmas.Add(_firma);
                    }

                    db.SaveChanges();

                    if (db.Firmas.Any(x => x.Id == _firma.Id && x.Vendedor.HasValue && x.Comprador.HasValue)) // Termino
                    {
                        var _intention = db.IntencionCompra.First(x => x.Id == intentionSellId);

                        _intention.Activo = false;
                        db.Entry(_intention).State = System.Data.Entity.EntityState.Modified;

                        db.Ventas.Add(new Venta
                        {
                            FirmaId = _firma.Id,
                            IntencionCompraId = intentionBuyId,
                            IntencionVentaId = intentionSellId,
                        });
                        db.SaveChanges();

                        response.Description = "El pago ha sido terminado. Ambas partes han firmado!";
                    }
                    else
                    {
                        response.Description = "Todavia la otra parte tiene que firmar para concretarse la venta/compra";
                    }
                    #endregion
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
        /// Obtiene todas las ventas culminadas en el mercado. /!\ PARA USO DE AUDITORIA MAYORMENTE! /!\
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="token">token</param>
        /// <returns>Todas las ventas</returns>
        public SellsResponse GetAllSells(string userName, string token)
        {
            SellsResponse response = new SellsResponse();

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
                   
                    foreach (var sell in db.Ventas.ToList())
                    {
                        var _sell = new Sell
                        {
                            IntentionBuyId = sell.IntencionCompraId,
                            IntentionSellId = sell.IntencionVentaId,
                            CreationDate = sell.Firma.FechaFinal,
                            BuyerId = sell.IntencionCompra.UsuarioId,
                            Buyer = db.Usuarios.First(x => x.Id == sell.IntencionCompra.UsuarioId).Nombre,
                            SellerId = sell.IntencionVenta.UsuarioId,
                            Seller = db.Usuarios.First(x => x.Id == sell.IntencionVenta.UsuarioId).Nombre
                        };

                        // CHECK: Se aceptan los productos propuestos por el vendedor en un acuerdo mutuo fuera de este mundo de 01!
                        foreach (var prod in db.ProductoIntencionVenta
                            .Where(x => x.IntencionVentaId == sell.IntencionVentaId).ToList())
                        {
                            var _producto = db.Productos.FirstOrDefault(x => x.Id == prod.ProductoId);
                            var _tipoUnidad = db.TipoUnidad.FirstOrDefault(x => x.Id == prod.TipoUnidadId);

                            if (_producto == null || _tipoUnidad == null) continue; // TODO: Manejar error, loguear, etc.

                            _sell.ProductList.Add(new ProductIntention
                            {
                                PriceUnit = prod.PrecioUnidad,
                                ProductCode = _producto.Codigo,
                                ProductName = _producto.Descripcion,
                                ProductUnitId = prod.TipoUnidadId,
                                ProductUnit = _tipoUnidad.Descripcion,
                                Quantity = prod.Cantidad
                            });

                            response.SellList.Add(_sell);
                        }
                    }
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
    }
}
