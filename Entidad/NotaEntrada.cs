using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class NotaEntrada
    {

        public int idNotaEntrada { get; set; }
        public string serie { get; set; }
        public string correlativo { get; set; }
        public Fecha fecha { get; set; }
        public Fecha fechaEntrada { get; set; }
        public string observacion { get; set; }
        public int estadoEntrega { get; set; }
        public int estado { get; set; }
        public int idTipoDocumento { get; set; }
        public int idAlmacen { get; set; }
        public string idCompra { get; set; }
        public object numeroDocumento { get; set; }
        public object nombreProveedor { get; set; }
        public object rucDni { get; set; }
        public string personal { get; set; }
        public string nombre { get; set; }
        public int idSucursal { get; set; }
        public int idPersonal { get; set; }

        public string nombreAlmacen { get; set; }
        public object numeroDocumentoCompra { get; set; }


    }
}
