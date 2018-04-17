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
using Admeli.AlmacenBox.fecha;
using Admeli.AlmacenBox.Nuevo;
using Admeli.Componentes;
using Entidad;
using Entidad.Configuracion;
using Modelo;


namespace Admeli.AlmacenBox.buscar
{
    public partial class FormBuscardetalleNotaSalida : Form
    {


        // servicios necesarios

        AlmacenModel AlmacenModel = new AlmacenModel();
        ProductoModel productoModel = new ProductoModel();
        FechaModel fechaModel = new FechaModel();
        NotaSalidaModel NotaSalidaModel = new NotaSalidaModel();

        // objetos que cargan a un inicio

        private List<NotaSalidaR> listNotasalida { get; set; }
        private List<DetalleNotaSalida> listNotaSalidaDestalle { get; set; }
        public  List<DetalleNotaSalida> listDestalleSubmit { get; set; }

        // entidadades auxiliares

        private bool nuevo { get; set; }
        private string formato { get; set; }
        private int nroDecimales = 2;
        private FechaSistema fechaSistema { get; set; }



        //objetos en tiempo real
        private  NotaSalidaR currentNotaSalida { get; set; }
        private CheckBox HeaderCheckBoxProducto = null;

        public FormBuscardetalleNotaSalida(NotaSalidaR currentNotaSalida )
        {
            InitializeComponent();
            this.nuevo = true;
            formato = "{0:n" + nroDecimales + "}";

            listDestalleSubmit = new List<DetalleNotaSalida>();
            this.currentNotaSalida = currentNotaSalida;
        }


        public FormBuscardetalleNotaSalida(Compra currentCompra)
        {
            InitializeComponent();
            this.nuevo = false;
            formato = "{0:n" + nroDecimales + "}";
           
        }

        #region=======================metodos de apoyo
        private string darformato(object dato)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
        }
        #endregion
        #region ================================ Root Load ================================

        private void FormNotaSalidaNew_Load(object sender, EventArgs e)
        {
            reLoad();

        }
        private void reLoad()
        {
            cargarNotaSalidaDetalle();

            AddHeaderCheckBoxProducto();
            dgvNotaSalida.CellPainting += new DataGridViewCellPaintingEventHandler(dgvSelectAll_CellPaintingProducto);
            HeaderCheckBoxProducto.Click += new EventHandler(HeaderCheckBox_ClickedProducto);
            dgvNotaSalida.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellClickProducto);

        }





        #endregion

        #region ============================== Load ==============================


        public void AddHeaderCheckBoxProducto()
        {
            HeaderCheckBoxProducto = new CheckBox();

            HeaderCheckBoxProducto.Size = new Size(15, 15);

            //Add the CheckBox into the DataGridView
            this.dgvNotaSalida.Controls.Add(HeaderCheckBoxProducto);


        }
      
        private void dgvSelectAll_CellPaintingProducto(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
                ResetHeaderCheckBoxLocationProducto(e.ColumnIndex, e.RowIndex);
        }

        private void ResetHeaderCheckBoxLocationProducto(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dgvNotaSalida.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - HeaderCheckBoxProducto.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - HeaderCheckBoxProducto.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            HeaderCheckBoxProducto.Location = oPoint;
        }

        private void HeaderCheckBox_ClickedProducto(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            dgvNotaSalida.EndEdit();

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgvNotaSalida.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["chkbxseleccionDetalleNotaSalida"] as DataGridViewCheckBoxCell);
                checkBox.Value = HeaderCheckBoxProducto.Checked;
            }
        }

        private void DataGridView_CellClickProducto(object sender, DataGridViewCellEventArgs e)
        {
            //Check to ensure that the row CheckBox is clicked.
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                bool isChecked = true;
                foreach (DataGridViewRow row in dgvNotaSalida.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["chkbxseleccionDetalleNotaSalida"].EditedFormattedValue) == false)
                    {
                        isChecked = false;
                        break;
                    }
                }
                HeaderCheckBoxProducto.Checked = isChecked;
            }
        }


        private async void cargarNotaSalidaDetalle()
        {

           
            listNotaSalidaDestalle = await NotaSalidaModel.nSalidaDetalle(currentNotaSalida!=null?  currentNotaSalida.idNotaSalida : 0);

            detalleNotaSalidaBindingSource.DataSource = listNotaSalidaDestalle; 

        }
        #endregion

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtDocumentoCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

       

       

        private void dgvNotaSalida_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            entrarGuiaremision();

        }

        private void entrarGuiaremision()
        {
            

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {


            foreach (DataGridViewRow row in dgvNotaSalida.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["chkbxseleccionDetalleNotaSalida"] as DataGridViewCheckBoxCell);
                bool estaSeleccionado = Convert.ToBoolean(checkBox.EditedFormattedValue);
                if (estaSeleccionado)
                {
                    DataGridViewTextBoxCell idPresentacion = (row.Cells["idPresentacion"] as DataGridViewTextBoxCell);
                    DetalleNotaSalida presentacion = listNotaSalidaDestalle.Find(X => X.idPresentacion == Convert.ToInt32(idPresentacion.Value));
                    listDestalleSubmit.Add(presentacion);
                }

            }
            this.Close();
        }
    }
}
