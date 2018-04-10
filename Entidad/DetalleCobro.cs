using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class DetalleCobro
    {
        public int idDetalleCobro {get;set;}
        public decimal montoInteres {get;set;}
        public decimal monto {get;set;}
        public Fecha fechaCalendario {get;set;}
        public string observacion {get;set;}
        public string informacionVentaWeb {get;set;}
        public Fecha fechaPago{get;set;}
        public int estado {get;set;}
        public int idCobro {get;set;}
        public int idCajaSesion { get; set;}
}
}
