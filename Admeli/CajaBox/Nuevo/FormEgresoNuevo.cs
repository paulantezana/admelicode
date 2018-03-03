using Admeli.Componentes;
using Entidad;
using Entidad.Configuracion;
using Entidad.Util;
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
    public partial class FormEgresoNuevo : Form
    {
        private MonedaModel monedaModel = new MonedaModel();
        private EgresoModel egresoModel = new EgresoModel();
        private MedioPagoModel medioPagoModel = new MedioPagoModel();
        private CajaModel cajaModel = new CajaModel();

        private int currentIDEgreso { get; set; }
        private bool nuevo { get; set; }
        private Egreso currentEgreso { get; set; }
        private List<MedioPago> mediosDePagos { get; set; }


        public FormEgresoNuevo()
        {
            InitializeComponent();
            this.nuevo = true;
            dtpFechaPago.Value = DateTime.Now;
        }

        public FormEgresoNuevo(Egreso currentEgreso)
        {
            this.currentEgreso = currentEgreso;
            this.nuevo = false;
        }

        #region ========================= Root Load =========================
        private void FormEgresoNuevo_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        private void reLoad()
        {
            this.cargarMonedas();
            this.cargarMediosPago();
            this.cargarCorrelativo();

            // Verificacion del estado de caja
            this.verificarCaja();
        } 
        #endregion

        #region ========================= Loads =========================
        private async void cargarMonedas()
        {
            try
            {
                monedaBindingSource.DataSource = await monedaModel.monedas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        struct CorrelativoData
        {
            public string correlativoActual { get; set; }
            public string serie { get; set; }
        }

        private async void cargarCorrelativo()
        {
            try
            {
                CorrelativoData response = await cajaModel.correlativoSerie<CorrelativoData>(ConfigModel.asignacionPersonal.idCaja, 0);
                textNOperacion.Text = String.Format("{0} - {1}", response.serie, response.correlativoActual);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region ================================= Validator ====================================
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
        #endregion

        #region ========================== SAVE AND UPDATE ===========================
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            guardarSucursal();
        }

        private async void guardarSucursal()
        {
            if (!validarCampos()) return;
            try
            {
                // Verificacion
                List<Moneda> monedas = await cajaModel.cierreCajaIngresoMenosEgreso(mediosDePagos[0].idMedioPago, ConfigModel.cajaSesion.idCajaSesion);
                MessageBox.Show("Falta logica de programacion");

                // Guardar
                crearObjetoSucursal();
                if (nuevo)
                {
                    Response response = await egresoModel.guardar(currentEgreso);
                    MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Response response = await egresoModel.modificar(currentEgreso);
                    MessageBox.Show(response.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void crearObjetoSucursal()
        {
            currentEgreso = new Egreso();

            if (!nuevo) currentEgreso.idEgreso = currentIDEgreso; // Llenar el id categoria cuando este en esdo modificar

            currentEgreso.numeroOperacion = textNOperacion.Text;
            currentEgreso.monto = textMonto.Text;
            currentEgreso.moneda = cbxMoneda.Text;
            currentEgreso.idMoneda = Convert.ToInt32(cbxMoneda.SelectedValue);
            currentEgreso.motivo = textMotivo.Text;
            currentEgreso.observacion = textObcervacion.Text;
            Fecha currentFecha = new Fecha()
            {
                date = (DateTime)dtpFechaPago.Value
            };
            currentEgreso.fechaPago = currentFecha;
        }

        private bool validarCampos()
        {
            if (textNOperacion.Text == "")
            {
                errorProvider1.SetError(textNOperacion, "Este campo esta bacía");
                textNOperacion.Focus();
                return false;
            }
            errorProvider1.Clear();

            // validacion monto
            if (textMonto.Text.Trim() == "")
            {
                errorProvider1.SetError(textMonto, "Campo obligatorio");
                Validator.textboxValidateColor(textMonto, 0);
                return false;
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textMonto, 1);

            // validacion motivo
            if (textMotivo.Text.Trim() == "")
            {
                errorProvider1.SetError(textMotivo, "Campo obligatorio");
                Validator.textboxValidateColor(textMotivo, 0);
                return false;
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textMotivo, 1);

            // Toda las validaciones correctas
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region ========================= Decoration and value defaults =========================
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panel2, 157, 157, 157);
        }
        #endregion

        #region ============================ Validacion timpo real ============================
        private void textMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
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

        private void textMotivo_Validated(object sender, EventArgs e)
        {
            if (textMotivo.Text.Trim() == "")
            {
                errorProvider1.SetError(textMotivo, "Campo obligatorio");
                Validator.textboxValidateColor(textMotivo, 0);
                return;
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textMotivo, 1);
        }
        #endregion

        private void FormEgresoNuevo_Shown(object sender, EventArgs e)
        {
            textMonto.Focus();
        }
    }
}
