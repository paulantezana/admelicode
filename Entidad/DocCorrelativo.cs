using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class DocCorrelativo
    {
        public string id { get; set; }
        public string area { get; set; }
        public int idSucursal { get; set; }
        public string nombre { get; set; }
        public string nombreLabel { get; set; }
        public int idCorrelativo { get; set; }
        public string serie { get; set; }
        public string correlativoInicio { get; set; }
        public string correlativoFin { get; set; }
        public string correlativoActual { get; set; }
        public bool fin { get; set; }
        public int estado { get; set; }
        public int idDocumento { get; set; }
        public int idOperacion { get; set; }
        public string operacion { get; set; }

        private string estadoString;
        public string EstadoString
        {
            get
            {
                if (estado == 1) { return "Activo"; }
                else { return "Anulado"; }
            }
            set
            {
                estadoString = value;
            }
        }
    }

        public class VentaCorrelativo
    {

        public int idVentaCorrelativo { get; set; }
        public string serie { get; set; }
        public string correlativoActual { get; set; }
        public int estado { get; set; }

    }
    public class CajaCorrelativoM
    {

        public int idCajaCorrelativo { get; set; }
        public string serie { get; set; }
        public string correlativoActual { get; set; }
        public int estado { get; set; }

    }
    public class SucursalCorrelativo
    {

        public int idSucursalCorrelativo { get; set; }
        public string serie { get; set; }
        public string correlativoActual { get; set; }
        public int estado { get; set; }

    }
    public class AlmacenCorrelativo
    {

        public int idAlmacenCorrelativo { get; set; }
        public string serie { get; set; }
        public string correlativoActual { get; set; }
        public int estado { get; set; }
        public string correlativoInicio { get; set; }
        public string correlativoFin { get; set; }      
        public bool fin { get; set; }
    }




}
