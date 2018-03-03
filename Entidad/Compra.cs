using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Compra
    {
        public int idCompra { get; set; }
        public string numeroDocumento { get; set; }
        public string nroOrdenCompra { get; set; }
        public string nombreProveedor { get; set; }
        public string rucDni { get; set; }
        public string direccion { get; set; }
        public string formaPago { get; set; }
        public string moneda { get; set; }
        public Fecha fechaFacturacion { get; set; }
        public Fecha fechaPago { get; set; }
        public string descuento { get; set; }
        public string tipoCompra { get; set; }
        public string tipoCambio { get; set; }
        public string subTotal { get; set; }
        public string total { get; set; }
        public string observacion { get; set; }
        public int estado { get; set; }
        public int idProveedor { get; set; }
        public int idPago { get; set; }
        public int idPersonal { get; set; }
        public int idTipoDocumento { get; set; }
        public string nombreLabel { get; set; }
        public int idSucursal { get; set; }
        public string vendedor { get; set; }
        public int idCajaSesion { get; set; }
    }
}
