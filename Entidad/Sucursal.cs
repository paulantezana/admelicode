using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Sucursal
    {
        public int idSucursal { get; set; }
        public string nombre { get; set; }
        public bool principal { get; set; }
        public int estado { get; set; }
        public string estados { get; set; }
        public string direccion { get; set; }
        public int idUbicacionGeografica { get; set; }
        public string tieneRegistros { get; set; }
    }
    public class Sucursal_correlativo
    {
        public string serie { get; set; }
        public string correlativoActual { get; set; }

    }
}
