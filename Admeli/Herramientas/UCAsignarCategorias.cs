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
using Modelo;

namespace Admeli.Herramientas
{
    public partial class UCAsignarCategorias : UserControl
    {
        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }
        private ProductoModel productoModel = new ProductoModel();
        private CategoriaModel categoriaModel = new CategoriaModel();

        public UCAsignarCategorias()
        {
            InitializeComponent();
        }

        public UCAsignarCategorias(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelContainer);
        }

        #region ========================== Root Load ==========================
        private void UCAsignarCategorias_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        internal void reLoad(bool refreshData = true)
        {
            if (refreshData)
            {
                cargarCategorias();
                cargarProductos();
            }
            lisenerKeyEvents = true; // Active lisener key events
        } 
        #endregion

        #region ===================================== Loads =====================================
        private async void cargarCategorias()
        {
            loadState(true);
            try
            {
                categoriaBindingSource.DataSource = await categoriaModel.categorias21();
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

        private async void cargarProductos()
        {
            loadState(true);
            try
            {
                productoBindingSource.DataSource = await productoModel.productosSinCategoria();
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
        #endregion

        #region ==================================== Estados ====================================
        private void loadState(bool state)
        {
            formPrincipal.appLoadState(state);
        }



        #endregion

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

             label5.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();

           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            label6.Text = this.dataGridView2.CurrentRow.Cells[2].Value.ToString();
        }

        
       }
}
