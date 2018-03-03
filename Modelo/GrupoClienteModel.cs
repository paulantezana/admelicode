using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class GrupoClienteModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(GrupoCliente param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/gcliente/guardar
                return await webService.POST<GrupoCliente,Response>("gcliente", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(GrupoCliente param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/gcliente/modificar
                return await webService.POST<GrupoCliente, Response>("gcliente", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> desactivar(GrupoCliente param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/gcliente/desactivar
                return await webService.POST<GrupoCliente,Response>("gcliente", "desactivar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(GrupoCliente param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/gcliente/eliminar
                return await webService.POST<GrupoCliente,Response>("gcliente", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<GrupoCliente>> gclientes(int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/gclientes/estado/1/100
                RootObject<GrupoCliente> grupoClientes = await webService.GET<RootObject<GrupoCliente>>("gclientes", String.Format("estado/{0}/{1}", page, items));
                return grupoClientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GrupoCliente>> gclientes21()
        {
            try
            {
                // localhost/admeli/xcore/services.php/gclientes21
                List<GrupoCliente> list = await webService.GET<List<GrupoCliente>>("gclientes21");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
