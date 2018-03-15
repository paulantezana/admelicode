using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.Componentes;
using Admeli.Compras.Modificar;
using Modelo;

namespace Admeli.Ventas
{
    public partial class UCCuentaCobrar : UserControl
    {
        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }

        /*private cuenta cuenta { get; set; }
        private List<cuenta> cuenta { get; set; }*/

        private Paginacion paginacion;
        private SucursalModel sucursalModel = new SucursalModel();
        private AlmacenModel almacenModel = new AlmacenModel();
        private PersonalModel personalModel = new PersonalModel();

        #region ======================================== ROOT LOAD ========================================
        public UCCuentaCobrar()
        {
            InitializeComponent();

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
        }

        public UCCuentaCobrar(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
        } 
        #endregion

        #region ======================================== Root Load ========================================
        private void UCCuentaCobrar_Load(object sender, EventArgs e)
        {
            this.reLoad();

            // Preparando para los eventos de teclado
            this.ParentChanged += ParentChange; // Evetno que se dispara cuando el padre cambia // Este eveto se usa para desactivar lisener key events de este modulo
            if (TopLevelControl is Form) // Escuchando los eventos del formulario padre
            {
                (TopLevelControl as Form).KeyPreview = true;
                TopLevelControl.KeyUp += TopLevelControl_KeyUp;
            }
        }

        internal void reLoad(bool refreshData = true)
        {
            if (refreshData)
            {
                cargarRegistros();
            }
            lisenerKeyEvents = true; // Active lisener key events
        }
        #endregion

        #region ============================== PAINT ==============================
        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelContainer);
        } 
        #endregion

        #region ======================== KEYBOARD ========================
        // Evento que se dispara cuando el padre cambia
        private void ParentChange(object sender, EventArgs e)
        {
            // cambiar la propiedad de lisenerKeyEvents de este modulo
            if (lisenerKeyEvents) lisenerKeyEvents = false;
        }

        // Escuchando los Eventos de teclado
        private void TopLevelControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (!lisenerKeyEvents) return;
            switch (e.KeyCode)
            {
                case Keys.F3:
                    //executeNuevo();
                    break;
                case Keys.F4:
                    //executeModificar();
                    break;
                case Keys.F5:
                    cargarRegistros();
                    break;
                case Keys.F6:
                    //executeEliminar();
                    break;
                case Keys.F7:
                    //executeAnular();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region =================================== Loads ==================================
        private async void cargarSucursales()
        {
            try
            {
                sucursalBindingSource.DataSource = await sucursalModel.listarSucursalesActivos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarAlmacenes()
        {
            try
            {
                almacenBindingSource.DataSource = await almacenModel.almacenesPorSucursales(ConfigModel.sucursal.idSucursal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarPersonales()
        {
            try
            {
                personalBindingSource.DataSource = await personalModel.listarPersonalAlmacen(ConfigModel.sucursal.idSucursal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cargarRegistros()
        {
            // throw new NotImplementedException();
        }
        #endregion

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

        #region ==================== CRUD ====================
        /*  private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
          {
              executeModificar();
          }

          private void btnActualizar_Click(object sender, EventArgs e)
          {
              cargarRegistros();
          }

          private void btnNuevo_Click(object sender, EventArgs e)
          {
              executeNuevo();
          }

          private void btnEliminar_Click(object sender, EventArgs e)
          {
              executeEliminar();
          }

          private void btnModificar_Click(object sender, EventArgs e)
          {
              executeModificar();
          }

          private void btnDesactivar_Click(object sender, EventArgs e)
          {
              executeAnular();
          }

          private void executeNuevo()
          {
              FormCategoriaNuevo formCategoria = new FormCategoriaNuevo();
              formCategoria.ShowDialog();
              cargarRegistros();
          }

          private void executeModificar()
          {
              // Verificando la existencia de datos en el datagridview
              if (dataGridView.Rows.Count == 0)
              {
                  MessageBox.Show("No hay un registro seleccionado", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  return;
              }

              int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
              int idCategoria = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview

              Categoria categoria = categorias.Find(x => x.idCategoria == idCategoria); // Buscando la categoria en las lista de categorias

              // Mostrando el formulario de modificacion
              FormCategoriaNuevo formCategoria = new FormCategoriaNuevo(categoria);
              formCategoria.ShowDialog();
              cargarRegistros(); // recargando loas registros en el datagridview
          }

          private async void executeEliminar()
          {
              // Verificando la existencia de datos en el datagridview
              if (dataGridView.Rows.Count == 0)
              {
                  MessageBox.Show("No hay un registro seleccionado", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  return;
              }

              // Pregunta de seguridad de eliminacion
              DialogResult dialog = MessageBox.Show("¿Está seguro de eliminar este registro?", "Eliminar",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
              if (dialog == DialogResult.No) return;


              try
              {
                  int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
                  currentCategoria = new Categoria(); //creando una instancia del objeto categoria
                  currentCategoria.idCategoria = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview

                  loadState(true); // cambiando el estado
                  Response response = await categoriaModel.eliminar(currentCategoria); // Eliminando con el webservice correspondiente
                  MessageBox.Show(response.msj, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  cargarRegistros(); // recargando el datagridview
              }
              catch (Exception ex)
              {
                  MessageBox.Show("Error: " + ex.Message, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              }
              finally
              {
                  loadState(false); // cambiando el estado
              }
          }

          private async void executeAnular()
          {
              // Verificando la existencia de datos en el datagridview
              if (dataGridView.Rows.Count == 0)
              {
                  MessageBox.Show("No hay un registro seleccionado", "Desactivar o anular", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  return;
              }

              // Pregunta de seguridad de anular
              DialogResult dialog = MessageBox.Show("¿Está seguro de anular este registro?", "Anular",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
              if (dialog == DialogResult.No) return;

              try
              {
                  int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
                  currentCategoria = new Categoria(); //creando una instancia del objeto categoria
                  currentCategoria.idCategoria = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview

                  // Comprobando si la categoria ya esta desactivado
                  if (categorias.Find(x => x.idCategoria == currentCategoria.idCategoria).estado == 0)
                  {
                      MessageBox.Show("Este registro ya esta desactivado", "Desactivar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      return;
                  }

                  // Procediendo con las desactivacion
                  Response response = await categoriaModel.desactivar(currentCategoria);
                  MessageBox.Show(response.msj, "Desactivar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  cargarRegistros(); // recargando los registros en el datagridview
              }
              catch (Exception ex)
              {
                  MessageBox.Show("Error: " + ex.Message, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              }
          }*/

        #endregion

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }
    }
}
