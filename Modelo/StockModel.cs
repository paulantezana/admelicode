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
        public async Task<Response> guardarproductosp(ProductoStockGuardar param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/productosp/guardar
                return await webService.POST<ProductoStockGuardar, Response>("productosp", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // productosp/guardar
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

        //verificarstockproductossucursal
        public async Task<verificarStockReceive> getStockAlmacenByIdProductosIdCombinacionEIdSucursal(verificarStockSubmit param)
        {
            try
            {
                // localhost/admeli/xcore/services.php/verificarstockproductossucursal
                return await webService.POST<verificarStockSubmit, verificarStockReceive>("verificarstockproductossucursal", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<verificarStockReceive>> verificarstockproductossucursal(VerificarStock param)
        {
            try
            {
                // localhost/admeli/xcore/services.php/verificarstockproductossucursal
                return await webService.POST<VerificarStock, List<verificarStockReceive>>("verificarstockproductossucursal", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //verificarabastecealmacenventa
        //
        //
        public async Task<AbasteceReceive> Abastece(AbasteceV param)
        {
            try
            {
                // localhost/admeli/xcore/services.php/verificarabastecealmacenventa
                return await webService.POST<AbasteceV, AbasteceReceive>("verificarabastecealmacenventa", param);
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

        //http://localhost:8080/admeli/xcore/services.php/producto/4/combinacion/0/stock/suc/1/personal/1
        public async Task<List<StockReceive>> getStockProductoCombinacion(int idProducto, int idCombinacion , int idSucursul, int idPersonal)
        {
            try
            {
                // www.lineatienda.com/services.php/psalmacenes/producto/445
                List<StockReceive> list = await webService.GET<List<StockReceive>>("producto", String.Format("{0}/combinacion/{1}/stock/suc/{2}/personal/{3}", idProducto,idCombinacion, idSucursul,idPersonal ));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       



        //verificarstockproductossucursal


    }
}
