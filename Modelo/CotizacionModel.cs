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

            //cotizacion/guardartodo
            //TotalCotizacion
        }
        public async Task<Response> guardar(TotalCotizacion param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/contactoproveedor/guardar
                return await webService.POST<TotalCotizacion, Response>("cotizacion", "guardartodo", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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


        //succorrelativo/tipodoc/2/suc/1
        public async Task<List<CorrelativoCotizacion>> Correlativo(int idSucursal,int tipoDoc=2)
        {
            try
            {
               
                
                // www.lineatienda.com/services.php/succorrelativo/tipodoc/0/per/0/1/100
                List<CorrelativoCotizacion> almacenes = await webService.GET<List<CorrelativoCotizacion>>("succorrelativo", String.Format("tipodoc/{0}/suc/{1}", tipoDoc,idSucursal));
                return almacenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //detallecotizacion/15
        public async Task<List<DetalleV>> detalleCotizacion(int idCotizacion)
        {
            try
            {
                // www.lineatienda.com/services.php/sdetallecotizacion/100
                List<DetalleV> almacenes = await webService.GET<List<DetalleV>>("detallecotizacion", String.Format("{0}", idCotizacion));
                return almacenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
