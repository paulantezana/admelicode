using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Configuracion
{
    public class AsignacionPersonal
    {
        public int idPuntoCompra { get; set; }
        public int idAsignarPuntoCompra { get; set; }
        public int idPuntoAdministracion { get; set; }
        public int idAsignarPuntoAdministracion { get; set; }
        public int idPuntoGerencia { get; set; }
        public int idAsignarPuntoGerencia { get; set; }
        public int idAsignarCaja { get; set; }
        public int idCaja { get; set; }
        public int idPuntoVenta { get; set; }
        public int idAlmacen { get; set; }
        public int idAsignarPuntoVenta { get; set; }
    }
}
