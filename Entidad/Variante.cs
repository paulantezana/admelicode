using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Variante
    {
        public int idVariante { get; set; }
        public int idProducto { get; set; }
        public string nombreVariante { get; set; }
        public bool esCombo { get; set; }
        public Fecha fechaCreacion { get; set; }
        public Fecha fechaModificacion { get; set; }
        public int estado { get; set; }
    }
    public class CombinacioneGuaradar
    {
        public List<CombinacionStock> datos { get; set; }
        public int idAlmacen { get; set; }
        public int idProducto { get; set; }
    }
}
