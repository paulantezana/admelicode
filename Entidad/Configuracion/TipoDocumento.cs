using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Configuracion
{
    public class TipoDocumento
    {
        public int idTipoDocumento { get; set; }
        public string nombre { get; set; }
        public string nombreLabel { get; set; }
        public string descripcion { get; set; }
        public bool comprobante { get; set; }
        public string area { get; set; }
        public int tipoCliente { get; set; }
        public int estado { get; set; }

        public string formatoDocumento { get; set; }
        public bool redimensionarModelo { get; set; }
        public bool bordeDetalle { get; set; }

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
    public class FormatoDoc
    {
        public string formatoDocumento { get; set; }
        public int idTipoDocumento  { get; set; }

    }
    public class Redimensionar
    {
        public int idTipoDocumento { get; set; }
        public int bordeDetalle { get; set; }
        public int redimensionarModelo { get; set; }
    
    }


   }
