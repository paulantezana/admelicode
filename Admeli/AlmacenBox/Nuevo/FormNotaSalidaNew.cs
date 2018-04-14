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
using Admeli.AlmacenBox.buscar;
using Admeli.Componentes;
using Entidad;
using Entidad.Configuracion;
using Modelo;


namespace Admeli.AlmacenBox.Nuevo
{
    public partial class FormNotaSalidaNew : Form
    {


        // servicios necesarios

        AlmacenModel AlmacenModel = new AlmacenModel();
        ProductoModel productoModel = new ProductoModel();
        FechaModel fechaModel = new FechaModel();
        NotaSalidaModel notaSalidaModel = new NotaSalidaModel();
        PresentacionModel presentacionModel = new PresentacionModel();

        AlternativaModel alternativaModel = new AlternativaModel();
        // objetos que cargan a un inicio
        private  List<Producto> listProducto { get; set; }
        private List<Presentacion> listPresentacion { get; set; }
        private List<Almacen> listAlmacen { get; set; }
        private List<AlmacenCorrelativo> listCorrelativoA { get; set; }
        private List<DetalleNotaSalida> listDetalleNotaSalida { get; set; }

        // entidadades auxiliares

        private bool  nuevo { get; set; }
        private string formato { get; set; }
        private int nroDecimales = 2;
        private FechaSistema fechaSistema { get; set; }



        //objetos en tiempo real

       private Almacen currentAlmacen { get; set; }
       private AlmacenCorrelativo currentCorrelativoA { get; set; }
       private Producto currentProducto { get; set; }
       private Presentacion currentPresentacion { get; set; }

        private VentasNSalida currentVenta { get; set; }

        public FormNotaSalidaNew()
        {
            InitializeComponent();
            this.nuevo = true;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();
      

        }
        public FormNotaSalidaNew(Compra currentCompra)
        {
            InitializeComponent();
            this.nuevo = false;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();
        }

        #region=======================metodos de apoyo
        private string darformato(object dato)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
        }

        private double toDouble(string texto)
        {
            return double.Parse(texto, CultureInfo.GetCultureInfo("en-US")); ;
        }
        #endregion
        #region ================================ Root Load ================================

        private void FormNotaSalidaNew_Load(object sender, EventArgs e)
        {
            if (nuevo == true)
                this.reLoad();
            else
            {
                this.reLoad();
              
            }

        }
        private void reLoad()
        {
            cargarAlmacenes();
            cargarProductos();
            cargarPresentacion();
            cargarObjetos();
        }

        #endregion

        #region ============================== Load ==============================


        private void cargarObjetos()
        {
            ////==
            //comprobarNota = new ComprobarNota();
            //listint = new List<List<int>>();
            ////===
            //almacenNEntrada = new AlmacenNEntrada();
            //compraEntradaGuardar = new CompraEntradaGuardar();
            //dictionary = new Dictionary<string, int>();
            //DetallesNota = new Dictionary<string, CargaCompraSinNota>();
            //object4 = new object();
            //object5 = new object();
            //object6 = new object();
            //object7 = new object();
            //listElementosNotaEntrada = new List<object>();

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        private async void cargarAlmacenes()
        {
            //listAlmacen = await AlmacenModel.almacenesAsignados(ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);

            listAlmacen = await AlmacenModel.almacenesAsignados(1, 1);

            almacenBindingSource.DataSource = listAlmacen;
            cbxAlmacen.SelectedIndex = 0;
            currentAlmacen = listAlmacen[0];
            cargarDocCorrelativo(currentAlmacen.idAlmacen);

        }
        private async void cargarDocCorrelativo(int idAlmacen)
        {
            listCorrelativoA = await AlmacenModel.DocCorrelativoAlmacen(idAlmacen);
            currentCorrelativoA = listCorrelativoA[0];
            txtSerie.Text = currentCorrelativoA.serie;
            txtCorrelativo.Text = currentCorrelativoA.correlativoActual;
        }

        private async void cargarProductos()
        {
            try
            {
                listProducto  = await productoModel.productos();
                productoBindingSource.DataSource = listProducto;


                cbxCodigoProducto.SelectedIndex = -1;
                cbxDescripcion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarPresentacion()
        {                                                                                                    /// Cargar las precentaciones
            try
            {
                listPresentacion = await presentacionModel.presentacionesTodas();
                presentacionBindingSource.DataSource = listPresentacion;
                cbxDescripcion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar Presentacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void cargarFechaSistema()
        {
            try
            {
                if (!nuevo) return;
                fechaSistema = await fechaModel.fechaSistema();
                dtpFechaEntrega.Value = fechaSistema.fecha;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void btnImportarVenta_Click(object sender, EventArgs e)
        {
            FormBuscarVentas formBuscarVentas = new FormBuscarVentas();
            formBuscarVentas.ShowDialog();

            if (formBuscarVentas.currentVenta != null)
            {
                currentVenta = formBuscarVentas.currentVenta;
                txtNroDocumentoVenta.Text = currentVenta.numeroDocumento;
                txtDocumentoCliente.Text = currentVenta.rucDni;
                txtNombreCliente.Text = currentVenta.nombreCliente;
                cargarDetalle(currentVenta.idVenta);





            }
        }


        private  async void  cargarDetalle(int idVenta)
        {
            listDetalleNotaSalida = await notaSalidaModel.cargarDetalleNotaSalida(idVenta);
            detalleNotaSalidaBindingSource.DataSource = null;
            detalleNotaSalidaBindingSource.DataSource = listDetalleNotaSalida;
            dgvDetalleNotaSalida.Refresh();
        }
        private void cargarProductoDetalle(int tipo)
        {

            cbxUnidad.Items.Clear();
            if (tipo == 0)
            {

                if (cbxCodigoProducto.SelectedIndex == -1) return;
                try
                {
                    /// Buscando el producto seleccionado
                    cbxUnidad.Enabled = true;


                    int idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
                    currentProducto = listProducto.Find(x => x.idProducto == idProducto);
                    cbxUnidad.Items.Add(currentProducto.nombreProducto);
                    cbxUnidad.SelectedIndex = 0;
                    cbxUnidad.SelectedValue = idProducto;
                    cargarPresentaciones(idProducto, tipo);
                    cargarAlternativas(tipo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                if (cbxDescripcion.SelectedIndex == -1) return;
                try
                {
                    /// Buscando el producto seleccionado
                    /// 
                    int idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                    currentPresentacion = listPresentacion.Find(x => x.idPresentacion == idPresentacion);

                    cbxCodigoProducto.SelectedValue = currentPresentacion.idProducto;

                 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }


        }


        private async void cargarPresentaciones(int idProducto, int tipo)
        {
            List<Presentacion> listPresentacionaux = await presentacionModel.presentacionVentas(idProducto);
            currentPresentacion = listPresentacionaux[0];
            cbxDescripcion.Text = currentPresentacion.descripcion;

        }

        private async void cargarAlternativas(int tipo)
        {
            if (cbxCodigoProducto.SelectedIndex == -1) return; /// validacion
            try
            {
                List<AlternativaCombinacion> alternativaCombinacion = await alternativaModel.cAlternativa31(Convert.ToInt32(cbxCodigoProducto.SelectedValue));
                alternativaCombinacionBindingSource.DataSource = alternativaCombinacion;
            }                                                  /// cargando las alternativas del producto
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void cargarPresentacionDescripcion(int tipo)
        {
            cbxDescripcion.Text = currentPresentacion.descripcion;

        }
        private void cbxCodigoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCodigoProducto.SelectedIndex == -1) return;
            txtCantidad.Text = "1";
            

            cargarProductoDetalle(0);
        }

        private void cbxDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDescripcion.SelectedIndex == -1) return;
            txtCantidad.Text = "1";         
            cargarProductoDetalle(1);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //validando campos          
            if (txtCantidad.Text == "")
            {
                txtCantidad.Text = "0";
            }
            

            bool seleccionado = false;
            if (cbxCodigoProducto.SelectedValue != null)
                seleccionado = true;
            if (cbxDescripcion.SelectedValue != null)
                seleccionado = true;

            if (seleccionado)
            {
                if (listDetalleNotaSalida == null) listDetalleNotaSalida = new List<DetalleNotaSalida>();
                DetalleNotaSalida detalleNota = new DetalleNotaSalida();

                currentPresentacion = listPresentacion.Find(x => x.idPresentacion == Convert.ToInt32(cbxDescripcion.SelectedValue));
                DetalleNotaSalida find = listDetalleNotaSalida.Find(x => x.idPresentacion == Convert.ToInt32(cbxDescripcion.SelectedValue));
                if (find != null)
                {

                    MessageBox.Show("Este dato ya fue agregado", "presentacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }

                currentProducto = listProducto.Find(x => x.idProducto == currentPresentacion.idProducto);
                // Creando la lista
                detalleNota.cantidad = toDouble(txtCantidad.Text.Trim());               
                detalleNota.cantidadUnitaria = toDouble(txtCantidad.Text.Trim());
                detalleNota.codigoProducto = currentProducto.codigoProducto;
                detalleNota.descripcion = cbxDescripcion.Text.Trim();
                detalleNota.idCombinacionAlternativa = Convert.ToInt32(cbxVariacion.SelectedValue);
                detalleNota.idNotaSalida= 0;
                detalleNota.idDetalleNotaSalida = 0;
                detalleNota.idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                detalleNota.idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
                detalleNota.nombreCombinacion = cbxVariacion.Text;
                detalleNota.nombreMarca = currentProducto.nombreMarca;
                detalleNota.nombrePresentacion = currentPresentacion.nombrePresentacion;               
                detalleNota.estado = currentPresentacion.estado;
                detalleNota.idDetalleVenta = 0;

                detalleNota.idVenta = 0;
                detalleNota.nro = 1;
                detalleNota.precioEnvio = 0;
                detalleNota.descuento = 0;// ver en detalle al guardar
                detalleNota.total = toDouble(txtCantidad.Text.Trim()) * toDouble(currentPresentacion.precioCompra);
                detalleNota.alternativas = "";// falta ver este detalle

                detalleNota.nombrePresentacion = currentPresentacion.nombrePresentacion;      
                // agrgando un nuevo item a la lista
                listDetalleNotaSalida.Add(detalleNota);
                // Refrescando la tabla
                detalleNotaSalidaBindingSource.DataSource = null;
                detalleNotaSalidaBindingSource.DataSource = listDetalleNotaSalida;
                dgvDetalleNotaSalida.Refresh();
                // Calculo de totales y subtotales e impuestos

                limpiarCamposProducto();


            }
            else
            {

                MessageBox.Show("Error: elemento no seleccionado", "agregar Elemento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }


        private void limpiarCamposProducto()
        {
            cbxCodigoProducto.SelectedIndex = -1;
            cbxDescripcion.SelectedIndex = -1;
            cbxVariacion.SelectedIndex = -1;
            txtCantidad.Text = "";       
            cbxUnidad.Text = "";
            cbxUnidad.SelectedIndex = -1;
            cbxUnidad.Items.Clear();
        }

        private void dgvDetalleNotaSalida_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            if (dgvDetalleNotaSalida.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int index = dgvDetalleNotaSalida.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleNotaSalida.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview
            DetalleNotaSalida aux = listDetalleNotaSalida.Find(x => x.idPresentacion == idPresentacion); // Buscando la registro especifico en la lista de registros

            cbxCodigoProducto.SelectedValue = aux.idProducto;
            cbxDescripcion.SelectedValue = aux.idPresentacion;
            cbxVariacion.Text = aux.nombreCombinacion;
            txtCantidad.Text = Convert.ToInt32(aux.cantidad).ToString();
            

            btnModificar.Enabled = true;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = true;


        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleNotaSalida.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dgvDetalleNotaSalida.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleNotaSalida.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview
            DetalleNotaSalida aux = listDetalleNotaSalida.Find(x => x.idPresentacion == idPresentacion);

            currentPresentacion = listPresentacion.Find(X => X.idPresentacion == idPresentacion);
            aux.cantidad = toDouble(txtCantidad.Text);
            aux.cantidadUnitaria = toDouble(txtCantidad.Text);
            aux.total = toDouble(txtCantidad.Text) * toDouble(currentPresentacion.precioCompra);

            detalleNotaSalidaBindingSource.DataSource = null;
            detalleNotaSalidaBindingSource.DataSource = listDetalleNotaSalida;
            cbxCodigoProducto.Refresh();
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            limpiarCamposProducto();


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (dgvDetalleNotaSalida.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dgvDetalleNotaSalida.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleNotaSalida.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview
            DetalleNotaSalida aux = listDetalleNotaSalida.Find(x => x.idPresentacion == idPresentacion);
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            dgvDetalleNotaSalida.Rows.RemoveAt(index);
            listDetalleNotaSalida.Remove(aux);
            limpiarCamposProducto();
        }
    }
}
