using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Egreso
    {
        public int idEgreso { get; set; }
        public string numeroOperacion { get; set; }
        public Fecha fecha { get; set; }
        public Fecha fechaPago { get; set; }
        public string monto { get; set; }
        public string motivo { get; set; }
        public string observacion { get; set; }
        public string moneda { get; set; }
        public int estado { get; set; }
        public int idMoneda { get; set; }
        public int idCaja { get; set; }
        public int idCajaSesion { get; set; }
        public int idDetallePago { get; set; }
        public int idMedioPago { get; set; }
        public string medioPago { get; set; }
        public string personal { get; set; }
        public string esDeCompra { get; set; }

        private string estadoString;
        public string EstadoString
        {
            get
            {
                if(estado == 1) { return "Activo"; }
                else { return "Anulado"; }
            }
            set
            {
                estadoString = value;
            }
        }
    }
}
