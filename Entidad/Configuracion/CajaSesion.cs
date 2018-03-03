using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Configuracion
{
    public class CajaSesion
    {
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int idCajaSesion { get; set; }
        public Fecha fechaInicio { get; set; }
        public Fecha fechaCierre { get; set; }
        public int estado { get; set; }
        public string totalIngreso { get; set; }
        public string totalEgreso { get; set; }
        public string nombre { get; set; }
    }
}
