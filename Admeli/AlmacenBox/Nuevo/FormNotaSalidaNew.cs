using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.AlmacenBox.buscar;
using Admeli.Componentes;
using Admeli.Properties;
using Entidad;
using Entidad.Configuracion;
using Modelo;
using Newtonsoft.Json;

namespace Admeli.AlmacenBox.Nuevo
{
    public partial class FormNotaSalidaNew : Form
    {
        // objetos Necesarios para Guardar y modificar una nota entrada
        //==para verificacion
        ComprobarNotaSalida comprobarNota { get; set; }

        List<List<object>> listint { get; set; }
        //===
        AlmacenSalida almacenSalida { get; set; }
        VentaSalida ventaSalida { get; set; }
        Dictionary<string, double> dictionary { get; set; }
        Dictionary<string, DetalleNotaSalida> DetallesNotaSalida { get; set; }

        object object4 { get; set; }
        object object5 { get; set; }
        object object6 { get; set; }
        object object7 { get; set; }
        List<object> listElementosNotaSalida { get; set; }

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
        private int nroDecimales = ConfigModel.configuracionGeneral.numeroDecimales;
        private FechaSistema fechaSistema { get; set; }



        //objetos en tiempo real

       private Almacen currentAlmacen { get; set; }
       private AlmacenCorrelativo currentCorrelativoA { get; set; }
       private Producto currentProducto { get; set; }
       private Presentacion currentPresentacion { get; set; }

        private VentasNSalida currentVenta { get; set; }


        private NotaSalida currentNotaSalida { get; set; }


        private DetalleNotaSalida currentdetalleNotaSalida { get; set; }
        int indice = 0;

        private int numberOfItemsPerPage = 0;
        private int numberOfItemsPrintedSoFar = 0;

        List<FormatoDocumento> listformato;

       

        public FormNotaSalidaNew()
        {
            InitializeComponent();
            this.nuevo = true;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();
            btnQuitar.Enabled = false;

        }
        public FormNotaSalidaNew(NotaSalida currentNotaSalida)
        {
            InitializeComponent();
            this.nuevo = false;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();
            btnQuitar.Enabled = false;
            btnImportarVenta.Enabled = false;
            this.currentNotaSalida = currentNotaSalida;
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
                cargarNotaSalida();
            }

        }
        private void reLoad()
        {
            cargarAlmacenes();
            cargarProductos();
            cargarPresentacion();
            cargarObjetos();
            cargarFormatoDocumento();
        }

        #endregion

        #region ============================== Load ==============================

        private DetalleNotaSalida buscarElemento(int idPresentacion, int idCombinacion)
        {

            try
            {
                return listDetalleNotaSalida.Find(x => x.idPresentacion == idPresentacion && x.idCombinacionAlternativa == idCombinacion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar fechas del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }


        }










        private void cargarFormatoDocumento()
        {


            TipoDocumento tipoDocumento = ConfigModel.tipoDocumento.Find(X => X.idTipoDocumento == 8);// nota salida
            listformato = JsonConvert.DeserializeObject<List<FormatoDocumento>>(tipoDocumento.formatoDocumento);
            foreach (FormatoDocumento f in listformato)
            {
                string textoNormalizado = f.value.Normalize(NormalizationForm.FormD);
                //coincide todo lo que no sean letras y números ascii o espacio
                //y lo reemplazamos por una cadena vacía.
                Regex reg = new Regex("[^a-zA-Z0-9 ]");
                f.value = reg.Replace(textoNormalizado, "");
                f.value = f.value.Replace(" ", "");

            }
            string info = JsonConvert.SerializeObject(listformato);
        }
        private async void cargarNotaSalida()
        {

            //datos

            if (currentNotaSalida.idVenta != "0")
            {
                txtNroDocumentoVenta.Text = currentNotaSalida.numeroDocumento;
                txtNombreCliente.Text = currentNotaSalida.nombreCliente;
                txtDocumentoCliente.Text = currentNotaSalida.rucDni;

            }

            // serie
            txtSerie.Text = currentNotaSalida.serie;
            txtCorrelativo.Text = currentNotaSalida.correlativo;
            cbxAlmacen.SelectedValue = currentNotaSalida.idAlmacen;
            dtpFechaEntrega.Value = currentNotaSalida.fechaSalida.date;
            txtMotivo.Text = currentNotaSalida.motivo;
            txtDescripcion.Text = currentNotaSalida.descripcion;
            txtDireccionDestino.Text = currentNotaSalida.destino;

            string estado = "";
            switch (currentNotaSalida.estadoEnvio)
            {
                case 0:
                    estado = "Pendiente";
                    break;
                case 1:
                    estado = "Revisado";
                    break;
                case 2:
                    estado = "Enviado";
                    break;


            }
            cbxEstado.Text = estado;
            try
            {
                // cargar detalles de la nota
                listDetalleNotaSalida = await notaSalidaModel.cargarDetallesNota(currentNotaSalida.idNotaSalida);

                detalleNotaSalidaBindingSource.DataSource = listDetalleNotaSalida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Detalles de la Nota", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }




        private void cargarObjetos()
        {
            ////==
            comprobarNota = new ComprobarNotaSalida();
            listint = new List<List<object>>();
            ////===
            almacenSalida = new AlmacenSalida();
            ventaSalida = new VentaSalida();

            dictionary = new Dictionary<string, double>();
            DetallesNotaSalida = new Dictionary<string, DetalleNotaSalida>();
            object4 = new object();
            object5 = new object();
            object6 = new object();
            object7 = new object();
            listElementosNotaSalida = new List<object>();

           
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        private async void cargarAlmacenes()
        {
            try
            {
                //listAlmacen = await AlmacenModel.almacenesAsignados(ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);

                listAlmacen = await AlmacenModel.almacenesAsignados(ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);

                almacenBindingSource.DataSource = listAlmacen;
                cbxAlmacen.SelectedIndex = 0;
                currentAlmacen = listAlmacen[0];

                if (nuevo)
                {

                    cargarDocCorrelativo(currentAlmacen.idAlmacen);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Almacenes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
        private async void cargarDocCorrelativo(int idAlmacen)
        {
            try
            {
                listCorrelativoA = await AlmacenModel.DocCorrelativoAlmacen(idAlmacen);
                currentCorrelativoA = listCorrelativoA[0];
                txtSerie.Text = currentCorrelativoA.serie;
                txtCorrelativo.Text = currentCorrelativoA.correlativoActual;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Doc Correlativo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

                btnQuitar.Enabled = true;
            }
        }


        private  async void  cargarDetalle(int idVenta)
        {
            try
            {
                listDetalleNotaSalida = await notaSalidaModel.cargarDetalleNotaSalida(idVenta);
                detalleNotaSalidaBindingSource.DataSource = null;
                detalleNotaSalidaBindingSource.DataSource = listDetalleNotaSalida;
                dgvDetalleNotaSalida.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            try
            {
                List<Presentacion> listPresentacionaux = await presentacionModel.presentacionVentas(idProducto);
                currentPresentacion = listPresentacionaux[0];
                cbxDescripcion.Text = currentPresentacion.nombrePresentacion;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Presentaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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
            cbxDescripcion.Text = currentPresentacion.nombrePresentacion;

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

                currentProducto = listProducto.Find(x => x.idProducto == Convert.ToInt32(cbxCodigoProducto.SelectedValue));     

                DetalleNotaSalida find = buscarElemento( Convert.ToInt32(cbxDescripcion.SelectedValue), (int)cbxVariacion.SelectedValue);
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
                detalleNota.ventaVarianteSinStock = currentProducto.ventaVarianteSinStock;
                detalleNota.idVenta = 0;
                detalleNota.nro = 1;
                detalleNota.precioEnvio = 0;
                detalleNota.descuento = 0;// ver en detalle al guardar
                detalleNota.total = toDouble(txtCantidad.Text.Trim()) * toDouble(currentPresentacion.precioCompra);
                detalleNota.alternativas = "";// falta ver este detalle
                detalleNota.idNotaSalida = currentNotaSalida != null ? currentNotaSalida.idNotaSalida : 0;  
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
            indice = dgvDetalleNotaSalida.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleNotaSalida.Rows[indice].Cells[0].Value);
            int idCombinacion = Convert.ToInt32(dgvDetalleNotaSalida.Rows[indice].Cells[1].Value);// obteniedo el idRegistro del datagridview
            currentdetalleNotaSalida = buscarElemento(idPresentacion, idCombinacion); // Buscando la registro especifico en la lista de registros

            cbxCodigoProducto.SelectedValue = currentdetalleNotaSalida.idProducto;
            cbxDescripcion.SelectedValue = currentdetalleNotaSalida.idPresentacion;
            cbxVariacion.Text = currentdetalleNotaSalida.nombreCombinacion;
            txtCantidad.Text = Convert.ToInt32(currentdetalleNotaSalida.cantidad).ToString();
            

            btnModificar.Enabled = true;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = true;
            cbxDescripcion.Enabled = false;
            cbxCodigoProducto.Enabled = false;

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            
            currentPresentacion = listPresentacion.Find(X => X.idPresentacion == currentdetalleNotaSalida.idPresentacion);
            currentdetalleNotaSalida.cantidad = toDouble(txtCantidad.Text);
            currentdetalleNotaSalida.cantidadUnitaria = toDouble(txtCantidad.Text);
            currentdetalleNotaSalida.total = toDouble(txtCantidad.Text) * toDouble(currentPresentacion.precioCompra);
            currentdetalleNotaSalida.nombreCombinacion = cbxVariacion.Text;
            currentdetalleNotaSalida.idCombinacionAlternativa =(int) cbxVariacion.SelectedValue;


            detalleNotaSalidaBindingSource.DataSource = null;
            detalleNotaSalidaBindingSource.DataSource = listDetalleNotaSalida;
            cbxCodigoProducto.Refresh();
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            cbxDescripcion.Enabled = true;
            cbxCodigoProducto.Enabled = true;
            indice = 0;
            limpiarCamposProducto();


        }

      
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            // comprobamos la nota 

            if (nuevo)
            {

                comprobarNota.idVenta = currentVenta != null ? currentVenta.idVenta : 0;

            }
            else
            {
                comprobarNota.idVenta = currentNotaSalida != null ? Convert.ToInt32(currentNotaSalida.idVenta) : 0;

            }
            comprobarNota.idNotaSalida = currentNotaSalida != null ? currentNotaSalida.idNotaSalida : 0;
            comprobarNota.idAlmacen = (int)cbxAlmacen.SelectedValue;

            almacenSalida.descripcion = txtDescripcion.Text;
            almacenSalida.destino = txtDireccionDestino.Text;
            almacenSalida.idNotaSalida= currentNotaSalida != null ? currentNotaSalida.idNotaSalida : 0;

            int estado = 0;
            switch (cbxEstado.Text)
            {
                case "Pendiente":
                    estado = 0;
                    break;
                case "Revisado":
                    estado = 1;
                    break;
                case "Enviado":
                    estado = 2;
                    break;


            }
           
            almacenSalida.estadoEnvio = estado;
            string date1 = String.Format("{0:u}", dtpFechaEntrega.Value);
            date1 = date1.Substring(0, date1.Length - 1);
            almacenSalida.fechaSalida = date1;
            almacenSalida.idAlmacen = (int)cbxAlmacen.SelectedValue;
            almacenSalida.idPersonal = PersonalModel.personal.idPersonal;
            almacenSalida.idTipoDocumento = 8;//nota salida
            almacenSalida.motivo = txtMotivo.Text;

            int numert = 0;
            foreach (DetalleNotaSalida detalle in listDetalleNotaSalida)
            {              
                List<object> listaux = new List<object>();
               
                listaux.Add(detalle.idProducto);
                listaux.Add(detalle.idCombinacionAlternativa);
                int cantidad = Convert.ToInt32(detalle.cantidad, CultureInfo.GetCultureInfo("en-US"));
                listaux.Add(cantidad);
                listaux.Add(detalle.ventaVarianteSinStock);
                listint.Add(listaux);

                DetallesNotaSalida.Add("id" + numert, detalle);

                dictionary.Add("id" + numert, detalle.cantidadUnitaria);
                numert++;



            }
            comprobarNota.dato = listint;
            try
            {
                ResponseNotaSalida responseNotaSalida = await notaSalidaModel.verifcar(comprobarNota);

                if (responseNotaSalida.cumple.cumple == 1)
                {


                    listElementosNotaSalida.Add(almacenSalida);
                    listElementosNotaSalida.Add(ventaSalida);
                    listElementosNotaSalida.Add(DetallesNotaSalida);
                    listElementosNotaSalida.Add(dictionary);
                    listElementosNotaSalida.Add(object4);
                    listElementosNotaSalida.Add(object5);
                    listElementosNotaSalida.Add(object6);
                    listElementosNotaSalida.Add(object7);
                    ResponseNotaGuardar notaGuardar = null;
                    bool modificar = false;
                    if (nuevo)
                    {
                        notaGuardar = await notaSalidaModel.guardar(listElementosNotaSalida);

                        this.Close();
                    }
                    else
                    {
                        notaGuardar = await notaSalidaModel.modificar(listElementosNotaSalida);
                        modificar = true;
                    }

                    if (notaGuardar.id > 0)
                    {
                        if (!modificar)
                        {

                            DialogResult dialog = MessageBox.Show("¿Desea hacer la guia de remision?", "guia remision",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (dialog == DialogResult.No)
                            {

                                this.Close();
                                return;
                            }

                            // currentNotaSalida= 
                            List<NotaSalidaR> listNotasalida3 = await notaSalidaModel.nSalida((int)cbxAlmacen.SelectedValue);

                            FormRemisionNew formRemisionNew = new FormRemisionNew(listNotasalida3.Find(X => X.idNotaSalida == notaGuardar.id));
                            formRemisionNew.ShowDialog();
                            this.Close();

                        }
                        else
                        {

                            MessageBox.Show(notaGuardar.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();

                        }


                    }
                    else
                    {
                        MessageBox.Show(notaGuardar.msj, "guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                    }

                }
                else
                {

                    MessageBox.Show(" no cumple" + "exite: " + responseNotaSalida.abastece.cantidades + "  producto: " + responseNotaSalida.abastece.productos, "verificar", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dictionary.Clear();
                    DetallesNotaSalida.Clear();
                    listint.Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "btnGuardar_Click", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {

            currentNotaSalida = null;
            // cargar informacion
            txtNroDocumentoVenta.Text = "";
            txtNombreCliente.Text = "";
            txtDocumentoCliente.Text = "";
            detalleNotaSalidaBindingSource.DataSource = null;
            dgvDetalleNotaSalida.Refresh();
            btnQuitar.Enabled = false;          
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            cbxDescripcion.Enabled = true;
            cbxCodigoProducto.Enabled = true;
            dgvDetalleNotaSalida.Rows.RemoveAt(indice);
            listDetalleNotaSalida.Remove(currentdetalleNotaSalida);
            limpiarCamposProducto();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            int X = 0;
            int Y = 0;
            int XI = 0;


            Dictionary<string, Point> dictionary = new Dictionary<string, Point>();
            foreach (FormatoDocumento doc in listformato)
            {


                string tipo = doc.tipo;

                switch (tipo)
                {
                    case "Label":

                        int v = 0;
                        if (this.Controls.Find("txt" + doc.value, true).Count() > 0)
                            if (((this.Controls.Find("txt" + doc.value, true).First() as TextBox) != null))
                            {
                                TextBox textBox = this.Controls.Find("txt" + doc.value, true).First() as TextBox;
                                e.Graphics.DrawString(textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));
                                v++;
                            }
                        if (this.Controls.Find("cbx" + doc.value, true).Count() > 0)
                            if (((this.Controls.Find("cbx" + doc.value, true).First() as ComboBox) != null))
                            {
                                ComboBox textBox = this.Controls.Find("cbx" + doc.value, true).First() as ComboBox;
                                e.Graphics.DrawString(textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));
                                v++;
                            }
                        if (this.Controls.Find("dtp" + doc.value, true).Count() > 0)
                            if (((this.Controls.Find("dtp" + doc.value, true).First() as DateTimePicker) != null))
                            {
                                DateTimePicker textBox = this.Controls.Find("dtp" + doc.value, true).First() as DateTimePicker;
                                e.Graphics.DrawString(textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));
                                v++;
                            }


                        if (v == 0)
                        {

                            switch (doc.value)
                            {
                                case "SerieCorrelativo":

                                    e.Graphics.DrawString(txtSerie.Text + "-" + txtCorrelativo.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));


                                    break;
                                case "DescripcionEmpresa":

                                    e.Graphics.DrawString(ConfigModel.datosGenerales.razonSocial, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));


                                    break;

                                case "DireccionEmpresa":

                                    e.Graphics.DrawString(ConfigModel.datosGenerales.direccion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));


                                    break;
                                case "DocumentoEmpresa":

                                    e.Graphics.DrawString(ConfigModel.datosGenerales.ruc, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));


                                    break;
                                case "NombreEmpresa":

                                    e.Graphics.DrawString(ConfigModel.datosGenerales.razonSocial, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));


                                    break;




                            }




                        }

                        break;
                    case "ListGrid":
                        X = (int)doc.x;
                        Y = (int)doc.y;
                        XI = X;
                        break;
                    case "ListGridField":

                        e.Graphics.DrawString(doc.value, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(XI, Y));
                        dictionary.Add(doc.value, new Point(XI, Y));

                        //int YI = Y+30;
                        //foreach(DetalleV V in  detalleVentas)
                        //{
                        //    e.Graphics.DrawString(V.cantidad, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(XI, YI));
                        //    YI += 30;
                        //}
                        XI += X + (int)(doc.w);




                        break;
                    case "Img":

                        Image image = Resources.logo1;

                        e.Graphics.DrawImage(image, doc.x, doc.y, (int)doc.w, (int)doc.h);

                        break;

                }


            }

            Point point = dictionary["codigoProducto"];
            int YI = point.Y + 30;
            Point point1 = new Point();

            if (listDetalleNotaSalida == null) listDetalleNotaSalida = new List<DetalleNotaSalida>();



            for (int i = numberOfItemsPrintedSoFar; i < listDetalleNotaSalida.Count; i++)
            {
                numberOfItemsPerPage++;

                if (numberOfItemsPerPage <= 2)
                {
                    numberOfItemsPrintedSoFar++;

                    if (numberOfItemsPrintedSoFar <= listDetalleNotaSalida.Count)
                    {

                        if (dictionary.ContainsKey("codigoProducto"))
                        {

                            point1 = dictionary["codigoProducto"];
                            e.Graphics.DrawString(listDetalleNotaSalida[i].codigoProducto, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }

                        if (dictionary.ContainsKey("nombreCombinacion"))
                        {
                            point1 = dictionary["nombreCombinacion"];
                            e.Graphics.DrawString(listDetalleNotaSalida[i].nombreCombinacion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));

                        }
                        if (dictionary.ContainsKey("cantidad"))
                        {
                            point1 = dictionary["cantidad"];
                            e.Graphics.DrawString(darformato( listDetalleNotaSalida[i].cantidad), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                        if (dictionary.ContainsKey("cantidadcantidadUnitaria"))
                        {
                            point1 = dictionary["cantidadcantidadUnitaria"];
                            e.Graphics.DrawString(darformato(listDetalleNotaSalida[i].cantidadUnitaria), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }

                        if (dictionary.ContainsKey("nombrePresentacion"))
                        {
                            point1 = dictionary["nombrePresentacion"];
                            e.Graphics.DrawString(listDetalleNotaSalida[i].nombrePresentacion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                        if (dictionary.ContainsKey("descripcion"))
                        {
                            point1 = dictionary["descripcion"];
                            e.Graphics.DrawString(listDetalleNotaSalida[i].descripcion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));

                        }
                        if (dictionary.ContainsKey("nombreMarca"))
                        {
                            point1 = dictionary["nombreMarca"];
                            e.Graphics.DrawString(listDetalleNotaSalida[i].nombreMarca, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                       

                        if (dictionary.ContainsKey("total"))
                        {
                            point1 = dictionary["total"];


                            e.Graphics.DrawString(darformato(listDetalleNotaSalida[i].total), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                       
                        YI += 30;



                    }
                    else
                    {
                        e.HasMorePages = false;
                    }
                }
                else
                {
                    numberOfItemsPerPage = 0;
                    e.HasMorePages = true;
                    return;
                }
            }



            numberOfItemsPerPage = 0;
            numberOfItemsPrintedSoFar = 0;

            foreach (FormatoDocumento doc in listformato)
            {


                string tipo = doc.tipo;

                switch (tipo)
                {
                    case "Label":


                        if (this.Controls.Find("lb" + doc.value, true).Count() > 0)
                            if (((this.Controls.Find("lb" + doc.value, true).First() as Label) != null))
                            {
                                Label textBox = this.Controls.Find("lb" + doc.value, true).First() as Label;
                                if (doc.value == "Total")
                                {
                                    e.Graphics.DrawString(doc.value + ": " + textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x - 5, doc.y));


                                }
                                else
                                    e.Graphics.DrawString(doc.value + ": " + textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x - 31, doc.y));
                            }

                        break;
                }
            }

            numberOfItemsPerPage = 0;
            numberOfItemsPrintedSoFar = 0;




        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            FormatoDocumento doc = listformato.Last();
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("tamaño pagina", (int)doc.w, (int)doc.h);

            // pre visualizacion
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
    }
}
