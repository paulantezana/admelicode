using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Util
{
    public class Fecha
    {
        public DateTime date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
        public string sdate;
        
        public override string ToString()
        {
            return date.ToString();
        }

        public string Sdate
        {
            get { return date.ToString(); }
            set { sdate = value; }
        }
    }
    public class Fecharecibida
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }

    }
}
