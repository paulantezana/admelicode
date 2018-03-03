using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class GuiaRemisionModel
    {
        private WebService webService = new WebService();

        public void guardar()
        {

        }

        public void modificar()
        {

        }

        public void eliminar()
        {

        }

        public async Task<RootObject<GuiaRemision>> notaEntradas(int idSucursal, int idAlmacen, int estado, int page, int items)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/gremision/sucursal/0/almacen/0/estado/1/1/30
                RootObject<GuiaRemision> rootData = await webService.GET<RootObject<GuiaRemision>>("gremision", String.Format("sucursal/{0}/almacen/{1}/estado/{2}/{3}/{4}", idSucursal, idAlmacen, estado, page, items));
                return rootData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Response> eliminar(GuiaRemision currentGuiaRemision)
        {
            throw new NotImplementedException();
        }
    }
}
