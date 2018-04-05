using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class DocCorrelativoModel
    {
        private WebService webService = new WebService();

        public void guardar()
        {

        }

        public async Task<Response> modificarVentaCorrelativo(VentaCorrelativo param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/ventacorrelativo/modificar
                return await webService.POST<VentaCorrelativo, Response>("ventacorrelativo", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> modificarCajaCorrelativo(CajaCorrelativoM param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/ cajacorrelativo/modificar
                return await webService.POST<CajaCorrelativoM, Response>("cajacorrelativo", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> modificarSucursalCorrelativo(SucursalCorrelativo param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/sucursalcorrelativo/modificar
                return await webService.POST<SucursalCorrelativo, Response>("sucursalcorrelativo", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> modificarAlmacenCorrelativo(AlmacenCorrelativo param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/sucursalcorrelativo/modificar
                return await webService.POST<AlmacenCorrelativo, Response>("almacencorrelativo", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }












        public void eliminar()
        {

        }

        public async Task<RootObject<DocCorrelativo>> listartodoCorrelativo(int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/listartodocorrelativo/1/100
                RootObject<DocCorrelativo> docCorrelativo = await webService.GET<RootObject<DocCorrelativo>>("listartodocorrelativo", String.Format("{0}/{1}", page, items));
                return docCorrelativo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List< Sucursal_correlativo>> listarNroDocumentoSucursal(int idTipoDocumento,int idSucursal)
        {
            try
            {
                // www.lineatienda.com/services.php/succorrelativo/tipodoc/1/suc/1
                return await webService.GET<List<Sucursal_correlativo>>(String.Format("succorrelativo/tipodoc/{0}", idTipoDocumento), String.Format("suc/{0}", idSucursal));
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
