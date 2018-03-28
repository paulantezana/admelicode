using Entidad;
using Modelo.Recursos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ProductoModel
    {
        public static Producto producto { get; set; }
        private WebService webService = new WebService();

        // validacion de un producto     
        public async Task<List<Producto>> validarProducto(Producto param)
        {
            try
            {
                // localhost/admeli/xcore/services.php/verificarproductonombrecodigo
                List<Producto> list = await webService.POSTResponse("verificarproductonombrecodigo", param);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> guardar(Producto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/producto/guardar
                return await webService.POST<Producto,Response>("producto", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Producto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/producto/modificar
                return await webService.POST<Producto,Response>("producto", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Producto param)
        {
            try
            {
                // localhost/admeli/xcore/services.php/producto/eliminar
                return await webService.POST<Producto,Response>("producto", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Producto>> productosPorCategoria(Dictionary<string, int> dictionary, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/productos/categoria/1/100
                Dictionary<string, int>[] dataSend = { dictionary };

                RootObject<Producto> productos = await webService.POST<Dictionary<string, int>[], RootObject<Producto>>("productos", String.Format("categoria/{0}/{1}",page,items), dataSend);
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Producto>> productosPorCategoriaBuscar(Dictionary<string, int> dictionary, string like, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/productos/categoria/1/100/pantalla
                Dictionary<string, int>[] dataSend = { dictionary };

                RootObject<Producto> productos = await webService.POST<Dictionary<string, int>[], RootObject<Producto>>("productos", String.Format("categoria/{0}/{1}/{2}", page, items,like), dataSend);
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Producto>> productosStock(Dictionary<string, int> dictionary, string like, int idAlmacen, int idSucursal, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/productos/categoria/stock/1/100/1/1
                Dictionary<string, int>[] dataSend = { dictionary };

                RootObject<Producto> productos = await webService.POST<Dictionary<string, int>[], RootObject<Producto>>("productos", String.Format("categoria/stock/{0}/{1}/{2}/{3}", page, items, idAlmacen, idSucursal), dataSend);
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<RootObject<Producto>> productosStockLike(Dictionary<string, int> dictionary, string like, int idAlmacen, int idSucursal, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/productos/categoria/stock/1/100/pantalla/1/1
                Dictionary<string, int>[] dataSend = { dictionary };

                RootObject<Producto> productos = await webService.POST<Dictionary<string, int>[], RootObject<Producto>>("productos", String.Format("categoria/stock/{0}/{1}/{2}/{3}/{4}", page, items, like, idAlmacen, idSucursal), dataSend);
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> stockHerramienta<T>(Dictionary<string, int> dictionary, int idAlmacen, int idSucursal, int page, int items)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/productos/categoria/stockherramienta/1/100/0/0
                Dictionary<string, int>[] dataSend = { dictionary };

                T productos = await webService.POST<Dictionary<string, int>[], T>("productos", String.Format("categoria/stockherramienta/{0}/{1}/{2}/{3}", page, items, idAlmacen, idSucursal), dataSend);
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> stockHerramientaLike<T>(Dictionary<string, int> dictionary, string like, int idAlmacen, int idSucursal, int page, int items)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/productos/categoria/stockherramienta/1/100/0/0
                // localhost:8080/admeli/xcore/services.php/productos/categoria/stockherramienta/1/100000/moto/0/1
                Dictionary<string, int>[] dataSend = { dictionary };

                T productos = await webService.POST<Dictionary<string, int>[], T>("productos", String.Format("categoria/stockherramienta/{0}/{1}/{2}/{3}/{4}", page, items, like, idAlmacen, idSucursal), dataSend);
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Producto>> productos21(int idProducto)
        {
            try
            {
                // localhost/admeli/xcore/services.php/productos21/17
                List<Producto> list = await webService.GET<List<Producto>>("productos21", String.Format("{0}", idProducto));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Producto> productoDatos(int idProducto)
        {
            try
            {
                // localhost/admeli/xcore/services.php/producto/datos/21
                List<Producto> list = await webService.GET<List<Producto>>("producto", String.Format("datos/{0}", idProducto));
                return list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Producto>> productoRelacionado(int idProducto, string tipoRelacion, int productoRelacion)
        {
            try
            {
                // localhost/admeli/xcore/services.php/listarproductosnorelacionados/producto/21/tiporelacion/complementaria/productorelacion/0
                List<Producto> list = await webService.GET<List<Producto>>("listarproductosnorelacionados", String.Format("producto/{0}/tiporelacion/{1}/productorelacion/{2}", idProducto, tipoRelacion, productoRelacion));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Producto>> productos()
        {
            try
            {
                // localhost/admeli/xcore/services.php/productos41
                List<Producto> list = await webService.GET<List<Producto>>("productos41");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductoVenta>> productos(int idSucursal, int idPersonal)
        {
            try
            {
                // localhost/admeli/xcore/services.php/productos/suc/1/personal/1
                List<ProductoVenta> list = await webService.GET<List<ProductoVenta>>("productos", String.Format("suc/{0}/personal/{1}", idSucursal, idPersonal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Producto>> productosSinCategoria()
        {
            try
            {
                // localhost/admeli/xcore/services.php/productos41/categoria
                List<Producto> list = await webService.GET<List<Producto>>("productos41", "categoria");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //listarProductoPorIdProductoCodigoNombreSinImpuesto
        public async Task<List<ProductoSinImpuesto>> listarProductoPorIdProductoCodigoNombreSinImpuesto(int idSucursal)
        {
            try
            {
                // localhost/admeli/xcore/services.php/productos41/impuesto/sucursal/1
                List<ProductoSinImpuesto> list = await webService.GET<List<ProductoSinImpuesto>>("productos41", String.Format("impuesto/sucursal/{0}", idSucursal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
