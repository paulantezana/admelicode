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

namespace Admeli.Ventas.Nuevo.Detalle
{
    public partial class UCOfertaGeneral : UserControl
    {
        private FormClienteNuevo formClienteNuevo;

        private LocationModel locationModel = new LocationModel();
        private ProveedorModel proveedorModel = new ProveedorModel();
        private ClienteModel clienteModel = new ClienteModel();

        private DocumentoIdentificacionModel documentoIdentificacionModel = new DocumentoIdentificacionModel();
        private List<LabelUbicacion> labelUbicaciones { get; set; }
        private UbicacionGeografica ubicacionGeografica { get; set; }
        private SunatModel sunatModel = new SunatModel();
        private bool bandera;
        private DataSunat dataSunat;
        private RespuestaSunat respuestaSunat;
        public List<GrupoCliente> grupoClientes;
        private UCNuevoGrupo uCNuevoGrupo;
        private List<DocumentoIdentificacion> documentoIdentificaciones;

        public bool lisenerKeyEvents { get; internal set; }
        public UCOfertaGeneral()
        {
            InitializeComponent();
        }

        public UCOfertaGeneral(FormClienteNuevo formClienteNuevo)
        {
            InitializeComponent();
            this.formClienteNuevo = formClienteNuevo;

        }

        private void UCProveedorGeneral_Load(object sender, EventArgs e)
        {
            this.reLoad();

        }

        #region ================================= Loads =================================
      

        internal async void reLoad()
        {
            
           
        }


      



        #endregion


        #region ================== Formando los niveles de cada pais ==================
       

       
       

        

        
        

       

        #endregion

       

        #region ========================== SAVE AND UPDATE ===========================
       

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.formClienteNuevo.Close();
        }
        #endregion

        private void textTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        // TAREA hacer los cambios en todos los formularios de clientes y proveedores ver lo de paises 
       

        private string concidencias(string direccion)
        {
            int lenght = direccion.Length;
            if (lenght > 20)
            {
                int i = direccion.LastIndexOf('-');

                string ff = direccion.Substring(0, i);
                i = ff.LastIndexOf('-');
                string ff1 = ff.Substring(0, i);


                i = ff1.LastIndexOf(' ');
                string hhh = ff1.Substring(0, i);
                i = hhh.LastIndexOf(' ');
                string hh1 = hhh.Substring(0, i);
                return hh1;
            }
            else
                return "";

        }
        public async void Ver(string aux)
        {
            try
            {
                respuestaSunat = await sunatModel.obtenerDatos(aux);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "consulta sunat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void btnGrupoNuevo_Click(object sender, EventArgs e)
        {
           this.formClienteNuevo.togglePanelMain("Nuevorupo");      
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void UCOfertaGeneral_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.bottomLine(panelHeader);
            drawShape.lineBorder(panel12, 157, 157, 157);
            drawShape.lineBorder(panel2, 157, 157, 157);
            drawShape.lineBorder(panel3, 157, 157, 157);
        }
    }
}
