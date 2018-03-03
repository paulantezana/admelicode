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
}
