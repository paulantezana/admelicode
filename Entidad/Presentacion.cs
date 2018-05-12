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
        public string codigo { get; set; }
        public string codigoBarras { get; set; }
        public string nombrePresentacion { get; set; }
        public string descripcion { get; set; }
        public string simboloPresentacion { get; set; }
        public string precioCompra { get; set; }
        public Decimal cantidadUnitaria { get; set; }
        public bool presentacionPorDefecto { get; set; }
        public int estado { get; set; }
        public int idProducto { get; set; }


        private int _idPresentacionBase;
        public int idPresentacionBase
        {
            get { return _idPresentacionBase; }
            set
            {

                if(value==null)
                    _idPresentacionBase = 0;
                else
                     _idPresentacionBase = value;
            }
        }
        
    }

     public class FechaCreacion
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public  class FechaModificacion
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class PresentacionV
    {
        public string codigo { get; set; }
        public string codigoBarras { get; set; }
        public string nombrePresentacion { get; set; }
        public string descripcion { get; set; }
        public string simboloPresentacion { get; set; }
        public string cantidadUnitaria { get; set; }
        public bool presentacionPorDefecto { get; set; }
        public string precioCompra { get; set; }
        public FechaCreacion fechaCreacion { get; set; }
        public FechaModificacion fechaModificacion { get; set; }
        public int estado { get; set; }
        public int idPresentacion { get; set; }
    }



}
