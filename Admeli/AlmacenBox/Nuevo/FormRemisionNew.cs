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
using Admeli.AlmacenBox.buscar;
using Admeli.AlmacenBox.Nuevo.detalle;
using Admeli.Componentes;
using Entidad;
using Entidad.Configuracion;
using Modelo;


namespace Admeli.AlmacenBox.Nuevo
{
    public partial class FormRemisionNew : Form
    {


        // servicios necesarios

        AlmacenModel AlmacenModel = new AlmacenModel();
        ProductoModel productoModel = new ProductoModel();
        FechaModel fechaModel = new FechaModel();


        // objetos que cargan a un inicio
        private  List<Producto> listProducto { get; set; }
        private List<Presentacion> listPresentacion { get; set; }
        private List<Almacen> listAlmacen { get; set; }
        private List<AlmacenCorrelativo> listCorrelativoA { get; set; }


        // entidadades auxiliares

        private bool  nuevo { get; set; }
        private string formato { get; set; }
        private int nroDecimales = 2;
        private FechaSistema fechaSistema { get; set; }



        //objetos en tiempo real

       private Almacen currentAlmacen { get; set; }
       private AlmacenCorrelativo currentCorrelativoA { get; set; }
       private Producto currentProducto { get; set; }
       private Presentacion currentPresentacion { get; set; }
       private NotaSalidaR currentNotaSalida { get; set; }


        public FormRemisionNew()
        {
            InitializeComponent();
            this.nuevo = true;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();
      

        }
        public FormRemisionNew(NotaSalidaR notaSalidaR)
        {
            InitializeComponent();
            this.nuevo = false;

            this.currentNotaSalida = notaSalidaR;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();
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
            if (nuevo == true)
                this.reLoad();
            else
            {
                this.reLoad();
               
            }

        }
        private void reLoad()
        {
            cargarAlmacenes();
            cargarProductos();         
        }

        #endregion

        #region ============================== Load ==============================
        private async void cargarAlmacenes()
        {
            //listAlmacen = await AlmacenModel.almacenesAsignados(ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);

            listAlmacen = await AlmacenModel.almacenesAsignados(1, 1);

            

        }
        private async void cargarDocCorrelativo(int idAlmacen)
        {
            listCorrelativoA = await AlmacenModel.DocCorrelativoAlmacen(idAlmacen);
            currentCorrelativoA = listCorrelativoA[0];
            txtSerie.Text = currentCorrelativoA.serie;
            txtCorrelativo.Text = currentCorrelativoA.correlativoActual;
        }

        private async void cargarProductos()
        {
            try
            {
                listProducto  = await productoModel.productos();
                productoBindingSource.DataSource = listProducto;


        
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
              

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel30_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {


            FormBuscardetalleNotaSalida formBuscardetalleNotaSalida = new FormBuscardetalleNotaSalida();
            formBuscardetalleNotaSalida.ShowDialog();
        }

        private void btnBuscarOrigen_Click(object sender, EventArgs e)
        {

            formGeografia formGeografia = new formGeografia();
            formGeografia.ShowDialog();

        }

        private void btnBuscarDestino_Click(object sender, EventArgs e)
        {
            formGeografia formGeografia = new formGeografia();
            formGeografia.ShowDialog();
        }

        private void btnNewMotivo_Click(object sender, EventArgs e)
        {

            FormMotivo formMotivo = new FormMotivo();
            formMotivo.ShowDialog();

        }

        private void btnNewTransporte_Click(object sender, EventArgs e)
        {
            FormTransporteNew formTransporteNew = new FormTransporteNew();
            formTransporteNew.ShowDialog();
        }
    }
}
