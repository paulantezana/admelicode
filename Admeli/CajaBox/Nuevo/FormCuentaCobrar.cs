using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli.CajaBox.Nuevo
{
    public partial class FormCuentaCobrar : Form
    {
        private DatoCuentaCobrar currentCuentaCobrar;
        private int currentIdCliente;
        private CobroModel cobroModel = new CobroModel();
        private List<Cobro> listaCobros = new List<Cobro>();
        private Cobro currentCobro = new Cobro();
        private List<DetalleCobro> listaDetalleCobros = new List<DetalleCobro>();
        private DetalleCobro currentDetalleCobro = new DetalleCobro();
        
        public FormCuentaCobrar()
        {
            InitializeComponent();
        }
        
        public FormCuentaCobrar(DatoCuentaCobrar currentCuentaCobrar)
        {
            InitializeComponent();
            this.currentCuentaCobrar = currentCuentaCobrar;
            this.currentIdCliente = currentCuentaCobrar.idCliente;
            this.lbNombreCliente.Text = currentCuentaCobrar.nombreCliente;
            this.chkMostrarTodos.Checked = false;
            this.reLoad();
        }

        private async  void reLoad()
        {
            //Cargar Las Cuentas por cobrar del Cliente
            if (chkMostrarTodos.Checked)
            {
                listaCobros = await cobroModel.cobrosCliente(currentIdCliente, ConfigModel.sucursal.idSucursal, 1);
            }
            else
            {
                listaCobros = await cobroModel.cobrosCliente(currentIdCliente, ConfigModel.sucursal.idSucursal, 0);
            }
            cobroBindingSource.DataSource = listaCobros;
            dgvCobros.Refresh();
        }
        private void loadState(bool state)
        {
            panelBody.Enabled = state;
            panelFooder.Enabled = state;
        }
        private void FormCuentaCobrar_Load(object sender, EventArgs e)
        {
            loadState(false);
            try
            {
                // Formato de celdas

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadState(true);
            }
        }

        private void chkMostrarTodos_OnChange(object sender, EventArgs e)
        {
            MessageBox.Show("A");
            reLoad();
        }

        private void dgvCobros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Mostrar Detalles del Cobro en el panel Izquierdo
            executeDetalles();
        }
        private async void executeDetalles()
        {
            // Verificando la existencia de datos en el datagridview
            if (dgvCobros.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dgvCobros.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idCobro= Convert.ToInt32(dgvCobros.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview
            currentCobro = listaCobros.Find(x => x.idCobro == idCobro); //Buscando el registro especifico en la lista de registros

            //Mostrar Detalles del Cobro
            listaDetalleCobros = await cobroModel.detalleCobro(currentCobro.idCobro);

            // Mostrando el formulario de modificacion
            detalleCobroBindingSource.DataSource = listaDetalleCobros;
            dgvDetalleCobro.Refresh();
        }

        private void btnNuevoDetalleCobro_Click(object sender, EventArgs e)
        {
            //Verificar que se inicio caja
            if (!ConfigModel.cajaIniciada)
            {
                MessageBox.Show("Ud. no inició caja", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //Nuevo Detalle Cobro
            FormDetalleCobroNuevo formDetalleCobroNuevo = new FormDetalleCobroNuevo(currentCobro);
            formDetalleCobroNuevo.ShowDialog();
        }
    }
}
