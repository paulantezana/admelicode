using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class EgresoModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar<T>(T param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/egreso/guardarenuno
                return await webService.POST<T,Response>("egreso", "guardarenuno", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<Response> modificar(Egreso param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/egreso/modificarenuno
                return await webService.POST<Egreso,Response>("egreso", "modificarenuno", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> desactivar(Egreso param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/egreso/anularegresonocompra
                return await webService.POST<Egreso,Response>("egreso", "anularegresonocompra", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Response> eliminar(Egreso currentEgreso)
        {
            // throw new NotImplementedException();
            return null;
        }

        public async Task<RootObject<Egreso>> egresos(int idSucursal, int idPersonal, int idCajaSesion, string idEstado, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/egresos/sucursal/0/personal/0/cajasesion/0/estado/todos/1/100
                RootObject<Egreso> egresos = await webService.GET<RootObject<Egreso>>("egresos", String.Format("sucursal/{0}/personal/{1}/cajasesion/{2}/estado/{3}/{4}/{5}", idSucursal, idPersonal, idCajaSesion, idEstado, page, items));
                return egresos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
