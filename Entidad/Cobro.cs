using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Cobro
    {
        public int idCobro { get; set; }
        public string montoPagar { get; set; }
        public Fecha fecha { get; set; }
        public int cantidadCuotas { get; set; }
        public decimal interes { get; set; }
        public Fecha fechaModificacion { get; set; }
        public int estadoCobro { get; set; }
        public int estado { get; set; }
        public int idMoneda { get; set; }
    }
    //Cobro para el Formulario Inicial
    public class CobroLista
    {
        public string montoPagar { get; set; }
        public string valorPagado { get; set; }
        public string saldo { get; set; }
        public string numeroDocumento { get; set; }
        public string simbolo { get; set; }
        public string moneda { get; set; }
    }
}
