using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class DetallePago
    {
        public int idDetallePago { get; set; }
        public string numeroOperacion { get; set; }
        public decimal importe { get; set; }
        public Fecha fecha { get; set; }
        public Fecha fechaPago { get; set; }
        public string concepto { get; set; }
        public int estado { get; set; }
        public int idMedioPago { get; set; }
        public int idPago { get; set; }
        public int idCajaSesion { get; set; }
    }
}
