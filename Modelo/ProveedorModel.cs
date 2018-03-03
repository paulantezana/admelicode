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
    public class ProveedorModel
    {
        private WebService webService = new WebService();
        private LocationModel locationModel = new LocationModel();

        public async Task<Response> guardar(UbicacionGeografica ubicacionGeografica, Proveedor param)
        {
            try
            {
                // Obteniendo de la ubicacion geografica del sucursal
                Response res = await locationModel.guardarUbigeo(ubicacionGeografica);
                param.idUbicacionGeografica = res.id;

                // localhost:8080/admeli/xcore2/xcore/services.php/proveedor/guardar
                return await webService.POST<Proveedor,Response>("proveedor", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(UbicacionGeografica ubicacionGeografica, Proveedor param)
        {
            try
            {
                // Obteniendo de la ubicacion geografica del sucursal
                Response res = await locationModel.guardarUbigeo(ubicacionGeografica);
                param.idUbicacionGeografica = res.id;

                // localhost:8080/admeli/xcore2/xcore/services.php/proveedor/modificar
                return await webService.POST<Proveedor,Response>("proveedor", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Proveedor param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/proveedor/modificar
                return await webService.POST<Proveedor,Response>("proveedor", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Proveedor param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/proveedor/eliminar
                return await webService.POST<Proveedor,Response>("proveedor", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Proveedor>> proveedoreslike(int page, int items, string like = "")
        {
            try
            {
                RootObject<Proveedor> proveedores = await webService.GET<RootObject<Proveedor>>("proveedoreslike", String.Format("razonsocial/{0}/{1}/{2}", like, page, items));
                return proveedores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Proveedor>> proveedores( int page, int items, string estado = "estado")
        {
            try
            {
                RootObject<Proveedor> proveedores = await webService.GET<RootObject<Proveedor>>("proveedores", String.Format("{0}/{1}/{2}", estado, page, items));
                return proveedores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
