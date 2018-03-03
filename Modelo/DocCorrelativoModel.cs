using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class DocCorrelativoModel
    {
        private WebService webService = new WebService();

        public void guardar()
        {

        }

        public async Task<Response> modificar(DocCorrelativo param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/ventacorrelativo/modificar
                return await webService.POST<DocCorrelativo,Response>("ventacorrelativo", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminar()
        {

        }

        public async Task<RootObject<DocCorrelativo>> listartodoCorrelativo(int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/listartodocorrelativo/1/100
                RootObject<DocCorrelativo> docCorrelativo = await webService.GET<RootObject<DocCorrelativo>>("listartodocorrelativo", String.Format("{0}/{1}", page, items));
                return docCorrelativo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
