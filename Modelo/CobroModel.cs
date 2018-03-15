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

        public async Task<DatosdeCuentasCobrar> Cuentasporcobrar(int idSucursal, int estado, int pos, int tam)
        {
            //localhost:8080/admeli/xcore/services.php/clientescuentasporcobrar/suc/1/todo/0/1/15
            try
            {

                DatosdeCuentasCobrar Cuentasporcobrar = await webService.GET<DatosdeCuentasCobrar>("datosdeCuentasCobrar", String.Format("suc/{0}/todo/{1}/{2}/{3}",idSucursal,estado, pos, tam));
                return Cuentasporcobrar;
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
