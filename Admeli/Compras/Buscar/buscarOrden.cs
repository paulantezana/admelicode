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
    public partial class buscarOrden : Form
    {
        private ProveedorModel proveedorModel = new ProveedorModel();
        private OrdenCompraModel OrdenCompraModel = new OrdenCompraModel();

        public Proveedor currentProveedor { get; set; }
        public List<Proveedor> proveedores { get; set; }
        private DataTable dt = new DataTable();
        private List<OrdenCompraSinComprarM> sinComprarOrden ;

       public  OrdenCompraSinComprarM compraSinComprarM { get; set; }
        public buscarOrden()
        {
            InitializeComponent();
            sinComprarOrden = new List<OrdenCompraSinComprarM>();
        }

        private void BuscarProveedor_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        private void reLoad()
        {
            cargarProceedoresLike();
        }



        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                // HERE IS WHERE THE ERROR IS THROWN FOR NULLABLE TYPES
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }



        private async void cargarProceedoresLike()
        {

           



            loadState(true);
            try
            {

                
                List<OrdenCompraSinComprar> sinComprar;
                sinComprar = await OrdenCompraModel.listarOrdenCompraSinCompra(ConfigModel.sucursal.idSucursal);
               
                foreach (OrdenCompraSinComprar O in sinComprar)
                {

                    sinComprarOrden.Add( OrdenCompraSinComprarM.convertir(O));

                }
               


                dt = ConvertTo(sinComprarOrden);
                dataGridView.DataSource = dt;
                dataGridView.Columns[0].Visible=false;
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
            if (e.KeyCode == Keys.Enter)
            {
                string fieldName = string.Concat("[", dt.Columns[3].ColumnName, "]");
                dt.DefaultView.Sort = fieldName;
                DataView view = dt.DefaultView;
                view.RowFilter = string.Empty;
                if (textBuscar.Text != string.Empty)
                    view.RowFilter = fieldName + " LIKE '%" + textBuscar.Text + "%'";
                dataGridView.DataSource = view;
            }
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Rows.Count == 0) return;
            try
            {
                int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
                int idProveedor = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview
                compraSinComprarM = sinComprarOrden.Find(x => x.idOrdenCompra == idProveedor); // Buscando la registro especifico en la lista de registros
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Error proveedor", MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
        }

        private void ordenCompraSinComprarBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
