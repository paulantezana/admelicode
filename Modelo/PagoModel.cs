using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class PagoModel
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

        public async Task<List<Pago>> porPagar(int idPersonal, int idSucursal, int idAsignarCaja, int todos, int gerente)
        {
            try
            {
                // www.lineatienda.com/services.php/porpagar/3/1/3/1/1
                List<Pago> porcobrar = await webService.GET<List<Pago>>("porpagar", String.Format("{0}/{1}/{2}/{3}/{4}", idPersonal, idSucursal, idAsignarCaja, todos, gerente));
                return porcobrar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Pago>> getPagoById(int idPago)
        {
            try
            {
                // www.lineatienda.com/services.php/pago/:idp/estado
                List<Pago> porcobrar = await webService.GET<List<Pago>>("pago", String.Format("{0}/estado", idPago));
                return porcobrar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
