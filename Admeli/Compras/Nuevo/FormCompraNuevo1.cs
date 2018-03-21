using Admeli.Productos.Nuevo.PDetalle;
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
using Admeli.Compras.Buscar;

namespace Admeli.Compras.Nuevo
{
    public partial class FormCompraNuevo1 : Form
    {
        private UCAdicionalPD uCAdicionalPD { get; set; }
        private UCDescuentosPD uCDescuentosPD { get; set; }
        private UCGeneralesCD uCGeneralesPD { get; set; }
        private UCImpuestoPD uCImpuestoPD { get; set; }
        private UCStockPD uCStockPD { get; set; }
        private UCTiendaOnlinePD uCTiendaOnlinePD { get; set; } 

        public int currentIDProducto { get; set; }
        public Producto currentProducto { get; set; }
        public bool nuevo { get; set; }

        public ProductoModel productoModel = new ProductoModel();

        public FormCompraNuevo1(Producto currentProducto)
        {
            InitializeComponent();
            this.currentProducto = currentProducto;
            this.currentIDProducto = currentProducto.idProducto;
            this.nuevo = false;
            this.reLoad();
        }
        public FormCompraNuevo1(Object OcurrentProducto)
        {
            InitializeComponent();
           // OcurrentProducto.
            this.currentIDProducto = currentIDProducto;
            this.nuevo = false;

            this.reLoadinStock();
        }

        public FormCompraNuevo1()
        {
            InitializeComponent();
            this.nuevo = true;
            this.reLoad();
        }

        #region =============================== TOGGLE Panels ===============================
        private void togglePanelMain(string panelName)
        {
            limpiarControles();
            btnColor();
            switch (panelName)
            {
               
              
                case "generales":
                    if (uCGeneralesPD == null)
                    {
                        this.uCGeneralesPD = new UCGeneralesCD(this);
                        this.panelMainNP.Controls.Add(uCGeneralesPD);
                        this.uCGeneralesPD.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.uCGeneralesPD.Location = new System.Drawing.Point(0, 0);
                        this.uCGeneralesPD.Name = "uCGeneralesPD";
                        this.uCGeneralesPD.Size = new System.Drawing.Size(250, 776);
                        this.uCGeneralesPD.TabIndex = 0;
                    }
                    else
                    {
                        this.panelMainNP.Controls.Add(uCGeneralesPD);
                        this.uCGeneralesPD.reLoad();
                    }
                    break;
               
               
               
                default:
                    break;
            }
        }

        private void limpiarControles()
        {
            this.panelMainNP.Controls.Clear();
            if (uCAdicionalPD != null) uCAdicionalPD.lisenerKeyEvents = false;
            if (uCDescuentosPD != null) uCDescuentosPD.lisenerKeyEvents = false;
            if (uCGeneralesPD != null) uCGeneralesPD.lisenerKeyEvents = false;
            if (uCImpuestoPD != null) uCImpuestoPD.lisenerKeyEvents = false;
            if (uCStockPD != null) uCStockPD.lisenerKeyEvents = false;
            if (uCTiendaOnlinePD != null) uCTiendaOnlinePD.lisenerKeyEvents = false;
        }

        private void btnColor()
        {
            btnGenerales.BackColor = Color.FromArgb(230, 231, 232);
            btnStock.BackColor = Color.FromArgb(230, 231, 232);
            btnOtros.BackColor = Color.FromArgb(230, 231, 232);
            btnImpuestos.BackColor = Color.FromArgb(230, 231, 232);
            btnOfertas.BackColor = Color.FromArgb(230, 231, 232);
            btnWeb.BackColor = Color.FromArgb(230, 231, 232);
        }

        public void appLoadState(bool state)
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

        private void FormProductoNuevo_Load(object sender, EventArgs e)
        {
            togglePanelMain("generales");
            btnGenerales.BackColor = Color.White;
        }

        private void btnGenerales_Click(object sender, EventArgs e)
        {
            togglePanelMain("generales");
            btnGenerales.BackColor = Color.White;
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            togglePanelMain("stock");
            btnStock.BackColor = Color.White;
        }

        private void btnOtros_Click(object sender, EventArgs e)
        {
            togglePanelMain("adicionales");
            btnOtros.BackColor = Color.White;
        }

        private void btnImpuestos_Click(object sender, EventArgs e)
        {
            togglePanelMain("impuestos");
            btnImpuestos.BackColor = Color.White;
        }

        private void btnOfertas_Click(object sender, EventArgs e)
        {
            togglePanelMain("descuentos");
            btnOfertas.BackColor = Color.White;
        }

        private void btnWeb_Click(object sender, EventArgs e)
        {
            togglePanelMain("tiendaOnline");
            btnWeb.BackColor = Color.White;
        }
        #endregion

        #region ============================ Root Load ============================
        internal void reLoad()
        {
            if (this.nuevo)
            {
                this.btnImpuestos.Enabled = false;
                this.btnOfertas.Enabled = false;
                this.btnOtros.Enabled = false;
                this.btnWeb.Enabled = false;
                this.btnStock.Enabled = false;
            }
            else
            {
                this.btnImpuestos.Enabled = true;
                this.btnOfertas.Enabled = true;
                this.btnOtros.Enabled = true;
                this.btnWeb.Enabled = true;
                this.btnStock.Enabled = true;
                this.Text = "MANTENIMIENTO PRODUCTO " + currentProducto.nombreProducto;
                cargarDatosModificar();
            }
        }
        internal  void reLoadinStock()
        {
            if (this.nuevo)
            {
                this.btnImpuestos.Enabled = false;
                this.btnOfertas.Enabled = false;
                this.btnOtros.Enabled = false;
                this.btnWeb.Enabled = false;
                this.btnStock.Enabled = false;
            }
            else
            {
                this.btnImpuestos.Enabled = true;
                this.btnOfertas.Enabled = true;
                this.btnOtros.Enabled = true;
                this.btnWeb.Enabled = true;
                
                cargarDatosModificar();
                
                
                this.Text = "MANTENIMIENTO PRODUCTO ";
            }
        }

        #endregion

        private async void cargarDatosModificar()
        {
            currentProducto = await productoModel.productoDatos(currentIDProducto);
            
        }

        #region ========================== Guardar ==========================
        internal async void executeGuardar()
        {
            // Ejecutando el guardado
            try
            {
                if (nuevo)
                {
                    Response response = await productoModel.guardar(currentProducto);
                    MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Consulta de guardar =============================================
                    this.nuevo = false;
                    this.currentIDProducto = response.id;
                    this.reLoad();
                }
                else
                {
                    Response response = await productoModel.modificar(currentProducto);
                    MessageBox.Show(response.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        internal async void executeGuardarSalir()
        {
            // Ejecutando el guardado
            try
            {
                if (nuevo)
                {
                    Response response = await productoModel.guardar(currentProducto);
                    MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Response response = await productoModel.modificar(currentProducto);
                    MessageBox.Show(response.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        internal void executeCerrar()
        {
            this.Close();
        }
        #endregion

        private void progressBarApp_Click(object sender, EventArgs e)
        {

        }

        private void panelMainNP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
