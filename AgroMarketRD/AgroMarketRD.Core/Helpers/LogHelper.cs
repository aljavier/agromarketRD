using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroMarketRD.Core.Helpers
{
    public class LogHelper
    {
        // Parametros para logs del Event Viewer
        private const string EVENT_SOURCE = "AgroMarket";
        private const int EVENT_ID_INFORMATION = 1;
        private const int EVENT_ID_ERROR = 2;
        private const int EVENT_ID_WARNING = 3;
        private const int EVENT_ID_SUCCESS = 4;
        //

        /// <summary>
        /// Guarda un log con los datos suministrados en la base de datos.
        /// 
        /// </summary>
        /// <param name="mensaje">Mensaje</param>
        /// <param name="excepcion">Excepcionr</param>
        /// <param name="stacktrace">StackTrace</param>
        /// <param name="usuarioId">Usuario ID</param>
        public static void AddLog(string mensaje, string excepcion, string stacktrace, int? usuarioId)
        {
            try
            {
                using (AgroMarketDbContext db = new AgroMarketDbContext())
                {
                    db.ErrorLog.Add(new Entities.ErrorLog
                    {
                        Mensaje = mensaje,
                        Excepcion = excepcion,
                        StackTrace = stacktrace,
                        UsuarioId = usuarioId,
                        FechaCreacion = DateTime.Now
                    });

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex = ex.GetBaseException();

                try
                {
                    #region "Enviar mensaje de log y la exception al EventViewer"

                    StringBuilder _mensaje = new StringBuilder();

                    _mensaje.AppendLine($"Mensaje: {mensaje}")
                        .AppendFormat($"Excepcion: {excepcion}")
                        .AppendFormat($"StackTrace: {stacktrace}")
                        .Append($"Usuario ID: {usuarioId}")
                        .AppendLine() // Linea en blanco intencional
                        .AppendLine() // Linea en blanco intencional
                        .AppendFormat($"StackTrace: {stacktrace}");

                    LogToEventViewer(_mensaje.ToString(), EventLogEntryType.Error);

                    #endregion
                }
                catch
                {
                    // Do nothing
                }
            }
        }

        /// <summary>
        /// Guarda el log de acceso de un usuario.
        /// </summary>
        /// <param name="usuarioId">Usuario ID</param>
        /// <param name="solicitud">Solicitud ID</param>
        /// <param name="endpoint">Endpoint</param>
        public void AddAccesoLog(int usuarioId, string solicitud, string endpoint)
        {
            try
            {
                using (AgroMarketDbContext db = new AgroMarketDbContext())
                {
                    db.AccesoLog.Add(
                        new Entities.AccesoLog {
                            Solicitud = solicitud,
                            Endpoint = endpoint,
                            FechaCreacion = DateTime.Now,
                            UsuarioId = usuarioId
                        });
                }
            }
            catch (Exception ex)
            {
                AddLog(ex.Message, ex.ToString(), ex.StackTrace.ToString(), usuarioId);
            }
        }

        public static void AddLogPlainText(string ruta, string nombreArchivo, List<string> mensajesList, string delimitador = "")
        {
            using (StreamWriter sw = new StreamWriter($"{ruta}{nombreArchivo}.txt", true))
            {
                foreach (var mensaje in mensajesList)
                {
                    sw.WriteLine($"{mensaje}{delimitador}");
                }
            }
        }

        /// <summary>
        /// Loguea un mensaje al EventViewer de Windows.
        /// 
        /// </summary>
        /// <param name="message">Mensaje</param>
        /// <param name="logType">Tipo de Log</param>
        /// <see cref="http://stackoverflow.com/a/27640623/5852366"/>
        /// <example>
        /// /!\ COMO AGREGAR EVENT SOURCES DESDE EL CMD /!\
        /// 
        /// eventcreate /ID 1 /L APPLICATION /T INFORMATION  /SO MY_EVENT_SOURCE /D "My first log"
        /// 
        /// /ID es el que se debe poner aqui a la variable correspondiente
        /// de nombre EVENT_ID_[TIPO_ERROR]
        /// 
        /// /T es el tipo de error (va relacionado con  EVENT_ID_[TIPO_ERROR]): INFORMATION,
        /// WARNING, ERROR, SUCCESS...
        /// 
        /// /SO aqui va el nombre del EVENT SOURCE, eg. AgroMarket
        /// 
        /// /D primer mensaje de test para el log.
        /// 
        /// Ejemplo 1:
        /// eventcreate /ID 1 /L APPLICATION /T INFORMATION  /SO AgroMarket /D "First log..test!"
        /// 
        /// Ejemplo 2 (COMPLETO, full setup):
        /// eventcreate /ID 1 /L APPLICATION /T INFORMATION  /SO AgroMarket /D "First log..test!"
        /// eventcreate /ID 2 /L APPLICATION /T ERROR  /SO AgroMarket /D "First log ERROR..test!"
        /// eventcreate /ID 3 /L APPLICATION /T WARNING  /SO AgroMarket /D "First log WARNING..test!"
        /// eventcreate /ID 4 /L APPLICATION /T SUCCESS  /SO AgroMarket /D "First log SUCCESS..test!"
        /// 
        /// Nota: Entonces aqui restaria poner el valor "AgroMarket" a la constante EVENT_SOURCE
        /// y los valores de /ID a las constantes EVENT_ID_[TIPO_ERROR] (EVEN_ID_ERROR, etc.).
        /// </example>
        public static void LogToEventViewer(string message, EventLogEntryType logType)
        {
            using (EventLog eventLog = new EventLog())
            {
                bool _exists = EventLog.SourceExists(EVENT_SOURCE);

                // Si el Event Source no existe logueamos el error en el source de 
                // Windows "Application".
                eventLog.Source = _exists ? EVENT_SOURCE : "Application";

                if (_exists)
                {
                    int _event_id = 0;

                    switch (logType)
                    {
                        case EventLogEntryType.SuccessAudit:
                            _event_id = EVENT_ID_SUCCESS;
                            break;
                        case EventLogEntryType.Information:
                            _event_id = EVENT_ID_INFORMATION;
                            break;
                        case EventLogEntryType.Warning:
                            _event_id = EVENT_ID_WARNING;
                            break;
                        default:
                            _event_id = EVENT_ID_ERROR;
                            break;
                    }

                    eventLog.WriteEntry(message, logType, _event_id);
                }
                else
                {
                    // Aqui no podemos pasar un event id, saldrá un error 
                    // al respecto en el event viewer irremediablemente
                    eventLog.WriteEntry(message, logType);
                }
            }
        }
    }
}
