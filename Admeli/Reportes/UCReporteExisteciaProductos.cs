using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidad;
using Modelo;
using Admeli.Componentes;

namespace Admeli.Reportes
{
    public partial class UCReporteExisteciaProductos : UserControl
    {
        private Paginacion paginacion;
        private FormPrincipal formPrincipal;
        private CategoriaModel categoriaModel = new CategoriaModel();
        private SucursalModel sucursalModel = new SucursalModel();
        private AlmacenModel almacenModel = new AlmacenModel();
        private ProductoModel productoModel = new ProductoModel();
        private ReporteModel reporteModel = new ReporteModel();
        private List<Producto> productos { get; set; }

        public UCReporteExisteciaProductos()
        {
            InitializeComponent();
        }

        public UCReporteExisteciaProductos(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;
            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
            this.reload();
            btnFiltrar.Enabled = true;
            btnExcel.Enabled = true;
            btnImprimir.Enabled = true;
        }

        public void reload()
        {
            cargarCategorias();
            cargarSucursales();
            cargarAlmacenes();
        }

        private async void cargarCategorias()
        {
            loadState(true);
            try
            {
                // Cargando las categorias desde el webservice
                categoriaBindingSource.DataSource = await categoriaModel.categoriasTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar Categorias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            loadState(false);
        }
        private async void cargarSucursales()
        {
            // Cargando el combobox de personales
            loadState(true);
            try
            {
                sucursalBindingSource.DataSource = await sucursalModel.sucursales();
                cbxSucursales.SelectedValue = ConfigModel.sucursal.idSucursal;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar Sucursales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void cargarAlmacenes()
        {
            // Cargando el combobox de personales
            loadState(true);
            try
            {
                almacenBindingSource.DataSource = await almacenModel.almacenes();
                cbxAlmacenes.SelectedValue = ConfigModel.currentIdAlmacen;
                //buscarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar Almacenes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarRegistros()
        {
            loadState(true);
            try
            {
                List<ObjectReporteProducto> listaProductos = await reporteModel.existenciaProductos<List<ObjectReporteProducto>>(textBuscar.Text, int.Parse(cbxCategorias.SelectedValue.ToString()), int.Parse(cbxSucursales.SelectedValue.ToString()), int.Parse(cbxAlmacenes.SelectedValue.ToString()), 1);
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = listaProductos;
                return;
                Dictionary<string, int> list = new Dictionary<string, int>();
                list.Add("id0", 0);
                Dictionary<string, int> sendList = (ConfigModel.currentProductoCategory.Count == 0) ? list : ConfigModel.currentProductoCategory;

                RootObject<Producto,CombinacionStock> productos_combinacion = await productoModel.productosPorCategoria(sendList, paginacion.currentPage, paginacion.speed);

                // actualizando datos de páginacón
                paginacion.itemsCount = productos_combinacion.nro_registros;
                paginacion.reload();

                // Ingresando
                productos = productos_combinacion.datos;
                productoBindingSource.DataSource = productos;
                dgvProductos.Refresh();

                // Mostrando la paginacion
                mostrarPaginado();

                // Formato de celdas
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadState(false);
            }
        }

        private void loadState(bool state)
        {
            formPrincipal.appLoadState(state);
            panelNavigation.Enabled = !state;
            panelTools.Enabled = !state;
            panelTools.Enabled = !state;
            dgvProductos.Enabled = !state;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            //Recuperar Productos
            //http://localhost:8085/admeli/~admeli/reporte/srch-ptrn-xstnc/nombre/xpe/categoria/0/sucursal/0/almacen/0/pagina/0
            buscarProductos();
        }

        private async void buscarProductos()
        {
            try
            {
                string textoBuscado = textBuscar.Text;
                if (string.IsNullOrEmpty(textoBuscado.Trim())) { textoBuscado = "todos"; }
                List<ObjectReporteProducto> listaProductos = await reporteModel.existenciaProductos<List<ObjectReporteProducto>>(textoBuscado, int.Parse(cbxCategorias.SelectedValue.ToString()), int.Parse(cbxSucursales.SelectedValue.ToString()), int.Parse(cbxAlmacenes.SelectedValue.ToString()), 1);
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = listaProductos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Filtrar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region ===================== Eventos Páginación =====================
        private void mostrarPaginado()
        {
            lblCurrentPage.Text = paginacion.currentPage.ToString();
            lblPageAllItems.Text = String.Format("{0} Registros", paginacion.itemsCount.ToString());
            lblPageCount.Text = paginacion.pageCount.ToString();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (lblCurrentPage.Text != "1")
            {
                paginacion.previousPage();
                cargarRegistros();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (lblCurrentPage.Text != "1")
            {
                paginacion.firstPage();
                cargarRegistros();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (lblPageCount.Text == "0") return;
            if (lblPageCount.Text != lblCurrentPage.Text)
            {
                paginacion.nextPage();
                cargarRegistros();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (lblPageCount.Text == "0") return;
            if (lblPageCount.Text != lblCurrentPage.Text)
            {
                paginacion.lastPage();
                cargarRegistros();
            }
        }

        private void lblSpeedPages_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                paginacion.speed = Convert.ToInt32(lblSpeedPages.Text);
                paginacion.currentPage = 1;
                cargarRegistros();
            }
        }

        private void lblCurrentPage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                paginacion.reloadPage(Convert.ToInt32(lblCurrentPage.Text));
                cargarRegistros();
            }
        }

        private void lblCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void lblSpeedPages_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }
        #endregion

        private void btnExcel_Click(object sender, EventArgs e)
        {
            elegirCamposExportar formElegirCamposExportar = new elegirCamposExportar(dgvProductos);
            formElegirCamposExportar.ShowDialog();
        }

        private void textBuscar_OnValueChanged(object sender, EventArgs e)
        {

        }
    }

    public class ObjectReporteProducto
    {
        public string idProducto { get; set; }
        public string codigoProducto { get; set; }
        //public string idCombinacionAlternativa { get; set; }
        public string nombreProducto { get; set; }
        //public string stock { get; set; }
        //public string precioCombinacion { get; set; }
        public string nombreSucursal { get; set; }
        public string nombreAlmacen { get; set; }
        public string precioCompra { get; set; }
        //public string idAlmacen { get; set; }
        //public string idSucursal { get; set; }
        public string precioVenta { get; set; }
        public string valor { get; set; }
    }

}