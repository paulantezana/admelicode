using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MarcaModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(Marca param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/marca/guardar
                return await webService.POST<Marca,Response>("marca", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Marca param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/marca/modificar
                return await webService.POST<Marca,Response>("marca", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> desactivar(Marca param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/marca/desactivar
                return await webService.POST<Marca,Response>("marca", "desactivar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Marca param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/marca/eliminar
                return await webService.POST<Marca, Response>("marca", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Marca>> marcas(int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/marcas/estado/1/100
                RootObject<Marca> marcas = await webService.GET<RootObject<Marca>>("marcas", String.Format("estado/{0}/{1}", page, items));
                return marcas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Marca>> marcas(int estado = 1)
        {
            try
            {
                // www.admeli.com/demo2/services.php/marcas/id/nombre/estado/1
                List<Marca> marcas = await webService.GET<List<Marca>>("marcas", String.Format("id/nombre/estado/{0}", estado));
                return marcas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
