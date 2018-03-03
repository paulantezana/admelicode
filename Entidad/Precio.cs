using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Precio
    {
        public int idPrecioProducto { get; set; }
        public string precioVenta { get; set; }
        public string precioCompetencia { get; set; }
        public int estado { get; set; }
        public string utilidad { get; set; }
        public string moneda { get; set; }
        public int idProducto { get; set; }
        public int idMoneda { get; set; }
        public int idSucursal { get; set; }
        public string nombreSucursal { get; set; }
    }
}
