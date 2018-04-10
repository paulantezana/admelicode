using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Pago
    {
        public int idPago { get; set; }
        public string valorTotal { get; set; }
        public string valorPagado { get; set; }
        public string saldo { get; set; }
        public string motivo { get; set; }
        public Fecha fechaCreacion { get; set; }
        public int estado { get; set; }
        public int estadoPago { get; set; }
        public int idMoneda { get; set; }

        //public int idPago { get; set; }
        //public string valorTotal { get; set; }
        //public string valorPagado { get; set; }
        //public string saldo { get; set; }
        //public string motivo { get; set; }
        //public Fecha fechaCreacion { get; set; }
        //public string numeroDocumento { get; set; }
        //public string simbolo { get; set; }
        //public string moneda { get; set; }

        //public int estado { get; set; }
        //public int estadoPago { get; set; }
        //public int idMoneda { get; set; }
    }
}
