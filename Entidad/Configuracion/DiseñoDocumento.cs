using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Configuracion
{
    public class DiseñoDocumento
    {

        public int idTipoDocumento { get; set; }
        public string nombre { get; set; }
        public string nombreLabel { get; set; }
        public string area { get; set; }
        public string formatoDocumento { get; set; }
        public bool redimensionarModelo { get; set; }
        public int tipoCliente { get; set; }
        public int estado { get; set; }
    }


    public class FormatoDocumento
    {
        public string value { get; set; }
        public string tipo { get; set; }
        public string formato { get; set; }
        public string color { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public double w { get; set; }
        public double h { get; set; }
    }
}
