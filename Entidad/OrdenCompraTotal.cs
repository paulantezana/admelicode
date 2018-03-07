using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
   
    




    public class PagoA
    {
        public double valorTotal { get; set; }
        public int valorPagado { get; set; }
        public string motivo { get; set; }
        public int estado { get; set; }
        public int estadoPago { get; set; }
        public double saldo { get; set; }
        public int idMoneda { get; set; }
        public int idPago { get; set; }
    }

    public class CompraA
    {
        public int tipoCambio { get; set; }
        public string observacion { get; set; }
        public string direccionEntrega { get; set; }
        public string moneda { get; set; }
        public string ubicacion { get; set; }
        public string formaPago { get; set; }
        public string nombreProveedor { get; set; }
        public string rucDni { get; set; }
        public string direccion { get; set; }
        public string plazoEntrega { get; set; }
        public int idCompraValor { get; set; }
        public string numeroDocumento { get; set; }
        public int idProveedor { get; set; }
        public string tipoCompra { get; set; }
        public double subTotal { get; set; }
        public double total { get; set; }
        public int estado { get; set; }
        public int idPersonal { get; set; }
        public int idTipoDocumento { get; set; }
        public int idSucursal { get; set; }
        public string fechaFacturacion { get; set; }
        public string fechaPago { get; set; }
        public int descuento { get; set; }
    }

    public class DetalleA
    {
        public int idDetalleCompra { get; set; }
        public int nro { get; set; }
        public string descripcion { get; set; }
        public string nombreMarca { get; set; }
        public int cantidad { get; set; }
        public int cantidadUnitaria { get; set; }
        public double precioUnitario { get; set; }
        public double descuento { get; set; }
        public double total { get; set; }
        public int estado { get; set; }
        public int idCombinacionAlternativa { get; set; }
        public int idPresentacion { get; set; }
        public int idProducto { get; set; }
        public int idCompra { get; set; }
        public string codigoProducto { get; set; }
        public string nombreCombinacion { get; set; }
        public string nombrePresentacion { get; set; }
        public int idSucursal { get; set; }
       
}

public class OrdenCompraA
{
    public int tipoCambio { get; set; }
    public string observacion { get; set; }
    public string direccionEntrega { get; set; }
    public string moneda { get; set; }
    public string ubicacion { get; set; }
    public string formaPago { get; set; }
    public string nombreProveedor { get; set; }
    public string rucDni { get; set; }
    public string direccion { get; set; }
    public string plazoEntrega { get; set; }
    public int idCompraValor { get; set; }
    public string numeroDocumento { get; set; }
    public int idProveedor { get; set; }
    public string tipoCompra { get; set; }
    public double subTotal { get; set; }
    public double total { get; set; }
    public int estado { get; set; }
    public int idPersonal { get; set; }
    public int idTipoDocumento { get; set; }
    public int idSucursal { get; set; }
    public string fechaFacturacion { get; set; }
    public string fechaPago { get; set; }
    public int descuento { get; set; }
    public int idUbicacionGeografica { get; set; }
    public int idOrdenCompra { get; set; }
}

public class OrdenCompraTotal
{
    public PagoA pago { get; set; }
    public CompraA compra { get; set; }
    public List<DetalleA> detalle { get; set; }
    public OrdenCompraA ordencompra { get; set; }
}


}
