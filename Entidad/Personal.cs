using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Personal
    {
        public int idPersonal { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public Fecha fechaNacimiento { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string sexo { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string direccion { get; set; }
        public int estado { get; set; }
        public int idUbicacionGeografica { get; set; }
        public int idDocumento { get; set; }
    }
   public class Permisos
    {
        public int idRegistro { get; set; }
        public int idDepartamento { get; set; }
        public string nombre { get; set; }
        public int idPadre { get; set; }
        public bool estado { get; set; }
    }



    public class AsignarCaja
    {
        public int idCaja { get; set; }
        public int idPersonal { get; set; }
        public int estado { get; set; }
    }

    public class PersonalAlmacen
    {
        public int idAlmacen { get; set; }
        public int idPersonal { get; set; }
        public bool estado { get; set; }
    }

    public class AsignarPuntoCompra
    {
        public int idPuntoCompra { get; set; }
        public int idPersonal { get; set; }
        public int estado { get; set; }
    }

    public class AsignarPuntoVenta
    {
        public int idPuntoVenta { get; set; }
        public int idPersonal { get; set; }
        public bool estado { get; set; }
    }

    public class AsignarPuntoAdministracion
    {
        public int idPuntoAdministracion { get; set; }
        public int idPersonal { get; set; }
        public int estado { get; set; }
    }

    public class AsignarPuntoGerencia
    {
        public int idPuntoGerencia { get; set; }
        public int idPersonal { get; set; }
        public int estado { get; set; }
    }

    public class Responsabilidades
    {
        public AsignarCaja asignarCaja { get; set; }
        public List<PersonalAlmacen> personalAlmacen { get; set; }
        public AsignarPuntoCompra asignarPuntoCompra { get; set; }
        public List<AsignarPuntoVenta> asignarPuntoVenta { get; set; }
        public AsignarPuntoAdministracion asignarPuntoAdministracion { get; set; }
        public AsignarPuntoGerencia asignarPuntoGerencia { get; set; }
    }

}
