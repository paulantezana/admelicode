using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class OfertaModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(Oferta param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/oferta/guardar
                return await webService.POST<Oferta,Response>("oferta", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Oferta param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/oferta/modificar
                return await webService.POST<Oferta,Response>("oferta", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Oferta param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/oferta/eliminar
                return await webService.POST<Oferta, Response>("oferta", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Oferta>> ofertas(int idProducto)
        {
            try
            {
                // localhost/admeli/xcore/services.php/oferta/producto/21
                List<Oferta> list = await webService.GET<List<Oferta>>("oferta", String.Format("producto/{0}", idProducto));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
