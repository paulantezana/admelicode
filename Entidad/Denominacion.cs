using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Denominacion
    {
        public int idDenominacion { get; set; }
        public string tipoMoneda { get; set; }
        public string nombre { get; set; }
        public string valor { get; set; }
        public string imagen { get; set; }
        public int estado { get; set; }
        public int idMoneda { get; set; }
        public string moneda { get; set; }
        public string anular { get; set; }

        private string estadoString;
        public string EstadoString
        {
            get
            {
                if (estado == 1) { return "Activo"; }
                else { return "Anulado"; }
            }
            set
            {
                estadoString = value;
            }
        }
    }
}
