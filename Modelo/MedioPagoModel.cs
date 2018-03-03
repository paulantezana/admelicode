using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MedioPagoModel
    {
        private WebService webService = new WebService();

        public async Task<List<MedioPago>> medioPagos(int estado = 1)
        {
            try
            {
                // localhost/admeli/xcore/services.php/mediopagos/estado/1
                List<MedioPago> list = await webService.GET<List<MedioPago>>("mediopagos", String.Format("estado/{0}", estado));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
