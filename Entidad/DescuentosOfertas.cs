using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    
    public class DatosDescuentosOfertas
    {
        public string codigo { get; set; }
        public string descuento { get; set; }
        public  Fecha fechaInicio { get; set; }
        public Fecha fechaFin { get; set; }
        public string tipoDescuento { get; set; }
        public string tipo { get; set; }
        public string cantidadMinima { get; set; }
        public string cantidadMaxima { get; set; }
        public int estado { get; set; }
        public int idGrupoCliente { get; set; }
        public string nombreGrupo { get; set; }
        public int idSucursal { get; set; }
        public string nombre { get; set; }
        public int idAfectoProducto { get; set; }

        private DateTime sFechaInicio;
        private DateTime sFechaFin;
        public DateTime SFechaInicio
        {

            get
            {

                return fechaInicio.date;


            }
            set
            {
                sFechaInicio = value;


            }

        }
        public DateTime SFechaFin
        {

            get
            {

                return fechaFin.date;


            }
            set{

                sFechaFin = value;
            }

        }
    }

    public class ObjetosDescuentosOfertas
    {
        public int nro_registros { get; set; }
        public List<DatosDescuentosOfertas> datos { get; set; }
    }       
}


