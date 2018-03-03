using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.Componentes;
using Entidad;
using Modelo;

namespace Admeli.Configuracion.Modificar
{
    public partial class FormAsignarCorrelativoModificar : Form
    {
        private DocCorrelativo currentDocCorrelativo;

        private PuntoVentaModel puntoVentaModel = new PuntoVentaModel();
        private SucursalModel sucursalModel = new SucursalModel();
        private TipoDocumentoModel tipoDocumentoModel = new TipoDocumentoModel();
        private DocCorrelativoModel docCorrelativoModel = new DocCorrelativoModel();

        #region ========================= Constructor =========================
        public FormAsignarCorrelativoModificar()
        {
            InitializeComponent();
        }

        public FormAsignarCorrelativoModificar(DocCorrelativo currentDocCorrelativo)
        {
            InitializeComponent();
            this.currentDocCorrelativo = currentDocCorrelativo;
        }

        #endregion

        #region ================================== Root Load ==================================
        private void FormAsignarCorrelativoModificar_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        private void reLoad()
        {
            this.cargarSucursales();
            this.cargarTipoDocumento();
        }
        #endregion


        #region ================================== Loads ==================================
        private async void cargarSucursales()
        {
            try
            {

                sucursalBindingSource.DataSource = await sucursalModel.sucursales();
                cargarPuntoVenta();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Load", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarTipoDocumento()
        {
            try
            {

                tipoDocumentoBindingSource.DataSource = await tipoDocumentoModel.tipoDocumentoVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Load", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarPuntoVenta()
        {
            try
            {
                int sucursalID = (cbxSucursal.SelectedIndex == -1) ? 0 : Convert.ToInt32(cbxSucursal.SelectedValue);
                puntoDeVentaBindingSource.DataSource = await puntoVentaModel.puntoventas(sucursalID);
                cargarDatosModificar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Load", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cargarDatosModificar()
        {
            cbxSucursal.SelectedValue = currentDocCorrelativo.idSucursal;
            cbxTipoDocumento.SelectedValue = currentDocCorrelativo.idDocumento;
            textSerie.Text = currentDocCorrelativo.serie;
            textCorrelativoSiguiente.Text = currentDocCorrelativo.correlativoActual;
        }

        #endregion

        #region ============================ Decoration ============================
        private void FormAsignarCorrelativoModificar_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panel2, 157, 157, 157);
            drawShape.lineBorder(panel4, 157, 157, 157);
            drawShape.lineBorder(panel5, 157, 157, 157);
            drawShape.lineBorder(panel6, 157, 157, 157);
        }
        #endregion

        #region ========================== SAVE AND UPDATE ===========================
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            guardarSucursal();
        }

        private async void guardarSucursal()
        {
            if (!validarCampos()) return;
            try
            {
                crearObjetoSucursal();
                Response response = await docCorrelativoModel.modificar(currentDocCorrelativo);
                MessageBox.Show(response.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void crearObjetoSucursal()
        {
            currentDocCorrelativo.serie = textSerie.Text;
            currentDocCorrelativo.correlativoActual = textCorrelativoSiguiente.Text;
            currentDocCorrelativo.estado = Convert.ToInt32(chkActivoSucursal.Checked);
            currentDocCorrelativo.idSucursal = Convert.ToInt32(cbxSucursal.SelectedValue);
            currentDocCorrelativo.idDocumento = Convert.ToInt32(cbxTipoDocumento.SelectedValue);
            //currentDocCorrelativo.
        }

        private bool validarCampos()
        {
            if (textSerie.Text == "")
            {
                errorProvider1.SetError(textSerie, "Este campo esta bacía");
                textSerie.Focus();
                return false;
            }
            errorProvider1.Clear();

            if (textCorrelativoSiguiente.Text == "")
            {
                errorProvider1.SetError(textCorrelativoSiguiente, "Este campo esta bacía");
                textCorrelativoSiguiente.Focus();
                return false;
            }
            errorProvider1.Clear();
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
