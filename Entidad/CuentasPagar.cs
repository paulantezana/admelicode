using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class CuentasPagar
    {
        public int idCliente { get; set; }
        public string nombreCliente { get; set; }
        public string numeroDocumento { get; set; }
        public string direccion { get; set; }
        public string nombreGrupo { get; set; }
        public string nombre { get; set; }
    }
    public class DatosdeCuentasPagar
    {
        public int nro_registros { get; set; }
        public List<CuentasPagar> datos1 { get; set; }
    }
}
