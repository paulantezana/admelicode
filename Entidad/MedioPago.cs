using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class MedioPago
    {
        public int idMedioPago { get; set; }
        public string nombre { get; set; }
        public int estado { get; set; }
    }

    public class CambioMoneda
    {
        public int idMonedaCambio { get; set; }
        public int idMonedaActual { get; set; }
    }
    public class ValorcambioMoneda
    {
        public string cambioMonedaActual { get; set; }
        public string cambioMonedaCambio { get; set; }
    }


}
