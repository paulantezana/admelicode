using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class DepartamentoModel
    {
        private WebService webService = new WebService();

        // www.admeli.com/demo2/services.php/listarareasporsucursal/ids/1/personal/0

        public async Task<List<Departamento>> listarAreasPorSucursal(int ids, int idPersonal)
        {
            try
            {
                List<Departamento> list = await webService.GET<List<Departamento>>("listarareasporsucursal",String.Format("ids/{0}/personal/{1}",ids,idPersonal));
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
