using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class CobroModel
    {
        private WebService webService = new WebService();

        public void guardar()
        {

        }

        public void modificar()
        {

        }

        public void eliminar()
        {

        }

        public async Task<List<Cobro>> porCobrar(int idPersonal, int idSucursal, int idAsignarCaja, int todos, int gerente)
        {
            try
            {
                // www.lineatienda.com/services.php/porcobrar/3/1/3/1/1
                List<Cobro> porcobrar = await webService.GET<List<Cobro>>("porcobrar", String.Format("{0}/{1}/{2}/{3}/{4}",idPersonal,idSucursal,idAsignarCaja,todos,gerente));
                return porcobrar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
