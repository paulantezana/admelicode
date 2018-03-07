using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Almacen
    {
        public int idAlmacen { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public bool principal { get; set; }
        public int estado { get; set; }
        public int idSucursal { get; set; }
        public int idUbicacionGeografica { get; set; }
        public string nombreSucursal { get; set; }
        public string tieneRegistros { get; set; }

        public int idPersonalAlmacen { get; set; }
    }

    public class AlmacenComra
    {

        public int idAlmacen { get; set; }
        public string nombre { get; set; }
        public int idPersonalAlmacen { get; set; }
        public bool principal { get; set; }

    }
}
