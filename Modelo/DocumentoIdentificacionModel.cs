using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class DocumentoIdentificacionModel
    {
        private WebService webService = new WebService();

        public async Task<List<DocumentoIdentificacion>> docIdentificacionNatural()
        {
            try
            {
                // www.lineatienda.com/services.php/documentoidentificacionesnatural
                return await webService.GET<List<DocumentoIdentificacion>>("documentoidentificacionesnatural");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> guardar(DocumentoIdentificacion param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/documentoidentificacion/guardar
                return await webService.POST<DocumentoIdentificacion,Response>("documentoidentificacion", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(DocumentoIdentificacion param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/documentoidentificacion/modificar
                return await webService.POST<DocumentoIdentificacion, Response>("documentoidentificacion", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Response> desactivar(DocumentoIdentificacion param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/documentoidentificacion/modificar
                return await webService.POST<DocumentoIdentificacion,Response>("documentoidentificacion", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(DocumentoIdentificacion param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/documentoidentificacion/eliminar
                return await webService.POST<DocumentoIdentificacion,Response>("documentoidentificacion", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<DocumentoIdentificacion>> verificar(string nombre, int idDocumento)
        {
            try
            {
                // localhost/admeli/xcore/services.php/verificardocumentoidentificaciones/nombre/rerg/documento/0
                List<DocumentoIdentificacion> list = await webService.GET<List<DocumentoIdentificacion>>("verificardocumentoidentificaciones", String.Format("nombre/{0}/documento/{1}", nombre, idDocumento));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<DocumentoIdentificacion>> documentoidentificaciones(int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/documentoidentificaciones/1/100
                RootObject<DocumentoIdentificacion> listRoot = await webService.GET<RootObject<DocumentoIdentificacion>>("documentoidentificaciones", String.Format("{0}/{1}", page, items));
                return listRoot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
