using Entidad;
using Entidad.Configuracion;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class PuntoVentaModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(PuntoDeVenta param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/puntoventa/guardar
                return await webService.POST<PuntoDeVenta,Response>("puntoventa", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(PuntoDeVenta param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/puntoventa/modificar
                return await webService.POST<PuntoDeVenta,Response>("puntoventa", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> anular(PuntoDeVenta param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/puntoventa/anular
                return await webService.POST<PuntoDeVenta,Response>("puntoventa", "anular", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(PuntoDeVenta param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/puntoventa/eliminar
                return await webService.POST<PuntoDeVenta,Response>("puntoventa", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<PuntoDeVenta>> puntoventas(int idSucursal, string idEstado, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/puntoventas/sucursal/0/estado/todos/1/100
                RootObject<PuntoDeVenta> puntoVenta = await webService.GET<RootObject<PuntoDeVenta>>("puntoventas", String.Format("sucursal/{0}/estado/{1}/{2}/{3}", idSucursal, idEstado, page, items));
                return puntoVenta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PuntoDeVenta>> puntoventas(int idSucursal)
        {
            try
            {
                // www.lineatienda.com/services.php/puntoventas/suc/1
                List<PuntoDeVenta> puntoVenta = await webService.GET<List<PuntoDeVenta>>("puntoventas", String.Format("suc/{0}", idSucursal));
                return puntoVenta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PuntoDeVenta>> puntoVentasyTodos(int idSucursal)
        {
            try
            {
                // www.lineatienda.com/services.php/puntoventasytodos/suc/0
                List<PuntoDeVenta> puntoVenta = await webService.GET<List<PuntoDeVenta>>("puntoventasytodos", String.Format("suc/{0}", idSucursal));
                return puntoVenta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
