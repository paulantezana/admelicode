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
using Newtonsoft.Json;

namespace Admeli.AlmacenBox.Nuevo
{
    public partial class FormEntradaNew : Form
    {

        // objetos Necesarios para Guardar y modificar una nota entrada
        //==para verificacion
        ComprobarNota comprobarNota { get; set; }

        List<List<int>> listint { get; set; }
        //===
        AlmacenNEntrada almacenNEntrada { get; set; }
        CompraEntradaGuardar compraEntradaGuardar { get; set; }
        Dictionary<string, int> dictionary { get; set; }
        Dictionary<string, CargaCompraSinNota> DetallesNota  { get; set; }

        object object4 { get; set; }
        object object5 { get; set; }
        object object6 { get; set; }
        object object7 { get; set; }
        List<object> listElementosNotaEntrada { get; set; }
        // servicios necesarios
        AlmacenModel AlmacenModel = new AlmacenModel();
        ProductoModel productoModel = new ProductoModel();
        PresentacionModel presentacionModel = new PresentacionModel();
        FechaModel fechaModel = new FechaModel();
        CompraModel compraModel = new CompraModel();
        NotaEntradaModel entradaModel = new NotaEntradaModel();

        AlternativaModel alternativaModel = new AlternativaModel();
        // objetos que cargan a un inicio
        private  List<Producto> listProducto { get; set; }
        private List<Presentacion> listPresentacion { get; set; }
        private List<Almacen> listAlmacen { get; set; }
        private List<AlmacenCorrelativo> listCorrelativoA { get; set; }
        private List<CargaCompraSinNota> listcargaCompraSinNota { get; set; }// detalles de compra
          


        // entidadades auxiliares

        private bool  nuevo { get; set; }
        private string formato { get; set; }
        private int nroDecimales = 2;
        private FechaSistema fechaSistema { get; set; }

        private CompraNEntrada currentCompraNEntrada { get; set; }

        //objetos en tiempo real

        private Almacen currentAlmacen { get; set; }
       private AlmacenCorrelativo currentCorrelativoA { get; set; }
       private Producto currentProducto { get; set; }
       private Presentacion currentPresentacion { get; set; }

        private NotaEntrada currentNotaEntrada { get; set; } 

        public FormEntradaNew()
        {
            InitializeComponent();
            this.nuevo = true;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();
      

        }
        public FormEntradaNew(NotaEntrada currentNotaEntrada)
        {
            InitializeComponent();
            this.nuevo = false;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();
            this.currentNotaEntrada = currentNotaEntrada;

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
                cargarDatosNotaEntrada();
            
            }

        }
        private void reLoad()
        {
            cargarObjetos();
            cargarAlmacenes();
            cargarProductos();
            cargarPresentacion();
        }

        #endregion

        #region ============================== Load ==============================


        private async void cargarDatosNotaEntrada()
        {
            //datos

            if (currentNotaEntrada.idCompra != "0")
            {
                txtNroDocumento.Text = currentNotaEntrada.numeroDocumento;
                txtNombreProveedor.Text = currentNotaEntrada.nombreProveedor;
                txtDocumentoProveedor.Text = currentNotaEntrada.rucDni;
                currentCompraNEntrada = new CompraNEntrada();
                currentCompraNEntrada.idCompra =Convert.ToInt32(currentNotaEntrada.idCompra);
            }
        
            // serie
            txtSerie.Text = currentNotaEntrada.serie;
            txtCorrelativo.Text = currentNotaEntrada.correlativo;
            cbxAlmacen.SelectedValue = currentNotaEntrada.idAlmacen;
            dtpFechaEntrega.Value = currentNotaEntrada.fechaEntrada.date;
            txtObservaciones.Text = currentNotaEntrada.observacion;
            chbxEntrega.Checked = currentNotaEntrada.estadoEntrega==1? true : false;

            // cargar Compra relaciona si es que exite





            // cargar detalles de la nota
            listcargaCompraSinNota = await entradaModel.cargarDetallesNota(currentNotaEntrada.idNotaEntrada);
            cargaCompraSinNotaBindingSource.DataSource = listcargaCompraSinNota;

        }
        private void cargarObjetos()
        {   
            //==
             comprobarNota = new ComprobarNota();
             listint = new List<List<int>>();
            //===
             almacenNEntrada = new AlmacenNEntrada();
             compraEntradaGuardar = new CompraEntradaGuardar();
             dictionary = new Dictionary<string, int>();
             DetallesNota = new Dictionary<string, CargaCompraSinNota>();
             object4 = new object();
             object5 = new object();
             object6 = new object();
             object7 = new object();
             listElementosNotaEntrada = new List<object>();

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        private async void cargarAlmacenes()
        {
            listAlmacen = await AlmacenModel.almacenesAsignados(ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);          
            almacenBindingSource.DataSource = listAlmacen;
            cbxAlmacen.SelectedIndex = 0;
            currentAlmacen = listAlmacen[0];
            if(nuevo)
                cargarDocCorrelativo(currentAlmacen.idAlmacen);

        }
        private async void cargarDocCorrelativo(int idAlmacen)
        {
            listCorrelativoA = await AlmacenModel.DocCorrelativoAlmacen(idAlmacen,7);
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


        //eventos



        private async void btnImportarCompra_Click(object sender, EventArgs e)
        {
            FormBuscarCompra formBuscarCompra = new FormBuscarCompra();
            formBuscarCompra.ShowDialog();

            currentCompraNEntrada = formBuscarCompra.currentCompraNEntrada;

            if(currentCompraNEntrada!=null)
            {
                // cargar informacion
                txtNroDocumento.Text = currentCompraNEntrada.numeroDocumento;
                txtNombreProveedor.Text = currentCompraNEntrada.nombreProveedor;
                txtDocumentoProveedor.Text = currentCompraNEntrada.rucDni;
                listcargaCompraSinNota=  await entradaModel.CargarCompraSinNota(currentCompraNEntrada.idCompra);
                cargaCompraSinNotaBindingSource.DataSource = null;
                cargaCompraSinNotaBindingSource.DataSource = listcargaCompraSinNota;
            }
            
        }

        private void cbxCodigoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (cbxCodigoProducto.SelectedIndex == -1) return;
            txtCantidad.Text =  "1" ;
            txtCantidadRecibida.Text = "1" ;

            cargarProductoDetalle(0);
        }
        private void cbxDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxDescripcion.SelectedIndex == -1) return;
            txtCantidad.Text = "1";
            txtCantidadRecibida.Text = "1";
            cargarProductoDetalle(1);
        }


        // metodos usados por lo eventos
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

                    //int idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                    //currentPresentacion = listPresentacion.Find(x => x.idPresentacion == idPresentacion);

                

                    //cbxCodigoProducto.se
                    //cbxUnidad.Items.Add(currentProducto.nombreProducto);
                    //cbxUnidad.SelectedIndex = 0;
                    //cargarPresentacionDescripcion(tipo);
                    //cargarAlternativas(tipo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }


        }


        private async void cargarPresentaciones(int idProducto, int tipo)
        {
            List<Presentacion> presentaciones = await presentacionModel.presentacionVentas(idProducto);
            currentPresentacion = presentaciones[0];

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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //validando campos          
            if (txtCantidad.Text == "")
            {
                txtCantidad.Text = "0";
            }
            if (txtCantidadRecibida.Text == "")
            {
                txtCantidadRecibida.Text = "0";
            }

            bool seleccionado = false;
            if (cbxCodigoProducto.SelectedValue != null)
                seleccionado = true;
            if (cbxDescripcion.SelectedValue != null)
                seleccionado = true;

            if (seleccionado)
            {
                if (listcargaCompraSinNota == null) listcargaCompraSinNota = new List<CargaCompraSinNota>();
                CargaCompraSinNota detalleNota = new CargaCompraSinNota();

                currentPresentacion = listPresentacion.Find(x => x.idPresentacion == Convert.ToInt32(cbxDescripcion.SelectedValue));
                CargaCompraSinNota find = listcargaCompraSinNota.Find(x => x.idPresentacion == Convert.ToInt32(cbxDescripcion.SelectedValue));
                if (find != null)
                {

                    MessageBox.Show("Este dato ya fue agregado", "presentacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }

                currentProducto = listProducto.Find(x => x.idProducto == currentPresentacion.idProducto);
                // Creando la lista
                detalleNota.cantidad = toDouble(txtCantidad.Text.Trim());
                /// Busqueda presentacion
                detalleNota.cantidadUnitaria = toDouble(txtCantidad.Text.Trim());
                detalleNota.codigoProducto = currentProducto.codigoProducto;
                detalleNota.descripcion = cbxDescripcion.Text.Trim();                           
                detalleNota.idCombinacionAlternativa = Convert.ToInt32(cbxVariacion.SelectedValue);
               
                detalleNota.idNotaEntrada = 0;
                detalleNota.idDetalleNotaEntrada = 0;
                detalleNota.idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                detalleNota.idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);              
                detalleNota.nombreCombinacion = cbxVariacion.Text;
                detalleNota.nombreMarca = currentProducto.nombreMarca;
                detalleNota.nombrePresentacion = currentPresentacion.nombrePresentacion;
                detalleNota.cantidadRecibida = toDouble(txtCantidadRecibida.Text.Trim());
                detalleNota.estado = currentPresentacion.estado;
                // agrgando un nuevo item a la lista
                listcargaCompraSinNota.Add(detalleNota);
                // Refrescando la tabla
                cargaCompraSinNotaBindingSource.DataSource = null;
                cargaCompraSinNotaBindingSource.DataSource = listcargaCompraSinNota;
                dgvDetalleNota.Refresh();
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
            txtCantidadRecibida.Text = "";
            cbxUnidad.Text = "";
            cbxUnidad.SelectedIndex = -1;
            cbxUnidad.Items.Clear();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            // comprobamos la nota             
            comprobarNota.idCompra = currentCompraNEntrada != null ? currentCompraNEntrada.idCompra : 0;
            comprobarNota.idNotaEntrada = currentNotaEntrada !=null ? currentNotaEntrada.idNotaEntrada: 0;// en modificar puede variar         
            almacenNEntrada.estadoEntrega = chbxEntrega.Checked? 1 : 0 ;

            almacenNEntrada.idNotaEntrada = currentNotaEntrada != null ? currentNotaEntrada.idNotaEntrada : 0; ;
            string date1 = String.Format("{0:u}", dtpFechaEntrega.Value);
            date1 = date1.Substring(0, date1.Length - 1);
            almacenNEntrada.fechaEntrada = date1;// probar con el otro 
            almacenNEntrada.idAlmacen = (int)cbxAlmacen.SelectedValue;
            almacenNEntrada.idPersonal = PersonalModel.personal.idPersonal;
            almacenNEntrada.idTipoDocumento = 7;// nota de entrada
            almacenNEntrada.observacion = txtObservaciones.Text;
            
            compraEntradaGuardar.idCompra = currentCompraNEntrada != null ? currentCompraNEntrada.idCompra:0;
                            
            int numert = 0;
            foreach (CargaCompraSinNota detalle in listcargaCompraSinNota)
            {
               
                DetallesNota.Add("id" + numert, detalle);

                dictionary.Add("id" + numert, detalle.idPresentacion);
                numert++;
                List<int> listaux = new List<int>();
                listaux.Add(detalle.idProducto);
                listaux.Add(detalle.idCombinacionAlternativa);

                int cantidad = Convert.ToInt32(detalle.cantidad,CultureInfo.GetCultureInfo("en-US"));
                listaux.Add(cantidad);
                listint.Add(listaux);

            }
            comprobarNota.dato = listint;
            ResponseNota responseNota= await entradaModel.verifcar(comprobarNota);

            if (responseNota.cumple == 1)
            {
                listElementosNotaEntrada.Add(almacenNEntrada);
                listElementosNotaEntrada.Add(compraEntradaGuardar);
                listElementosNotaEntrada.Add(DetallesNota);
                listElementosNotaEntrada.Add(dictionary);
                listElementosNotaEntrada.Add(object4);
                listElementosNotaEntrada.Add(object5);
                listElementosNotaEntrada.Add(object6);
                listElementosNotaEntrada.Add(object7);
                ResponseNotaGuardar notaGuardar = null;
                if (nuevo)
                {
                   notaGuardar = await entradaModel.guardar(listElementosNotaEntrada);

                }
                else
                {
                    notaGuardar = await entradaModel.modificar(listElementosNotaEntrada);
                }
                
                if (notaGuardar.id > 0)
                {
                    MessageBox.Show(notaGuardar.msj, "guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                string request = JsonConvert.SerializeObject(listElementosNotaEntrada);
            }
            else
            {


                MessageBox.Show(" no cumple" + responseNota.existeProducto+" " +responseNota.idCombinacionAlternativa+" "+ responseNota.idProducto, "verificar", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
           
        }

        private void dgvDetalleNota_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificando la existencia de datos en el datagridview
            if (dgvDetalleNota.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int index = dgvDetalleNota.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleNota.Rows[index].Cells[3].Value); // obteniedo el idRegistro del datagridview
            CargaCompraSinNota aux = listcargaCompraSinNota.Find(x => x.idPresentacion == idPresentacion); // Buscando la registro especifico en la lista de registros
            
            cbxCodigoProducto.SelectedValue = aux.idProducto;
            cbxDescripcion.SelectedValue = aux.idPresentacion;
            cbxVariacion.Text = aux.nombreCombinacion;
            txtCantidad.Text = Convert.ToInt32(aux.cantidad).ToString();
            txtCantidadRecibida.Text =Convert.ToInt32(aux.cantidadRecibida).ToString();

            btnModificar.Enabled = true;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleNota.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dgvDetalleNota.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleNota.Rows[index].Cells[3].Value); // obteniedo el idRegistro del datagridview
            CargaCompraSinNota aux = listcargaCompraSinNota.Find(x => x.idPresentacion == idPresentacion);
            aux.cantidad = toDouble(txtCantidad.Text);
            aux.cantidadUnitaria = toDouble(txtCantidad.Text);
            aux.cantidadRecibida= toDouble(txtCantidadRecibida.Text);
            detalleCompraBindingSource.DataSource = null;
            detalleCompraBindingSource.DataSource = listcargaCompraSinNota;
            dgvDetalleNota.Refresh();
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled= false;
            limpiarCamposProducto();


        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleNota.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dgvDetalleNota.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleNota.Rows[index].Cells[3].Value); // obteniedo el idRegistro del datagridview
            CargaCompraSinNota aux = listcargaCompraSinNota.Find(x => x.idPresentacion == idPresentacion);                    
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            dgvDetalleNota.Rows.RemoveAt(index);
            listcargaCompraSinNota.Remove(aux);
            limpiarCamposProducto();
        }
    }
}
