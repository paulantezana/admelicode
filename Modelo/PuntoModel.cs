using Entidad.Configuracion;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class PuntoModel
    {
        private WebService webService = new WebService();

        public async Task<PuntoAdministracion> puntoAdministracion(int idSucursal)
        {
            try
            {
                // www.admeli.com/demo2/services.php/puntoadministracion/suc/5
                List<PuntoAdministracion> responseData = await webService.GET<List<PuntoAdministracion>>("puntoadministracion", String.Format("suc/{0}",idSucursal));
                if (responseData.Count > 0)
                    return responseData[0];
                else
                   return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PuntoCompra> puntoCompra(int idSucursal)
        {
            try
            {
                // www.admeli.com/demo2/services.php/puntocompra/suc/5
                List<PuntoCompra> responseData = await webService.GET<List<PuntoCompra>>("puntocompra", String.Format("suc/{0}", idSucursal));

                if (responseData.Count > 0)
                    return responseData[0];
                else
                    return null;

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<PuntoDeVenta>> puntoVentas(int idSucursal)
        {
            try
            {
                // www.admeli.com/demo2/services.php/puntoventas/suc/5
                List<PuntoDeVenta> responseData = await webService.GET<List<PuntoDeVenta>>("puntoventas", String.Format("suc/{0}", idSucursal));
                return responseData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Caja>> cajas(int idSucursal)
        {
            try
            {
                // www.admeli.com/demo2/services.php/cajas/suc/5
                List<Caja> responseData = await webService.GET<List<Caja>>("cajas", String.Format("suc/{0}", idSucursal));
                return responseData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PuntoGerencia> puntoGerencia(int idSucursal)
        {
            try
            {
                // www.admeli.com/demo2/services.php/puntogerencia/suc/5
                List<PuntoGerencia> responseData = await webService.GET<List<PuntoGerencia>>("puntogerencia", String.Format("suc/{0}", idSucursal));
                if (responseData.Count > 0)
                    return responseData[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
