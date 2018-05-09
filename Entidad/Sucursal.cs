using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Sucursal
    {
        public int idSucursal { get; set; }
        public string nombre { get; set; }
        public bool principal { get; set; }
        public int estado { get; set; }
        public string estados { get; set; }
        public string direccion { get; set; }
        public int idUbicacionGeografica { get; set; }
        public string tieneRegistros { get; set; }

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
    public class Sucursal_correlativo
    {
        public string serie { get; set; }
        public string correlativoActual { get; set; }

    }



    public class cajaS
    {
        public int idCaja { get; set; }
        public int estado { get; set; }
        public int idSucursal { get; set; }
    }
    public class AdministracionS
    {
        public int idPuntoAdministracion { get; set; }
        public int estado { get; set; }
        public int idSucursal { get; set; }
    }
    public class GerenciaS
    {
        public int idPuntoGerencia { get; set; }
        public int estado { get; set; }
        public int idSucursal { get; set; }
    }
    public class CompraS
    {
        public int idPuntoCompra { get; set; }
        public int estado { get; set; }
        public int idSucursal { get; set; }
    }
    public class VentaS
    {
        public int idPuntoVenta { get; set; }
        public int estado { get; set; }
        public int idSucursal { get; set; }
    }
}
