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
using Admeli.Compras.Buscar;
using Admeli.Ventas.Buscar;
using Entidad;
using Entidad.Configuracion;
using Modelo;

namespace Admeli.Ventas.Nuevo
{
    public partial class FormVentaNuevo1 : Form
    {

        // compra 
        PagoC pagoC;
        CompraC compraC;
        List<DetalleC> detalleC;
        PagocompraC pagocompraC;
        List<DatoNotaEntradaC> datoNotaEntradaC;
        NotaentradaC notaentrada;
        compraTotal compraTotal;

        // para modificar compra
        List<CompraModificar> list;
        List<CompraRecuperar> datosProveedor;

        // datos de proveedor
        List<Proveedor> ListProveedores = new List<Proveedor>();
        private MonedaModel monedaModel = new MonedaModel();
        private TipoDocumentoModel tipoDocumentoModel = new TipoDocumentoModel();
        private ProductoModel productoModel = new ProductoModel();
        private AlternativaModel alternativaModel = new AlternativaModel();
        private PresentacionModel presentacionModel = new PresentacionModel();
        private FechaModel fechaModel = new FechaModel();
        private CompraModel compraModel = new CompraModel();
        private MedioPagoModel medioPagoModel = new MedioPagoModel();
        private AlmacenModel almacenModel = new AlmacenModel();
        private CompraModel compra = new CompraModel();
        private ProveedorModel proveedormodel = new ProveedorModel();
        private VentaModel ventaModel = new VentaModel();
        private DescuentoModel descuentoModel = new DescuentoModel();

        private StockModel stockModel = new StockModel();
        private DocumentoIdentificacionModel documentoIdentificacionModel = new DocumentoIdentificacionModel();
        private List<DocumentoIdentificacion> documentoIdentificaciones;
        /// Sus datos se cargan al abrir el formulario
        private List<Moneda> monedas { get; set; }
        private List<TipoDocumento> tipoDocumentos { get; set; }
        private FechaSistema fechaSistema { get; set; }

        private List<ProductoVenta> productos { get; set; }
        private List<MedioPago> medioPagos { get; set; }

        /// Llenan los datos en las interacciones en el formulario 
        private List<PresentacionV> presentaciones { get; set; }
        private ProductoVenta currentProducto { get; set; }
        private Proveedor currentProveedor { get; set; }// no sirver
        private Cliente currentCliente { get; set; }
        private Venta currentVenta { get; set; }
        private Sucursal sucursal { get; set; }
        private Personal personal { get; set; }

        /// Se preparan para realizar la compra de productos
        private List<DetalleCompra> detalleCompras { get; set; }

        private List<DetalleV> detalleVentas { get; set; }

        private Compra currentCompra { get; set; } // notaEntrada,pago,pagoCompra
        private NotaEntrada currentNotaEntrada { get; set; }
        private Pago currentPago { get; set; }
        private PagoCompra currentPagoCompra { get; set; }
        private bool mostrarOriginal = false;
        private List<DescuentoReceive> descuentoReceive;
        private DescuentoProductoReceive descuentoProducto;
        private AlmacenComra currentAlmacenCompra { get; set; }
        private List<AlmacenComra> Almacen { get; set; }
        private bool nuevo { get; set; }

        #region ============================ Constructor ============================
        public FormVentaNuevo1(Sucursal sucursal, Personal personal)
        {
            InitializeComponent();
            this.nuevo = true;
            cargarFechaSistema();
            this.personal = personal;
            this.sucursal = sucursal;
            pagoC = new PagoC();
            compraC = new CompraC();
            detalleC = new List<DetalleC>();
            pagocompraC = new PagocompraC();
            datoNotaEntradaC = new List<DatoNotaEntradaC>();
            notaentrada = new NotaentradaC();
            compraTotal = new compraTotal();

        }

        public FormVentaNuevo1(Compra currentCompra, Sucursal sucursal, Personal personal)
        {
            InitializeComponent();
            this.currentCompra = currentCompra;

            this.nuevo = false;

            cargarFechaSistema();
            this.personal = personal;
            this.sucursal = sucursal;
            pagoC = new PagoC();
            compraC = new CompraC();
            detalleC = new List<DetalleC>();
            pagocompraC = new PagocompraC();
            datoNotaEntradaC = new List<DatoNotaEntradaC>();
            notaentrada = new NotaentradaC();
            compraTotal = new compraTotal();

            txtDireccion.Enabled = false;
            txtNombreCliente.Enabled = false;
            btnImportarCotizacion.Visible = false;
        }
        #endregion

        #region ============================== CRUDS ==============================
        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            executeBuscarCliente();
        }
        #endregion

        #region ================================ Root Load ================================
        private void FormComprarNuevo_Load(object sender, EventArgs e)
        {
            //Maximizar formulario 
            //this.WindowState = FormWindowState.Maximized;
            

            if (nuevo == true)
                this.reLoad();
            else
            {
                this.reLoad();
                listarDetalleCompraByIdCompra();
                listarDatosProveedorCompra();
                //this.cargarOrden();
                //cargarImpuesto();
                //cargarubigeoActual();
                //cargarProductos();
                //cargarProveedor();

                btnRealizarCompra.Text = "Modificar compra";
            }
            AddButtonColumn();

            //AutoScroll
            this.plDerecho.AutoScroll = true;
            this.plIzquierdoCentrado.AutoScroll = true;
            
        }


        private void reLoad()
        {
            cargarMonedas();
            cargarTipoDocumento();
            cargarFechaSistema();
            cargarProductos();
            cargarMedioPago();
            cargarAlmacen();
            cargartiposDocumentos();
            int i = ConfigModel.cajaSesion != null ? ConfigModel.cajaSesion.idCajaSesion : 0;
            if (i == 0)
            {


                cbxPagarCompra.Enabled = false;
                cbxPagarCompra.Checked = false;
            }
        }
        #endregion

        private void pintarBordePanel()
        {
            DrawShape drawShape = new DrawShape();
            //drawShape.bottomLine(panelHeader);
            drawShape.lineBorder(plTipoComprobante, 157, 157, 157);
            //drawShape.lineBorder(plTipoDocumento, 157, 157, 157);
            //drawShape.lineBorder(plFechaPago, 157, 157, 157);
            //drawShape.lineBorder(plFechaVenta, 157, 157, 157);
            //drawShape.lineBorder(plMoneda, 157, 157, 157);
            //drawShape.lineBorder(plProducto, 157, 157, 157);
            //drawShape.lineBorder(plPresentacion, 157, 157, 157);
            //drawShape.lineBorder(plVariante, 157, 157, 157);
            //drawShape.lineBorder(plDescripcion, 157, 157, 157);
            //drawShape.lineBorder(plIzquierdo, 30, 144, 255);
        }

        #region ============================== Load ==============================
        private async void cargartiposDocumentos()
        {

            documentoIdentificaciones = await documentoIdentificacionModel.docIdentificacionNatural();
            documentoIdentificacionBindingSource.DataSource = documentoIdentificaciones;
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

                //buttons.DisplayIndex = 0;
            }

            dataGridView.Columns.Add(buttons);

        }
        private async void listarDetalleCompraByIdCompra()
        {

            list = await compra.dCompras(currentCompra.idCompra);
            // cargar datos correpondienetes
            if (detalleCompras == null) detalleCompras = new List<DetalleCompra>();
            foreach (CompraModificar C in list)
            {
                DetalleCompra aux = new DetalleCompra();
                aux.idCompra = C.idCompra;
                aux.cantidad = C.cantidad;
                aux.cantidadUnitaria = C.cantidadUnitaria;
                aux.codigoProducto = C.codigoProducto;
                aux.descripcion = C.descripcion;
                aux.descuento = C.descuento;
                aux.estado = C.estado;
                aux.idCombinacionAlternativa = C.idCombinacionAlternativa;
                aux.idDetalleCompra = C.idDetalleCompra;
                aux.idPresentacion = C.idPresentacion;
                aux.idProducto = C.idProducto;
                aux.idSucursal = C.idSucursal;
                aux.nombreCombinacion = C.nombreCombinacion;
                aux.nombreMarca = C.nombreMarca;
                aux.nombrePresentacion = C.nombrePresentacion;
                aux.nro = C.nro;
                aux.precioUnitario = C.precioUnitario;
                aux.total = C.total;
                detalleCompras.Add(aux);


            }
            // Refrescando la tabla
            detalleCompraBindingSource.DataSource = null;
            detalleCompraBindingSource.DataSource = detalleCompras;
            dataGridView.Refresh();

            // Calculo de totales y subtotales
            calculoSubtotal();
        }

        private async void listarDatosProveedorCompra()
        {

            datosProveedor = await compraModel.Compras(currentCompra.idCompra);
            txtDireccion.Text = datosProveedor[0].nombreProveedor;
            txtNombreCliente.Text = datosProveedor[0].direccion;
            dtpFechaVenta.Value = datosProveedor[0].fechaFacturacion.date;
            dtpFechaPago.Value = datosProveedor[0].fechaPago.date;
            txtSubTotal.Text = Convert.ToString(datosProveedor[0].subTotal);
            txtTotal.Text = Convert.ToString(datosProveedor[0].total);
            cbxMoneda.Text = datosProveedor[0].moneda;



        }
        private async void cargarMonedas()
        {
            try
            {
                monedas = await monedaModel.monedas();
                cbxMoneda.DataSource = monedas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarTipoDocumento()
        {
            try
            {
                tipoDocumentos = await tipoDocumentoModel.tipoDocumentoVentas();
                cbxTipoComprobante.DataSource = tipoDocumentos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarFechaSistema()
        {
            try
            {
                if (!nuevo) return;
                fechaSistema = await fechaModel.fechaSistema();
                dtpFechaVenta.Value = fechaSistema.fecha;
                dtpFechaPago.Value = fechaSistema.fecha;
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

                productos = await productoModel.productos(ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);
                productoVentaBindingSource.DataSource = productos;
                cbxCodigoProducto.SelectedIndex = -1;
                cbxDescripcion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarMedioPago()
        {
            try
            {
                medioPagos = await medioPagoModel.medioPagos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
        private async void cargarAlmacen()
        {

            Almacen = await almacenModel.almacenesCompra(sucursal.idSucursal, personal.idPersonal);
            currentAlmacenCompra = Almacen[0];
        }


        #region ============================  Agregar Producto Al Carro ============================
        private void cbxCodigoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProductoDetalle();
        }

        private void cbxCodigoProducto_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cargarProductoDetalle();
        }

        private void cbxDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDescripcionDetalle();
        }
        private void cbxDescripcion_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cargarDescripcionDetalle();
        }
        private void textCantidad_OnValueChanged(object sender, EventArgs e)
        {

            // calcular descuento que abrira para el producto

            calcularTotal();


        }

        private void textDescuento_OnValueChanged(object sender, EventArgs e)
        {
            calcularTotal();
        }

        private void cbxUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            // calcularPrecioUnitario();
            calcularTotal();
        }
        private void cbxPresentacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcularTotal();
        }
        private bool exitePresentacion(int idPresentacion)
        {
            foreach (DetalleV detalleV in detalleVentas)
            {
                if (detalleV.idPresentacion == idPresentacion)
                    return true;
            }

            return false;

        }
        private async void btnAddCard_Click(object sender, EventArgs e)
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
                if (exitePresentacion(Convert.ToInt32(cbxPresentacion.SelectedValue)))
                 {

                        MessageBox.Show("Este dato ya fue agregado", "presentacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;

                 }
                // Creando la lista
                detalleV.cantidad = double.Parse(txtCantidad.Text.Trim(), CultureInfo.GetCultureInfo("en-US"));//1

                //determinamos el stock
                determinarStock(0);
                /// Busqueda presentacion
                PresentacionV findPresentacion = presentaciones.Find(x => x.idPresentacion == Convert.ToInt32(cbxPresentacion.SelectedValue));
                detalleV.cantidadUnitaria = double.Parse(findPresentacion.cantidadUnitaria, CultureInfo.GetCultureInfo("en-US"));//2

                detalleV.codigoProducto = cbxCodigoProducto.Text.Trim();//3
                detalleV.descripcion = cbxDescripcion.Text.Trim();//4
                double descuento = Math.Round(Convert.ToDouble(txtDescuento.Text.Trim()), 2);

                detalleV.descuento = descuento;// determinar el descuentogreupo 5



                detalleV.estado = 1;//5
                detalleV.idCombinacionAlternativa = Convert.ToInt32(cbxCombinacion.SelectedValue);//7


                detalleV.idPresentacion = Convert.ToInt32(cbxPresentacion.SelectedValue);

                detalleV.idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
                detalleV.idSucursal = ConfigModel.sucursal.idSucursal;
                detalleV.nombreCombinacion = cbxCombinacion.Text;
                detalleV.nombreMarca = currentProducto.nombreMarca;
                detalleV.nombrePresentacion = cbxPresentacion.Text;
                detalleV.nro = 1;
              

                double precioUnitario = double.Parse(txtPrecioUnitario.Text.Trim(), CultureInfo.GetCultureInfo("en-US"));

                precioUnitario = precioUnitario - (descuento / 100) * precioUnitario;

                detalleV.precioUnitario = Math.Round(precioUnitario, 2);

                detalleV.precioVenta = Math.Round(precioUnitario, 2);

                detalleV.total = Math.Round(double.Parse(txtTotal.Text.Trim(), CultureInfo.GetCultureInfo("en-US")), 2);

                detalleV.precioVentaReal = Math.Round(double.Parse(txtPrecioUnitario.Text.Trim(), CultureInfo.GetCultureInfo("en-US")), 2);
                detalleV.eliminar = "";
                int scotk = 0;
                if (lbStock.Text != "")
                {
                        scotk = Convert.ToInt32(lbStock.Text.Substring(1, lbStock.Text.Length - 1));

                }
                
                detalleV.existeStock = (scotk > 0 && scotk >= Convert.ToInt32(txtCantidad.Text.Trim())) ? 1 : 0;


                detalleV.idDetalleVenta = 0;
                detalleV.idPresentacion = findPresentacion.idPresentacion;
                detalleV.idVenta = 0;

                ProductoVenta aux = productos.Find(x => x.idProducto == (int)cbxCodigoProducto.SelectedValue);
                detalleV.nombreMarca = aux.nombreMarca;
                detalleV.nombrePresentacion = findPresentacion.nombrePresentacion;
                detalleV.precioEnvio = 0;
                detalleV.precioVenta = Math.Round(precioUnitario, 2); ;
                detalleV.totalGeneral = detalleV.total;

                detalleV.ventaVarianteSinStock = false;






                // agrgando un nuevo item a la lista
                detalleVentas.Add(detalleV);

                // calcular los descuentos


                // Refrescando la tabla
                detalleCompraBindingSource.DataSource = null;
                detalleCompraBindingSource.DataSource = detalleVentas;
                dataGridView.Refresh();

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

        private void descuentoTotal()
        {

            double descuentoTotal = 0;
            // calcular el descuento total
            foreach (DetalleV V in detalleVentas)
            {
                // calculamos el decuento para cada elemento
                double total = V.precioVentaReal * V.cantidad;
                double descuentoV = total - V.total;
                descuentoTotal += descuentoV;

            }
            int i = 2;
            string formato = "{0:n" + i + "}";
            txtDescuentoVenta.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), formato, descuentoTotal);

        }

        private void cargarProductoDetalle()
        {
            if (cbxCodigoProducto.SelectedIndex == -1) return;
            try
            {
                /// Buscando el producto seleccionado
                int idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
                currentProducto = productos.Find(x => x.idProducto == idProducto);
                cbxDescripcion.Text = currentProducto.codigoProducto;
                // Llenar los campos del producto escogido.............!!!!!
                txtCantidad.Text = "1";
                txtDescuento.Text = "0";
                /// Cargando presentaciones
                cargarPresentacionesProducto();
                /// Cargando alternativas del producto
                cargarAlternativasProducto();
                determinarDescuento();
                determinarStock(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        public async void determinarStock(double cantidad)
        {

            
            // determinamos el stock del producto seleccionado
            List<StockReceive> stockReceive = await stockModel.getStockProductoByIdProductoIdCombinacionIdSucursal((int)cbxCodigoProducto.SelectedValue, cbxCombinacion.SelectedValue == null ? 0 : (int)cbxCombinacion.SelectedValue, ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);
                double stockTotal = stockReceive[0].stock_total;
                double stockDetalle = 0;
                // si exite en el producto en lista detalle
                if (detalleVentas != null && (cbxPresentacion.SelectedIndex != -1))
                {
                    foreach (DetalleV V in detalleVentas)
                    {
                        if (V.idPresentacion == (int)cbxPresentacion.SelectedValue)
                        {
                            stockDetalle = V.cantidad;
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
                    else {
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
        private void cargarDescripcionDetalle()
        {

            if (cbxDescripcion.SelectedIndex == -1) return;
            try
            {
                /// Buscando el producto seleccionado
                int idProducto = Convert.ToInt32(cbxDescripcion.SelectedValue);
                currentProducto = productos.Find(x => x.idProducto == idProducto);
                cbxCodigoProducto.Text = currentProducto.nombreProducto;
                // Llenar los campos del producto escogido.............!!!!!
                txtCantidad.Text = "1";
                txtDescuento.Text = "0";

                /// Cargando presentaciones
                cargarPresentacionesDescripcion();

                /// Cargando alternativas del producto
                cargarAlternativasdescripcion();
                determinarDescuento();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }



        private async void cargarPresentacionesProducto()
        {
            if (cbxCodigoProducto.SelectedIndex == -1) return; /// validacion
                                                               /// Cargar las precentaciones
            string f = cbxCodigoProducto.SelectedValue.ToString();
            presentaciones = await presentacionModel.presentacionVentas(Convert.ToInt32(cbxCodigoProducto.SelectedValue), 0);
            presentacionBindingSource.DataSource = presentaciones;
            //cbxPresentacion.SelectedIndex = -1;

            /// calculos
            calcularPrecioUnitarioProducto();
            calcularTotal();
        }
        private async void cargarPresentacionesDescripcion()
        {
            if (cbxDescripcion.SelectedIndex == -1) return; /// validacion
                                                            /// Cargar las precentaciones
            string f = cbxDescripcion.SelectedValue.ToString();
            presentaciones = await presentacionModel.presentacionVentas(Convert.ToInt32(cbxDescripcion.SelectedValue), 0);
            presentacionBindingSource.DataSource = presentaciones;
            //cbxPresentacion.SelectedIndex = -1;

            /// calculos
            calcularPrecioUnitarioDescripcion();
            calcularTotal();
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

        private void calculoSubtotal()
        {
            double subtotal = 0;
            foreach (DetalleV item in detalleVentas)
            {
                subtotal += item.total;
            }

            txtSubTotal.Text = subtotal.ToString();
            double impuesto = double.Parse(txtImpuesto.Text, CultureInfo.GetCultureInfo("en-US"));
            txtTotalNeto.Text = (subtotal + impuesto).ToString();
        }

        /// <summary>
        /// Calcular Total
        /// </summary>
        private void calcularTotal()
        {
            try
            {
                if (txtCantidad.Text.Trim() == "") txtTotal.Text = "0";
                if (txtPrecioUnitario.Text.Trim() == "" || txtCantidad.Text.Trim() == "" || txtDescuento.Text.Trim() == "") return; /// Validación

                double precioUnidario = double.Parse(txtPrecioUnitario.Text, CultureInfo.GetCultureInfo("en-US"));
                double cantidad = double.Parse(txtCantidad.Text, CultureInfo.GetCultureInfo("en-US"));
                double descuento = double.Parse(txtDescuento.Text, CultureInfo.GetCultureInfo("en-US"));
                double totalNormal = (precioUnidario * cantidad);
                double total = totalNormal - (descuento / 100) * totalNormal;
                int i = 2;
                string formato = "{0:n" + i + "}";
                txtTotal.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), formato, total);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Calcular total", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Calcular Precio Unitario
        /// </summary>
        private void calcularPrecioUnitarioProducto() {
            if (cbxCodigoProducto.SelectedIndex == -1) return; /// Validación
            try
            {
                if (cbxPresentacion.SelectedIndex == -1)
                {
                    txtPrecioUnitario.Text = currentProducto.precioVenta;
                }
                else
                {
                    // Buscar presentacion elegida
                    int idPresentacion = Convert.ToInt32(cbxPresentacion.SelectedValue);
                    PresentacionV findPresentacion = presentaciones.Find(x => x.idPresentacion == idPresentacion);

                    // Realizando el calculo
                    double precioCompra = double.Parse(currentProducto.precioVenta, CultureInfo.GetCultureInfo("en-US"));
                    double cantidadUnitario = double.Parse(findPresentacion.cantidadUnitaria, CultureInfo.GetCultureInfo("en-US"));
                    double precioUnidatio = precioCompra * cantidadUnitario;

                    // Imprimiendo valor
                    txtPrecioUnitario.Text = String.Format(CultureInfo.GetCultureInfo("en-US"), "{0}", precioUnidatio);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Calcular total", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void calcularPrecioUnitarioDescripcion()
        {
            if (cbxDescripcion.SelectedIndex == -1) return; /// Validación
            try
            {
                if (cbxPresentacion.SelectedIndex == -1)
                {
                    txtPrecioUnitario.Text = currentProducto.precioVenta;
                }
                else
                {
                    // Buscar presentacion elegida
                    int idPresentacion = Convert.ToInt32(cbxPresentacion.SelectedValue);
                    PresentacionV findPresentacion = presentaciones.Find(x => x.idPresentacion == idPresentacion);

                    // Realizando el calculo
                    double precioCompra = double.Parse(currentProducto.precioVenta, CultureInfo.GetCultureInfo("en-US"));
                    double cantidadUnitario = double.Parse(findPresentacion.cantidadUnitaria, CultureInfo.GetCultureInfo("en-US"));
                    double precioUnidatio = precioCompra * cantidadUnitario;

                    // Imprimiendo valor
                    txtPrecioUnitario.Text = String.Format(CultureInfo.GetCultureInfo("en-US"), "{0}", precioUnidatio);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Calcular total", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void executeBuscarCliente()
        {
            Buscarcliente buscarCliente = new Buscarcliente();
            buscarCliente.ShowDialog();
            this.currentCliente = buscarCliente.currentCliente;
            mostrarCliente();
            determinarDescuento();
        }

        private void mostrarCliente()
        {
            if (currentCliente != null)
            {
                txtDireccion.Text = currentCliente.direccion;
                txtNombreCliente.Text = currentCliente.nombreCliente;
                txtNroIdentificacion.Text = currentCliente.numeroDocumento;
                cbxTipoDocumento.Text = currentCliente.nombre;
            }
        }

        private void textNombreEmpresa_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            executeBuscarCliente();
        }


        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            BuscarProducto buscarProducto = new BuscarProducto();
            buscarProducto.ShowDialog();

        }

        private void cargarProductoSeleccionado()
        {
            if (currentProducto == null) return;
        }

        #region =============================== Realizar La Venta ===============================
        private async void btnRealizarCompra_Click(object sender, EventArgs e)
        {

            //pago
            pagoC.estado = 1;// activo
            pagoC.estadoPago = 1;//ver que significado

            // Moneda aux = monedaBindingSource.;
            int i = cbxMoneda.SelectedIndex;
            //  Moneda aux = monedaBindingSource.List[i] as Moneda;
            pagoC.idMoneda = monedas[i].idMoneda;

            pagoC.idPago = currentCompra != null ? currentCompra.idPago : 0;
            pagoC.motivo = "COMPRA";
            pagoC.saldo = Convert.ToDouble(txtTotal.Text);
            pagoC.valorPagado = 0;
            pagoC.valorTotal = Convert.ToDouble(txtTotal.Text);
            // compra
            string date1 = String.Format("{0:u}", dtpFechaVenta.Value);
            date1 = date1.Substring(0, date1.Length - 1);
            string date = String.Format("{0:u}", dtpFechaPago.Value);
            date = date.Substring(0, date.Length - 1);

            compraC.idCompra = currentCompra != null ? currentCompra.idCompra : 0; ;
            compraC.numeroDocumento = "0";
            compraC.rucDni = currentProveedor != null ? currentProveedor.ruc : currentCompra.rucDni;
            compraC.direccion = currentProveedor != null ? currentProveedor.direccion : currentCompra.direccion;
            compraC.formaPago = "EFECTIVO";
            compraC.fechaPago = date;
            compraC.fechaFacturacion = date1;
            compraC.descuento = txtDescuento.Text.Trim() != "" ? Convert.ToDouble(txtDescuento.Text) : 0;
            compraC.tipoCompra = "Con productos";
            compraC.subTotal = Convert.ToDouble(txtSubTotal.Text);
            compraC.total = Convert.ToDouble(txtTotal.Text);
            compraC.observacion = txtObservacion.Text;
            compraC.estado = 1;
            compraC.idProveedor = currentProveedor != null ? currentProveedor.idProveedor : currentCompra.idProveedor;
            compraC.nombreProveedor = currentProveedor != null ? currentProveedor.razonSocial : currentCompra.nombreProveedor;
            compraC.idPago = currentCompra != null ? currentCompra.idPago : 0; ;
            compraC.idPersonal = PersonalModel.personal.idPersonal;
            compraC.tipoCambio = 1;
            int j = cbxTipoComprobante.SelectedIndex;
            TipoDocumento aux = cbxTipoComprobante.Items[j] as TipoDocumento;
            compraC.idTipoDocumento = (aux.idTipoDocumento);
            compraC.idSucursal = ConfigModel.sucursal.idSucursal;
            compraC.nombreLabel = aux.nombreLabel;
            compraC.vendedor = PersonalModel.personal.nombres;

            compraC.moneda = monedas[i].moneda;
            compraC.idCompra = currentCompra != null ? currentCompra.idCompra : 0;

            //detalle


            foreach (DetalleCompra detalle in detalleCompras)
            {
                DetalleC aux1 = new DetalleC();

                aux1.cantidad = Convert.ToInt32(detalle.cantidad);
                aux1.cantidadUnitaria = Convert.ToInt32(detalle.cantidadUnitaria);
                aux1.codigoProducto = detalle.codigoProducto;
                aux1.descripcion = detalle.descripcion;
                aux1.descuento = detalle.descuento;
                aux1.estado = detalle.estado;
                aux1.idCombinacionAlternativa = detalle.idCombinacionAlternativa;
                aux1.idCompra = detalle.idCompra;
                aux1.idDetalleCompra = detalle.idDetalleCompra;
                aux1.idPresentacion = detalle.idPresentacion;
                aux1.idProducto = detalle.idProducto;
                aux1.idSucursal = detalle.idSucursal;
                aux1.nombreCombinacion = detalle.nombreCombinacion;
                aux1.nombreMarca = detalle.nombreMarca;
                aux1.nombrePresentacion = detalle.nombrePresentacion;
                aux1.nro = detalle.nro;
                aux1.precioUnitario = detalle.precioUnitario;
                aux1.total = detalle.total;
                detalleC.Add(aux1);

                DatoNotaEntradaC aux2 = new DatoNotaEntradaC();
                aux2.idProducto = detalle.idProducto;
                aux2.cantidad = detalle.cantidad;
                aux2.idCombinacionAlternativa = detalle.idCombinacionAlternativa;
                aux2.idAlmacen = currentAlmacenCompra.idAlmacen;
                aux2.descripcion = detalle.descripcion;

                datoNotaEntradaC.Add(aux2);
            }
            pagocompraC.idCaja = FormPrincipal.asignacion.idCaja;
            pagocompraC.idPago = currentCompra != null ? currentCompra.idPago : 0; ;
            pagocompraC.moneda = monedas[i].moneda;
            pagocompraC.idMoneda = monedas[i].idMoneda;
            pagocompraC.idMedioPago = medioPagos[0].idMedioPago;
            pagocompraC.idCajaSesion = ConfigModel.cajaSesion != null ? ConfigModel.cajaSesion.idCajaSesion : 0;
            pagocompraC.pagarCompra = cbxPagarCompra.Checked == true ? 1 : 0;

            notaentrada.datoNotaEntrada = datoNotaEntradaC;
            notaentrada.generarNotaEntrada = cbxNotaEntrada.Checked == true ? 1 : 0;
            notaentrada.idCompra = currentCompra != null ? currentCompra.idPago : 0; ;
            notaentrada.idTipoDocumento = 7;
            notaentrada.idPersonal = PersonalModel.personal.idPersonal;


            compraTotal = new compraTotal();
            compraTotal.detalle = detalleC;
            compraTotal.compra = compraC;
            compraTotal.notaentrada = notaentrada;
            compraTotal.pago = pagoC;
            compraTotal.pagocompra = pagocompraC;




            await compraModel.ralizarCompra(compraTotal);

            if (nuevo)
                MessageBox.Show("Datos Guardados", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Datos  modificador", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        private void crearObjetoCompra()
        {
            Compra compra = new Compra();
            compra.descuento = txtDescuentoVenta.Text;
            compra.direccion = txtNombreCliente.Text;
            compra.estado = 1;
            // compra.fechaFacturacion = dtpEmision.Value.ToString("yyyy-MMM-dd  hh:mm:ss");
            //compra.fechaPago = "";
            compra.formaPago = "";

        }


        #endregion
        private void btnAddMarca_Click(object sender, EventArgs e)
        {
            buscarOrden importarOrden = new buscarOrden();
            importarOrden.ShowDialog();
            OrdenCompraSinComprarM aux = importarOrden.compraSinComprarM;
            // datos del proveedor

            if (aux != null)
            {

                txtNombreCliente.Text = aux.direccionProveedor;
                txtDireccion.Text = aux.nombreProveedor;


                currentCompra = new Compra();

                currentCompra.idSucursal = ConfigModel.sucursal.idSucursal;
                currentCompra.descuento = txtDescuento.Text;

                currentCompra.direccion = txtNombreCliente.Text;

                currentCompra.estado = 1;
                //currentCompra.fechaFacturacion = " ";

                currentCompra.formaPago = "EFECTIVO";
                currentCompra.idCajaSesion = ConfigModel.cajaSesion != null ? ConfigModel.cajaSesion.idCajaSesion : 0;
                currentCompra.idCompra = aux.idCompra;
                currentCompra.idPago = aux.idPago;
                currentCompra.idPersonal = personal.idPersonal;
                //currentCompra.idProveedor = aux.;
                currentCompra.idTipoDocumento = aux.idTipoDocumento;
                currentCompra.moneda = aux.moneda;
                currentCompra.nombreProveedor = aux.nombreProveedor;

                currentCompra.numeroDocumento = "";// falta definir o entender para q sirve
                currentCompra.observacion = aux.observacion;
                currentCompra.rucDni = aux.rucDni;
                currentCompra.tipoCompra = "con productos";
                currentCompra.vendedor = personal.nombres;
                if (detalleCompras != null)
                    detalleCompras.Clear();// limpiamos la lista de detalle productos
                detalleCompras = new List<DetalleCompra>();

                detalleCompraBindingSource.DataSource = null;

                dataGridView.Refresh();
                this.reLoad();
                listarDetalleCompraByIdCompra();
                listarDatosProveedorCompra();
                // Calculo de totales y subtotales
                calculoSubtotal();

                Ruc ruc = new Ruc();
                ruc.nroDocumento = aux.rucDni;

                obtenerid(ruc);

            }
        }

        private async void obtenerid(Ruc ruc)
        {

            ListProveedores = await proveedormodel.buscarPorDni(ruc);

            currentCompra.idProveedor = ListProveedores[0].idProveedor;

        }

        private void textTotal_OnValueChanged(object sender, EventArgs e)
        {

        }


        // modifcar el producto de detalle de venta

        private void button1_Click(object sender, EventArgs e)
        {
            // Verificando la existencia de datos en el datagridview
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idOrdenCompra = Convert.ToInt32(dataGridView.Rows[index].Cells[3].Value); // obteniedo el idRegistro del datagridview
            DetalleV aux = detalleVentas.Find(x => x.idPresentacion == idOrdenCompra);
            aux.cantidad = Convert.ToDouble(txtCantidad.Text, CultureInfo.GetCultureInfo("en-US"));
            aux.precioUnitario = Convert.ToDouble(txtPrecioUnitario.Text,CultureInfo.GetCultureInfo("en-US"));
            aux.total = double.Parse(txtTotal.Text, CultureInfo.GetCultureInfo("en-US"));
            detalleCompraBindingSource.DataSource = null;
            detalleCompraBindingSource.DataSource = detalleVentas;
            dataGridView.Refresh();

            // Calculo de totales y subtotales
            calculoSubtotal();
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            // Verificando la existencia de datos en el datagridview
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

         
            int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idOrdenCompra = Convert.ToInt32(dataGridView.Rows[index].Cells[3].Value); // obteniedo el idRegistro del datagridview
            DetalleV aux = detalleVentas.Find(x => x.idPresentacion == idOrdenCompra); // Buscando la registro especifico en la lista de registros
            cbxCodigoProducto.Text = aux.codigoProducto;
            cbxDescripcion.Text = aux.descripcion;
            cbxCombinacion.Text = aux.nombreCombinacion;
            cbxPresentacion.Text = aux.nombrePresentacion;
            txtCantidad.Text = Convert.ToString(aux.cantidad, CultureInfo.GetCultureInfo("en-US"));
            txtPrecioUnitario.Text = Convert.ToString(aux.precioUnitario, CultureInfo.GetCultureInfo("en-US"));
            txtDescuento.Text = Convert.ToString(aux.descuento, CultureInfo.GetCultureInfo("en-US"));
            txtTotal.Text = Convert.ToString(aux.total, CultureInfo.GetCultureInfo("en-US"));
            determinarStock(1);

            btnAddProducto.Enabled = false;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();            
        }
        private void limpiarCamposProducto()
        {
            cbxCodigoProducto.SelectedIndex = -1;
            cbxPresentacion.SelectedIndex = -1;
            cbxDescripcion.SelectedIndex = -1;
            cbxCombinacion.SelectedIndex = -1;
            txtCantidad.Text = "";
            txtDescuento.Text = "";
            txtPrecioUnitario.Text = "";          
            lbStock.Text = "";
           

            
        }


        private static void ShowWindowsMessage()
        {
            MessageBox.Show("");
        }
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int y = e.ColumnIndex;

            if (dataGridView.Columns[y].Name == "acciones")
            {
                if (dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
                int idOrdenCompra = Convert.ToInt32(dataGridView.Rows[index].Cells[3].Value); // obteniedo el idRegistro del datagridview
                DetalleV aux = detalleVentas.Find(x => x.idPresentacion == idOrdenCompra);

                dataGridView.Rows.RemoveAt(index);

                detalleVentas.Remove(aux);
            }

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuMetroTextbox3_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textDescuentoCompra_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private async void cbxTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idTipoDocumento = (int)cbxTipoComprobante.SelectedValue;
            List<Venta_correlativo> list = await ventaModel.listarNroDocumentoVenta(idTipoDocumento, ConfigModel.asignacionPersonal.idPuntoVenta);
            txtSerie.Text = list[0].correlativoActual;
            txtSerie.Text = list[0].serie;

        }

        private void btnImportarCotizacion_Click(object sender, EventArgs e)
        {
            buscarOrden importarOrden = new buscarOrden();
            importarOrden.ShowDialog();
            OrdenCompraSinComprarM aux = importarOrden.compraSinComprarM;
            // datos del proveedor

            if (aux != null)
            {

                txtNombreCliente.Text = aux.direccionProveedor;
                txtDireccion.Text = aux.nombreProveedor;


                currentCompra = new Compra();

                currentCompra.idSucursal = ConfigModel.sucursal.idSucursal;
                currentCompra.descuento = txtDescuento.Text;

                currentCompra.direccion = txtNombreCliente.Text;

                currentCompra.estado = 1;
                //currentCompra.fechaFacturacion = " ";

                currentCompra.formaPago = "EFECTIVO";
                currentCompra.idCajaSesion = ConfigModel.cajaSesion != null ? ConfigModel.cajaSesion.idCajaSesion : 0;
                currentCompra.idCompra = aux.idCompra;
                currentCompra.idPago = aux.idPago;
                currentCompra.idPersonal = personal.idPersonal;
                //currentCompra.idProveedor = aux.;
                currentCompra.idTipoDocumento = aux.idTipoDocumento;
                currentCompra.moneda = aux.moneda;
                currentCompra.nombreProveedor = aux.nombreProveedor;

                currentCompra.numeroDocumento = "";// falta definir o entender para q sirve
                currentCompra.observacion = aux.observacion;
                currentCompra.rucDni = aux.rucDni;
                currentCompra.tipoCompra = "con productos";
                currentCompra.vendedor = personal.nombres;
                if (detalleCompras != null)
                    detalleCompras.Clear();// limpiamos la lista de detalle productos
                detalleCompras = new List<DetalleCompra>();

                detalleCompraBindingSource.DataSource = null;

                dataGridView.Refresh();
                this.reLoad();
                listarDetalleCompraByIdCompra();
                listarDatosProveedorCompra();
                // Calculo de totales y subtotales
                calculoSubtotal();

                Ruc ruc = new Ruc();
                ruc.nroDocumento = aux.rucDni;

                obtenerid(ruc);

            }
        }

        private void plTipoComprobante_Paint(object sender, PaintEventArgs e)
        {
            pintarBordePanel();
        }

        private void textCantidad_TextChanged(object sender, EventArgs e)
        {
            calcularDescuentos();
        }


        private void calcularDescuentos()
        {


            determinarDescuento();



        }

        public async void determinarDescuento()
        {
            if (txtCantidad.Text != "" && cbxCodigoProducto.SelectedValue != null)
            {
                DescuentoProductoSubmit descuentoProductoSubmit = new DescuentoProductoSubmit();

                descuentoProductoSubmit.cantidad = Convert.ToInt32(txtCantidad.Text);
                descuentoProductoSubmit.cantidades = "";
                descuentoProductoSubmit.idGrupoCliente = currentCliente != null ? currentCliente.idGrupoCliente : 1;
                descuentoProductoSubmit.idProducto = (int)cbxCodigoProducto.SelectedValue;
                descuentoProductoSubmit.idProductos = "";
                descuentoProductoSubmit.idSucursal = ConfigModel.sucursal.idSucursal;
                descuentoProducto = await descuentoModel.descuentoTotalALaFecha(descuentoProductoSubmit);

                txtDescuento.Text = descuentoProducto.descuento.ToString();

                calcularTotal();
            

            if (detalleVentas != null )

                if(detalleVentas.Count != 0)         
                 {
                //primero traemos los descuento correspondientes
                DescuentoSubmit descuentoSubmit = new DescuentoSubmit();
                string cantidades = "";
                string idProductos = "";
                foreach(DetalleV V in detalleVentas)
                {
                    cantidades += V.cantidad + ",";
                    idProductos += V.idProducto + ",";

                }
                descuentoSubmit.idProductos = idProductos.Substring(0,idProductos.Length-1);
                descuentoSubmit.cantidades = cantidades.Substring(0,cantidades.Length-1);
                descuentoSubmit.idGrupoCliente= currentCliente!=null ?currentCliente.idGrupoCliente: 1;
                descuentoSubmit.idSucursal = ConfigModel.sucursal.idSucursal;
                List< DescuentoReceive> descuentoReceive =await  descuentoModel.descuentoTotalALaFechaGrupo(descuentoSubmit);

                int i = 0;
                foreach (DetalleV V in detalleVentas)
                {
                    V.descuento = descuentoReceive[i++].descuento;
                    // nuevo Precio unitario
                    double precioUnitario = V.precioVentaReal;
                    precioUnitario = precioUnitario - (V.descuento / 100) * precioUnitario;
                    double total = V.total;
                    V.total = precioUnitario * V.cantidad;
                    V.precioVenta = precioUnitario;
                    V.precioUnitario = precioUnitario;
                }

                i = 0;
                detalleCompraBindingSource.DataSource = null;
                detalleCompraBindingSource.DataSource = detalleVentas;
                dataGridView.Refresh();

                // Calculo de totales y subtotales
                calculoSubtotal();
                descuentoTotal();

            }
                 
            }
        }

        private void plPVenta_Click(object sender, EventArgs e)
        {
            if (!mostrarOriginal)
            {
                //Perdido en la edicion
                //plPVenta.BorderStyle = BorderStyle.FixedSingle;

                mostrarOriginal = true;
            }
            else
            {
                ////Perdido en la edicion
                //plPVenta.BorderStyle = BorderStyle.None; ;
                mostrarOriginal = false;
            }

        }

        private void dtpVenta_ValueChanged(object sender, EventArgs e)
        {
            dtpFechaPago.Value = dtpFechaVenta.Value;
        }

        private void textCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void textPrecioUnidario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, txtPrecioUnitario.Text);
        }

        private void textDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, txtDescuento.Text);
        }

        private void textDescuento_TextChanged(object sender, EventArgs e)
        {
            
            calcularTotal();
        }

        private void textPrecioUnidario_TextChanged(object sender, EventArgs e)
        {
            calcularTotal();
        }


        private void txtCantidad_Validated(object sender, EventArgs e)
        {
            calcularDescuentos();
        }

        private void txtdescuento_Validated(object sender, EventArgs e)
        {
            calcularTotal();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void txtdescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void txtPrecioUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void txtPrecioUnitario_Validated(object sender, EventArgs e)
        {
            calcularTotal();
        }

        private void btnAddProducto_Click(object sender, EventArgs e)
        {

        }
        //Modificar el Producto Seleccionado
        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Verificando la existencia de datos en el datagridview
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idOrdenCompra = Convert.ToInt32(dataGridView.Rows[index].Cells[3].Value); // obteniedo el idRegistro del datagridview
            DetalleV aux = detalleVentas.Find(x => x.idPresentacion == idOrdenCompra);
            aux.cantidad = Convert.ToDouble(txtCantidad.Text, CultureInfo.GetCultureInfo("en-US"));
            aux.precioUnitario = Convert.ToDouble(txtPrecioUnitario.Text, CultureInfo.GetCultureInfo("en-US"));
            aux.total = double.Parse(txtTotal.Text, CultureInfo.GetCultureInfo("en-US"));
            detalleCompraBindingSource.DataSource = null;
            detalleCompraBindingSource.DataSource = detalleVentas;
            dataGridView.Refresh();

            // Calculo de totales y subtotales
            calculoSubtotal();
        }

        private void dtpFechaVenta_Validated(object sender, EventArgs e)
        {
            dtpFechaPago.Value = dtpFechaVenta.Value;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            executeBuscarCliente();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void FormVentaNuevo1_Resize(object sender, EventArgs e)
        {
            //Cuando se modifica el tamaño del formulario se vuelve a centrar todo
            if(plIzquierdo.Width - plIzquierdoCentrado.Width < 0)
            {
                plIzquierdoCentrado.Left = (plIzquierdo.Width - plIzquierdoCentrado.Width) / 2;
            }
            if(plIzquierdo.Height - plIzquierdoCentrado.Height<0)
            {
                plIzquierdoCentrado.Top = (plIzquierdo.Height - plIzquierdoCentrado.Height) / 2;
            }
            //label20.Text="IZ:"+plIzquierdo.Width+" IZC:"+plIzquierdoCentrado.Width +

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrecioUnitario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
