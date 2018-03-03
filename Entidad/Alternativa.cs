using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Alternativa
    {
        public int idAlternativa { get; set; }
        public int idVariante { get; set; }
        public string descripcionAlternativa { get; set; }
        public bool seleccionado { get; set; }
        public int ordenPosicion { get; set; }
        public int estado { get; set; }
    }
}
