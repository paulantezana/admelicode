using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ComentarioModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(Comentario param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/comentario/guardar
                return await webService.POST<Comentario,Response>("comentario", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Comentario param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/comentario/modificar
                return await webService.POST<Comentario,Response>("comentario", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Comentario param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/comentario/eliminar
                return await webService.POST<Comentario,Response>("comentario", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Comentario>> comentarios(int idProducto)
        {
            try
            {
                // localhost/admeli/xcore/services.php/comentarios/21
                List<Comentario> list = await webService.GET<List<Comentario>>("comentarios", String.Format("{0}", idProducto));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
