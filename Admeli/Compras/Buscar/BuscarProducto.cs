using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli.Compras.Buscar
{
    public partial class BuscarProducto : Form
    {
        private ProductoModel productoModel = new ProductoModel();

        private List<Producto> productos { get; set; }
        private Producto currentProducto { get; set; }

        public BuscarProducto()
        {
            InitializeComponent();
        }

        private void BuscarProducto_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        private void reLoad()
        {
            cargarProductos();
        }

        private async void cargarProductos()
        {
            //loadState(true);
            try
            {
                List<Producto> list = await productoModel.productos();

                // actualizando datos de páginacón
               // paginacion.itemsCount = productosRoot.nro_registros;
                // paginacion.reload();

                // Ingresando
                productos = list;
                productoBindingSource.DataSource = productos;
                dataGridView.Refresh();

                // Mostrando la paginacion
                // mostrarPaginado();

                // Formato de celdas
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                // loadState(false);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Rows.Count == 0) return;
            try
            {
                int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
                int idProducto = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview
                currentProducto = productos.Find(x => x.idProducto == idProducto); // Buscando la registro especifico en la lista de registros
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Error proveedor", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnAddMarca_Click(object sender, EventArgs e)
        {

        }
    }
}
