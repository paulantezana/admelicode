using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Configuracion
{
    public class PuntoDeVenta
    {
        public int idPuntoVenta { get; set; }
        public string nombre { get; set; }
        public bool ventaWeb { get; set; }
        public int estado { get; set; }
        public int idSucursal { get; set; }
        public string sucursal { get; set; }

        public int idAsignarPuntoVenta { get; set; }

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