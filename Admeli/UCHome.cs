using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;
using Admeli.Componentes;
using Entidad;
using LiveCharts;
using LiveCharts.Wpf;

namespace Admeli
{
    public partial class UCHome : UserControl
    {
        private FormPrincipal formPrincipal;
        private CompraModel compraModel = new CompraModel();
        private VentaModel ventaModel = new VentaModel();
        private NotaEntradaModel notaEntradaModel = new NotaEntradaModel();
        private CobroModel cobroModel = new CobroModel();
        private PagoModel pagoModel = new PagoModel();
        private FormPrincipal formHomeDarck;

        public bool lisenerKeyEvents { get; internal set; }

        #region ============================= Contructor =============================
        public UCHome()
        {
            InitializeComponent();
        }

        public UCHome(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;
            cargarGraficoVentas();
        }
        #endregion

        struct ultimasVentas
        {
            public string dia { get; set; }
            public dynamic idVenta { get; set; }
            public string total { get; set; }
        }

        private async void cargarGraficoVentas()
        {
            try
            {
                /// Cargando datos
                List<ultimasVentas> ventas = await ventaModel.ventasPorMes<ultimasVentas>();
                
                ChartValues<double> serie1Values = new ChartValues<double>();
                String[] xValues = new string[ventas.Count];
                for (int i = 0; i < ventas.Count; i++)
                {
                    xValues[i] = String.Format("Dia {0}", ventas[i].dia);
                    serie1Values.Add(Convert.ToDouble(ventas[i].total));
                }

                /// 
                cartesianChart1.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Total",
                        Values = serie1Values
                    }
                };

                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "Dias",
                    Labels = xValues,
                    Separator = new Separator // force the separator step to 1, so it always display all labels
                    {
                        Step = 1,
                        IsEnabled = true //disable it to make it invisible.
                    }
                });

                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Numero De Ventas",
                    LabelFormatter = value => value + "",
                    Separator = new Separator()
                    //LabelFormatter = value => value.ToString("C")
                });

                cartesianChart1.LegendLocation = LegendLocation.Right;

                //modifying any series values will also animate and update the chart
                cartesianChart1.Series[0].Values.Add(5d);


                // cartesianChart1.DataClick += CartesianChart1OnDataClick;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private async void cargarGraficoCompra()
        {

        }



        private void UCHome_Load(object sender, EventArgs e)
        {
          /*  cargarRegistros1();
            cargarRegistros2();
            cargarRegistros3();
            cargarRegistros4();
            cargarRegistros5();
            cargarRegistros6();*/
        }

        private async void cargarRegistros1()
        {
            this.formPrincipal.appLoadState(true);
            compraBindingSource.DataSource = await compraModel.comprasUltimas(PersonalModel.personal.idPersonal,ConfigModel.sucursal.idSucursal,1,1);
            this.formPrincipal.appLoadState(false);
        }

        private async void cargarRegistros2()
        {
            this.formPrincipal.appLoadState(true);
            ventaBindingSource.DataSource = await ventaModel.ventasUltimas(PersonalModel.personal.idPersonal,ConfigModel.sucursal.idSucursal,1,1);
            this.formPrincipal.appLoadState(false);
        }

        private async void cargarRegistros3()
        {
            this.formPrincipal.appLoadState(true);
            notaEntradaBindingSource.DataSource = await notaEntradaModel.nEntradaPendientes(PersonalModel.personal.idPersonal, ConfigModel.sucursal.idSucursal, 1, 1);
            this.formPrincipal.appLoadState(false);
        }

        internal void reLoad()
        {
            // throw new NotImplementedException();
        }

        private void cargarRegistros4()
        {
            // 
        }
        private async void cargarRegistros5()
        {
            this.formPrincipal.appLoadState(true);
            cobroBindingSource.DataSource = await cobroModel.porCobrar(PersonalModel.personal.idPersonal, ConfigModel.sucursal.idSucursal, ConfigModel.asignacionPersonal.idAsignarCaja, 1, 1);
            this.formPrincipal.appLoadState(false);
        }
        private async void cargarRegistros6()
        {
            this.formPrincipal.appLoadState(true);
            pagoBindingSource.DataSource = await pagoModel.porPagar(PersonalModel.personal.idPersonal, ConfigModel.sucursal.idSucursal, ConfigModel.asignacionPersonal.idAsignarCaja, 1, 1);
            this.formPrincipal.appLoadState(false);
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
           /* int sizeW = panelContainer.Size.Width;
            int sizeH = panelContainer.Size.Height;
            sizeW = sizeW / 2;
            panelItem1.Size = new Size(sizeW, sizeH);*/
        }

        private void panelContainer_Paint_1(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelContainer);
        }
    }
}
