using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class CotizacionModel
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

        public async Task<RootObject<Cotizacion>> cotizaciones(int idSucursal, int idPersonal, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/cotizaciones/suc/0/per/0/1/100
                RootObject<Cotizacion> almacenes = await webService.GET<RootObject<Cotizacion>>("cotizaciones", String.Format("suc/{0}/per/{1}/{2}/{3}", idSucursal, idPersonal, page, items));
                return almacenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Response> eliminar(Cotizacion currentCotizacion)
        {
            throw new NotImplementedException();
        }

        public Task<Response> desactivar(Cotizacion currentCotizacion)
        {
            throw new NotImplementedException();
        }
    }
}
