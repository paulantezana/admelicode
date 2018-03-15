using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{

    public class FechaInicio
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class FechaFin
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class DatosDescuentosOfertas
    {
        public string codigo { get; set; }
        public string descuento { get; set; }
        public FechaInicio fechaInicio { get; set; }
        public FechaFin fechaFin { get; set; }
        public string tipoDescuento { get; set; }
        public string tipo { get; set; }
        public string cantidadMinima { get; set; }
        public string cantidadMaxima { get; set; }
        public int estado { get; set; }
        public int idGrupoCliente { get; set; }
        public string nombreGrupo { get; set; }
        public int idSucursal { get; set; }
        public string nombre { get; set; }
        public int idAfectoProducto { get; set; }

    }

    public class ObjetosDescuentosOfertas
    {
        public int nro_registros { get; set; }
        public List<DatosDescuentosOfertas> datos { get; set; }
    }
    public class Datosconvertidos
    {
        public string codigo { get; set; }
        public string descuento { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string tipoDescuento { get; set; }
        public string tipo { get; set; }
        public string cantidadMinima { get; set; }
        public string cantidadMaxima { get; set; }
        public int estado { get; set; }
        public int idGrupoCliente { get; set; }
        public string nombreGrupo { get; set; }
        public int idSucursal { get; set; }
        public string nombre { get; set; }
        public int idAfectoProducto { get; set; }

     public static List<Datosconvertidos> convertir(List<DatosDescuentosOfertas> datoslist)
        {
            List<Datosconvertidos> datosnuevos = new List<Datosconvertidos>();
            foreach (DatosDescuentosOfertas datos in datoslist)
            {
                Datosconvertidos datosConvertidos = new Datosconvertidos(); 
                datosConvertidos.cantidadMaxima = datos.cantidadMaxima;
                datosConvertidos.codigo = datos.codigo;
                datosConvertidos.descuento = datos.descuento;
                datosConvertidos.fechaInicio = datos.fechaInicio.date;
                datosConvertidos.fechaFin = datos.fechaFin.date;
                datosConvertidos.tipoDescuento = datos.tipoDescuento;
                datosConvertidos.tipo = datos.tipo;
                datosConvertidos.cantidadMinima = datos.cantidadMinima;
            
                datosConvertidos.idGrupoCliente = datos.idGrupoCliente;
                datosConvertidos.nombreGrupo = datos.nombreGrupo;
                datosConvertidos.idSucursal = datos.idSucursal;
                datosConvertidos.nombreGrupo = datos.nombreGrupo;
                datosConvertidos.idSucursal = datos.idSucursal;
                datosConvertidos.nombre = datos.nombre;
                datosConvertidos.idAfectoProducto = datos.idAfectoProducto;

                datosnuevos.Add(datosConvertidos);
            }

            return datosnuevos;
            
       
         }


    }
   

}


