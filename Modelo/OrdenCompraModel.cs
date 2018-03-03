using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class OrdenCompraModel
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
        public void cambiarClave()
        {

        }
        public async Task<RootObject<OrdenCompra>> ocompras(int idSucursal, int idPersonal, int page, int items)
        {
            try
            {
                RootObject<OrdenCompra> ordenCompra = await webService.GET<RootObject<OrdenCompra>>("ocompras", String.Format("suc/{0}/per/{1}/{2}/{3}", idSucursal, idPersonal, page, items));
                return ordenCompra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Response> anular(OrdenCompra currentOrdenCompra)
        {
            throw new NotImplementedException();
        }

        public Task<Response> eliminar(OrdenCompra currentOrdenCompra)
        {
            throw new NotImplementedException();
        }
    }
}
