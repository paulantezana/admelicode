using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class GuiaRemision
    {
        public int idGuiaRemision { get; set; }
        public string serie { get; set; }
        public string correlativo { get; set; }
        public Fecha fecha { get; set; }
        public string marcaYPlaca { get; set; }
        public string licenciaDeConducir { get; set; }
        public string direccionOrigen { get; set; }
        public string direccionDestino { get; set; }
        public int origen { get; set; }
        public int destino { get; set; }
        public string observacion { get; set; }
        public int estado { get; set; }
        public int idTipoDocumento { get; set; }
        public int idEmpresaTransporte { get; set; }
        public int idMotivoTraslado { get; set; }
        public string razonSocial { get; set; }
        public string nombre { get; set; }
        public int idNotaSalida { get; set; }
        public string motivo { get; set; }
        public string numeroDocumento { get; set; }
        public int idAlmacen { get; set; }

        public Task<Response> anular(GuiaRemision currentGuiaRemision)
        {
            throw new NotImplementedException();
        }

        private string estadoString;
        public string EstadoString
        {
            get
            {
                if(estado == 1) { return "Activo"; }
                else { return "Anulado"; }
            }
            set
            {
                estadoString = value;
            }
        }
    }


    public class MotivoTraslado
    {
        public int idMotivoTraslado { get; set; }
        public string nombre { get; set; }
        public int estado { get; set; }
    }
    public class EmpresaTransporte
    {
        public int idEmpresaTransporte { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public int estado { get; set; }
        public int idUbicacionGeografica { get; set; }
    }

    public class GuiaRemisionGuardar
    {

        public string marcaYPlaca { get; set; }
        public string licenciaDeConducir { get; set; }
        public string serie { get; set; }
        public string correlativo { get; set; }
        public string direccionOrigen { get; set; }
        public string direccionDestino { get; set; }
        public int idTipoDocumento { get; set; }
        public string observacion { get; set; }
        public int estado { get; set; }
        public int origen { get; set; }
        public int destino { get; set; }
        public int idEmpresaTransporte { get; set; }
        public int idMotivoTraslado { get; set; }

        public int idGuiaRemision { get; set; }
    
        public DateTime fecha { get; set; }
     
        public string razonSocial { get; set; }
        public string nombre { get; set; }
        public int idNotaSalida { get; set; }
        public string motivo { get; set; }
        public string numeroDocumento { get; set; }
        public int idAlmacen { get; set; }


    }
    public class NotaSalidaGuardarRemision
    {
        public int IdNotaSalida { get; set; }
      
    }

    public class AlmacenGuardarRemision
    {
        public int IdAlmacen { get; set; }

    }

}
