using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class OrdenCompra
    {
        public int idOrdenCompra { get; set; }
        public string serie { get; set; }
        public string correlativo { get; set; }
        public string nombreProveedor { get; set; }
        public string rucDni { get; set; }
        public string direccionProveedor { get; set; }
        public string moneda { get; set; }
        public Fecha fecha { get; set; }
        public Fecha plazoEntrega { get; set; }
        public string observacion { get; set; }
        public string direccion { get; set; }
        public int estado { get; set; }
        public int idUbicacionGeografica { get; set; }
        public int idTipoDocumento { get; set; }
        public int idCompra { get; set; }
        public string subTotal { get; set; }
        public string total { get; set; }
        public int idPago { get; set; }
        public int idProveedor { get; set; }
        public int estadoCompra { get; set; }
        public string nombres { get; set; }
    }


    public class CompraOrdenCompra{
        public int idOrdenCompra { get; set; }
        public string formaPago { get; set; }
        public double subTotal { get; set; }
        public double total { get; set; }
        public int estado { get; set; }
        public string moneda { get; set; }
        public int tipoCambio { get; set; }
        public int idMoneda { get; set; }


    }


    public class PlazoEntrega
    {
        public DateTime date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class OrdenCompraSinComprar
    {
        public int idOrdenCompra { get; set; }
        public string serie { get; set; }
        public string correlativo { get; set; }
        public string nombreProveedor { get; set; }
        public string rucDni { get; set; }
        public string direccionProveedor { get; set; }
        public string moneda { get; set; }
        public PlazoEntrega plazoEntrega { get; set; }
        public string observacion { get; set; }
        public string direccion { get; set; }
        public int estado { get; set; }
        public int idUbicacionGeografica { get; set; }
        public int idTipoDocumento { get; set; }
        public int idCompra { get; set; }
        public int idPago { get; set; }
        public string nombres { get; set; }
        public DateTime dateView;
        public DateTime DateView
        {
            get
            {

                return plazoEntrega.date;

            }
            set
            {

                dateView = value;

            }




        }

    }

    public class OrdenCompraSinComprarM
    {
        public int idOrdenCompra { get; set; }
        public string serie { get; set; }
        public string correlativo { get; set; }
        public string nombreProveedor { get; set; }
        public string rucDni { get; set; }
        public string direccionProveedor { get; set; }
        public string moneda { get; set; }
        public DateTime plazoEntrega { get; set; }
        public string observacion { get; set; }
        public string direccion { get; set; }
        public int estado { get; set; }
        public int idUbicacionGeografica { get; set; }
        public int idTipoDocumento { get; set; }
        public int idCompra { get; set; }
        public int idPago { get; set; }
        public string nombres { get; set; }

       


    }
}
