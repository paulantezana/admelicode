using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class DatoCuentaPagar
    {
        public int idProveedor { get; set; }
        public string razonSocial { get; set; }
        public string ruc { get; set; }
        public string actividadPrincipal { get; set; }
        public string tipoProveedor { get; set; }
        public string direccion { get; set; }
    }
    public class DatosdeCuentasPagar
    {
        public int nro_registros { get; set; }
        public List<DatoCuentaPagar> datos { get; set; }
    }
}
