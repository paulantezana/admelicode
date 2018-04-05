using Admeli.Componentes;
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

namespace Admeli.CajaBox.Nuevo
{
    public partial class FormDetalleCobroNuevo : Form
    {
        private MonedaModel monedaModel = new MonedaModel();
        private MedioPagoModel medioPagoModel = new MedioPagoModel();
        private CajaModel cajaModel = new CajaModel();
        private List<MedioPago> mediosDePagos { get; set; }

        private Cobro currentCobro = new Cobro();
        public FormDetalleCobroNuevo()
        {
            InitializeComponent();
        }
        public FormDetalleCobroNuevo(Cobro currentCobro)
        {
            InitializeComponent();
            this.currentCobro = currentCobro;
            this.reLoad();

        }

        private void reLoad()
        {
            this.verificarCaja();
            this.fechaSistema();
            this.cargarMonedas();
            this.cargarMediosPago();

        }

        private void verificarCaja()
        {
            if (ConfigModel.cajaIniciada)
            {
                lblCajaEstado.Visible = false;
            }
            else
            {
                Validator.labelAlert(lblCajaEstado, 0, "No se inició la caja");
                lblCajaEstado.Visible = true;
                btnAceptar.Enabled = false;
            }
        }

        private async void fechaSistema()
        {
            try
            {
                //No existe un Modelo para fechaSistema, por ello se hace directmente
                //http://localhost:8085/admeli/xcore/services.php/fechasystema
                Modelo.Recursos.WebService webService = new Modelo.Recursos.WebService();
                FechaSistema fecha = await webService.GET<FechaSistema>("fechasystema");
                dtpFechaCalendario.Value = fecha.fecha;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void cargarMonedas()
        {
            try
            {
                monedaBindingSource.DataSource = await monedaModel.monedas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarMediosPago()
        {
            try
            {
                mediosDePagos = await medioPagoModel.medioPagos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        struct CorrelativoData
        {
            public string correlativoActual { get; set; }
            public string serie { get; set; }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
