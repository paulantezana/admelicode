using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using Modelo;
using Entidad;

namespace Admeli.Reportes
{
    public partial class UCReporteImpuestos : UserControl
    {
        private FormPrincipal formPrincipal;
        private SucursalModel sucursalModel = new SucursalModel();
        private FechaModel fechaModel = new FechaModel();
        private ReporteModel reporteModel = new ReporteModel();
        private int maxAnio = 2018;
        private int maxMes = 04;
        public UCReporteImpuestos()
        {
            InitializeComponent();
        }

        public UCReporteImpuestos(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;
            this.reLoad();
        }

        public void reLoad()
        {
            loadState(true);
            cargarFechas();
            cargarSucursales();
            dibujarGrafico();
            loadState(false);
        }

        private async void cargarFechas()
        {
            try
            {
                //Cargar la fecha del sistema
                FechaSistema fechaSistema = await fechaModel.fechaSistema();
                maxAnio = fechaSistema.fecha.Year;
                maxMes = fechaSistema.fecha.Month;
                limitarComboAnio(maxAnio);
                limitarComboMes(maxMes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Fecha", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void limitarComboMes(int limiteMes)
        {
            cbxMes.Items.Clear();
            cbxMes.Items.Add("Todo");
            for (int i = 1; i <= limiteMes; i++)
            {
                cbxMes.Items.Add(i);
            }
            cbxMes.SelectedItem = limiteMes;
        }
        private void limitarComboAnio(int limiteAnio)
        {
            cbxAnio.Items.Clear();
            cbxAnio.Items.Add("Todo");
            for (int i = 2000; i <= limiteAnio; i++)
            {
                cbxAnio.Items.Add(i);
            }
            cbxAnio.SelectedItem = limiteAnio;
        }

        private async void cargarSucursales()
        {

            try
            {
                // Cargando las categorias desde el webservice
                sucursalBindingSource.DataSource = await sucursalModel.listarSucursalesActivos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar Categorias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void loadState(bool state)
        {
            formPrincipal.appLoadState(state);
            panelBody.Enabled = !state;
            dgvImpuestos.Enabled = !state;
        }

        private void dibujarGrafico()
        {
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();

            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "",
                    Values = new ChartValues<double> { 90, 50, 39, 50 }
                }
            };

            //adding series will update and animate the chart automatically


            //also adding values updates and animates the chart automatically


            cartesianChart1.AxisX.Add(new Axis
            {
                Labels = new[] { "Compras", "Ventas", "Ingresos", "Egresos" }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                LabelFormatter = value => value.ToString("N")
            });
        }
        private void panelBody_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnConsuitar_Click(object sender, EventArgs e)
        {
            consultar();
            MessageBox.Show(cbxAnio.SelectedItem.ToString()+" M: "+cbxMes.SelectedText);
        }

        private async void consultar()
        {
            
            try
            {
                string stringMes = cbxMes.SelectedItem.ToString();
                if (stringMes == "Todo") { stringMes = "00"; }
                if (int.Parse(stringMes) < 10) { stringMes = "0" + stringMes; }
                ObjectReporteImpuesto objetoImpuesto = await reporteModel.comprasSucursal<ObjectReporteImpuesto>(cbxSucursal.SelectedValue.ToString(), 1, stringMes, cbxAnio.SelectedItem.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Filtrar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void cbxAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAnio.SelectedItem.ToString() == "Todo")
            {
                limitarComboMes(12);
                return;
            }
            if (int.Parse(cbxAnio.SelectedItem.ToString()) != maxAnio)
            {
                limitarComboMes(12);
            }
            else
            {
                limitarComboMes(maxMes);
            }
        }
    }

    public class ObjectMes{
        public string nombre { get; set; }
        public string numero { get; set; }
    }

    public class ObjectReporteImpuesto
    {
        public int idImpuesto { get; set; }
        public string nombreImpuesto { get; set; }
        public string siglasImpuesto { get; set; }
        public string valorImpuesto { get; set; }
        public bool porcentual { get; set; }
        public bool porDefecto { get; set; }
        public int estado { get; set; }
        public string totalVentas { get; set; }
        public string totalCompras { get; set; }
        public string ingresoBruto { get; set; }
        public string egresoBruto { get; set; }
        public string IB_EB { get; set; }
        public string impuesto { get; set; }
        public string utilidadBruta { get; set; }
        public string I { get; set; }
        public string E { get; set; }
        public string I_E { get; set; }
        public string ventas { get; set; }
        public string utilidadProductos { get; set; }
        public string utilidadFiltrada { get; set; }
        public string utilidadFiltradaFormula { get; set; }
        public string utilidadProductosFormula { get; set; }
        public string impuestoFormula { get; set; }
        public string utilidadBrutaFormula { get; set; }
    }

}
