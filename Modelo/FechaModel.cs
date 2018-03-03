using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class FechaModel
    {
        private WebService webService = new WebService();

        public async Task<FechaSistema> fechaSistema()
        {
            try
            {
                // localhost/admeli/xcore/services.php/fechasystema
                FechaSistema data = await webService.GET<FechaSistema>("fechasystema");
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
