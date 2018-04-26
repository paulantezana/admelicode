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
using Admeli.Componentes;
using Admeli.Compras.buscar;
using Admeli.Compras.Buscar;
using Admeli.Productos.Nuevo;
using Admeli.Properties;
using Admeli.Ventas.buscar;
using Admeli.Ventas.Buscar;
using Entidad;
using Entidad.Configuracion;
using Entidad.Location;
using Modelo;
using Newtonsoft.Json;

namespace Admeli.Ventas.Nuevo
{
    public partial class FormVentaNewR : Form
    {



        // Variables para verificar stock
        List<List<object>> dato { get; set; }
        VerificarStock verificarStock { get; set; }


        AbasteceV abastece { get; set; }

        AbasteceReceive abasteceReceive { get; set; }
        //variables para realizar  un venta


        private Cobrov cobrov { get; set; }
        private Ventav ventav { get; set; }
        private CobroVentaV cobroVentaV { get; set; }
        private List<DatosNotaSalidaVenta> datosNotaSalidaVenta { get; set; }
        private NotasalidaVenta notasalidaVenta { get; set; }
        private VentaTotal ventaTotal { get; set; }









   







        //webservice utilizados

        private TipoDocumentoModel tipoDocumentoModel = new TipoDocumentoModel();
        private DocCorrelativoModel docCorrelativoModel = new DocCorrelativoModel();
        private AlternativaModel alternativaModel = new AlternativaModel();
        private FechaModel fechaModel = new FechaModel();
        private MedioPagoModel medioPagoModel = new MedioPagoModel();
        private LocationModel locationModel = new LocationModel();
        private MonedaModel monedaModel = new MonedaModel();
        private DocumentoIdentificacionModel documentoIdentificacionModel = new DocumentoIdentificacionModel();
        private ClienteModel clienteModel = new ClienteModel();
        private CotizacionModel cotizacionModel = new CotizacionModel();
        private ImpuestoModel impuestoModel = new ImpuestoModel();
        private ProductoModel productoModel = new ProductoModel();
        private PresentacionModel presentacionModel = new PresentacionModel();
        private DescuentoModel descuentoModel = new DescuentoModel();
        private StockModel stockModel = new StockModel();
        private VentaModel ventaModel = new VentaModel();
        private AlmacenModel almacenModel = new AlmacenModel();

        /// Sus datos se cargan al abrir el formulario
        private List<Moneda> monedas { get; set; }
        private List<TipoDocumento> tipoDocumentos { get; set; }
        private FechaSistema fechaSistema { get; set; }
        private List<MedioPago> medioPagos { get; set; }
        private List<DocumentoIdentificacion> documentoIdentificacion { get; set; }
        private List<Cliente> listClientes { get; set; }
        private List<Impuesto> listImpuesto { get; set; }
        private List<ImpuestoDocumento> listIDocumento { get; set; }
        private List<ProductoVenta> listProductos { get; set; }
        private DescuentoProductoReceive descuentoProducto { get; set; }
        private List<DetalleV> detalleVentas { get; set; }
        private List<Presentacion> listPresentacion { get; set; }
        private List<ImpuestoProducto> listImpuestosProducto { get; set; }

        private List<AlmacenComra> listAlmecenes { get; set; }

        List<AlternativaCombinacion> alternativaCombinacion { get; set; }
        public UbicacionGeografica CurrentUbicacionGeografica;
        /// Llenan los datos en las interacciones en el formulario 


        private AlmacenComra almacenVenta { get; set; }
        private Presentacion currentPresentacion { get; set; }
        private CorrelativoCotizacion correlativoCotizacion { get; set; }
        private ProductoVenta currentProducto { get; set; }
        private ImpuestoProducto impuestoProducto { get; set; }
        private Cliente CurrentCliente { get; set; }
        private CotizacionBuscar currentCotizacion { get; set; }
        private Venta currentVenta { get; set; }
        private DetalleV currentdetalleV { get; set; }
        public int nroNuevo = 0;

        private bool nuevo { get; set; }
        private bool enModificar { get; set; }
        private int idPresentacionDatagriview = 0;
        int nroDecimales = 2;
        string formato { get; set; }
        private int nroCaracteres = 0;
        private double subTotal = 0;
        private double Descuento = 0;
        private double impuesto = 0;
        private double total = 0;
        // variables para poder imprimir la cotizacion

        private int numberOfItemsPerPage = 0;
        private int numberOfItemsPrintedSoFar = 0;
        List<FormatoDocumento> listformato;

        public FormVentaNewR()
        {
            InitializeComponent();




            this.nuevo = true;
            cargarFechaSistema();


            formato = "{0:n" + nroDecimales + "}";
            if (ConfigModel.cajaSesion == null)
            {
                chbxPagarCompra.Checked = false;
                chbxPagarCompra.Enabled = false;


            }
            chbxGuiaRemision.Checked = false;
            chbxGuiaRemision.Enabled = false;

            cargarResultadosIniciales();

        }



        #region============= metods de apoyo en formato de decimales

        private void cargarResultadosIniciales()
        {


            lbSubtotal.Text = "s/" + ". " + darformato(0);
            lbDescuentoVenta.Text = "s/" + ". " + darformato(0);
            lbImpuesto.Text = "s/" + ". " + darformato(0);
            lbTotalVentas.Text = "s/" + ". " + darformato(0);

        }


        private string darformato(object dato)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
        }
        private string darformatoGuardar(object dato)
        {

            string var1 = string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
            var1 = var1.Replace(",", "");
            return var1;
        }

        private double toDouble(string texto)
        {
            return double.Parse(texto, CultureInfo.GetCultureInfo("en-US")); ;
        }
        private int toEntero(string texto)
        {
            return Int32.Parse(texto, CultureInfo.GetCultureInfo("en-US")); ;
        }


        #endregion============================

        public FormVentaNewR(Venta currentVenta)
        {

            InitializeComponent();

            this.nuevo = false;
            formato = "{0:n" + nroDecimales + "}";
            this.currentVenta = currentVenta;

            if (ConfigModel.cajaSesion == null)
            {
                chbxPagarCompra.Checked = false;
                chbxPagarCompra.Enabled = false;


            }
            chbxGuiaRemision.Checked = false;
            chbxGuiaRemision.Enabled = false;
            deshabilitarControles();

        }

        private void deshabilitarControles()
        {
            cbxNombreDocumento.Enabled = false;
            txtSerie.Enabled = false;
            txtCorrelativo.Enabled = false;
            chbxEditar.Enabled = false;
            cbxTipoDocumento.Enabled = false;
            txtDocumentoCliente.Enabled = false;
            cbxNombreRazonCliente.Enabled = false;
            cbxTipoMoneda.Enabled = false;
            dtpFechaPago.Enabled = false;
            btnActulizar.Enabled = false;
            btnAgregar.Enabled=false;
            btnBuscarCliente.Enabled = false;
            btnVenta.Enabled = false;
            btnModificar.Enabled = false;
            btnNuevoProducto.Enabled = false;
            btnImportarCotizacion.Visible = false;
        }
        #region ================================ Root Load ================================
        private void FormCompraNew_Load(object sender, EventArgs e)
        {


            if (nuevo == true)
            {
                this.reLoad();


            }
            else
            {
                this.reLoad();

                cargarVentas();
                cargarCorrelativo();
            }

            AddButtonColumn();

            btnModificar.Enabled = false;
        }
        private void reLoad()
        {

            cargarMonedas();
            cargarFechaSistema();
            cargarProductos();
            cargartiposDocumentos();
            cargarClientes();
            cargarTipoComprobantes();
            cargarImpuesto();
            cargarPresentacion();
            cargarObjetos();
            cargarAlmacen();
            

        }
        #endregion
        #region ============================== Load ==============================

        private void cargarFormatoDocumento( int idTipoDocumento)
        {


            TipoDocumento tipoDocumento = ConfigModel.tipoDocumento.Find(X => X.idTipoDocumento == idTipoDocumento);// cotizacion
            listformato = JsonConvert.DeserializeObject<List<FormatoDocumento>>(tipoDocumento.formatoDocumento);

            if (listformato == null) return;
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



        private void cargarCorrelativo()
        {

            cbxNombreDocumento.SelectedValue = currentVenta.idTipoDocumento;

            txtSerie.Text = currentVenta.serie;
            txtCorrelativo.Text = currentVenta.correlativo; 
        } 
        private async void cargarAlmacen()
        {

             listAlmecenes = await almacenModel.almacenesCompra(ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);
             almacenVenta = listAlmecenes[0];
            
        }



        private async void cargarVentas()
        {

            
            detalleVentas = await ventaModel.listarDetalleventas(currentVenta.idVenta);



            double impuesto = 0;
            foreach (DetalleV V in detalleVentas)
            {

                double Im = toDouble(V.valor);
                impuesto += Im;


                double precioUnitario = toDouble(V.precioUnitario);
                double cantidad = toDouble(V.cantidad);
                double cantidad1 = toDouble(V.cantidadUnitaria);

                V.precioUnitario = darformato(precioUnitario);
                V.cantidad = darformato(cantidad);
                V.cantidadUnitaria = darformato(cantidad1);

                double total1 = toDouble(V.total);
                V.total = darformato(total1);
                double total = precioUnitario * cantidad + Im;
                V.totalGeneral = darformato(total);
                V.precioVenta = darformato(total / cantidad);

                double precioVenta = toDouble(V.precioVenta);
                double d = 1 - toDouble(V.descuento) / 100;



                V.precioVentaReal = darformato(precioVenta / d);
                listImpuestosProducto = await impuestoModel.impuestoProducto(V.idProducto, ConfigModel.sucursal.idSucursal);
                // calculamos lo impuesto posibles del producto
                double porcentual = 0;
                double efectivo = 0;
                foreach (ImpuestoProducto I in listImpuestosProducto)
                {
                    if (I.porcentual)
                    {
                        porcentual += toDouble(I.valorImpuesto);
                    }
                    else
                    {
                        efectivo += toDouble(I.valorImpuesto);
                    }
                }
                V.Porcentual = darformato(porcentual);
                V.Efectivo = darformato(efectivo);


            }
            detalleVBindingSource.DataSource = detalleVentas;
            limpiarCamposProducto();

        }
        private async void cargarCotizacion()
        {
            detalleVentas=  await cotizacionModel.detalleCotizacion(currentCotizacion.idCotizacion);
           
           

            double impuesto = 0; 
            foreach(DetalleV V in detalleVentas)
            {
               
                double Im = toDouble(V.valor);
                impuesto += Im;


                double precioUnitario = toDouble(V.precioUnitario);
                double cantidad = toDouble(V.cantidad);
                double cantidad1 = toDouble(V.cantidadUnitaria);

                V.precioUnitario = darformato(precioUnitario);
                V.cantidad = darformato(cantidad);
                V.cantidadUnitaria= darformato(cantidad1);

                double total1 = toDouble(V.total);
                V.total = darformato(total1);             
                double total = precioUnitario * cantidad + Im;
                V.totalGeneral = darformato(total);
                V.precioVenta = darformato(total/ cantidad);

                double precioVenta = toDouble(V.precioVenta);
                double d=  1-toDouble(V.descuento) / 100;
                


                V.precioVentaReal = darformato(precioVenta/d);
                listImpuestosProducto = await impuestoModel.impuestoProducto(V.idProducto, ConfigModel.sucursal.idSucursal);
                // calculamos lo impuesto posibles del producto
                double porcentual = 0;
                double efectivo = 0;
                foreach (ImpuestoProducto I in listImpuestosProducto)
                {
                    if (I.porcentual)
                    {
                        porcentual += toDouble(I.valorImpuesto);
                    }
                    else
                    {
                        efectivo += toDouble(I.valorImpuesto);
                    }
                }
                V.Porcentual = darformato(porcentual);
                V.Efectivo = darformato(efectivo);


            }
            detalleVBindingSource.DataSource = detalleVentas;         
            limpiarCamposProducto();

        }
        private void cargarObjetos() {

            cobrov = new Cobrov();
            ventav = new Ventav();
            cobroVentaV = new CobroVentaV();
            datosNotaSalidaVenta = new List<DatosNotaSalidaVenta>();
            notasalidaVenta = new NotasalidaVenta();
            ventaTotal = new VentaTotal();
            dato = new List<List<object>>();
            verificarStock = new VerificarStock();
            abastece = new AbasteceV();


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
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void cargarProductos()
        {
            try
            {
                listProductos = await productoModel.productos(ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);
                productoVentaBindingSource.DataSource = listProductos;
                cbxCodigoProducto.SelectedIndex = -1;
                cbxDescripcion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void cargarClientes()
        {
            try
            {
                
                listClientes = await clienteModel.ListarClientesActivos();
                clienteBindingSource.DataSource = listClientes;
                if (!nuevo)
                {

                    Cliente cliente = listClientes.Find(X => X.idCliente == currentVenta.idCliente);
                    cbxNombreRazonCliente.SelectedValue = currentVenta.idCliente;
                    cbxTipoDocumento.SelectedValue = cliente.idDocumento;
                }
                else
                {
                    cbxNombreRazonCliente.SelectedIndex = -1;

                }

            }
            catch ( Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar Clientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                    
        }
        private async void cargartiposDocumentos()
        {

            try
            {

                documentoIdentificacion = await documentoIdentificacionModel.docIdentificacion();
                documentoIdentificacionBindingSource.DataSource = documentoIdentificacion;



                cbxTipoDocumento.SelectedValue = documentoIdentificacion.Find(X => X.tipoDocumento =="Jurídico").idDocumento;



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar Doc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }       
        }
        private async void cargarImpuesto(int tipo=3)
        {
            try
            {
                // variables necesarios para el calculo del impuesto de la venta
                listImpuesto = await impuestoModel.listarImpuesto();

                listIDocumento = await impuestoModel.impuestoTipoDoc(ConfigModel.sucursal.idSucursal, tipo); // tipo documento 4 

              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar Impuesto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private async void cargarTipoComprobantes()
        {
            try
            {
                tipoDocumentos = await tipoDocumentoModel.tipoDocumentoVentas();
                cbxNombreDocumento.DataSource = tipoDocumentos;               
                if (!nuevo)
                {
                    cbxNombreDocumento.SelectedValue = currentVenta.idTipoDocumento;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }      
        private async void cargarMonedas()
        {
            try
            {
                monedas = await monedaModel.monedas();
                cbxTipoMoneda.DataSource = monedas;
                if (!nuevo)
                {

                    Moneda moneda = monedas.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);  
                    cbxTipoMoneda.Text = currentVenta.moneda;
                    txtObservaciones.Text = currentVenta.observacion;
                    this.Descuento = toDouble(currentVenta.descuento);

                    lbDescuentoVenta.Text = moneda.simbolo + ". " + darformato(Descuento);

                    this.total = toDouble(currentVenta.total);
                    lbTotalVentas.Text = moneda.simbolo + ". " + darformato(total);

                    this.subTotal = toDouble(currentVenta.subTotal);
                    lbSubtotal.Text = moneda.simbolo + ". " + darformato(subTotal);
                    double impuesto = total - subTotal;
                    lbImpuesto.Text = moneda.simbolo + ". " + darformato(impuesto);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void cargarFechaSistema()
        {
            try
            {
                if (!nuevo)
                {
                    dtpFechaEmision.Value = currentVenta.fechaVenta.date;
                    dtpFechaPago.Value= currentVenta.fechaPago.date;
               
                }
                else
                {
                    fechaSistema = await fechaModel.fechaSistema();
                    dtpFechaEmision.Value = fechaSistema.fecha;
                    dtpFechaPago.Value = fechaSistema.fecha;

                }

                //dtpFechaPago.Value = fechaSistema.fecha;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
       










                      
        private void AddButtonColumn()
        {
            DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
            {
                buttons.HeaderText = "Acciones";
                buttons.Text = "X";
                buttons.UseColumnTextForButtonValue = true;
                //buttons.AutoSizeMode =
                //   DataGridViewAutoSizeColumnMode.AllCells;
                buttons.FlatStyle = FlatStyle.Popup;
                buttons.CellTemplate.Style.BackColor = Color.Red;
                buttons.CellTemplate.Style.ForeColor = Color.White;

                buttons.Name = "acciones";           
            }

            dgvDetalleOrdenCompra.Columns.Add(buttons);

        }
        
        

        
       

        // para el cbx de descripcion
       
       

        #endregion

        #region=========== METODOS DE APOYO EN EL CALCULO


        private  void calculoSubtotal()
        {

            if (cbxTipoMoneda.SelectedValue == null)
                return;
          
            double subTotalLocal = 0;
            double TotalLocal = 0;
            foreach (DetalleV  item in detalleVentas)
            {
                if(item.estado==1)
                subTotalLocal += toDouble( item.total);
                TotalLocal += toDouble(item.totalGeneral);
            }


            Moneda moneda = monedas.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);
            this.subTotal = subTotalLocal;

            lbSubtotal.Text = moneda.simbolo + ". " + darformato(subTotalLocal);
            // determinar impuesto de cada producto
            double impuestoTotal = TotalLocal- subTotalLocal;

            // arreglar esto esta mal la logica ya que el impuesto es procentual
           
            this.impuesto = impuestoTotal;
            lbImpuesto.Text = moneda.simbolo + ". " + darformato(impuestoTotal);

            // determinar impuesto de cada producto
            this.total = TotalLocal;
            lbTotalVentas.Text = moneda.simbolo + ". " + darformato(TotalLocal);

        }

        /// <summary>
        /// Calcular Total
        /// </summary>
        private void calcularTotal()
        {
            try
            {
                if (txtCantidad.Text.Trim() == "") txtTotalProducto.Text = "0";
                if (txtPrecioUnitario.Text.Trim() == "" || txtCantidad.Text.Trim() == "" || txtDescuento.Text.Trim() == "") return; /// Validación
                double precioUnidario =toDouble(txtPrecioUnitario.Text);
                double cantidad = toDouble(txtCantidad.Text);
                double descuento = toDouble(txtDescuento.Text);
                double totalConDescuento = (precioUnidario * cantidad) - (descuento / 100) * (precioUnidario * cantidad);
                txtTotalProducto.Text = darformato(totalConDescuento);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Calcular total", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        /// <summary>
        /// Calcular Precio Unitario
        /// </summary>
       


        #endregion
        private void cargarProductoDetalle()
        {
            if (cbxCodigoProducto.SelectedIndex == -1) return;
            try
            {
                /// Buscando el producto seleccionado
                int idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
                currentProducto = listProductos.Find(x => x.idProducto == idProducto);
                cbxDescripcion.Text = currentProducto.codigoProducto;
                // Llenar los campos del producto escogido.............!!!!!

                if (!enModificar)
                {
                txtCantidad.Text = "1";
                txtDescuento.Text = "0";

                }
               
                /// Cargando presentaciones
                cargarPresentacionesProducto();
                /// Cargando alternativas del producto
                cargarAlternativasProducto();
                determinarDescuentoEImpuesto();
                determinarStock(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private  void cargarPresentacionesProducto()
        {

            // arreglar esto  no es necesrio ir a un servicio
            if (cbxCodigoProducto.SelectedIndex == -1) return; /// validacion   
            int idProducto = (int)cbxCodigoProducto.SelectedValue;
            ProductoVenta productoVenta=  listProductos.Find(X => X.idProducto == idProducto);
            /// Cargar las precentacione
            cbxDescripcion.SelectedValue =productoVenta.idPresentacion ;
            presentacionBindingSource.DataSource = listPresentacion;
            
        
        }
        private void calcularPrecioUnitarioProducto()
        {
            if (cbxCodigoProducto.SelectedIndex == -1) return; /// Validación
            try
            {
                if (cbxDescripcion.SelectedIndex == -1)
                {
                    txtPrecioUnitario.Text = currentProducto.precioVenta;
                }
                else
                {
                    // Buscar presentacion elegida
                    int idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                    Presentacion findPresentacion = listPresentacion.Find(x => x.idPresentacion == idPresentacion);

                    // Realizando el calculo
                    double precioCompra = toDouble(currentProducto.precioVenta);
                    double cantidadUnitario = toDouble(findPresentacion.cantidadUnitaria);
                    double precioUnidatio = precioCompra * cantidadUnitario;

                    // Imprimiendo valor
                    txtPrecioUnitario.Text = darformato(precioUnidatio);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Calcular total", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarAlternativasProducto()
        {
            if (cbxCodigoProducto.SelectedIndex == -1) return; /// validacion
                                                               /// cargando las alternativas del producto
            alternativaCombinacion = await alternativaModel.cAlternativa31(Convert.ToInt32(cbxCodigoProducto.SelectedValue));
            alternativaCombinacionBindingSource.DataSource = alternativaCombinacion;
            cbxVariacion.SelectedIndex = -1;
            cbxVariacion.SelectedValue = alternativaCombinacion[0].idCombinacionAlternativa;

            if (!nuevo)
            {
                if (cbxVariacion.SelectedIndex != -1 && currentdetalleV != null)
                    cbxVariacion.SelectedValue = currentdetalleV.idCombinacionAlternativa;

            }


            if (alternativaCombinacion[0].idCombinacionAlternativa <= 0)
                calcularPrecioUnitarioProducto();
            calcularTotal();
        }

      
        // el calculo se hace entre las fechas  entre fechas
        public async void determinarDescuentoEImpuesto()
        {
            if (txtCantidad.Text != "" && cbxCodigoProducto.SelectedValue != null)
            {
                DescuentoProductoSubmit descuentoProductoSubmit = new DescuentoProductoSubmit();

                descuentoProductoSubmit.cantidad = toDouble(txtCantidad.Text);
                descuentoProductoSubmit.cantidades = "";
                descuentoProductoSubmit.idGrupoCliente = CurrentCliente != null ? CurrentCliente.idGrupoCliente : 1;
                descuentoProductoSubmit.idProducto = (int)cbxCodigoProducto.SelectedValue;
                descuentoProductoSubmit.idProductos = "";
                descuentoProductoSubmit.idSucursal = ConfigModel.sucursal.idSucursal;
                string dateEmision = String.Format("{0:u}", dtpFechaEmision.Value);
                dateEmision = dateEmision.Substring(0, dateEmision.Length - 1);
                string dateVecimiento = String.Format("{0:u}", dtpFechaPago.Value);
                dateVecimiento = dateVecimiento.Substring(0, dateVecimiento.Length - 1);
                descuentoProductoSubmit.fechaInicio = dateEmision;
                descuentoProductoSubmit.fechaFin = dateVecimiento;


                descuentoProducto = await descuentoModel.descuentoTotalALaFecha(descuentoProductoSubmit);

                txtDescuento.Text =darformato( descuentoProducto.descuento);

                calcularTotal();

                determinarDescuento();
                // para el descuento en grupo


            }
        }

        public async void determinarDescuento()
        {
            string dateEmision = String.Format("{0:u}", dtpFechaEmision.Value);
            dateEmision = dateEmision.Substring(0, dateEmision.Length - 1);
            string dateVecimiento = String.Format("{0:u}", dtpFechaPago.Value);
            dateVecimiento = dateVecimiento.Substring(0, dateVecimiento.Length - 1);


            if (detalleVentas != null)
                    if (detalleVentas.Count != 0)
                    {
                        //primero traemos los descuento correspondientes
                        DescuentoSubmit descuentoSubmit = new DescuentoSubmit();
                        string cantidades = "";
                        string idProductos = "";
                        foreach (DetalleV V in detalleVentas)
                        {
                            cantidades += V.cantidad + ",";
                            idProductos += V.idProducto + ",";
                        }

                        descuentoSubmit.idProductos = idProductos.Substring(0, idProductos.Length - 1);
                        descuentoSubmit.cantidades = cantidades.Substring(0, cantidades.Length - 1);
                        descuentoSubmit.idGrupoCliente = CurrentCliente != null ? CurrentCliente.idGrupoCliente : 1;
                        descuentoSubmit.idSucursal = ConfigModel.sucursal.idSucursal;
                        descuentoSubmit.fechaInicio = dateEmision;
                        descuentoSubmit.fechaFin = dateVecimiento;

                        List<DescuentoReceive> descuentoReceive = await descuentoModel.descuentoTotalALaFechaGrupo(descuentoSubmit);




                        int i = 0;
                        foreach (DetalleV V in detalleVentas)
                        {
                            double descuento=descuentoReceive[i++].descuento;

                            V.descuento = darformato(descuento);
                            // nuevo Precio unitario
                            double precioUnitario =toDouble(V.precioVentaReal);
                            double precioUnitarioDescuento = precioUnitario - (descuento / 100) * precioUnitario;
                            V.precioVenta = darformato(precioUnitarioDescuento);

                            double precioUnitarioI1 = precioUnitarioDescuento;

                            double porcentual = toDouble(V.Porcentual); 
                            double  efectivo  = toDouble(V.Efectivo);
                            if (porcentual != 0)
                            {
                                double datoaux = (porcentual / 100) + 1;
                                precioUnitarioI1 = precioUnitarioDescuento / datoaux;
                            }
                            double precioUnitarioImpuesto = precioUnitarioI1 - efectivo;
                            V.precioUnitario = darformato(precioUnitarioImpuesto);
                            V.total = darformato(precioUnitarioImpuesto * toDouble(V.cantidad));// utilizar para sacar el subtotal
                            V.totalGeneral = darformato(precioUnitarioDescuento * toDouble(V.cantidad));
                           
                            
                        }

                        i = 0;
                    detalleVBindingSource.DataSource = null;
                    detalleVBindingSource.DataSource = detalleVentas;
                       

                        // Calculo de totales y subtotales
                        calculoSubtotal();
                        descuentoTotal();

                    }

        }




        private void descuentoTotal()
        {
            if (cbxTipoMoneda.SelectedValue == null)
                return;
            double descuentoTotal = 0;
            // calcular el descuento total
            foreach (DetalleV V in detalleVentas)
            {
                // calculamos el decuento para cada elemento
                double precioReal = toDouble(V.precioVentaReal);
                double cantidad = toDouble(V.cantidad);
                double total = precioReal * cantidad;
                double descuentoV = total -toDouble( V.totalGeneral);
                descuentoTotal += descuentoV;

            }
            this.Descuento = descuentoTotal;
            Moneda moneda = monedas.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);          
            

            lbDescuentoVenta.Text= moneda.simbolo + ". " + darformato(descuentoTotal);


        }

        public async void determinarStock(double cantidad)
        {


            // determinamos el stock del producto seleccionado
            List<StockReceive> stockReceive = await stockModel.getStockProductoByIdProductoIdCombinacionIdSucursal((int)cbxCodigoProducto.SelectedValue, cbxVariacion.SelectedValue == null ? 0 : (int)cbxVariacion.SelectedValue, ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);
            double stockTotal = stockReceive[0].stock_total;
            double stockDetalle = 0;
            // si exite en el producto en lista detalle
            if (detalleVentas != null && (cbxDescripcion.SelectedIndex != -1))
            {
                foreach (DetalleV V in detalleVentas)
                {
                    if (V.idPresentacion == (int)cbxDescripcion.SelectedValue)
                    {
                        stockDetalle = toDouble( V.cantidad);
                    }

                }
            }
            // restamos si exite en detalles de venta

            
            if (cantidad == 0)
            {

                if (stockTotal == 0)
                {
                    lbStock.Text = "/0";
                }
                else
                {
                    stockTotal -= stockDetalle;
                    lbStock.Text = "/" + stockTotal.ToString();
                }
            }
            else
            {

                if (stockTotal == 0)
                {
                    lbStock.Text = "/0";
                }
                else
                    lbStock.Text = "/" + stockTotal.ToString();
            }



        }

        // comenzando eventos

        private void cbxCodigoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProductoDetalle();
        }

        private void cargarDescripcionDetalle()
        {

            if (cbxDescripcion.SelectedIndex == -1) return;
            try
            {
                /// Buscando el producto seleccionado
                int idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                Presentacion presentacion  = listPresentacion.Find(x => x.idPresentacion == idPresentacion);
                cbxCodigoProducto.SelectedValue = presentacion.idProducto;
                // Llenar los campos del producto escogido.............!!!!!
                if (!enModificar)
                {
                    txtCantidad.Text = "1";
                    txtDescuento.Text = "0";

                }
                /// Cargando presentaciones           

                /// Cargando alternativas del producto
                cargarAlternativasdescripcion();
                determinarDescuentoEImpuesto();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        

        private void calcularPrecioUnitarioDescripcion()
        {
            if (cbxDescripcion.SelectedIndex == -1) return; /// Validación
            try
            {
                if (cbxDescripcion.SelectedIndex == -1)
                {
                    txtPrecioUnitario.Text = currentProducto.precioVenta;
                }
                else
                {
                    // Buscar presentacion elegida
                    int idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                    Presentacion findPresentacion = listPresentacion.Find(x => x.idPresentacion == idPresentacion);

                    // Realizando el calculo
                    double precioCompra = toDouble(currentProducto.precioVenta);
                    double cantidadUnitario =toDouble(findPresentacion.cantidadUnitaria);
                    double precioUnidatio = precioCompra * cantidadUnitario;

                    // Imprimiendo valor
                    txtPrecioUnitario.Text =darformato(precioUnidatio);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Calcular total", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarAlternativasdescripcion()
        {
            /// cargando las alternativas del producto
            alternativaCombinacion = await alternativaModel.cAlternativa31(Convert.ToInt32(cbxDescripcion.SelectedValue));
            alternativaCombinacionBindingSource.DataSource = alternativaCombinacion;
            cbxVariacion.SelectedIndex = -1;
            cbxVariacion.SelectedValue = alternativaCombinacion[0].idCombinacionAlternativa;
            if (!nuevo)
            {
                if (cbxVariacion.SelectedIndex != -1 && currentdetalleV != null)
                    cbxVariacion.SelectedValue = currentdetalleV.idCombinacionAlternativa;

            }
            if (alternativaCombinacion[0].idCombinacionAlternativa <= 0)
                calcularPrecioUnitarioDescripcion();
           
            calcularTotal();
        }


        private void cbxDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDescripcionDetalle();
        }
        // validaciones
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void txtPrecioUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, txtPrecioUnitario.Text);
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, txtDescuento.Text);
        }

        private void txtTotalProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, txtTotalProducto.Text);
        }

        private void dgvDetalleCompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

                int y = e.ColumnIndex;

                if (dgvDetalleOrdenCompra.Columns[y].Name == "acciones")
                {
                    if (dgvDetalleOrdenCompra.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay un registro seleccionado", "eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (nuevo)
                    {
                        int index = dgvDetalleOrdenCompra.CurrentRow.Index; // Identificando la fila actual del datagridview
                        int idPresentacion = Convert.ToInt32(dgvDetalleOrdenCompra.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview
                        DetalleV aux = detalleVentas.Find(x => x.idPresentacion == idPresentacion);

                        dgvDetalleOrdenCompra.Rows.RemoveAt(index);

                        detalleVentas.Remove(aux);

                        calculoSubtotal();
                        calcularDescuento();
                    }
                    else
                    {
                        int index = dgvDetalleOrdenCompra.CurrentRow.Index;
                        int idPresentacion = Convert.ToInt32(dgvDetalleOrdenCompra.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview
                        DetalleV aux = detalleVentas.Find(x => x.idPresentacion == idPresentacion);

                        aux.estado = 9;
                        
                        dgvDetalleOrdenCompra.ClearSelection();
                        dgvDetalleOrdenCompra.Rows[index].DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 224);
                        dgvDetalleOrdenCompra.Rows[index].DefaultCellStyle.ForeColor = Color.FromArgb(250, 5, 73);

                        decorationDataGridView();
                        calculoSubtotal();
                        calcularDescuento();


                     }

                }

            
            
           


        }

        private void dgvDetalleCompra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            // Verificando la existencia de datos en el datagridview
            if (dgvDetalleOrdenCompra.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            enModificar = true;
            int index = dgvDetalleOrdenCompra.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleOrdenCompra.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview
            currentdetalleV = detalleVentas.Find(x => x.idPresentacion == idPresentacion); // Buscando la registro especifico en la lista de registros
            txtCantidad.Text = darformato(toDouble(currentdetalleV.cantidad));
            cbxCodigoProducto.Text = currentdetalleV.codigoProducto;
            cbxDescripcion.Text = currentdetalleV.descripcion;
            txtCantidad.Text = darformato(toDouble(currentdetalleV.cantidad));

            cbxVariacion.Text = currentdetalleV.nombreCombinacion;               
            txtPrecioUnitario.Text = darformato(currentdetalleV.precioVentaReal);
            txtDescuento.Text = darformato(currentdetalleV.descuento);
            txtTotalProducto.Text = darformato(currentdetalleV.totalGeneral);               
            btnAgregar.Enabled = false;
            btnModificar.Enabled = true;
           
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleOrdenCompra.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int index = dgvDetalleOrdenCompra.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleOrdenCompra.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview
            DetalleV aux = detalleVentas.Find(x => x.idPresentacion == idPresentacion);



            aux.cantidad = txtCantidad.Text.Trim(); 
            aux.cantidadUnitaria= txtCantidad.Text.Trim();
            double descuento = toDouble(txtDescuento.Text.Trim());
            aux.descuento = darformato(descuento);
            double precioUnitario = toDouble(txtPrecioUnitario.Text.Trim());

            aux.precioVentaReal = darformato(precioUnitario);
            double precioUnitarioDescuento = precioUnitario - (descuento / 100) * precioUnitario;
            aux.precioVenta = darformato(precioUnitarioDescuento);

            // si es que exite impuesto al producto 

            // impuestoProducto
            // calculamos lo impuesto posibles del producto
            double porcentual = toDouble(aux.Porcentual);
            double efectivo = toDouble(aux.Efectivo);                      
            double precioUnitarioI1 = precioUnitarioDescuento;
            if (porcentual != 0)
            {
                double datoaux = (porcentual / 100) + 1;
                precioUnitarioI1 = precioUnitarioDescuento / datoaux;
            }
            double precioUnitarioImpuesto = precioUnitarioI1 - efectivo;
            aux.precioUnitario = darformato(precioUnitarioImpuesto);
            aux.total = darformato(precioUnitarioImpuesto * toDouble(aux.cantidad));// utilizar para sacar el subtotal
            aux.totalGeneral = darformato(precioUnitarioDescuento * toDouble(aux.cantidad));//utilizar para sacar el suTotal 


            detalleVBindingSource.DataSource = null;
            detalleVBindingSource.DataSource = detalleVentas;
            dgvDetalleOrdenCompra.Refresh();
            calculoSubtotal();
            descuentoTotal();
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            limpiarCamposProducto();
            enModificar = false;
            
        }


        private async void btnAgregar_Click(object sender, EventArgs e)
        {



            if (txtPrecioUnitario.Text == "")
            {
                txtPrecioUnitario.Text = "0";
            }
            if (txtDescuento.Text == "")
            {

                txtDescuento.Text = "0";
            }
            if (txtCantidad.Text == "")
            {
                txtCantidad.Text = "0";
            }

            bool seleccionado = false;
            if (cbxCodigoProducto.SelectedValue != null)
                seleccionado = true;
            if (cbxDescripcion.SelectedValue != null)
                seleccionado = true;
            // if(idProducto)


            if (seleccionado)
            {
                if (detalleVentas == null) detalleVentas = new List<DetalleV>();
                DetalleV detalleV = new DetalleV();

                DetalleV aux = detalleVentas.Find(X => X.idPresentacion == (int)cbxDescripcion.SelectedValue);
                if (aux != null)
                {

                    MessageBox.Show("Este dato ya fue agregado", "presentacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }



                // Creando la lista
                detalleV.cantidad = darformato(txtCantidad.Text.Trim());//1

                //determinamos el stock
                determinarStock(0);
                /// Busqueda presentacion
                Presentacion findPresentacion = listPresentacion.Find(x => x.idPresentacion == Convert.ToInt32(cbxDescripcion.SelectedValue));

                detalleV.idDetalleVenta = 0;
                detalleV.idVenta = 0; 
                detalleV.cantidadUnitaria = darformato(txtCantidad.Text.Trim());
                detalleV.codigoProducto = cbxCodigoProducto.Text.Trim();
                detalleV.descripcion = findPresentacion.descripcion;

                double descuento = toDouble(txtDescuento.Text.Trim());
                detalleV.descuento = darformato(descuento);
                double precioUnitario = toDouble(txtPrecioUnitario.Text.Trim());

                detalleV.precioVentaReal = darformato(precioUnitario);
                double precioUnitarioDescuento = precioUnitario - (descuento / 100) * precioUnitario;
                detalleV.precioVenta = darformato(precioUnitarioDescuento);

                // si es que exite impuesto al producto 

                // impuestoProducto
                listImpuestosProducto = await impuestoModel.impuestoProducto(findPresentacion.idProducto, ConfigModel.sucursal.idSucursal);

                                
                //listImpuesto
                // calculamos lo impuesto posibles del producto
                double porcentual = 0;
                double efectivo = 0;

                int i = 0;
                foreach (ImpuestoProducto I in listImpuestosProducto)
                {
                    if(listIDocumento.Count > 0)
                    if(I.idImpuesto== listIDocumento[i++].idImpuesto)
                    {
                        if (I.porcentual)
                        {
                            porcentual += toDouble(I.valorImpuesto);
                        }
                        else
                        {
                            efectivo += toDouble(I.valorImpuesto);
                        }                  
                    }
                   
                }

                detalleV.Porcentual =darformato( porcentual);
                detalleV.Efectivo = darformato(efectivo);

                double precioUnitarioI1 = precioUnitarioDescuento;
                if (porcentual != 0)
                {
                    double datoaux = (porcentual / 100) + 1;
                    precioUnitarioI1 = precioUnitarioDescuento / datoaux;
                }
                double precioUnitarioImpuesto = precioUnitarioI1 - efectivo;
                detalleV.precioUnitario = darformato(precioUnitarioImpuesto);
                detalleV.total = darformato(precioUnitarioImpuesto * toDouble(detalleV.cantidad));// utilizar para sacar el subtotal
                detalleV.totalGeneral = darformato(precioUnitarioDescuento * toDouble(detalleV.cantidad));//utilizar para sacar el suTotal
                detalleV.valor = darformato(toDouble(detalleV.totalGeneral) - toDouble(detalleV.total));
                // fin cde calculo de necesarios par detalles productos



                detalleV.estado = 1;//5
                detalleV.idCombinacionAlternativa = Convert.ToInt32(cbxVariacion.SelectedValue);//7
                detalleV.idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                detalleV.idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
                detalleV.idSucursal = ConfigModel.sucursal.idSucursal;
                detalleV.nombreCombinacion = cbxVariacion.Text;
                detalleV.nombreMarca = currentProducto.nombreMarca;
                detalleV.nombrePresentacion = cbxDescripcion.Text;
                detalleV.nro = 1;
                // determinar el impuesto                 

                detalleV.eliminar = "";
                int scotk = 0;
                if (lbStock.Text != "")
                {
                    scotk = Convert.ToInt32(lbStock.Text.Substring(1, lbStock.Text.Length - 1));

                }

                detalleV.existeStock = (scotk > 0 && scotk >= Convert.ToInt32(txtCantidad.Text.Trim())) ? 1 : 0;             
                ProductoVenta aux1 = listProductos.Find(x => x.idProducto == (int)cbxCodigoProducto.SelectedValue);
                detalleV.nombreMarca = aux1.nombreMarca;
                detalleV.nombrePresentacion = findPresentacion.nombrePresentacion;
                detalleV.precioEnvio = "0";
                detalleV.ventaVarianteSinStock = aux1.ventaVarianteSinStock;
                // agrgando un nuevo item a la lista
                detalleVentas.Add(detalleV);

                // calcular los descuentos


                // Refrescando la tabla
                detalleVBindingSource.DataSource = null;
                detalleVBindingSource.DataSource = detalleVentas;
                dgvDetalleOrdenCompra.Refresh();

                // Calculo de totales y subtotales
                calculoSubtotal();

                descuentoTotal();

                limpiarCamposProducto();

            }
            else
            {

                MessageBox.Show("Error: elemento no seleccionado", "agregar Elemento", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }



          


        }


        
       

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            determinarDescuentoEImpuesto();
        }

        private void txtPrecioUnitario_TextChanged(object sender, EventArgs e)
        {
            calcularTotal();
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            calcularTotal();
        }

        


        private void calcularDescuento()
        {



            if (cbxTipoMoneda.SelectedValue == null)
                return;

            double descuentoTotal = 0;
            Moneda moneda = monedas.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);

            // calcular el descuento total
            foreach (DetalleV V in detalleVentas)
            {
                // calculamos el decuento para cada elemento

                if (V.estado == 1)
                {
                    double total = toDouble( V.precioUnitario) * toDouble( V.cantidad);
                    double descuentoC = total - toDouble(V.total);
                    descuentoTotal += descuentoC;
                }
                   
            }
            this.Descuento = descuentoTotal;

            lbDescuentoVenta.Text = moneda.simbolo + ". " + darformato(descuentoTotal);

        }

        
        private bool exitePresentacion(int idPresentacion)
        {
            foreach (DetalleV C in detalleVentas)
            {
                if (C.idPresentacion == idPresentacion)
                    return true;
            }

            return false;

        }

        private void decorationDataGridView()
        {
            if (dgvDetalleOrdenCompra.Rows.Count == 0) return;

            foreach (DataGridViewRow row in dgvDetalleOrdenCompra.Rows)
            {
                int idPresentacion = Convert.ToInt32(row.Cells[0].Value); // obteniedo el idCategoria del datagridview

                DetalleV aux = detalleVentas.Find(x => x.idPresentacion == idPresentacion); // Buscando la categoria en las lista de categorias
                if (aux.estado == 0 || aux.estado==9)
                {
                    dgvDetalleOrdenCompra.ClearSelection();
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 224);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(250, 5, 73);
                }
            }
        }
      

        private void limpiarCamposProducto()
        {
            cbxCodigoProducto.SelectedIndex = -1;
            cbxDescripcion.SelectedIndex = -1;

            cbxVariacion.SelectedIndex = -1;
            txtCantidad.Text = "";
            txtDescuento.Text = "";
            txtPrecioUnitario.Text = "";
            txtTotalProducto.Text = "";
        }

        private async void btnComprar_Click(object sender, EventArgs e)
        {
            
            cobrov.cantidadCuotas = 1;
            cobrov.estado = 1;
            cobrov.estadoCobro = 1;
            cobrov.idCobro = 0;
            cobrov.idMoneda = (int)cbxTipoMoneda.SelectedValue;
            cobrov.interes = 0;
            cobrov.montoPagar = 0;

            cobroVentaV.idCaja = FormPrincipal.asignacion.idCaja;
            cobroVentaV.idCajaSesion = ConfigModel.cajaSesion != null ? ConfigModel.cajaSesion.idCajaSesion : 0;
            cobroVentaV.idMedioPago = 1;
            cobroVentaV.idMoneda = (int)cbxTipoMoneda.SelectedValue;
            cobroVentaV.moneda = cbxTipoMoneda.Text;
            cobroVentaV.pagarVenta = chbxPagarCompra.Checked ? 1 : 0;


            foreach (DetalleV V in detalleVentas)
            {
                DatosNotaSalidaVenta aux = new DatosNotaSalidaVenta();
                aux.cantidad = toEntero(V.cantidad);
                aux.descripcion = V.descripcion;
                aux.idAlmacen = almacenVenta.idAlmacen;
                aux.idCombinacionAlternativa = V.idCombinacionAlternativa;
                aux.idProducto = V.idProducto;


                V.descuento = V.descuento.Replace(",", "");
                V.precioUnitario = V.precioUnitario.Replace(",", "");
                V.total = V.total.Replace(",", "");
                V.precioVenta = V.precioVenta.Replace(",", "");
                V.precioVentaReal = V.precioVentaReal.Replace(",", "");
                V.totalGeneral = V.totalGeneral.Replace(",", "");
                V.valor = V.valor.Replace(",", "");
                List<object> list = new List<object>();
                list.Add( V.idProducto);
                list.Add(V.idCombinacionAlternativa);
                list.Add(toEntero( V.cantidad));
                list.Add(V.ventaVarianteSinStock);

                dato.Add(list);


                datosNotaSalidaVenta.Add(aux);
            }

            notasalidaVenta.datosNotaSalida = datosNotaSalidaVenta;
            notasalidaVenta.generarNotaSalida = chbxNotaEntrada.Checked ? 1 : 0;
            notasalidaVenta.idPersonal = PersonalModel.personal.idPersonal;
            notasalidaVenta.idTipoDocumento = 8; // de nota de salida

            ventav.correlativo = txtCorrelativo.Text.Trim();
            ventav.descuento = darformato(this.Descuento).Replace(",", "");
            ventav.direccion = txtDireccionCliente.Text;
            ventav.documentoIdentificacion = cbxTipoDocumento.Text;
            ventav.editar = chbxEditar.Checked;
            ventav.estado = 1;

            string fechaVenta = String.Format("{0:u}", dtpFechaEmision.Value);
            fechaVenta = fechaVenta.Substring(0, fechaVenta.Length - 1);
            string fechaPago = String.Format("{0:u}", dtpFechaPago.Value);
            fechaPago = fechaPago.Substring(0, fechaPago.Length - 1);
            ventav.fechaPago = fechaPago;
            ventav.fechaVenta = fechaVenta;
            ventav.formaPago = "EFECTIVO";
            ventav.idAsignarPuntoVenta = FormPrincipal.asignacion.idAsignarPuntoVenta;
            ventav.idCliente = (int)cbxNombreRazonCliente.SelectedValue;
            ventav.idDocumentoIdentificacion = (int)cbxTipoDocumento.SelectedValue;
            ventav.idPuntoVenta= FormPrincipal.asignacion.idPuntoVenta;
            ventav.idTipoDocumento = (int)cbxNombreDocumento.SelectedValue;
            ventav.idVenta = 0;
            ventav.moneda = cbxTipoMoneda.Text;
            ventav.nombreCliente = cbxNombreRazonCliente.Text;
            ventav.observacion = txtObservaciones.Text;
            ventav.rucDni = txtDocumentoCliente.Text;
            ventav.serie = txtSerie.Text;
            ventav.subTotal= darformato(this.subTotal).Replace(",", "");
            ventav.tipoCambio = 1;
            ventav.tipoVenta = "Con producto";
            ventav.total= darformato(this.total).Replace(",", "");



            // datos para comprobara stock

            verificarStock.dato = dato;
            verificarStock.idPersonal = PersonalModel.personal.idPersonal;
            verificarStock.idSucursal = ConfigModel.sucursal.idSucursal;
            verificarStock.idVenta = 0;

            abastece.dato = dato;
            abastece.idAlmacen = almacenVenta.idAlmacen;
            abastece.idVenta = 0;
            List<verificarStockReceive>  verificarStockReceive = await stockModel.verificarstockproductossucursal(verificarStock);

            abasteceReceive = await stockModel.Abastece(abastece);

            if (abasteceReceive.abastece == 0)
            {
                return;
            }

            DialogResult dialog = MessageBox.Show("¿Desea guardar y a se podra modificar", "Venta",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialog == DialogResult.No)
            {

                this.Close();
                return;
            }
            ventaTotal.cobro = cobrov;
            ventaTotal.cobroventa = cobroVentaV;
            ventaTotal.detalle = detalleVentas;
            ventaTotal.notasalida = notasalidaVenta;
            ventaTotal.venta = ventav;

            ResponseVenta response= await ventaModel.guardar(ventaTotal);
          
            if (response.id>0)
            {
                if(nuevo)
                {
                    MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                else
                {
                    MessageBox.Show(response.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }             
            }
         
                           
        }

        private void Observaciones_Click(object sender, EventArgs e)
        {

        }

     

       
        private void cbxProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxNombreRazonCliente.SelectedIndex == -1) return;

            CurrentCliente = listClientes.Find(X => X.idCliente == (int)cbxNombreRazonCliente.SelectedValue);

            datosClientes();

            determinarDescuento();

            
        }



        private void btnModificar_EnabledChanged(object sender, EventArgs e)
        {
            if(btnModificar.Enabled)
                this.btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(103)))), ((int)(((byte)(178)))));
            else
                btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(88)))), ((int)(((byte)(152)))));       
        }

        private void btnAgregar_EnabledChanged(object sender, EventArgs e)
        {

            if(btnModificar.Enabled)
                this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            else
                
                btnAgregar.BackColor= System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(139)))), ((int)(((byte)(23)))));

        }

        private void btnActulizar_Click(object sender, EventArgs e)
        {            
            cargarProductos();
                    
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            FormProductoNuevo formProductoNuevo = new FormProductoNuevo();
            formProductoNuevo.ShowDialog();
            cargarProductos();
           
        }

        private void txtDni_TextChanged(object sender, EventArgs e)
        {
            String aux = txtDocumentoCliente.Text;

            int nroCaracteres = aux.Length;
            bool exiteProveedor = false;
            if (nroCaracteres == 11 || nroCaracteres ==8)
            {
                try
                {                    
                    CurrentCliente = listClientes.Find(X=>X.numeroDocumento ==aux);
                    if (CurrentCliente!=null)
                    {                
                        exiteProveedor = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "buscar cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (exiteProveedor)
                {
                    // llenamos los dato con el current proveerdor
                    datosClientes();
                }
                else
                {
                    //llenamos los datos en FormproveerdorNuevo
                    FormClienteNuevo formClienteNuevo = new FormClienteNuevo(aux);
                    formClienteNuevo.ShowDialog();
                    cargarClientes();
                    Response response = formClienteNuevo.uCClienteGeneral.rest;
                    if (response != null)
                        if (response.id > 0)
                        {
                            CurrentCliente = listClientes.Find(X => X.idCliente == response.id);
                            datosClientes();
                        }
                }
            }

        }



        private void  datosClientes()
        {
            if (CurrentCliente != null)
            {

                txtDocumentoCliente.removePlaceHolder();
                txtDocumentoCliente.Text = CurrentCliente.numeroDocumento;
                txtDireccionCliente.Text = CurrentCliente.direccion;
                cbxNombreRazonCliente.Text = CurrentCliente.nombreCliente;
                cbxTipoDocumento.SelectedValue = CurrentCliente.idDocumento;

            }
            
        }

        private void executeBuscarCliente()
        {
            Buscarcliente buscarCliente = new Buscarcliente();
            buscarCliente.ShowDialog();
            this.CurrentCliente = buscarCliente.currentCliente;
            datosClientes();
            //determinarDescuento();
        }

      

        

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            executeBuscarCliente();
        }

        private void chbxNotaEntrada_OnChange(object sender, EventArgs e)
        {
            if (!txtCorrelativo.Enabled)
                txtCorrelativo.Enabled = true;
            else
            {
                txtCorrelativo.Enabled = false;
            }
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void cbxTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxNombreDocumento.SelectedIndex == -1) return;


            if (nuevo)
            {
                int idTipoDocumento = (int)cbxNombreDocumento.SelectedValue;
                TipoDocumento tipoDocumento = tipoDocumentos.Find(X => X.idTipoDocumento == idTipoDocumento);

                if(tipoDocumento.tipoCliente == 2)
                {

                    try
                    {
                        DocumentoIdentificacion dd = documentoIdentificacion.Find(X => X.tipoDocumento == "Jurídico");
                        cbxTipoDocumento.SelectedValue= dd.idDocumento;

                    }
                    catch(Exception ex)
                    {


                    }
               

                }
                else
                {
                    DocumentoIdentificacion dd = documentoIdentificacion.Find(X => X.tipoDocumento == "Natural");
                    cbxTipoDocumento.SelectedValue = dd.idDocumento;

                }

                cargarImpuesto((int)cbxNombreDocumento.SelectedValue);
                List<Venta_correlativo> list = await ventaModel.listarNroDocumentoVenta(idTipoDocumento, ConfigModel.asignacionPersonal.idPuntoVenta);
                txtCorrelativo.Text = list[0].correlativoActual;
                txtSerie.Text = list[0].serie;



            }
            cargarFormatoDocumento((int)cbxNombreDocumento.SelectedValue);


        }

        private void btnImportarCotizacion_Click(object sender, EventArgs e)
        {

            FormBuscarCotizacion formBuscarCotizacion = new FormBuscarCotizacion();
            formBuscarCotizacion.ShowDialog();

            currentCotizacion = formBuscarCotizacion.currentCotizacion;
            if (currentCotizacion != null)
            {
                cargarDatosCotizacion();

            }

        }
        
        private void cargarDatosCotizacion()
        {

            cbxNombreRazonCliente.SelectedValue = currentCotizacion.idCliente;
            cbxTipoMoneda.Text = currentCotizacion.moneda;
            cargarCotizacion();

            // resultados
            Moneda moneda = monedas.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);
            cbxTipoMoneda.Text = currentCotizacion.moneda;          
            this.Descuento = toDouble(currentCotizacion.descuento);

            lbDescuentoVenta.Text = moneda.simbolo + ". " + darformato(Descuento);

            this.total = toDouble(currentCotizacion.total);
            lbTotalVentas.Text = moneda.simbolo + ". " + darformato(total);

            this.subTotal = toDouble(currentCotizacion.subTotal);
            lbSubtotal.Text = moneda.simbolo + ". " + darformato(subTotal);
            double impuesto = total - subTotal;
            lbImpuesto.Text = moneda.simbolo + ". " + darformato(impuesto);
        }



        private  void cbxDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
           
           
        }

        private void cbxTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTipoDocumento.SelectedIndex == -1) return;
            if (nuevo)
            {
                if (cbxTipoDocumento.SelectedIndex == -1) return;
                int idTipoDocumento = (int)cbxTipoDocumento.SelectedValue;
                DocumentoIdentificacion tipoDocumento = documentoIdentificacion.Find(X => X.idDocumento == idTipoDocumento);
                if (tipoDocumento.tipoDocumento == "Jurídico")
                {
                    if (tipoDocumentos == null || listClientes ==null) return; 

                    TipoDocumento tipoDocumento2 = tipoDocumentos.Find(X => X.tipoCliente == 2);
                    cbxNombreDocumento.SelectedValue = tipoDocumento2.idTipoDocumento;

                    Cliente cliente = listClientes.Find(X => X.idDocumento == tipoDocumento.idDocumento);
                    if (cliente == null)
                    {

                        limpiarCamposCliente();
                    }

                }

                else
                {

                    if (tipoDocumentos == null || listClientes == null) return;

                    TipoDocumento tipoDocumento2 = tipoDocumentos.Find(X => X.tipoCliente == 1);
                    cbxNombreDocumento.SelectedValue = tipoDocumento2.idTipoDocumento;

                    Cliente cliente = listClientes.Find(X => X.idDocumento == tipoDocumento.idDocumento);
                    if (cliente == null)
                    {

                        limpiarCamposCliente();
                    }
                }


            }
            

        }

        private void limpiarCamposCliente()
        {
            txtDocumentoCliente.Clear();
            cbxNombreRazonCliente.SelectedIndex = -1;
            txtDireccionCliente.Clear(); 


        }

        private void cbxVariacion_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (cbxVariacion.SelectedIndex == -1) return;


            AlternativaCombinacion alternativa = alternativaCombinacion.Find(X => X.idCombinacionAlternativa == (int)cbxVariacion.SelectedValue);
            currentProducto = listProductos.Find(X => X.idProducto == (int)cbxCodigoProducto.SelectedValue);
            double precioUnitario = toDouble(currentProducto.precioVenta) + toDouble(alternativa.precio);
            txtPrecioUnitario.Text = darformato(precioUnitario);

        }

        private async void btnVenta_Click(object sender, EventArgs e)
        {

            cobrov.cantidadCuotas = 1;
            cobrov.estado = 1;
            cobrov.estadoCobro = 1;
            cobrov.idCobro = 0;
            cobrov.idMoneda = (int)cbxTipoMoneda.SelectedValue;
            cobrov.interes = 0;
            cobrov.montoPagar = 0;

            cobroVentaV.idCaja = FormPrincipal.asignacion.idCaja;
            cobroVentaV.idCajaSesion = ConfigModel.cajaSesion != null ? ConfigModel.cajaSesion.idCajaSesion : 0;
            cobroVentaV.idMedioPago = 1;
            cobroVentaV.idMoneda = (int)cbxTipoMoneda.SelectedValue;
            cobroVentaV.moneda = cbxTipoMoneda.Text;
            cobroVentaV.pagarVenta = chbxPagarCompra.Checked ? 1 : 0;


            foreach (DetalleV V in detalleVentas)
            {
                DatosNotaSalidaVenta aux = new DatosNotaSalidaVenta();
                aux.cantidad = toEntero(V.cantidad);
                aux.descripcion = V.descripcion;
                aux.idAlmacen = almacenVenta.idAlmacen;
                aux.idCombinacionAlternativa = V.idCombinacionAlternativa;
                aux.idProducto = V.idProducto;


                V.descuento = V.descuento.Replace(",", "");
                V.precioUnitario = V.precioUnitario.Replace(",", "");
                V.total = V.total.Replace(",", "");
                V.precioVenta = V.precioVenta.Replace(",", "");
                V.precioVentaReal = V.precioVentaReal.Replace(",", "");
                V.totalGeneral = V.totalGeneral.Replace(",", "");
                V.valor = V.valor.Replace(",", "");
                List<object> list = new List<object>();
                list.Add(V.idProducto);
                list.Add(V.idCombinacionAlternativa);
                list.Add(toEntero(V.cantidad));
                list.Add(V.ventaVarianteSinStock);

                dato.Add(list);


                datosNotaSalidaVenta.Add(aux);
            }

            notasalidaVenta.datosNotaSalida = datosNotaSalidaVenta;
            notasalidaVenta.generarNotaSalida = chbxNotaEntrada.Checked ? 1 : 0;
            notasalidaVenta.idPersonal = PersonalModel.personal.idPersonal;
            notasalidaVenta.idTipoDocumento = 8; // de nota de salida

            ventav.correlativo = txtCorrelativo.Text.Trim();
            ventav.descuento = darformato(this.Descuento).Replace(",", "");
            ventav.direccion = txtDireccionCliente.Text;
            ventav.documentoIdentificacion = cbxTipoDocumento.Text;
            ventav.editar = chbxEditar.Checked;
            ventav.estado = 1;

            string fechaVenta = String.Format("{0:u}", dtpFechaEmision.Value);
            fechaVenta = fechaVenta.Substring(0, fechaVenta.Length - 1);
            string fechaPago = String.Format("{0:u}", dtpFechaPago.Value);
            fechaPago = fechaPago.Substring(0, fechaPago.Length - 1);
            ventav.fechaPago = fechaPago;
            ventav.fechaVenta = fechaVenta;
            ventav.formaPago = "EFECTIVO";
            ventav.idAsignarPuntoVenta = FormPrincipal.asignacion.idAsignarPuntoVenta;
            ventav.idCliente = (int)cbxNombreRazonCliente.SelectedValue;
            ventav.idDocumentoIdentificacion = (int)cbxTipoDocumento.SelectedValue;
            ventav.idPuntoVenta = FormPrincipal.asignacion.idPuntoVenta;
            ventav.idTipoDocumento = (int)cbxNombreDocumento.SelectedValue;
            ventav.idVenta = 0;
            ventav.moneda = cbxTipoMoneda.Text;
            ventav.nombreCliente = cbxNombreRazonCliente.Text;
            ventav.observacion = txtObservaciones.Text;
            ventav.rucDni = txtDocumentoCliente.Text;
            ventav.serie = txtSerie.Text;
            ventav.subTotal = darformato(this.subTotal).Replace(",", "");
            ventav.tipoCambio = 1;
            ventav.tipoVenta = "Con producto";
            ventav.total = darformato(this.total).Replace(",", "");



            // datos para comprobara stock

            verificarStock.dato = dato;
            verificarStock.idPersonal = PersonalModel.personal.idPersonal;
            verificarStock.idSucursal = ConfigModel.sucursal.idSucursal;
            verificarStock.idVenta = 0;

            abastece.dato = dato;
            abastece.idAlmacen = almacenVenta.idAlmacen;
            abastece.idVenta = 0;
            List<verificarStockReceive> verificarStockReceive = await stockModel.verificarstockproductossucursal(verificarStock);

            abasteceReceive = await stockModel.Abastece(abastece);

            if (abasteceReceive.abastece == 0)
            {
                return;
            }

            DialogResult dialog = MessageBox.Show("¿Desea guardar y a se podra modificar", "Venta",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialog == DialogResult.No)
            {

                this.Close();
                return;
            }
            ventaTotal.cobro = cobrov;
            ventaTotal.cobroventa = cobroVentaV;
            ventaTotal.detalle = detalleVentas;
            ventaTotal.notasalida = notasalidaVenta;
            ventaTotal.venta = ventav;

            ResponseVenta response = await ventaModel.guardar(ventaTotal);

            if (response.id > 0)
            {
                if (nuevo)
                {
                    MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                else
                {
                    MessageBox.Show(response.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
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

            if (detalleVentas == null) detalleVentas = new List<DetalleV>();



            for (int i = numberOfItemsPrintedSoFar; i < detalleVentas.Count; i++)
            {
                numberOfItemsPerPage++;

                if (numberOfItemsPerPage <= 2)
                {
                    numberOfItemsPrintedSoFar++;

                    if (numberOfItemsPrintedSoFar <= detalleVentas.Count)
                    {

                        if (dictionary.ContainsKey("codigoProducto"))
                        {

                            point1 = dictionary["codigoProducto"];
                            e.Graphics.DrawString(detalleVentas[i].codigoProducto, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }

                        if (dictionary.ContainsKey("nombreCombinacion"))
                        {
                            point1 = dictionary["nombreCombinacion"];
                            e.Graphics.DrawString(detalleVentas[i].nombreCombinacion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));

                        }
                        if (dictionary.ContainsKey("cantidad"))
                        {
                            point1 = dictionary["cantidad"];
                            e.Graphics.DrawString(detalleVentas[i].cantidad, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }

                        if (dictionary.ContainsKey("nombrePresentacion"))
                        {
                            point1 = dictionary["nombrePresentacion"];
                            e.Graphics.DrawString(detalleVentas[i].nombrePresentacion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                        if (dictionary.ContainsKey("descripcion"))
                        {
                            point1 = dictionary["descripcion"];
                            e.Graphics.DrawString(detalleVentas[i].descripcion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));

                        }
                        if (dictionary.ContainsKey("nombreMarca"))
                        {
                            point1 = dictionary["nombreMarca"];
                            e.Graphics.DrawString(detalleVentas[i].nombreMarca, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                        if (dictionary.ContainsKey("precioUnitario"))
                        {
                            point1 = dictionary["precioUnitario"];
                            e.Graphics.DrawString(detalleVentas[i].precioUnitario, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }

                        if (dictionary.ContainsKey("total"))
                        {
                            point1 = dictionary["total"];


                            e.Graphics.DrawString(detalleVentas[i].total, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                        if (dictionary.ContainsKey("precioVenta"))
                        {

                            point1 = dictionary["precioVenta"];
                            e.Graphics.DrawString(detalleVentas[i].precioVenta, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));

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

            if(listformato== null)
            {
                MessageBox.Show("no exite un formato, para este tipo de documento", "Imprimir", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            FormatoDocumento doc = listformato.Last();
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("tamaño pagina", (int)doc.w, (int)doc.h);

            // pre visualizacion
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();


        }
    }
}
