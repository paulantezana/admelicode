using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class UnidadMedidaModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(UnidadMedida param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/unimed/guardar
                return await webService.POST<UnidadMedida,Response>("unimed", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(UnidadMedida param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/unimed/modificar
                return await webService.POST<UnidadMedida,Response>("unimed", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> desactivar(UnidadMedida param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/unimed/desactivar
                return await webService.POST<UnidadMedida,Response>("unimed", "desactivar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(UnidadMedida param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/unimed/eliminar
                return await webService.POST<UnidadMedida,Response>("unimed", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<UnidadMedida>> unimedidas(int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/unimedidas/estado/1/100
                RootObject<UnidadMedida> unidadesMedidad = await webService.GET<RootObject<UnidadMedida>>("unimedidas", String.Format("estado/{0}/{1}", page, items));
                return unidadesMedidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UnidadMedida>> unimedidas(int estado = 1)
        {
            try
            {
                // www.admeli.com/demo2/services.php/unimedidas/id/nombre/estado/1
                List<UnidadMedida> list = await webService.GET<List<UnidadMedida>>("unimedidas", String.Format("id/nombre/estado/{0}", estado));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
