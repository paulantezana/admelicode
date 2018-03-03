using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class PagoCompra
    {
        public int idCaja { get; set; }
        public int idCajaSesion { get; set; }
        public int idMedioPago { get; set; }
        public int idMoneda { get; set; }
        public int idPago { get; set; }
        public string moneda { get; set; }
        public double pagarCompra { get; set; }
    }
}
