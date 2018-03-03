using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class NotaSalida
    {
        public int idNotaSalida { get; set; }
        public string serie { get; set; }
        public string correlativo { get; set; }
        public string motivo { get; set; }
        public Fecha fecha { get; set; }
        public Fecha fechaSalida { get; set; }
        public string descripcion { get; set; }
        public string destino { get; set; }
        public int estadoEnvio { get; set; }
        public int estado { get; set; }
        public int idAlmacen { get; set; }
        public int idTipoDocumento { get; set; }
        public string idVenta { get; set; }
        public object numeroDocumento { get; set; }
        public object nombreCliente { get; set; }
        public object rucDni { get; set; }
        public string personal { get; set; }
        public string nombre { get; set; }
        public int idSucursal { get; set; }
        public int idPersonal { get; set; }
    }
}
