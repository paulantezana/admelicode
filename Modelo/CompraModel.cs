using Entidad;
using Modelo.Recursos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class CompraModel
    {
        private WebService webService = new WebService();


        public async Task<Response> ralizarCompra(Compra compra, List<DetalleCompra> detalleCompra, NotaEntrada notaEntrada, Pago pago, PagoCompra pagoCompra)
        {
            try
            {
                string stringCompra = JsonConvert.SerializeObject(compra);
                string stringDetalleCompra = JsonConvert.SerializeObject(detalleCompra);
                string stringNotaEntrada = JsonConvert.SerializeObject(notaEntrada);
                string stringPago = JsonConvert.SerializeObject(pago);
                string stringPagoCompra = JsonConvert.SerializeObject(pagoCompra);

                String sendData = "{" + string.Format("{0,2,3,4}", stringCompra, stringDetalleCompra, stringNotaEntrada, stringPago, stringPagoCompra) +  "}";

                // localhost:8080/admeli/xcore2/xcore/services.php/compra/guardartotal
                return await webService.POST<String, Response>("compra", "guardartotal", sendData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Response> guardar(Producto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/compra/guardar
                return await webService.POST<Producto,Response>("compra", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Compra param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/compra/modificar
                return await webService.POST<Compra,Response>("compra", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> anular(Compra param)
        {
            try
            {
                // localhost/admeli/xcore/services.php/compra/anular
                return await webService.POST<Compra,Response>("compra", "anular", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<RootObject<Compra>> getByPersonalEstado(int idSucursal, int idPersonal, string idEstado, int page, int items)
        {
            try
            {
                RootObject<Compra> ordenCompra = await webService.GET<RootObject<Compra>>("compras", String.Format("sucursal/{0}/personal/{1}/estado/{2}/{3}/{4}", idSucursal,idPersonal,idEstado, page, items));
                return ordenCompra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Compra>> comprasUltimas(int idPersonal, int idSucursal, int todos, int gerente)
        {
            try
            {
                // www.lineatienda.com/services.php/comprasultimas/3/1/1/1
                List<Compra> comprasultimas = await webService.GET<List<Compra>>("comprasultimas", String.Format("{0}/{1}/{2}/{3}", idPersonal, idSucursal, todos, gerente));
                return comprasultimas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
