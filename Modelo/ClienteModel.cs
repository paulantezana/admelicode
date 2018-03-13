using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ClienteModel
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

        public async Task<RootObject<Cliente>> clientes(int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/clientes/estado/1/100
                RootObject<Cliente> clientes = await webService.GET<RootObject<Cliente>>("clientes", String.Format("estado/{0}/{1}", page, items));
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Cliente>> buscarClientesLike(string like, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/buscarclienteslike/nombre/Jhon/1/100
                RootObject<Cliente> clientes = await webService.GET<RootObject<Cliente>>("buscarclienteslike", String.Format("nombre/{0}/{1}/{2}",like, page, items));
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Response> eliminar(Cliente currentCliente)
        {
            throw new NotImplementedException();
        }

        public Task<Response> desactivar(Cliente currentCliente)
        {
            throw new NotImplementedException();
        }
        public async Task<List<GrupoCliente>> listarGrupoClienteIdGCNombreByActivos()
        {
            try
            {
                // www.lineatienda.com/services.php/gclientes21
                List<GrupoCliente> clientes = await webService.GET<List<GrupoCliente>>("gclientes21");
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
         public async Task<List<GrupoClienteC>> documentoidentificaciones()
        {
            try
            {
                // www.lineatienda.com/services.php/gclientes21
                List<GrupoClienteC> clientes = await webService.GET<List<GrupoClienteC>>("gclientes21", "");
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
