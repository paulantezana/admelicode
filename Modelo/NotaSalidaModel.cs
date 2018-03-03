using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class NotaSalidaModel
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

        public async Task<RootObject<NotaSalida>> notaEntradas(int idSucursal, int idAlmacen, int idPersonal, int estado, int page, int items)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/nsalida/sucursal/0/almacen/0/personal/0/estado/0/1/30
                RootObject<NotaSalida> rootData = await webService.GET<RootObject<NotaSalida>>("nsalida", String.Format("sucursal/{0}/almacen/{1}/personal/{2}/estado/{3}/{4}/{5}", idSucursal, idAlmacen, idPersonal, estado, page, items));
                return rootData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Response> eliminar(NotaSalida currentNotaSalida)
        {
            throw new NotImplementedException();
        }

        public Task<Response> anular(NotaSalida currentNotaSalida)
        {
            throw new NotImplementedException();
        }
    }
}
