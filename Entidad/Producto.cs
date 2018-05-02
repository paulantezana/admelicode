using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Producto
    {
        public int idProducto { get; set; }
        public bool cantidadFraccion { get; set; }
        public string codigoBarras { get; set; }
        public string codigoProducto { get; set; }
        public string controlSinStock { get; set; }
        public string descripcionCorta { get; set; }
        public string descripcionLarga { get; set; }
        public bool enCategoriaEstrella { get; set; }
        public bool enPortada { get; set; }
        public bool enUso { get; set; }
        public bool estado { get; set; }
        public int idMarca { get; set; }
        public int idUnidadMedida { get; set; }
        public string keywords { get; set; }
        public string limiteMaximo { get; set; }
        public string limiteMinimo { get; set; }
        public bool mostrarPrecioWeb { get; set; }
        public bool mostrarVideo { get; set; }
        public bool mostrarWeb { get; set; }
        public string nombreMarca { get; set; }
        public string nombreProducto { get; set; }
        public string nombreUnidad { get; set; }
        public dynamic precioCompra { get; set; }
        public string urlVideo { get; set; }
        public bool ventaVarianteSinStock { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
        private string estadoString;
        public string EstadoString
        {
            get
            {
                if(estado == true) { return "Activo"; }
                else { return "Anulado"; }
            }
            set
            {
                estadoString = value;
            }
        }

        public string precioVenta { get; set; }
        public string stock { get; set; }
        public string stockFinanciero { get; set; }
        public int idPresentacionAfectada { get; set; }

    }

    public class ProductoVenta
    {
            public int idProducto { get; set; }
            public string codigoProducto { get; set; }
            public string nombreProducto { get; set; }
            public string precioVenta { get; set; }
            public int idPresentacion { get; set; }
            public bool ventaVarianteSinStock { get; set; }
            public string nombreMarca { get; set; }

       
    }

    public class ProductoSinImpuesto
    {
        public int idProducto { get; set; }
        public int idPresentacion { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string precioCompra { get; set; }
    }
    public class ProductoData
    {
        public int idProducto { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string descripcionCorta { get; set; }
        public string precioCompra { get; set; }
        public object precioConImpuesto { get; set; }
        public string utilidad { get; set; }
        public int estado { get; set; }
        public int idUnidadMedida { get; set; }
        public int idMarca { get; set; }
        public string nombreUnidad { get; set; }
        public string nombreMarca { get; set; }
        public bool enUso { get; set; }
        public bool mostrarWeb { get; set; }
        public bool mostrarPrecioWeb { get; set; }
        public string precioVenta { get; set; }
        public string stock { get; set; }
        public string stockFinanciero { get; set; }
        public int idSucursal { get; set; }
        public string sucursal { get; set; }
        public int idAlmacen { get; set; }
        public string almacen { get; set; }
        public string productoAlmacen { get; set; }
        private string estadoString;
        public List<ImpuestoData> impuesto { get; set; }

        public string EstadoString
        {
            get
            {
                if (estado == 1) { return "Activo"; }
                else { return "Anulado"; }
            }
            set
            {
                estadoString = value;
            }
        }
    }
    public class ImpuestoData
    {
        public int idImpuesto { get; set; }
        public string valorImpuesto { get; set; }
        public bool porcentual { get; set; }
    }



}
