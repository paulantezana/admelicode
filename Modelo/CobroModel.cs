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
        public async Task<Response> anularCobroDetalle(DetalleCobro detalleCobro)
        {
            try
            {
                //http://localhost:8085/admeli/xcore/services.php/detallecobroingreso/anular
                Response response = await webService.POST<DetalleCobro, Response>("detallecobroingreso", "anular", detalleCobro);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> guardarCobroDetalle<T>(T param)
        {
            try
            {
                //http://localhost:8085/admeli/xcore/services.php/detallecobroingreso/guardar
                Response response = await webService.POST<T, Response>("detallecobroingreso", "guardar", param);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<DatosdeCuentasCobrar> Cuentasporcobrar(int idSucursal, int estado, int pos, int tam)
        {
            //localhost:8080/admeli/xcore/services.php/clientescuentasporcobrar/suc/1/todo/0/1/15
            try
            {

                DatosdeCuentasCobrar Cuentasporcobrar = await webService.GET<DatosdeCuentasCobrar>("clientescuentasporcobrar", String.Format("suc/{0}/todo/{1}/{2}/{3}",idSucursal,estado, pos, tam));
                return Cuentasporcobrar;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<DatosdeCuentasCobrar> buscarClienteCuentasPorCobrar(string nombreCliente,int idSucursal, int estado, int pos, int tam)
        {
            //localhost:8085/admeli/xcore/services.php/buscarclientescuentasporcobrar/suc/1/nombre/dennys/todo/0/1/15
            try
            {

                DatosdeCuentasCobrar Cuentasporcobrar = await webService.GET<DatosdeCuentasCobrar>("buscarclientescuentasporcobrar", String.Format("suc/{0}/nombre/{1}/todo/{2}/{3}/{4}", idSucursal,nombreCliente, estado, pos, tam));
                return Cuentasporcobrar;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<List<DetalleCobro>> detalleCobro(int idCobro)
        {
            try
            {
                //www.lineatienda.com/services.php/detallecobros/cobro/3
                List<DetalleCobro> listaDetalleCobro = await webService.GET<List<DetalleCobro>>("detallecobros", String.Format("cobro/{0}", idCobro));
                return listaDetalleCobro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Cobro>> cobrosCliente(int idCliente,int idSucursal,int estado)
        {
            try
            {
                //www.lineatienda.com/services.php/cobros/cliente/4/sucursal/1/estado/0
                List<Cobro> listaCobroCliente = await webService.GET<List<Cobro>>("cobros", String.Format("cliente/{0}/sucursal/{1}/estado/{2}", idCliente, idSucursal, estado));
                return listaCobroCliente;
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
