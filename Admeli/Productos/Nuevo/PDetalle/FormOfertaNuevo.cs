using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.Configuracion.Nuevo;
using Entidad;
using Modelo;

namespace Admeli.Productos.Nuevo.PDetalle
{
    public partial class FormOfertaNuevo : Form
    {
        private FormProductoNuevo formProductoNuevo;
        private Oferta currentOferta { get; set; }
        private int currentIDOferta { get; set; }
        private bool nuevo { get; set; }

        private SucursalModel sucursalModel = new SucursalModel();
        private GrupoClienteModel grupoClienteModel = new GrupoClienteModel();
        private ProductoModel productoModel = new ProductoModel();
        private OfertaModel ofertaModel = new OfertaModel();

        #region ================================ Constructor ================================
        public FormOfertaNuevo()
        {
            InitializeComponent();
        }

        public FormOfertaNuevo(FormProductoNuevo formProductoNuevo)
        {
            InitializeComponent();
            this.formProductoNuevo = formProductoNuevo;
        }

        public FormOfertaNuevo(FormProductoNuevo formProductoNuevo, Oferta currentOferta)
        {
            InitializeComponent();
            this.formProductoNuevo = formProductoNuevo;
            this.currentOferta = currentOferta;
            this.currentIDOferta = currentOferta.idOfertaProductoGrupo;
        }
        #endregion

        #region ================================== Root load ==================================
        private void FormOfertaNuevo_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        internal void reLoad()
        {
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;

            cargarProducto21();
            cargarGrupoCliente();
            cargarSucursales();
        } 
        #endregion

        #region ================================ Loads ================================
        private async void cargarSucursales()
        {
            try
            {
                sucursalBindingSource.DataSource = await sucursalModel.sucursalesPrecio(formProductoNuevo.currentIDProducto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarGrupoCliente()
        {
            try
            {
                grupoClienteBindingSource.DataSource = await grupoClienteModel.gclientes21();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarProducto21()
        {
            try
            {
                productoBindingSource.DataSource = await productoModel.productos21(formProductoNuevo.currentIDProducto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        } 
        #endregion

        #region ====================== Form Add Registers ======================
        private void btnAddGrupoCliente_Click(object sender, EventArgs e)
        {
            FormGrupoClienteNuevo formGrupoCliente = new FormGrupoClienteNuevo();
            formGrupoCliente.ShowDialog();
        }

        private void btnAddSucursal_Click(object sender, EventArgs e)
        {
            FormSucursalNuevo formSucursal = new FormSucursalNuevo();
            formSucursal.ShowDialog();
        } 
        #endregion

        #region ========================== SAVE AND UPDATE ===========================
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            executeGuardar();
        }

        private async void executeGuardar()
        {
            if (!validarCampos()) return;
            try
            {
                crearObjetoSucursal();
                if (nuevo)
                {
                    Response response = await ofertaModel.guardar(currentOferta);
                    MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Response response = await ofertaModel.modificar(currentOferta);
                    MessageBox.Show(response.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void crearObjetoSucursal()
        {
            currentOferta = new Oferta();
            if (!nuevo) currentOferta.idOfertaProductoGrupo = currentIDOferta; // Llenar el id categoria cuando este en esdo modificar

            currentOferta.codigo = textCodigoOferta.Text;
            // currentOferta.fechaFin = dtpFechaFin.Value;

        }

        private bool validarCampos()
        {
            if (textCodigoOferta.Text == "")
            {
                errorProvider1.SetError(textCodigoOferta, "Este campo esta bacía");
                textCodigoOferta.Focus();
                return false;
            }
            errorProvider1.Clear();

            if (textDescuento.Text == "")
            {
                errorProvider1.SetError(textDescuento, "Este campo esta bacía");
                textDescuento.Focus();
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
