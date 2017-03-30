using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroMarketRD.Core.Enums
{
    public class Enumeradores
    {
        public enum Errores
        {
            // TODO: Los errores deberian tener nombres auto descriptivos, sacar el codigo de la db por el numero
            AG000 = 3, // Success
            AG001 = 1, // Auth failed
            AG002 = 2, // No Controlado
            AG003 = 4, // Session no valida
            AG004 = 5 // Controlado
        }
    }
}
