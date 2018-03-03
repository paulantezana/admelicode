using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class PrecioModel
    {
        private WebService webService = new WebService();

        public async Task<List<Precio>> precioProducto(int idProducto)
        {
            try
            {
                // www.lineatienda.com/services.php/precio/producto/445
                List<Precio> list = await webService.GET<List<Precio>>("precio", String.Format("producto/{0}", idProducto));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Response> desactivar(Precio currentPrecio)
        {
            throw new NotImplementedException();
        }
    }
}
