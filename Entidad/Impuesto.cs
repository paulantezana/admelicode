using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Impuesto
    {
        public int idImpuesto { get; set; }
        public string nombreImpuesto { get; set; }
        public string siglasImpuesto { get; set; }
        public string valorImpuesto { get; set; }
        public bool porcentual { get; set; }
        public bool porDefecto { get; set; }
        public int estado { get; set; }
        public bool enUso { get; set; }
    }
}
