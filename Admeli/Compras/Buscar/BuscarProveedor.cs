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
using System.Web.UI;


namespace Admeli.Compras.Buscar
{
    public partial class BuscarProveedor : Form
    {
        private ProveedorModel proveedorModel = new ProveedorModel();

        public Proveedor currentProveedor { get; set; }
        public List<Proveedor> proveedores { get; set; }
        private DataTable dataTable { get; set; }
        public BuscarProveedor()
        {
            InitializeComponent();
        }

        private void BuscarProveedor_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        public void BuscarLINQ()
        {
            DataTable dt;
            DataView dv;
            dt= proveedorBindingSource.DataSource as DataTable;

          
         

        }
       void filtro(string busqueda)
        {
            
            string Filtro = String.Format("{0} LIKE '%{1}%'", "razonSocial", textBuscar.Text);
            proveedorBindingSource.Filter = Filtro;
            dataGridView.Refresh();
        }
      


        private void reLoad()
        {
            cargarProceedoresLike();
        }

        private async void cargarProceedoresLike()
        {
            loadState(true);
            try
            {
                RootObject<Proveedor> proveedorRoot;
                if (textBuscar.Text == "")
                {
                    proveedorRoot = await proveedorModel.proveedores(1, 2000);
                }
                else
                {
                    proveedorRoot = await proveedorModel.proveedoreslike(1, 2000, textBuscar.Text);
                }

                // actualizando datos de páginacón
                // paginacion.itemsCount = ordenCompra.nro_registros;
                // paginacion.reload();

                // Ingresando
                proveedores = proveedorRoot.datos;
                proveedorBindingSource.DataSource = proveedores;
                dataGridView.Refresh();

               //  mostrarPaginado();
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
            if (state)
            {
                Cursor.Current = Cursors.WaitCursor;
                progressBarApp.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                Cursor.Current = Cursors.Default;
                progressBarApp.Style = ProgressBarStyle.Blocks;
            }
        }

        private void textBuscar_KeyUp(object sender, KeyEventArgs e)
        {

            // tomar como base esta expresion
            BindingList<Proveedor> filtered = new BindingList<Proveedor>(proveedores.Where(obj => obj.razonSocial.ToUpper().Contains(textBuscar.Text.ToUpper())).ToList());
            dataGridView.DataSource = filtered;
            dataGridView.Update();
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Rows.Count == 0) return;
            try
            {
                int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
                int idProveedor = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview
                currentProveedor = proveedores.Find(x => x.idProveedor == idProveedor); // Buscando la registro especifico en la lista de registros
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Error proveedor", MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
        }
    }
}
