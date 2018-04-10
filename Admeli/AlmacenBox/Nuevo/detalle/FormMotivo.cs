using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;
using Entidad.Location;
using Entidad;
using Admeli.Componentes;
using Admeli.Compras.Nuevo;



namespace Admeli.AlmacenBox.Nuevo.detalle
{
    public partial class FormMotivo : Form
    {
       

        private LocationModel locationModel = new LocationModel();
        private ProveedorModel proveedorModel = new ProveedorModel();

        private List<LabelUbicacion> labelUbicaciones { get; set; }
        public  UbicacionGeografica ubicacionGeografica { get; set; }
        private SunatModel sunatModel=new SunatModel();
        private bool bandera;
   
        private FormProveedorNuevo formProveedorNuevo;
        public int idUbicacionGeografia { get; set; }
        
        public string cadena = "";
        public FormMotivo()
        {
            InitializeComponent();

            
        }

        public FormMotivo(FormProveedorNuevo formProveedorNuevo)
        {
            InitializeComponent();
            this.formProveedorNuevo = formProveedorNuevo;
        }

        private void FormTransporteNew_Load(object sender, EventArgs e)
        {

        }
    }
}
