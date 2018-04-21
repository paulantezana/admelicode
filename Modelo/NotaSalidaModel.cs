using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class NotaSalidaModel
    {
        private WebService webService = new WebService();


//
        

        public async Task<ResponseNotaGuardar> guardar(List<object> param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/nsalida/guardar
                return await webService.POST<List<object>, ResponseNotaGuardar>("nsalida", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseNotaGuardar> modificar(List<object> param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/nsalida/modificar
                return await webService.POST<List<object>, ResponseNotaGuardar>("nsalida", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> anular(NotaSalida currentNotaSalida)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/nsalida/modificar
                return await webService.POST<NotaSalida, Response>("nsalida", "anular", currentNotaSalida);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseNotaSalida> verifcar(ComprobarNotaSalida param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/moneda/desactivar
                return await webService.POST<ComprobarNotaSalida, ResponseNotaSalida>("verificarstockcantidadnotasalida", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







        public async Task<RootObject<NotaSalida>> notaEntradas(int idSucursal, int idAlmacen, int idPersonal, int estado, int page, int items)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/nsalida/sucursal/0/almacen/0/personal/0/estado/0/1/30
                RootObject<NotaSalida> rootData = await webService.GET<RootObject<NotaSalida>>("nsalida", String.Format("sucursal/{0}/almacen/{1}/personal/{2}/estado/{3}/{4}/{5}", idSucursal, idAlmacen, idPersonal, estado, page, items));
                return rootData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DetalleNotaSalida>> cargarDetallesNota(int idNotaSalida)
        {
            try
            {
                // www.lineatienda.com/services.php/nsalida/detalle/:id
                List<DetalleNotaSalida> rootData = await webService.GET<List<DetalleNotaSalida>>("nsalida", String.Format("detalle/{0}", idNotaSalida));
                return rootData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public Task<Response> eliminar(NotaSalida currentNotaSalida)
        {
            throw new NotImplementedException();
        }

       


       

        public async Task<List<VentasNSalida>> VentasSinNotasSalida (int idsucursal)
        {
            try
            {

                // www.lineatienda.com/services.php/ventasinnotasalida/sucursal/1
                List<VentasNSalida> notas = await webService.GET<List<VentasNSalida>>("ventasinnotasalida", String.Format("sucursal/{0}", idsucursal));
                return notas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<List<DetalleNotaSalida>> cargarDetalleNotaSalida(int idVenta)
        {
            try
            {

                // www.lineatienda.com/services.php/ventasinnotasalida/dventas/2
                List<DetalleNotaSalida> notas = await webService.GET<List<DetalleNotaSalida>>("dventas", String.Format("{0}", idVenta));
                return notas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public async Task<List<NotaSalidaR>> nSalida( int idAlmacen ,int estado=1)
        {
            try
            {
               
                // www.lineatienda.com/services.php/nsalida/estado/1/1
                List<NotaSalidaR> notas = await webService.GET<List<NotaSalidaR>>("nsalida", String.Format("estado/{0}/{1}", estado, idAlmacen));
                return notas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<DetalleNotaSalida>> nSalidaDetalle(int idNotaSalida)
        {
            try
            {
                // www.lineatienda.com/services.php/nsalida/estado/1/1
                List<DetalleNotaSalida> notas = await webService.GET<List<DetalleNotaSalida>>("nsalida", String.Format("detalle/guia/{0}", idNotaSalida));
                return notas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        








    }
}
