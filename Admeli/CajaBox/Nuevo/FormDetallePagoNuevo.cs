using Admeli.Componentes;
using Entidad;
using Entidad.Configuracion;
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
    public partial class FormDetallePagoNuevo : Form
    {
        private bool nuevo { get; set; }
        private SaveObjectPagoDetalle currentSaveObject = new SaveObjectPagoDetalle();
        private MonedaModel monedaModel = new MonedaModel();
        private MedioPagoModel medioPagoModel = new MedioPagoModel();
        private PagoModel pagoModel = new PagoModel();
        private CajaModel cajaModel = new CajaModel();
        private Pago currentPago = new Pago();
        private List<MedioPago> mediosDePagos { get; set; }

        public FormDetallePagoNuevo()
        {
            InitializeComponent();
        }

        public FormDetallePagoNuevo(Pago currentPago)
        {
            InitializeComponent();
            this.nuevo = true;
            this.currentPago = currentPago;
            this.reLoad();
        }

        private void reLoad()
        {
            this.cargarMonedas();
            this.cargarMediosPago();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            guardarPagoDetalle();
        }

        private async void guardarPagoDetalle()
        {
            //validar los campos
            if (!validarCampos()) return;
            try
            {
                //Verificar Caja Asignada y recuperar idCajaSesion
                //currentCajaSesion=await cajaModel.cajaSesion(ConfigModel.asignacionPersonal.idAsignarCaja);
                //Registrar CobroDetalle

                //Verificar que los fondos sean mayores a los egresos actuaeles
                
                List<Moneda> monedas = await cajaModel.cierreCajaIngresoMenosEgreso(mediosDePagos[0].idMedioPago, ConfigModel.cajaSesion.idCajaSesion);
                double fondoCaja = monedas.Find(x=>x.idMoneda == int.Parse(cbxMoneda.SelectedValue.ToString())).total;
                if (fondoCaja < double.Parse(textMonto.Text))
                {
                    MessageBox.Show("No hay suficientes fondos en la caja actual", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }                
                crearObjetoPagoDetalle();
                if (nuevo)
                {
                    Response response = await pagoModel.guardarPagoDetalle(currentSaveObject);
                    MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //No se puede modificar
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void crearObjetoPagoDetalle()
        {
            
            currentSaveObject = new SaveObjectPagoDetalle();
            currentSaveObject.estado = Convert.ToInt32(chkActivo.Checked);
            currentSaveObject.fechaPago = dtpFechaPago.Value.ToString("yyyy-MM-dd HH':'mm':'ss");
            currentSaveObject.idCaja = ConfigModel.asignacionPersonal.idCaja;
            currentSaveObject.idCajaSesion = ConfigModel.cajaSesion.idCajaSesion;
            currentSaveObject.idMedioPago = mediosDePagos[0].idMedioPago;
            currentSaveObject.idMoneda = Convert.ToInt32(cbxMoneda.SelectedValue);
            currentSaveObject.idPago = currentPago.idPago;
            currentSaveObject.medioPago = mediosDePagos[0].nombre;
            currentSaveObject.moneda = cbxMoneda.Text;
            currentSaveObject.monto = textMonto.Text;
            currentSaveObject.motivo = "PAGO COMPRA";
            currentSaveObject.numeroOperacion = " ";
            currentSaveObject.observacion = textObservacion.Text;

            // currentSaveObject
            currentSaveObject.idMoneda = Convert.ToInt32(cbxMoneda.SelectedValue);

        }

        private bool validarCampos()
        {
            //Validar Monto
            if (textMonto.Text.Trim() == "")
            {
                errorProvider1.SetError(textMonto, "Este campo está vacío");
                Validator.textboxValidateColor(textMonto, 0);
                textMonto.Focus();
                return false;
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textMonto, 1);
            //Moneda
            if (cbxMoneda.SelectedIndex < 0)
            {
                errorProvider1.SetError(cbxMoneda, "Este campo está vacío, vuelva a cargar el formulario");
                return false;
            }
            //Fecha Pago
            if (dtpFechaPago.Value == null)
            {
                errorProvider1.SetError(dtpFechaPago, "Este campo está vacío");
                return false;
            }
            // Toda las validaciones correctas
            return true;
        }

        private void textMonto_Validated(object sender, EventArgs e)
        {
            if (textMonto.Text.Trim() == "")
            {
                errorProvider1.SetError(textMonto, "Campo obligatorio");
                Validator.textboxValidateColor(textMonto, 0);
                return;
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textMonto, 1);
        }

        private void textMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, textMonto.Text);
        }

        private void panelMoneda_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelMoneda, 157, 157, 157);
        }
    }

    class SaveObjectPagoDetalle
    {
        public int idPago { get; set; }
        public string monto { get; set; }
        public string motivo { get; set; }
        public int estado { get; set; }
        public int idCaja { get; set; }
        public string numeroOperacion{ get; set; }
        public int idCajaSesion { get; set; }
        public int idMedioPago { get; set; }
        public string medioPago { get; set; }
        public int idMoneda { get; set; }
        public string moneda { get; set; }
        public string observacion { get; set; }
        public string fechaPago { get; set; }
    }
}
