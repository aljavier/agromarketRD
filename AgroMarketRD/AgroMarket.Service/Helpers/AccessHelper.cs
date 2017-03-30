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
    }
}