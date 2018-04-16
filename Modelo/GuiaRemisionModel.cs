using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class GuiaRemisionModel
    {
        private WebService webService = new WebService();

        public async Task<ResponseNotaGuardar> guardar(List<object> param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/gremision/guardar
                return await webService.POST<List<object>, ResponseNotaGuardar>("gremision", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> modificar(List<object> param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/gremision/modificar
                return await webService.POST<List<object>, Response>("gremision", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<Response> guardarETransporte(EmpresaTransporte param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/etransporte/guardar
                return await webService.POST<EmpresaTransporte, Response>("etransporte", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //mtraslado/guardar
        public async Task<Response> guardarMTraslado(MotivoTraslado param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/mtraslado/guardar
                return await webService.POST<MotivoTraslado, Response>("mtraslado", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
       

        public void eliminar()
        {

        }



        //etransporte/guardar
        public async Task<RootObject<GuiaRemision>> notaEntradas(int idSucursal, int idAlmacen, int estado, int page, int items)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/gremision/sucursal/0/almacen/0/estado/1/1/30
                RootObject<GuiaRemision> rootData = await webService.GET<RootObject<GuiaRemision>>("gremision", String.Format("sucursal/{0}/almacen/{1}/estado/{2}/{3}/{4}", idSucursal, idAlmacen, estado, page, items));
                return rootData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DetalleNotaSalida>> cargarDetallesNota(int idGuiRemision)
        {
            try
            {
                // www.lineatienda.com/services.php/gremision/detalle/:id
                List<DetalleNotaSalida> rootData = await webService.GET<List<DetalleNotaSalida>>("gremision", String.Format("detalle/{0}", idGuiRemision));
                return rootData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public Task<Response> eliminar(GuiaRemision currentGuiaRemision)
        {
            throw new NotImplementedException();
        }
         public async Task<List<MotivoTraslado>> Motivo(int estado=1)
        {
            try
            {
                // www.lineatienda.com/services.php/mtraslado/estado/1
                List<MotivoTraslado> listmotivo = await webService.GET<List<MotivoTraslado>>("mtraslado", String.Format("estado/{0}",estado));
                return listmotivo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<EmpresaTransporte>> ETransporte(int estado = 1)
        {
            try
            {
                // www.lineatienda.com/services.php/etransporte/estado/1
                List<EmpresaTransporte> listEmpresaTransporte = await webService.GET<List<EmpresaTransporte>>("etransporte", String.Format("estado/{0}", estado));
                return listEmpresaTransporte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //mtraslado/estado/1
    }
}
