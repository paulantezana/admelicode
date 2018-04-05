using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Configuracion
{
    public class Moneda
    {
        public int idMoneda { get; set; }
        public int idMonedaPorDefecto { get; set; }

        public string moneda { get; set; }
        public string simbolo { get; set; }
        public bool porDefecto { get; set; }
        public int estado { get; set; }
        public double tipoCambio { get; set; }
        public Fecha fechaCreacion { get; set; }
        public dynamic idPersonal { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string tieneRegistros { get; set; }

        public string fechaPago { get; set; }
        public int idCaja { get; set; }
        public int idCajaSesion { get; set; }
        public int idMedioPago { get; set; }
        public string medioPago { get; set; }
        public double monto { get; set; }
        public string motivo { get; set; }
        public string numeroOperacion { get; set; }
        public string observacion { get; set; }

        //Para algunos servicios que requieran un arespuesta del total
        public double total { get; set; }
    }
}
