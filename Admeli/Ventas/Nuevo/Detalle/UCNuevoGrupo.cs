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
    public partial class UCNuevoGrupo : UserControl
    {
        private FormClienteNuevo formClienteNuevo;

        private LocationModel locationModel = new LocationModel();
        private ProveedorModel proveedorModel = new ProveedorModel();
        private ClienteModel clienteModel = new ClienteModel();
        private GrupoClienteModel grupoClienteModel = new GrupoClienteModel(); 
        private DocumentoIdentificacionModel documentoIdentificacionModel = new DocumentoIdentificacionModel();
        private List<LabelUbicacion> labelUbicaciones { get; set; }
        private UbicacionGeografica ubicacionGeografica { get; set; }
        private SunatModel sunatModel = new SunatModel();
        Response respuesta;
        UCClienteGeneral uCClienteGeneral;

        private List<DocumentoIdentificacion> documentoIdentificaciones;
        public UCNuevoGrupo()
        {
            InitializeComponent();
        }

        public UCNuevoGrupo(FormClienteNuevo formClienteNuevo)
        {
            InitializeComponent();
            this.formClienteNuevo = formClienteNuevo;
        }

        private void UCProveedorGeneral_Load(object sender, EventArgs e)
        {
           // this.txtNombreGrupo.LostFocus += new EventHandler(Validate_TextBox);
        }



        #region ========================== SAVE AND UPDATE ===========================
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
        }

        private async void VerificarNombreGCliente(string nombreGrupo)
        {
           respuesta = await grupoClienteModel.VerificarNombreGCliente(nombreGrupo);
        }






        private void btnClose_Click(object sender, EventArgs e)
        {
            this.formClienteNuevo.Close();
        }
        #endregion

        private void textTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {


            }
        }

        private void ll(object sender, EventArgs e)
        {

        }

        private void txtNombreGrupo_Click(object sender, EventArgs e)
        {


            
        }

        private void UCNuevoGrupo_Click(object sender, EventArgs e)
        {

            if (txtNombreGrupo.Text == "ddd")
                txtNombreGrupo.Text = "fff";
            else
                if (txtNombreGrupo.Text == "fff")
                txtNombreGrupo.Text = "ddd";
        }

        private void txtNombreGrupo_Leave(object sender, EventArgs e)
        {

            if(txtNombreGrupo.Text=="ddd")
                txtNombreGrupo.Text = "fff";
            else
                if(txtNombreGrupo.Text == "fff")
                txtNombreGrupo.Text = "ddd";
        }

        // TAREA hacer los cambios en todos los formularios de clientes y proveedores ver lo de paises 




    }
}
