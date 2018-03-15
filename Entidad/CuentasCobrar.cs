using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class DatoCuentaCobrar
    {
        public int idCliente { get; set; }
        public string nombreCliente { get; set; }
        public string numeroDocumento { get; set; }
        public string direccion { get; set; }
        public string nombreGrupo { get; set; }
        public string nombre { get; set; }
    }

    public class DatosdeCuentasCobrar
    {
        public int nro_registros { get; set; }
        public List<DatoCuentaCobrar> datos { get; set; }
    }
}
