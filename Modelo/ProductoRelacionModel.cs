using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ProductoRelacionModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(ProductoRelacion param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/prelacion/guardar
                return await webService.POST<ProductoRelacion,Response>("prelacion", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(ProductoRelacion param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/prelacion/modificar
                return await webService.POST<ProductoRelacion,Response>("prelacion", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(ProductoRelacion param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/prelacion/eliminar
                return await webService.POST<ProductoRelacion,Response>("prelacion", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductoRelacion>> productoRelaciones(int idProducto)
        {
            try
            {
                // localhost/admeli/xcore/services.php/prelacion/producto/21
                List<ProductoRelacion> list = await webService.GET<List<ProductoRelacion>>("prelacion", String.Format("producto/{0}", idProducto));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
