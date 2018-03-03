using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ProductoRelacion
    {
        public int idProductoRelacion { get; set; }
        public string tipoRelacion { get; set; }
        public int ordenPosicion { get; set; }
        public int idRelacionProducto { get; set; }
        public string nombreProducto { get; set; }
        public int estado { get; set; }
        public int idProducto { get; set; }
    }
}
