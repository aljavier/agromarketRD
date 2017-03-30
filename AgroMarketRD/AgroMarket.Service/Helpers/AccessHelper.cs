using AgroMarketRD.Core;
using AgroMarketRD.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace AgroMarketRD.Service.Helpers
{
    /// <summary>
    /// Helper relacionado al acceso al servicio web y el logging de estos.
    /// </summary>
    public class AccessHelper
    {
        /// <summary>
        /// Envia a la db un acceso.
        /// </summary>
        /// <param name="userName">User ID</param>
        /// <param name="context">Context</param>
        public static void Add(string userName, OperationContext context)
        {
            try
            {
                LogHelper.AddAccesoLog(userName, context.IncomingMessageHeaders.ToString(),
                    context.EndpointDispatcher.EndpointAddress.ToString());
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message, ex.ToString(), ex.StackTrace.ToString(), userName);
            }
        }

        /// <summary>
        /// Valida la session de un usuario.
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="token">Token</param>
        /// <returns>True si es válida, false en caso contrario</returns>
        public static bool IsSessionValid(string userName, string token)
        {
            try
            {
                using (var db = new AgroMarketDbContext())
                {
                    var _user = db.Usuarios.FirstOrDefault(x => x.NombreUsuario == userName);

                    if (_user != null)
                    {
                        return db.Sesiones.Any(x => x.UsuarioId == _user.Id && x.Token == token && x.Activo); // TODO: Validar por fecha de expiracion tambien
                     }
                }
            }
            catch (Exception ex)
            {

                LogHelper.AddLog(ex.Message, ex.ToString(), ex.StackTrace.ToString(), userName);
            }

            return false;
        }
    }
}