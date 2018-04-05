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
    public class listaEnviada
    {
        public List<object> listita { get; set; }
    }

    public class ImpuestosSiglas
    {
        public int idImpuesto { get; set; }
        public string nombreImpuesto { get; set; }
        public string siglasImpuesto { get; set; } 
    }
}
