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

        public async Task<Response> anularPagoDetalle(DetallePago detallePago)
        {
            try
            {
                //http://localhost:8085/admeli/xcore/services.php/detallepagoiegreso/anular
                Response response = await webService.POST<DetallePago, Response>("detallepagoiegreso", "anular", detallePago);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DetallePago>> detallePago(int idPago)
        {
            try
            {
                //http://localhost:8085/admeli/xcore/services.php/detallepagos/pago/4
                List<DetallePago> listaDetallePago = await webService.GET<List<DetallePago>>("detallepagos", String.Format("pago/{0}", idPago));
                return listaDetallePago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Pago>> pagosProveedor(int idProveedor, int idSucursal,int estado)
        {
            try
            {
                //http://localhost:8085/admeli/xcore/services.php/pagos/proveedor/1/sucursal/1/0
                List<Pago> listaPagos=await webService.GET<List<Pago>>("pagos", String.Format("proveedor/{0}/sucursal/{1}/{2}", idProveedor,idSucursal,estado));
                return listaPagos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DatosdeCuentasPagar> buscarProveedorCuentasPorPagar(string nombreProveedor,int estado, int pos, int tam)
        {
            //http://localhost:8085/ad_meli/xcore/services.php/buscarproveedoresfaltapagarlike/nombre/empresa/todo/1/1/15
            try
            {

                DatosdeCuentasPagar Cuentasporcobrar = await webService.GET<DatosdeCuentasPagar>("buscarproveedoresfaltapagarlike", String.Format("nombre/{0}/todo/{1}/{2}/{3}",nombreProveedor, estado, pos, tam));
                return Cuentasporcobrar;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<DatosdeCuentasPagar> Cuentasporpagar(int estado, int pos, int tam)
        {
            //localhost:8085/admeli/xcore/services.php/proveedoresfaltapagar/todo/0/1/15
            try
            {
                DatosdeCuentasPagar CuentasporPagar = await webService.GET<DatosdeCuentasPagar>("proveedoresfaltapagar", String.Format("todo/{0}/{1}/{2}",estado,pos,tam));
                return CuentasporPagar;
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
