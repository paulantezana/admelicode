using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ImpuestoModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(Impuesto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/impuesto/guardar
                return await webService.POST<Impuesto,Response>("impuesto", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Impuesto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/impuesto/modificar
                return await webService.POST<Impuesto,Response>("impuesto", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> desactivar(Impuesto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/impuesto/desactivar
                return await webService.POST<Impuesto,Response>("impuesto", "desactivar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Impuesto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/impuesto/eliminar
                return await webService.POST<Impuesto,Response>("impuesto", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // prod/21/suc/1

        public async Task<List<Impuesto>> impuestoProductoSucursal(int idProducto, int idSucursal)
        {
            try
            {
                // localhost/admeli/xcore/services.php/impuestos/prod/21/suc/1
                List<Impuesto> list = await webService.GET<List<Impuesto>>("impuestos", String.Format("prod/{0}/suc/{1}", idProducto, idSucursal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Impuesto>> impuestos(int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/impuestos/estado/1/100
                RootObject<Impuesto> impuestos = await webService.GET<RootObject<Impuesto>>("impuestos", String.Format("estado/{0}/{1}", page, items));
                return impuestos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
