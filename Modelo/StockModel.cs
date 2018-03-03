using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class StockModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(Stock param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/psalmacen/guardar
                return await webService.POST<Stock,Response>("psalmacen", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Stock param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/psalmacen/modificar
                return await webService.POST<Stock,Response>("psalmacen", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Stock param)
        {
            try
            {
                // localhost/admeli/xcore/services.php/psalmacen/eliminar
                return await webService.POST<Stock, Response>("psalmacen", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Stock>> stockProducto(int idProducto)
        {
            try
            {
                // www.lineatienda.com/services.php/psalmacenes/producto/445
                List<Stock> list = await webService.GET<List<Stock>>("psalmacenes", String.Format("producto/{0}", idProducto));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
