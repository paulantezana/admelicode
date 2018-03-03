using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class DataSunat
    {
        
            public string RUC { get; set; }
            public string RazonSocial { get; set; }
            public string Telefono { get; set; }
            public string Condicion { get; set; }
            public string NombreComercial { get; set; }
            public string Tipo { get; set; }
            public string Inscripcion { get; set; }
            public string Estado { get; set; }
            public string Direccion { get; set; }
            public string SistemaEmision { get; set; }
            public string ActividadExterior { get; set; }
            public string SistemaContabilidad { get; set; }
            public string Oficio { get; set; }
            public string EmisionElectronica { get; set; }
            public string PLE { get; set; }
            public List<object> representantes_legales { get; set; }
            public List<object> cantidad_trabajadores { get; set; }
        }

        
    
}
