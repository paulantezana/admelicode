using Entidad;
using Entidad.Location;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class SucursalModel
    {
        private WebService webService = new WebService();

        private LocationModel locationModel = new LocationModel();


        public async Task<Response> guardar(UbicacionGeografica ubicacionGeografica, Sucursal param)
        {
            try
            {
                // Obteniendo de la ubicacion geografica del sucursal
                Response res = await locationModel.guardarUbigeo(ubicacionGeografica);
                param.idUbicacionGeografica = res.id;

                // localhost/admeli/xcore2/xcore/services.php/sucursal/guardar
                return await webService.POST<Sucursal,Response>("sucursal", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> modificar(UbicacionGeografica ubicacionGeografica, Sucursal param)
        {
            try
            {
                // Obteniendo de la ubicacion geografica del sucursal
                Response res = await locationModel.guardarUbigeo(ubicacionGeografica);
                param.idUbicacionGeografica = res.id;

                // localhost/admeli/xcore2/xcore/services.php/sucursal/modificar
                return await webService.POST<Sucursal,Response>("sucursal", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Sucursal param)
        {
            try
            {
                // localhost/admeli/xcore2/xcore/services.php/sucursal/eliminar
                return await webService.POST<Sucursal,Response>("sucursal", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> existeSucursal(string nombre, int idSucursal)
        {
            try
            {
                // localhost/admeli/xcore2/xcore/services.php/sucursal/nombre/cusco/ids/0
                return await webService.GET<Response>("sucursal", String.Format("nombre/{0}/ids/{1}", nombre,idSucursal));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Sucursal>> listarSucursalesActivos()
        {
            try
            {
                // www.lineatienda.com/services.php/listarsucursalesactivos
                List<Sucursal> listSucursal = await webService.GET<List<Sucursal>>("listarsucursalesactivos");
                return listSucursal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Sucursal>> sucursales(int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/sucursales/estado/1/100
                RootObject<Sucursal> sucursales = await webService.GET<RootObject<Sucursal>>("sucursales", String.Format("estado/{0}/{1}", page, items));
                return sucursales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Sucursal>> sucursales()
        {
            try
            {
                // www.lineatienda.com/services.php/sucursales/id/nombre/estado/1
                List<Sucursal> listSucursal = await webService.GET<List<Sucursal>>("sucursales", "id/nombre/estado/1");
                return listSucursal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Sucursal>> sucursalesPrecio(int idProducto)
        {
            try
            {
                // localhost/admeli/xcore/services.php/sucursales/precio/17
                List<Sucursal> listSucursal = await webService.GET<List<Sucursal>>("sucursales", String.Format("precio/{0}",idProducto));
                return listSucursal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
