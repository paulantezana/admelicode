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
        public double valorImpuesto { get; set; }
        public bool porcentual { get; set; }
        public bool porDefecto { get; set; }
        public int estado { get; set; }
        public bool enUso { get; set; }

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

    public class OrdenCompraImpuesto
    {
        public int idProducto { get; set; }
        public List<Impuesto> impuestos { get; set; }
    }
    public class ImpuestoProductoTodo
    {
        public List<Impuesto> producto { get; set; }
        public List<Impuesto> todo { get; set; }
    }
    public class ImpuestosEnviados
    {
        public objectIm impuestos { get; set; }
        public Producto producto { get; set; }
        public Sucursal sucursal { get; set; }
    }
    public class objectIm
    {
        public string impuestos { get; set; }
    }
    public class ImpuestoGeneral
    {

        public int idImpuesto { get; set; }
        public string nombreImpuesto { get; set; }
        public string siglasImpuesto { get; set; }


    }   
    public class ImpuestosSiglas : ImpuestoGeneral
    {
      
    }


    public class ImpuestoDocumento: ImpuestoGeneral
    {
       
    }

    public class ImpuestoProducto
    {
        public int idImpuesto { get; set; }
        public string valorImpuesto { get; set; }
        public string siglasImpuesto { get; set; }
        public bool porcentual { get; set; }
    }
    public class ImpuestoComprobante
    {
        public int idTipoDocumento { get; set; }
        public int idSucursal { get; set; }
        public string impuestos { get; set; }
    }


}
