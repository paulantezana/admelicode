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
using Admeli.Componentes;
using Admeli.Compras.buscar;
using Admeli.Compras.Buscar;
using Admeli.Productos.Nuevo;
using Admeli.Ventas.Buscar;
using Entidad;
using Entidad.Configuracion;
using Entidad.Location;
using Modelo;


namespace Admeli.Ventas.Nuevo
{
    public partial class FormCotizacionaNew : Form
    {


        //variables para realizar  un orden de compra ordenCompra

        private CompraOrden compraA { get; set; }
        private PagoOrden pagoA { get; set; }
        private OrdenCompraOrden ordenCompraA { get; set; }
        private List<DetalleOrden> detalleA { get; set; }
        private OrdenCompraTotal compraTotal { get; set; }


        //webservice utilizados
        private MonedaModel monedaModel = new MonedaModel();
        private TipoDocumentoModel tipoDocumentoModel = new TipoDocumentoModel();
        private DocCorrelativoModel docCorrelativoModel = new DocCorrelativoModel();
        
        private AlternativaModel alternativaModel = new AlternativaModel();
      
        private FechaModel fechaModel = new FechaModel();
        private CompraModel compraModel = new CompraModel();
        private OrdenCompraModel ordenCompraModel = new OrdenCompraModel();
        private MedioPagoModel medioPagoModel = new MedioPagoModel();
       
        private LocationModel locationModel = new LocationModel();
       



        private DocumentoIdentificacionModel documentoIdentificacionModel = new DocumentoIdentificacionModel();
        private ClienteModel clienteModel = new ClienteModel();
        private CotizacionModel cotizacionModel = new CotizacionModel();
        private ImpuestoModel impuestoModel = new ImpuestoModel();
        private ProductoModel productoModel = new ProductoModel();
        private PresentacionModel presentacionModel = new PresentacionModel();
        private DescuentoModel descuentoModel = new DescuentoModel();
        private StockModel stockModel = new StockModel();



        /// Sus datos se cargan al abrir el formulario
        private List<Moneda> monedas { get; set; }
        private List<TipoDocumento> tipoDocumentos { get; set; }
        private FechaSistema fechaSistema { get; set; }
       
        private List<MedioPago> medioPagos { get; set; }
        private List<OrdenCompraImpuesto> ordenCompraImpuestos { get; set; }
        private List<OrdenCompraModificar> ordenCompraModificar { get; set; }
        private List<Proveedor> proveedores { get; set; }


        private List<DocumentoIdentificacion> documentoIdentificacion { get; set; }
        private List<Cliente> listClientes { get; set; }
        private List<Impuesto> listImpuesto { get; set; }



        private List<ImpuestoDocumento> listIDocumento { get; set; }
        private List<ProductoVenta> listProductos { get; set; }
        private DescuentoProductoReceive  descuentoProducto{ get; set; }
        private List<DetalleV> detalleVentas { get; set; }
        private List<Presentacion> listPresentacion { get; set; }
        private List<ImpuestoProducto> listImpuestosProducto { get; set; }



        public UbicacionGeografica CurrentUbicacionGeografica;

        private List<DetalleOrden> detalleModificar { get; set; }

        /// Llenan los datos en las interacciones en el formulario 
     
       
      
        private Presentacion currentPresentacion { get; set; }
        private OrdenCompra currentOrdenCompra { get; set; }
        private int currentIdOrden { get; set; }
     


        private CorrelativoCotizacion correlativoCotizacion { get; set; }
        private ProductoVenta currentProducto { get; set; }

        private ImpuestoProducto impuestoProducto { get; set; }



        /// Se preparan para realizar la compra de productos

        // notaEntrada,pago,pagoCompra
        private NotaEntrada currentNotaEntrada { get; set; }
        private Pago currentPago { get; set; }
        public PagoCompra currentPagoCompra { get; set; }


        private Cliente CurrentCliente { get; set; }












        public Sucursal idSucursal { get; set; }
        public Personal personal;
        public int nroNuevo = 0;
       
        private bool nuevo { get; set; }

        private int idPresentacionDatagriview = 0;    
        int nroDecimales = 2;
        string formato { get; set; }
        private int nroCaracteres = 0;
        private double subTotal = 0;
        private double Descuento = 0;
        private double impuesto = 0;
        private double total = 0;
        
        public FormCotizacionaNew()
        {
            InitializeComponent();


       
           
            this.nuevo = true;
            cargarFechaSistema();
            compraA = new CompraOrden();
            pagoA = new PagoOrden();
            ordenCompraA = new OrdenCompraOrden();
            detalleA = new List<DetalleOrden>();
            compraTotal = new OrdenCompraTotal();          
            formato = "{0:n" + nroDecimales + "}";
            
            currentIdOrden = 0;
            cargarResultadosIniciales();

        }

        private void cargarResultadosIniciales()
        {


            lbSubtotal.Text = "s/" + ". " + darformato(0);
            lbDescuentoVentas.Text = "s/" + ". " + darformato(0);
            lbImpuesto.Text = "s/" + ". " + darformato(0);
            lbTotalCompra.Text = "s/" + ". " + darformato(0);

        }
        private string darformato(object dato)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
        }

        private double toDouble(string texto)
        {
            return double.Parse(texto, CultureInfo.GetCultureInfo("en-US")); ;
        }
        public FormCotizacionaNew(OrdenCompra currentOrdenCompra)
        {
            InitializeComponent();     
            this.currentOrdenCompra = currentOrdenCompra;
            this.currentIdOrden = currentOrdenCompra.idOrdenCompra;
            this.nuevo = false;
            compraA = new CompraOrden();
            pagoA = new PagoOrden();
            ordenCompraA = new OrdenCompraOrden();
            detalleA = new List<DetalleOrden>();
            compraTotal = new OrdenCompraTotal();
            

            
                      
            formato = "{0:n" + nroDecimales + "}";
            detalleModificar = new List<DetalleOrden>();

        }
        #region ================================ Root Load ================================
        private void FormCompraNew_Load(object sender, EventArgs e)
        {


            if (nuevo == true)
            {
                this.reLoad();
                cargarCorrelactivo();
               
            }
            else
            {
                this.reLoad();               
                cargarImpuesto();                
                cargarProductos();                            
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
            cargarCorrelactivo();
            cargarImpuesto();
            cargarPresentacion();

        }
        #endregion








        #region ============================== Load ==============================

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar Doc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }       
        }
        private async void cargarImpuesto()
        {
            try
            {
                // variables necesarios para el calculo del impuesto de la venta
                listImpuesto = await impuestoModel.listarImpuesto();

                listIDocumento = await impuestoModel.impuestoTipoDoc(ConfigModel.sucursal.idSucursal, 4); // tipo documento 4 

              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar Impuesto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private async void cargarCorrelactivo()
        {
            List<CorrelativoCotizacion> list = await cotizacionModel.Correlativo(ConfigModel.sucursal.idSucursal);
            correlativoCotizacion = list[0];
            txtComprobante.Text = "COTIZACION";
            txtSerie.Text = correlativoCotizacion.serie;
            txtCorrelativo.Text = correlativoCotizacion.correlativoActual;
        }
        private async void cargarMonedas()
        {
            try
            {
                monedas = await monedaModel.monedas();
                cbxTipoMoneda.DataSource = monedas;
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
                    dtpEmision.Value = currentOrdenCompra.fecha.date;

                }
                else
                {

                    fechaSistema = await fechaModel.fechaSistema();
                    dtpEmision.Value = fechaSistema.fecha;
                    dtpFechaVecimiento.Value = fechaSistema.fecha;

                }

                //dtpFechaPago.Value = fechaSistema.fecha;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
       










        //private async void cargarOrden()
        //{
        //    try
        //    {
        //        ordenCompraModificar = await ordenCompraModel.dcomprasordencompra(currentIdOrden);
        //        if (nuevo == false)
        //        {
        //            cargardatagriw();
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message, "cargar Orden", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
           
        //}
        //private void cargardatagriw()
        //{
        //    DetalleOrden detalleCompra;
        //    if (detalleA == null) detalleA = new List<DetalleOrden>();
        //    foreach (OrdenCompraModificar o in ordenCompraModificar)
        //    {

        //        detalleCompra = new DetalleOrden();

        //        detalleCompra.cantidad = o.cantidad;
        //        detalleCompra.cantidadUnitaria = o.cantidadUnitaria;
        //        detalleCompra.codigoProducto = o.codigoProducto;
        //        detalleCompra.descripcion = o.descripcion;
        //        detalleCompra.descuento = o.descuento;
        //        detalleCompra.estado = o.estado;
        //        detalleCompra.idCombinacionAlternativa = o.idCombinacionAlternativa;
        //        detalleCompra.idCompra = o.idCompra;
        //        detalleCompra.idDetalleCompra = o.idDetalleCompra;
        //        detalleCompra.idPresentacion = o.idPresentacion;
        //        detalleCompra.idProducto = o.idProducto;
        //        detalleCompra.idSucursal = o.idSucursal;
        //        detalleCompra.nombreCombinacion = o.nombreCombinacion;
        //        detalleCompra.nombreMarca = o.nombreMarca;
        //        detalleCompra.nombrePresentacion = o.nombrePresentacion;
        //        detalleCompra.nro = o.nro;
        //        detalleCompra.precioUnitario = o.precioUnitario;
        //        detalleCompra.total = o.total;

        //        // agrgando un nuevo item a la lista
        //        detalleA.Add(detalleCompra);

        //        // Refrescando la tabla

        //    }
        //    detalleModificar.AddRange(detalleA);
        //    detalleOrdenBindingSource.DataSource = null;
        //    detalleOrdenBindingSource.DataSource = detalleA;
        //    //dataGridView.Refresh();
        //    // Calculo de totales y subtotales
        //    calculoSubtotal();
        //    calcularDescuento();

        //    decorationDataGridView();

        //}                 
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
            lbTotalCompra.Text = moneda.simbolo + ". " + darformato(TotalLocal);

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
                txtCantidad.Text = "1";
                txtDescuento.Text = "0";
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
            List<AlternativaCombinacion> alternativaCombinacion = await alternativaModel.cAlternativa31(Convert.ToInt32(cbxCodigoProducto.SelectedValue));
            alternativaCombinacionBindingSource.DataSource = alternativaCombinacion;
            /// calculos
            calcularPrecioUnitarioProducto();
            calcularTotal();
        }

      
        // el calculo se hace entre las fechas  entre fechas
        public async void determinarDescuentoEImpuesto()
        {
            if (txtCantidad.Text != "" && cbxCodigoProducto.SelectedValue != null)
            {
                DescuentoProductoSubmit descuentoProductoSubmit = new DescuentoProductoSubmit();

                descuentoProductoSubmit.cantidad = Convert.ToInt32(txtCantidad.Text);
                descuentoProductoSubmit.cantidades = "";
                descuentoProductoSubmit.idGrupoCliente = CurrentCliente != null ? CurrentCliente.idGrupoCliente : 1;
                descuentoProductoSubmit.idProducto = (int)cbxCodigoProducto.SelectedValue;
                descuentoProductoSubmit.idProductos = "";
                descuentoProductoSubmit.idSucursal = ConfigModel.sucursal.idSucursal;
                string dateEmision = String.Format("{0:u}", dtpEmision.Value);
                dateEmision = dateEmision.Substring(0, dateEmision.Length - 1);
                string dateVecimiento = String.Format("{0:u}", dtpFechaVecimiento.Value);
                dateVecimiento = dateVecimiento.Substring(0, dateVecimiento.Length - 1);
                descuentoProductoSubmit.fechaInicio = dateEmision;
                descuentoProductoSubmit.fechaFin = dateVecimiento;


                descuentoProducto = await descuentoModel.descuentototalentrefechas(descuentoProductoSubmit);

                txtDescuento.Text =darformato( descuentoProducto.descuento);

                calcularTotal();

                determinarDescuento();
                // para el descuento en grupo


            }
        }

        public async void determinarDescuento()
        {
            string dateEmision = String.Format("{0:u}", dtpEmision.Value);
            dateEmision = dateEmision.Substring(0, dateEmision.Length - 1);
            string dateVecimiento = String.Format("{0:u}", dtpFechaVecimiento.Value);
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

                            double porcentual = toDouble(V.porcentual); 
                            double  efectivo  = toDouble(V.efectivo);
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
                        dgvDetalleOrdenCompra.Refresh();

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
            

            lbDescuentoVentas.Text= moneda.simbolo + ". " + darformato(descuentoTotal);


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
                txtCantidad.Text = "1";
                txtDescuento.Text = "0";
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
            if (cbxDescripcion.SelectedIndex == -1) return; /// validacion
                                                            /// cargando las alternativas del producto
            List<AlternativaCombinacion> alternativaCombinacion = await alternativaModel.cAlternativa31(Convert.ToInt32(cbxDescripcion.SelectedValue));
            alternativaCombinacionBindingSource.DataSource = alternativaCombinacion;

            /// calculos
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
                        int idPresentacion = Convert.ToInt32(dgvDetalleOrdenCompra.Rows[index].Cells[1].Value); // obteniedo el idRegistro del datagridview
                        DetalleOrden aux = detalleA.Find(x => x.idPresentacion == idPresentacion);

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
            int index = dgvDetalleOrdenCompra.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleOrdenCompra.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview
            DetalleV aux = detalleVentas.Find(x => x.idPresentacion == idPresentacion); // Buscando la registro especifico en la lista de registros
            cbxCodigoProducto.Text = aux.codigoProducto;
            cbxDescripcion.Text = aux.descripcion;
            cbxVariacion.Text = aux.nombreCombinacion;
            cbxDescripcion.Text = aux.nombrePresentacion;
            txtCantidad.Text =darformato(aux.cantidad);
            txtPrecioUnitario.Text = darformato(aux.precioVentaReal);
            txtDescuento.Text = darformato( aux.descuento);
            txtTotalProducto.Text = darformato( aux.totalGeneral);
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
            double porcentual = toDouble(aux.porcentual);
            double efectivo = toDouble(aux.efectivo);                      
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

                detalleV.idDetalleCotizacion=0;
                detalleV.idCotizacion = 0;// depende luego 
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
                detalleV.porcentual =darformato( porcentual);
                detalleV.efectivo = darformato(efectivo);

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
                detalleV.idDetalleVenta = 0;
                detalleV.idVenta = 0;
                ProductoVenta aux1 = listProductos.Find(x => x.idProducto == (int)cbxCodigoProducto.SelectedValue);
                detalleV.nombreMarca = aux1.nombreMarca;
                detalleV.nombrePresentacion = findPresentacion.nombrePresentacion;
                detalleV.precioEnvio = "";
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













            ////validando campos
            //if (txtPrecioUnitario.Text == "")
            //{
            //    txtPrecioUnitario.Text = "0";
            //}
            //if (txtDescuento.Text == "")
            //{

            //    txtDescuento.Text = "0";
            //}
            //if (txtCantidad.Text == "")
            //{
            //    txtCantidad.Text = "0";
            //}

            //bool seleccionado = false;
            //if (cbxCodigoProducto.SelectedValue != null)
            //    seleccionado = true;
            //if (cbxDescripcion.SelectedValue != null)
            //    seleccionado = true;

            //if (seleccionado)
            //{




            //    if (detalleA == null) detalleA = new List<DetalleOrden>();
            //    DetalleOrden detalleCompra = new DetalleOrden();

            //    if (exitePresentacion(Convert.ToInt32(cbxDescripcion.SelectedValue)))
            //    {

            //        MessageBox.Show("Este dato ya fue agregado", "presentacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return;

            //    }
            //    // Creando la lista
            //    detalleCompra.cantidad = Int32.Parse(txtCantidad.Text.Trim(), CultureInfo.GetCultureInfo("en-US"));
            //    /// Busqueda presentacion
            //    Presentacion findPresentacion = presentaciones.Find(x => x.idPresentacion == Convert.ToInt32(cbxDescripcion.SelectedValue));
            //    detalleCompra.cantidadUnitaria = toDouble( txtCantidad.Text);
            //    detalleCompra.codigoProducto = cbxCodigoProducto.Text.Trim();
            //    detalleCompra.descripcion = cbxDescripcion.Text.Trim();
            //    detalleCompra.descuento = toDouble(txtDescuento.Text.Trim());
            //    detalleCompra.estado = 1;
            //    detalleCompra.idCombinacionAlternativa = Convert.ToInt32(cbxVariacion.SelectedValue);
            //    detalleCompra.idCompra = 0;
            //    detalleCompra.idDetalleCompra = 0;
            //    detalleCompra.idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
            //    detalleCompra.idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
            //    detalleCompra.idSucursal = ConfigModel.sucursal.idSucursal;
            //    detalleCompra.nombreCombinacion = cbxVariacion.Text;
            //    detalleCompra.nombreMarca = currentProducto.nombreMarca;
            //    detalleCompra.nombrePresentacion = cbxDescripcion.Text;
            //    detalleCompra.nro = 1;
            //    detalleCompra.precioUnitario = toDouble(txtPrecioUnitario.Text.Trim());
            //    detalleCompra.total = toDouble(txtTotalProducto.Text.Trim());
            //    // agrgando un nuevo item a la lista
            //    detalleA.Add(detalleCompra);
            //    // Refrescando la tabla
            //    detalleOrdenBindingSource.DataSource = null;
            //    detalleOrdenBindingSource.DataSource = detalleA;
            //    dgvDetalleOrdenCompra.Refresh();
            //    // Calculo de totales y subtotales e impuestos
            //    calculoSubtotal();
            //    calcularDescuento();
            //    limpiarCamposProducto();

            //    decorationDataGridView();

            //}
            //else
            //{

            //    MessageBox.Show("Error: elemento no seleccionado", "agregar Elemento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}


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
            foreach (DetalleOrden C in detalleA)
            {
                // calculamos el decuento para cada elemento

                if (C.estado == 1)
                {
                    double total = C.precioUnitario * C.cantidad;
                    double descuentoC = total - C.total;
                    descuentoTotal += descuentoC;
                }
                   
            }
            this.Descuento = descuentoTotal;

            lbDescuentoVentas.Text = moneda.simbolo + ". " + darformato(descuentoTotal);

        }

        
        private bool exitePresentacion(int idPresentacion)
        {
            foreach (DetalleOrden C in detalleA)
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
                int idPresentacion = Convert.ToInt32(row.Cells[1].Value); // obteniedo el idCategoria del datagridview

                DetalleOrden aux = detalleA.Find(x => x.idPresentacion == idPresentacion); // Buscando la categoria en las lista de categorias
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




            //if (nroNuevo != 1)
            //{
            //    //pago
            //    pagoA.estado = 1;// 8 si
            //    pagoA.estadoPago = 1;//ver que significado
            //    // Moneda aux = monedaBindingSource.;
                

            //    Moneda currentMoneda = monedas.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);
            //    //  Moneda aux = monedaBindingSource.List[i] as Moneda;
            //    pagoA.idMoneda = currentMoneda.idMoneda;
            //    pagoA.idPago = currentOrdenCompra != null ? currentOrdenCompra.idPago : 0;
            //    pagoA.motivo = "COMPRA";
            //    pagoA.saldo = this.total;
            //    pagoA.valorPagado = 0;
            //    pagoA.valorTotal = this.total;
            //    // compra
            //    string date = String.Format("{0:u}", dtpEmision.Value);
            //    date = date.Substring(0, date.Length - 1);
            //    compraA.descuento = this.Descuento;//CAMBIAR SEGUN DATOS

            //    if(currentProveedor==null)
            //    {
            //        //validar 
            //        MessageBox.Show("no hay ningun proveedor seleccionado", "proveedor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            //        return;


            //    }
            //    compraA.direccion = currentProveedor .direccion ;
                
            //    compraA.estado = 8;//es una orden de compra que no ha sido asignado a una compra
            //    compraA.fechaFacturacion = date; // la fecha data en  dptfecha Entrega
            //    compraA.fechaPago = date;
            //    compraA.formaPago = "EFECTIVO";
            //    compraA.idCompraValor = currentOrdenCompra != null ? currentOrdenCompra.idCompra : 0;

            //    compraA.idPersonal =PersonalModel.personal.idPersonal;
            //    compraA.idProveedor = currentProveedor.idProveedor;
            //    compraA.idSucursal = ConfigModel.sucursal.idSucursal;
            //    compraA.idTipoDocumento = 1;// orden compra

            //    compraA.moneda = cbxTipoMoneda.Text;// ver si es correcto

            //    compraA.numeroDocumento = "";//
            //    compraA.observacion = txtObservaciones.Text;
            //    compraA.plazoEntrega = date; // ver si es correcto
            //    compraA.rucDni = currentProveedor.ruc;
            //    compraA.subTotal = this.subTotal;
                
            //    compraA.tipoCompra = "Con productos";
            //    compraA.total = this.total;
              
            //    compraA.nombreProveedor = currentProveedor.razonSocial;
               
            //    //orden de compra
      
            //    ordenCompraA.total = total;
            //    ordenCompraA.estado = 1;
           
            //    ordenCompraA.moneda = currentMoneda.moneda;
            //    ordenCompraA.observacion = txtObservaciones.Text;
            //    ordenCompraA.tipoCambio = Convert.ToInt32(currentMoneda.tipoCambio);
            //    ordenCompraA.formaPago = "EFECTIVO";
            //    ordenCompraA.nombreProveedor = currentProveedor.razonSocial;
            //    ordenCompraA.rucDni = currentProveedor.ruc;
            //    ordenCompraA.direccion = currentProveedor.direccion;
            //    ordenCompraA.plazoEntrega = date;
            //    ordenCompraA.idCompraValor = currentOrdenCompra != null ? currentOrdenCompra.idCompra : 0;//algunas dudas sobre este dato
            //    ordenCompraA.numeroDocumento = "";
            //    ordenCompraA.idProveedor = currentProveedor.idProveedor;
            //    ordenCompraA.tipoCompra = "con productos";
            //    ordenCompraA.subTotal =this.subTotal;

            //    ordenCompraA.estado = 1;
            //    ordenCompraA.idPersonal = PersonalModel.personal.idPersonal;
            //    ordenCompraA.idTipoDocumento = 1;// orden compra
            //    ordenCompraA.idSucursal = ConfigModel.sucursal.idSucursal;
            //    ordenCompraA.fechaFacturacion = date;
            //    ordenCompraA.fechaPago = date;
            //    ordenCompraA.idUbicacionGeografica = CurrentUbicacionGeografica.idUbicacionGeografica;
            //    ordenCompraA.idOrdenCompra = currentOrdenCompra != null ? currentOrdenCompra.idOrdenCompra : 0;

            //    compraTotal.compra = compraA;
            //    compraTotal.detalle = detalleA;
            //    compraTotal.ordencompra = ordenCompraA;
            //    compraTotal.pago = pagoA;
            //    //

            //    await ordenCompraModel.guardar(compraTotal);


            //    if (nuevo)
            //    {
            //        MessageBox.Show("Datos Guardados", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        nroNuevo = 1;
            //    }
            //    else
            //        MessageBox.Show("Datos  modificador", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //else
            //    btnComprar.Enabled = false;
        }

        private void Observaciones_Click(object sender, EventArgs e)
        {

        }

     

       
        private void cbxProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCliente.SelectedIndex == -1) return;

            CurrentCliente = listClientes.Find(X => X.idCliente == (int)cbxCliente.SelectedValue);

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

        private async void btnComprarOrdenCompra_Click(object sender, EventArgs e)
        {
            if (currentOrdenCompra != null)
            {

                CompraOrdenCompra compra = new CompraOrdenCompra();
                compra.estado = 1;// 
                compra.formaPago = "EFECTIVO";

                int i = cbxTipoMoneda.SelectedIndex;
                compra.idMoneda = monedas[i].idMoneda;
                compra.idOrdenCompra = currentOrdenCompra.idOrdenCompra;
                compra.moneda = monedas[i].moneda;
                compra.subTotal = subTotal;
                compra.tipoCambio = Convert.ToInt32(monedas[i].tipoCambio);
                compra.total = total;

                try
                {

                    await ordenCompraModel.comprarOrdenCompra(compra);


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message, "consulta sunat", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            
                MessageBox.Show("Orden de compra realizada", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
              
                btnComprar.Enabled = false;
            }

            else
                MessageBox.Show("no exite orden de compra", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void txtDni_TextChanged(object sender, EventArgs e)
        {
            String aux = txtDni.Text;

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
            txtDni.removePlaceHolder();
            txtDni.Text = CurrentCliente.numeroDocumento;
            txtDireccionCliente.Text = CurrentCliente.direccion;
            cbxCliente.Text = CurrentCliente.nombreCliente;
            cbxTipoDocumento.SelectedValue = CurrentCliente.idDocumento;
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
    }
}
