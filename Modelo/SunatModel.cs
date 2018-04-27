using Entidad;
using Entidad.Location;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public  class SunatModel
    {
        private WebService webService = new WebService();

        private LocationModel locationModel = new LocationModel();
        public async Task<RespuestaSunat> obtenerDatos(string dni)
        {
            try
            {
                // localhost/admeli/xcore2/xcore/services.php/sunat/dni
                return await webService.GET<RespuestaSunat>("sunat", String.Format("{0}", dni));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> obtenerDatos1(string dni)
        {
            try
            {
                // localhost/admeli/xcore2/xcore/services.php/sunat/dni
                return await webService.GET<int>("sunat", String.Format("{0}", dni));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
