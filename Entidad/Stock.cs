using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Stock
    {
        public int idProductoStockAlmacen { get; set; }
        public int idProducto { get; set; }
        public int idAlmacen { get; set; }
        public string stock { get; set; }
        public string stockIdeal { get; set; }
        public string stockMinimo { get; set; }
        public string alertaStock { get; set; }
        public int estado { get; set; }
        public string nombreAlmacen { get; set; }
    }
    public class verificarStockSubmit // para el evento de agregar detalle de venta

    {
        public int idVenta { get; set; }
        public int idPersonal { get; set; }
        public int idSucursal { get; set; }
        public List<List<object>> dato { get; set; }
    }
    public class verificarStockReceive// para el evento de agregar detalle de venta
    {
        public int stock_total { get; set; }
        public string idCombinacionAlternativa { get; set; }
        public int idProducto { get; set; }


    }


    //http://localhost:8080/admeli/xcore/services.php/producto/4/combinacion/0/stock/suc/1/personal/1

    public class StockReceive// para el evento de agregar detalle de venta
    {
        public double stock_total { get; set; }

    }



    public class VerificarStock
    {
        public int idVenta { get; set; }
        public int idPersonal { get; set; }
        public int idSucursal { get; set; }
        public List<List<object>> dato { get; set; }

    }

    public class AbasteceV
    {
        public int idVenta { get; set; }
        public int idAlmacen { get; set; }
        public List<List<object>> dato { get; set; }

    }
    public class AbasteceReceive
    {
        public int abastece { get; set; }
        public string productos { get; set; }
        public string combinaciones { get; set; }
        public string cantidades { get; set; }
    }



}
