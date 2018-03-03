using Admeli.Componentes;
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

namespace Admeli
{
    public partial class FormConfigInicial : Form
    {
        public FormLogin formLogin { get; set; }

        public FormConfigInicial()
        {
            InitializeComponent();
        }

        public FormConfigInicial(FormLogin formLogin)
        {
            this.formLogin = formLogin;
            InitializeComponent();
        }

        private void FormConfigInicial_Shown(object sender, EventArgs e)
        {
            cbxAlmacenes.DataSource = ConfigModel.alamacenes;
            cbxAlmacenes.DisplayMember = "nombre";
            cbxAlmacenes.ValueMember = "idAlmacen";
            cbxAlmacenes.SelectedIndex = 0;

            cbxPuntosVenta.DataSource = ConfigModel.puntosDeVenta;
            cbxPuntosVenta.DisplayMember = "nombre";
            cbxPuntosVenta.ValueMember = "idPuntoVenta";
            cbxPuntosVenta.SelectedIndex = 0;
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (cbxAlmacenes.SelectedIndex == -1)
            {
                errorProvider1.SetError(cbxAlmacenes, "No se seleccionó nungun almacen");
                cbxAlmacenes.Focus();
                return;
            }
            errorProvider1.Clear();

            if (cbxPuntosVenta.SelectedIndex == -1)
            {
                errorProvider1.SetError(cbxPuntosVenta, "No se seleccionó nungun puntos de venta");
                cbxPuntosVenta.Focus();
                return;
            }
            errorProvider1.Clear();

            // Estableciendo el almacen y punto de venta al personal asignado
            ConfigModel.currentIdAlmacen = Convert.ToInt32(cbxAlmacenes.SelectedValue.ToString());
            ConfigModel.currentPuntoVenta = Convert.ToInt32(cbxPuntosVenta.SelectedValue.ToString());

            // Mostrando el formulario principal
            this.Hide();
            FormPrincipal formPrincipal = new FormPrincipal(this.formLogin);
            formPrincipal.ShowDialog();
        }

        #region =============================== Paint ===============================
        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panel2);
            drawShape.lineBorder(panel3);
        }
        #endregion

        private void btnCLose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
