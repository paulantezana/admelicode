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
using Entidad;
using Entidad.Configuracion;
using Modelo;

namespace Admeli.Compras.Nuevo
{
    public partial class FormCompraNew : Form
    {
        // variable para hacer compra
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

        // objetos para cargar informacion necesaria
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

        /// Sus datos se cargan al abrir el formulario
        private List<Moneda> monedas { get; set; }
        private List<TipoDocumento> tipoDocumentos { get; set; }
        private FechaSistema fechaSistema { get; set; }
        private List<Producto> productos { get; set; }
        private List<MedioPago> medioPagos { get; set; }

        /// Llenan los datos en las interacciones en el formulario 
        private List<Presentacion> presentaciones { get; set; }

        private Producto currentProducto { get; set; }
        private Proveedor currentProveedor { get; set; }
        /// Se preparan para realizar la compra de productos
        private List<DetalleCompra> detalleCompras { get; set; }
        private Compra currentCompra { get; set; } // notaEntrada,pago,pagoCompra

        private Presentacion currentPresentacion { get; set; }
        private NotaEntrada currentNotaEntrada { get; set; }
        private Pago currentPago { get; set; }
        private PagoCompra currentPagoCompra { get; set; }
        private AlmacenComra currentAlmacenCompra { get; set; }
        private List<AlmacenComra> Almacen { get; set; }
        bool nuevo { get; set; }
        int nroDecimales = 2;
        string formato { get; set; }

        public FormCompraNew()
        {
            InitializeComponent();
            this.nuevo = true;
            cargarFechaSistema();
            pagoC = new PagoC();
            compraC = new CompraC();
            detalleC = new List<DetalleC>();
            pagocompraC = new PagocompraC();
            datoNotaEntradaC = new List<DatoNotaEntradaC>();
            notaentrada = new NotaentradaC();
            compraTotal = new compraTotal();


            formato = "{0:n" + nroDecimales + "}";

        }

        public FormCompraNew(Compra currentCompra)
        {
            InitializeComponent();
            this.currentCompra = currentCompra;
            this.nuevo = false;
            cargarFechaSistema();
            pagoC = new PagoC();
            compraC = new CompraC();
            detalleC = new List<DetalleC>();
            pagocompraC = new PagocompraC();
            datoNotaEntradaC = new List<DatoNotaEntradaC>();
            notaentrada = new NotaentradaC();
            compraTotal = new compraTotal();
            //datos del proveedor no editables
            txtNombreProveedor.Enabled = false;
            txtDireccionProveedor.Enabled = false;
            btnImportarOrdenCompra.Visible = false;
        }

        #region ================================ Root Load ================================
        private void FormCompraNew_Load(object sender, EventArgs e)
        {
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

                btnGuardarCompra.Text = "Modificar compra";
            }

            AddButtonColumn();

        }

        private void reLoad()
        {
            cargarMonedas();
            cargarTipoDocumento();
            cargarFechaSistema();
            cargarProductos();
            cargarMedioPago();
            cargarAlmacen();
            cargarPresentacion();
            int i = ConfigModel.cajaSesion != null ? ConfigModel.cajaSesion.idCajaSesion : 0;
            if (i == 0)
            {


                chbxPagarCompra.Enabled = false;
                chbxPagarCompra.Checked = false;
            }
        }
        #endregion



        #region ============================== Load ==============================

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

            dgwDetalleCompra.Columns.Add(buttons);

        }
        private async void listarDetalleCompraByIdCompra()
        {

            try
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

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }

            // Refrescando la tabla
            detalleCompraBindingSource.DataSource = null;
            detalleCompraBindingSource.DataSource = detalleCompras;
            dgwDetalleCompra.Refresh();

            // Calculo de totales y subtotales
            calculoSubtotal();
        }

        private async void listarDatosProveedorCompra()
        {
            try
            {

                datosProveedor = await compraModel.Compras(currentCompra.idCompra);
                txtNombreProveedor.Text = datosProveedor[0].nombreProveedor;
                txtDireccionProveedor.Text = datosProveedor[0].direccion;
                dtpFechaEmision.Value = datosProveedor[0].fechaFacturacion.date;
                dtpFechaPago.Value = datosProveedor[0].fechaPago.date;
                // textTotal.Text = Convert.ToString(datosProveedor[0].total);
                cbxTipoMoneda.Text = datosProveedor[0].moneda;
                txtTipoCambio.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }




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

        private async void cargarTipoDocumento()
        {
            try
            {
                tipoDocumentos = await tipoDocumentoModel.tipoDocumentoVentas();
                cbxTipoDocumento.DataSource = tipoDocumentos;
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
                if (!nuevo) return;
                fechaSistema = await fechaModel.fechaSistema();
                dtpFechaEmision.Value = fechaSistema.fecha;
                dtpFechaPago.Value = fechaSistema.fecha;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarProductos()
        {
            try
            {
                productos = await productoModel.productos();
                productoBindingSource.DataSource = productos;



                cbxCodigoProducto.SelectedIndex = -1;
                cbxDescripcion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // para el cbx de descripcion
        private async void cargarPresentacion()
        {                                                                                                    /// Cargar las precentaciones
            presentaciones = await presentacionModel.presentacionesTodas();
            presentacionBindingSource.DataSource = presentaciones;
            cbxDescripcion.SelectedIndex = -1;
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

        private async void cargarAlmacen()
        {

            Almacen = await almacenModel.almacenesCompra(ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);
            currentAlmacenCompra = Almacen[0];
        }

        #endregion

        #region=========== METODOS DE APOYO EN EL CALCULO


        private void calculoSubtotal()
        {
            double subtotal = 0;
            foreach (DetalleCompra item in detalleCompras)
            {
                subtotal += item.total;
            }

            lbSubTotal.Text = subtotal.ToString();
            double impuesto = double.Parse(lbImpuestos.Text, CultureInfo.GetCultureInfo("en-US"));
            lbTotalCompra.Text = (subtotal + impuesto).ToString();
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

                double precioUnidario = double.Parse(txtPrecioUnitario.Text, CultureInfo.GetCultureInfo("en-US"));
                double cantidad = double.Parse(txtCantidad.Text, CultureInfo.GetCultureInfo("en-US"));
                double descuento = double.Parse(txtDescuento.Text, CultureInfo.GetCultureInfo("en-US"));
                double total = (precioUnidario * cantidad) - descuento;

                txtTotalProducto.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), formato, total);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Calcular total", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Calcular Precio Unitario
        /// </summary>
        private void calcularPrecioUnitario(int tipo)
        {

            if (tipo == 0)
            {
                if (cbxCodigoProducto.SelectedIndex == -1) return; /// Validación
                try
                {
                    if (cbxDescripcion.SelectedIndex == -1)
                    {
                        txtPrecioUnitario.Text = currentProducto.precioCompra;
                    }
                    else
                    {
                        // Buscar presentacion elegida
                        int idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                        Presentacion findPresentacion = presentaciones.Find(x => x.idPresentacion == idPresentacion);

                        // Realizando el calculo
                        double precioCompra = double.Parse(currentProducto.precioCompra, CultureInfo.GetCultureInfo("en-US"));
                        double cantidadUnitario = double.Parse(findPresentacion.cantidadUnitaria, CultureInfo.GetCultureInfo("en-US"));
                        double precioUnidatio = precioCompra * cantidadUnitario;

                        // Imprimiendo valor
                        txtPrecioUnitario.Text = String.Format(CultureInfo.GetCultureInfo("en-US"), formato, precioUnidatio);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "determinar precio unitario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                if (cbxDescripcion.SelectedIndex == -1) return; /// Validación
                try
                {

                    // Buscar presentacion elegida
                    int idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                    Presentacion findPresentacion = presentaciones.Find(x => x.idPresentacion == idPresentacion);
                    Producto findProducto = productos.Find(x => x.idProducto == findPresentacion.idProducto);
                    cbxCodigoProducto.Text = findProducto.codigoProducto;
                    // Realizando el calculo
                    double precioCompra = double.Parse(findProducto.precioCompra, CultureInfo.GetCultureInfo("en-US"));
                    double cantidadUnitario = double.Parse(findPresentacion.cantidadUnitaria, CultureInfo.GetCultureInfo("en-US"));
                    double precioUnidatio = precioCompra * cantidadUnitario;

                    // Imprimiendo valor
                    txtPrecioUnitario.Text = String.Format(CultureInfo.GetCultureInfo("en-US"), formato, precioUnidatio);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "determinar precio unitario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



            }

        }

        #endregion







        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }


        // accione fde cargar productos
        private void cbxCodigoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProductoDetalle(0);
        }

        private void cbxDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProductoDetalle(1);
        }

        private async void txtDocIdentificacion_TextChanged(object sender, EventArgs e)
        {
            String aux = txtDocIdentificacion.Text;

            int nroCarateres = aux.Length;
            bool exiteProveedor = false;
            if (nroCarateres == 11)
            {

                try
                {


                    Ruc nroDocumento = new Ruc();
                    nroDocumento.nroDocumento = aux;
                    List<Proveedor> proveedores = await proveedormodel.buscarPorDni(nroDocumento);
                    currentProveedor = proveedores[0];
                    if (currentProveedor != null)
                        exiteProveedor = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "consulta sunat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (exiteProveedor)
                {
                    // llenamos los dato con el current proveerdor
                    txtDocIdentificacion.removePlaceHolder();
                    txtDocIdentificacion.Text= currentProveedor.ruc;
                    txtDireccionProveedor.removePlaceHolder();
                    txtDireccionProveedor.Text = currentProveedor.direccion;
                    txtNombreProveedor.removePlaceHolder();
                    txtNombreProveedor.Text = currentProveedor.razonSocial;

                }
                else
                {
                    //llenamos los datos en FormproveerdorNuevo
                    FormProveedorNuevo formProveedorNuevo = new FormProveedorNuevo(aux);
                    

                    formProveedorNuevo.ShowDialog();

                }




            }
            // Ver(aux);


            //if (respuestaSunat != null)
            //{

            //    dataSunat = respuestaSunat.result;
            //    textNIdentificacion.Text = dataSunat.RUC;
            //    textTelefono.Text = dataSunat.Telefono.Substring(1, dataSunat.Telefono.Length - 1);
            //    textNombreEmpresa.Text = dataSunat.RazonSocial;
            //    textActividadPrincipal.Text = dataSunat.Oficio;


            //    textDireccion.Text = concidencias(dataSunat.Direccion);
            //    //cbxPaises.Text = concidencias(dataSunat.Pais);


            //    respuestaSunat = null;

            //}


        }

        // metodoss usados por los eventos

        private void cargarProductoDetalle(int tipo)
        {
            if (tipo == 0)
            {

                if (cbxCodigoProducto.SelectedIndex == -1) return;
                try
                {
                    /// Buscando el producto seleccionado
                    int idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
                    currentProducto = productos.Find(x => x.idProducto == idProducto);
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
                    int idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                    currentPresentacion = presentaciones.Find(x => x.idPresentacion == idPresentacion);

                    cargarPresentacionDescripcion(tipo);
                    cargarAlternativas(tipo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }


        }

        private void cargarPresentacionDescripcion(int tipo)
        {
            cbxDescripcion.Text = currentPresentacion.descripcion;
            txtCantidad.Text = "1";
            txtDescuento.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), formato, 0);


            calcularPrecioUnitario(tipo);
            calcularTotal();

        }
        private async void cargarPresentaciones(int idProducto, int tipo)
        {


            List<Presentacion> presentaciones = await presentacionModel.presentacionVentas(idProducto);
            currentPresentacion = presentaciones[0];
            cbxDescripcion.Text = currentPresentacion.descripcion;
            txtCantidad.Text = "1";
            txtDescuento.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), formato, 0);
            calcularPrecioUnitario(tipo);
            calcularTotal();
        }

        private async void cargarAlternativas(int tipo)
        {
            if (cbxCodigoProducto.SelectedIndex == -1) return; /// validacion
                                                               /// cargando las alternativas del producto
            List<AlternativaCombinacion> alternativaCombinacion = await alternativaModel.cAlternativa31(Convert.ToInt32(cbxCodigoProducto.SelectedValue));
            alternativaCombinacionBindingSource.DataSource = alternativaCombinacion;

            /// calculos
            calcularPrecioUnitario(tipo);
            calcularTotal();
        }
        // validaciones de txt de producto
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

    
    }
}
