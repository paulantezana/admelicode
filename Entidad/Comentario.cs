using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Comentario
    {
        public int idComentario { get; set; }
        public string nombreUsuario { get; set; }
        public string correoElectronico { get; set; }
        public string pais { get; set; }
        public string tituloComentario { get; set; }
        public string comentario { get; set; }
        public int puntos { get; set; }
        public Fecha fechaCreacion { get; set; }
        public Fecha fechaModificacion { get; set; }
        public int estado { get; set; }
        public int idCliente { get; set; }
        public int idProducto { get; set; }
    }
}
