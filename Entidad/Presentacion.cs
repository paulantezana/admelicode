using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Presentacion
    {
        public int idPresentacion { get; set; }
        public int idProducto { get; set; }
        public string nombrePresentacion { get; set; }
        public string cantidadUnitaria { get; set; }
        public bool presentacionPorDefecto { get; set; }
        public string simboloPresentacion { get; set; }
        public int estado { get; set; }
    }
}
