using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Contacto
    {
        public int idContacto { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string numeroDocumento { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public string direccion { get; set; }
        public int estado { get; set; }
        public int idDocumento { get; set; }
        public int idProveedor { get; set; }
    }
}
