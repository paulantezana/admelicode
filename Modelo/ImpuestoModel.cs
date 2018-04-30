using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ImpuestoModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(Impuesto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/impuesto/guardar
                return await webService.POST<Impuesto,Response>("impuesto", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        //tipodocumentoimpuesto/guardar

        public async Task<Response> guardarImpuestoDocumento(ImpuestoComprobante param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/tipodocumentoimpuesto/guardar
                return await webService.POST<ImpuestoComprobante, Response>("tipodocumentoimpuesto", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // InsertarImpuestosProductosArray iproductoarray
        public async Task<Response> InsertarImpuestosProductosArray(List<Dictionary<string, int>> list)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/impuesto/guardar
                return await webService.POST <List < Dictionary<string, int>> , Response >("iproductoarray", list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Impuesto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/impuesto/modificar
                return await webService.POST<Impuesto,Response>("impuesto", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> desactivar(Impuesto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/impuesto/desactivar
                return await webService.POST<Impuesto,Response>("impuesto", "desactivar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Impuesto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/impuesto/eliminar
                return await webService.POST<Impuesto,Response>("impuesto", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // prod/21/suc/1

        public async Task<List<Impuesto>> impuestoProductoSucursal(int idProducto, int idSucursal)
        {
            try
            {
                // localhost/admeli/xcore/services.php/impuestos/prod/21/suc/1
                List<Impuesto> list = await webService.GET<List<Impuesto>>("impuestos", String.Format("prod/{0}/suc/{1}", idProducto, idSucursal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Impuesto>> impuestos(int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/impuestos/estado/1/100
                RootObject<Impuesto> impuestos = await webService.GET<RootObject<Impuesto>>("impuestos", String.Format("estado/{0}/{1}", page, items));
                return impuestos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //impcompraproductoordencompra

        public async Task<List<OrdenCompraImpuesto>> impcompraproductoordencompra(int idordenCompra, int idSucursal)
        {
            try
            {
                // http://localhost:8080/admeli/xcore/services.php/impcompraproductoordencompra/ordencomp/5/suc/1
                List<OrdenCompraImpuesto> impuestos = await webService.GET<List<OrdenCompraImpuesto>>("impcompraproductoordencompra", String.Format("ordencomp/{0}/suc/{1}", idordenCompra, idSucursal));
                return impuestos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //verificar/nombre/ImpuestoGeneral/siglas/IGV/impuesto/0
        public async Task<List<Impuesto>> verificarNombreSiglasImpuesto(string nombreImpuesto,string siglasImpuesto)
        {
            try
            {
                //http://localhost:8085/admeli/xcore/services.php/verificar/nombre/Impuesto/siglas/IGV/impuesto/0
                List<Impuesto> impuesto = await webService.GET<List<Impuesto>>("verificar", String.Format("nombre/{0}/siglas/{1}/impuesto/0", nombreImpuesto, siglasImpuesto));
                return impuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //iproducto/1/1
        public async Task<ImpuestoProductoTodo> impuestoProductoTodo(int idPresentacion,int idSucursal)
        {
            try
            {
                //http://localhost:8085/admeli/xcore/services.php/iproducto/2/1
                ImpuestoProductoTodo impuestoProducto = await webService.GET<ImpuestoProductoTodo>("iproducto", String.Format("{0}/{1}", idPresentacion, idSucursal));
                return impuestoProducto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //iproducto
        public async Task<Response> actualizarImpuestoProducto(List<Impuesto> impuestosEnviados,int idProducto,int idSucursal)
        {
            try
            {
                string stringImpuestos = "[{";
                for (int i = 0; i < impuestosEnviados.Count(); i++)
                {
                    stringImpuestos += "\"id" + i + "\":" + impuestosEnviados[i].idImpuesto + ",";
                }
                if (impuestosEnviados.Count() <= 0) { stringImpuestos += "},"; }
                else { stringImpuestos = stringImpuestos.Substring(0, stringImpuestos.Length - 1) + "},"; }

                stringImpuestos += "{\"idProducto\":" + idProducto + "},";
                stringImpuestos += "{\"idSucursal\":" + idSucursal + "}]";
                //http://localhost:8085/admeli/xcore/services.php/iproducto
                Response respuesta = await webService.PostSendTexto<Response>("iproducto", stringImpuestos);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //impuestos31
        public async Task<List<ImpuestosSiglas>> listarImpuestoIdImpuestoNombreSiglasByActivos()
        {
            try
            {
                //http://localhost:8085/admeli/xcore/services.php/impuestos31
                List<ImpuestosSiglas> impuesto = await webService.GET<List<ImpuestosSiglas>>("impuestos31");
                return impuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //listarImpuestosByIdSucursalEIdTipoDocumento
        //tipodocumentoimpuesto/sucursal/:ids/tipodoc/:idtd

        
        public async Task<List<ImpuestoDocumento >> impuestoTipoDoc(int idSucursal,int idTipoDoc)
        {
            try
            {
                //http://localhost:8085/admeli/xcore/services.php/tipodocumentoimpuesto/sucursal/:ids/tipodoc/:idtd
                List < ImpuestoDocumento > impuestoProducto = await webService.GET<List<ImpuestoDocumento>>("tipodocumentoimpuesto", String.Format("sucursal/{0}/tipodoc/{1}", idSucursal, idTipoDoc));
                return impuestoProducto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Impuesto>> listarImpuesto()
        {
            try
            {
                //http://localhost:8085/admeli/xcore/services.php/impuestos/estado/1
                List<Impuesto> impuestoProducto = await webService.GET<List<Impuesto>>("impuestos", String.Format("estado/1"));
                return impuestoProducto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //  impuestos/prod/4/suc/1
        public async Task<List<ImpuestoProducto>> impuestoProducto(int idProducto, int idSucursal )
        {
            try
            {
                //http://localhost:8085/admeli/xcore/services.php/impuestos/estado/1
                List<ImpuestoProducto> impuestoProducto = await webService.GET<List<ImpuestoProducto>>("impuestos", String.Format("prod/{0}/suc/{1}", idProducto, idSucursal));
                return impuestoProducto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
