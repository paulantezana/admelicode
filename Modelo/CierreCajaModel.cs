using Entidad;
using Entidad.Configuracion;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class CierreCajaModel
    {
        private WebService webService = new WebService();

        /*
        public async Task<Response> guardar(UbicacionGeografica ubicacionGeografica, Almacen param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/almacen/guardar
                return await webService.POSTSend("almacen", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(UbicacionGeografica ubicacionGeografica, Almacen param)
        {
            try
            {
                // Obteniendo de la ubicacion geografica del sucursal
                Response res = await locationModel.guardarUbigeo(ubicacionGeografica);
                param.idUbicacionGeografica = res.id;

                // localhost:8080/admeli/xcore2/xcore/services.php/almacen/modificar
                return await webService.POSTSend("almacen", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> desactivar(Almacen param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/almacen/desactivar
                return await webService.POSTSend("almacen", "desactivar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Almacen param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/almacen/eliminar
                return await webService.POSTSend("almacen", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */

        public async Task<Response> cierreCaja<T>(T param)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/cierrecaja/guardar
                return await webService.POST<T, Response>("cierrecaja", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> cierreCajaDetalle(Ingreso param)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/detallecierrecaja/guardar
                return await webService.POST<Ingreso, Response>("detallecierrecaja", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<CierreCaja>> cierreCajas(int idPersonal, int idSucursal, int page, int items)
        {
            try
            {
                // localhost/admeli/xcore/services.php/cierrecajas/personal/0/sucursal/0/1/15
                RootObject<CierreCaja> dataRoot = await webService.GET<RootObject<CierreCaja>>("cierrecajas", String.Format("personal/{0}/sucursal/{1}/{2}/{3}", idPersonal, idSucursal, page, items));
                return dataRoot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Moneda>> ingresoMenosEgreso(int idMedioPago, int idCajaSesion)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/cierrecajaingresomenosegreso/mediopago/1/cajasesion/3
                List<Moneda> list = await webService.GET<List<Moneda>>("cierrecajaingresomenosegreso", String.Format("mediopago/{0}/cajasesion/{1}", idMedioPago, idCajaSesion));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /*
        public async Task<List<Almacen>> almacenes(int estado = 1)
        {
            try
            {
                // www.lineatienda.com/services.php/almacenes/id/nombre/estado/1
                List<Almacen> list = await webService.GETLis<Almacen>("almacenes", String.Format("id/nombre/estado/{0}", estado));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Almacen>> almacenesPorSucursales(int idSucursal)
        {
            try
            {
                // www.lineatienda.com/services.php/almacenesporsucursal/sucursal/0
                List<Almacen> list = await webService.GETLis<Almacen>("almacenesporsucursal", String.Format("sucursal/{0}", idSucursal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */
    }
}
