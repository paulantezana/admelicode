using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.AlmacenBox.fecha;
using Admeli.AlmacenBox.Nuevo;
using Admeli.Componentes;
using Entidad;
using Entidad.Configuracion;
using Modelo;


namespace Admeli.Ventas.buscar
{
    public partial class FormBuscarCotizacion : Form
    {


        CotizacionModel cotizacionModel = new CotizacionModel(); 
        private List<CotizacionBuscar> listCotizacion { get; set; }

        public CotizacionBuscar currentCotizacion { get; set; }

            
        public FormBuscarCotizacion( )
        {
            InitializeComponent();
                     
        }

      

        #region ================================ Root Load ================================

        private void FormNotaSalidaNew_Load(object sender, EventArgs e)
        {
            reLoad();

        }
        private void reLoad()
        {
            cargarListaCotizacione();

          

        }





        #endregion

        #region ============================== Load ==============================


       

        private async void cargarListaCotizacione()
        {

           
           listCotizacion= await cotizacionModel.listaCotizaciones(ConfigModel.sucursal.idSucursal);
           cotizacionBuscarBindingSource.DataSource = listCotizacion;
        }
        #endregion

       

       

       

       

        private void dgvNotaSalida_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionarCotizacion();
        }

        private void seleccionarCotizacion()
        {
            // Verificando la existencia de datos en el datagridview
            if (dgvNotaSalida.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dgvNotaSalida.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idCotizacion = Convert.ToInt32(dgvNotaSalida.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview

            currentCotizacion = listCotizacion.Find(x => x.idCotizacion == idCotizacion); // Buscando la categoria en las lista de categorias
            this.Close();
         
        }

       

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            seleccionarCotizacion();
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            BindingList<CotizacionBuscar> filtered = new BindingList<CotizacionBuscar>(listCotizacion.Where(obj => obj.rucDni.Contains(txtCliente.Text.Trim()) || obj.nombreCliente.ToUpper().Contains(txtCliente.Text.ToUpper())).ToList());
            cotizacionBuscarBindingSource.DataSource = filtered;
            dgvNotaSalida.Update();
        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            BindingList<CotizacionBuscar> filtered = new BindingList<CotizacionBuscar>(listCotizacion.Where(obj => obj.direccion.Trim().ToUpper() .Contains(txtDireccion.Text.Trim().ToUpper())).ToList());
            cotizacionBuscarBindingSource.DataSource = filtered;
            dgvNotaSalida.Update();
        }

        private void txtMoneda_TextChanged(object sender, EventArgs e)
        {
            BindingList<CotizacionBuscar> filtered = new BindingList<CotizacionBuscar>(listCotizacion.Where(obj => obj.moneda.Trim().ToUpper().Contains(txtMoneda.Text.Trim().ToUpper())).ToList());
            cotizacionBuscarBindingSource.DataSource = filtered;
            dgvNotaSalida.Update();
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            BindingList<CotizacionBuscar> filtered = new BindingList<CotizacionBuscar>(listCotizacion.Where(obj => obj.descuento.Trim().ToUpper().Contains(txtDescuento.Text.Trim().ToUpper())).ToList());
            cotizacionBuscarBindingSource.DataSource = filtered;
            dgvNotaSalida.Update();
        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
            BindingList<CotizacionBuscar> filtered = new BindingList<CotizacionBuscar>(listCotizacion.Where(obj => obj.subTotal.Trim().ToUpper().Contains(txtSubTotal.Text.Trim().ToUpper())).ToList());
            cotizacionBuscarBindingSource.DataSource = filtered;
            dgvNotaSalida.Update();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            BindingList<CotizacionBuscar> filtered = new BindingList<CotizacionBuscar>(listCotizacion.Where(obj => obj.total.Trim().ToUpper().Contains(txtTotal.Text.Trim().ToUpper())).ToList());
            cotizacionBuscarBindingSource.DataSource = filtered;
            dgvNotaSalida.Update();
        }



        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, txtDescuento.Text);
        }

        private void txtSubTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, txtSubTotal.Text);
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, txtTotal.Text);
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
