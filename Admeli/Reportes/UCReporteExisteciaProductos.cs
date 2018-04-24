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
            cargarRegistros();
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
                Dictionary<string, int> list = new Dictionary<string, int>();
                list.Add("id0", 0);
                Dictionary<string, int> sendList = (ConfigModel.currentProductoCategory.Count == 0) ? list : ConfigModel.currentProductoCategory;

                RootObject<Producto> productosRoot = await productoModel.productosPorCategoria(sendList, paginacion.currentPage, paginacion.speed);

                // actualizando datos de páginacón
                paginacion.itemsCount = productosRoot.nro_registros;
                paginacion.reload();

                // Ingresando
                productos = productosRoot.datos;
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
            //recuperar texto guardado http://localhost:8085/ad_meli/xcore/services.php/productos/categoria/1/15

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
    }
}