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
            //  
            //compra
            //detalle
            //ordencompra
            //pago




        }

        public async Task<Response> guardar(OrdenCompraTotal param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/ordencompra/guardartodo
                return await webService.POST<OrdenCompraTotal, Response>("ordencompra", "guardartodo", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> comprarOrdenCompra(CompraOrdenCompra param)
        {
            try
            {
                // http://localhost:8080/admeli/xcore/services.php/compraordencompra/guardar
                return await webService.POST<CompraOrdenCompra, Response>("compraordencompra", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public async Task<Response> eliminar(OrdenCompraElimnar currentOrdenCompra)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/ocompras/eliminar
                return await webService.POST<OrdenCompraElimnar, Response>("ocompras", "eliminar", currentOrdenCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public async Task<List<OrdenCompraModificar>> dcomprasordencompra(int idOrdenCompra)
        {
            try
            {
                List<OrdenCompraModificar> ordenCompra = await webService.GET<List<OrdenCompraModificar>>("dcomprasordencompra", String.Format("{0}", idOrdenCompra));
                return ordenCompra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
