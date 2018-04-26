using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ReporteModel
    {
        private WebService webService = new WebService();
        

        public async Task<T> existenciaProductos<T>(string nombreProducto,int idcategoria,int idsucursal,int idalmacen,int pagina)
        {
            try
            {
                http://localhost:8085/admeli/xcore/services.php/reporteexistenciaproductos/nombre/xperi/categoria/0/sucursal/0/almacen/0/pagina/1
                return await webService.GET<T>("reporteexistenciaproductos", String.Format("nombre/{0}/categoria/{1}/sucursal/{2}/almacen/{3}/pagina/{4}",nombreProducto,idcategoria,idsucursal,idalmacen,pagina));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<T> comprasSucursal<T>(string idSucursal,int idPuntoVenta,string mes,string anio)
        {
            try
            {
                //http://www.lineatienda.com/services.php/compras/sucursal/1/puntoVenta/1/mes/00/anio/2018
                return await webService.GET<T>("compras", String.Format("sucursal/{0}/puntoVenta/{1}/mes/{2}/anio/{3}",idSucursal,idPuntoVenta,mes,anio));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
