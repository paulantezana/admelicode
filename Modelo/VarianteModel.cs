using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class VarianteModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(Variante param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/variante/guardar
                return await webService.POST<Variante,Response>("variante", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Variante param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/variante/modificar
                return await webService.POST<Variante,Response>("variante", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> desactivar(Variante param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/sssss/desactssssivar
                return await webService.POST<Variante,Response>("categoria", "desacsssssstivar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Variante param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/variante/eliminar
                return await webService.POST<Variante,Response>("variante", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Variante>> variantes(int idProducto)
        {
            try
            {
                // localhost/admeli/xcore/services.php/producto/21/variantes
                List<Variante> list = await webService.GET<List<Variante>>("producto", String.Format("{0}/variantes", idProducto));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
