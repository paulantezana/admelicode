using Admeli.Compras.Nuevo.Detalle;
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
using Admeli.Configuracion.Modificar;
using Bunifu.Framework.UI;
namespace Admeli.Compras.Nuevo
{
    public partial class FormCompraNuevo1 : Form
    {
      
        private UCGeneralesPD uCGeneralesPD { get; set; }
        private UCProveedorGeneralCompra uCProveedorGeneral { get; set; }
        private BunifuGradientPanel panel = new BunifuGradientPanel();
        public int currentIDProveedor { get; set; }
      
        public bool nuevo { get; set; }

        public ProductoModel productoModel = new ProductoModel();
        public Proveedor currentProveedor; 
        public FormCompraNuevo1(Proveedor currentProveedor)
        {
            InitializeComponent();
            this.currentProveedor = currentProveedor;
            this.currentIDProveedor = currentProveedor.idProveedor;
            this.nuevo = false;
            this.reLoad();
        }
        public FormCompraNuevo1(Object currentProveedor)
        {
            InitializeComponent();
           // OcurrentProducto.
           
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
               
                
                case "Compra":
                    if (uCGeneralesPD == null)
                    {
                        this.uCGeneralesPD = new UCGeneralesPD(this);
                        this.panelMainNP.Size = uCGeneralesPD.Size;
                        this.panelMainNP.Controls.Add(uCGeneralesPD);
                        this.uCGeneralesPD.Dock = System.Windows.Forms.DockStyle.None;
                        // this.uCGeneralesPD.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right|AnchorStyles.Left|AnchorStyles.Top );                  
                        this.uCGeneralesPD.Location = new System.Drawing.Point(0, 0);
                        this.uCGeneralesPD.Name = "uCGeneralesPD";
                        
                        this.panelMainNP.Size =new Size (800, 900);


                    }
                    else
                    {
                        this.panelMainNP.Controls.Add(uCGeneralesPD);
                        this.uCGeneralesPD.reLoad();
                    }
                    break;
                case "proveedor":
                    if (uCProveedorGeneral == null)
                    {

                        this.uCProveedorGeneral = new UCProveedorGeneralCompra(this);
                        

                      
                        this.panelMainNP.Controls.Add(uCProveedorGeneral);
                        ResizeableControl resizeableControl = new ResizeableControl(uCProveedorGeneral);

                        this.uCProveedorGeneral.Dock = System.Windows.Forms.DockStyle.None;
                        this.uCProveedorGeneral.Location = new System.Drawing.Point(0, 0);
                        this.uCProveedorGeneral.Name = "uCProveedorGeneralCompra";
                      
                        this.uCProveedorGeneral.TabIndex = 0;
                       
                    }
                    else
                    {
                        this.panelMainNP.Controls.Add(uCProveedorGeneral);
                        this.uCProveedorGeneral.reLoad();
                    }
                    break;

                default:
                    break;
            }
        }

        private void limpiarControles()
        {
            this.panelMainNP.Controls.Clear();
            if (uCGeneralesPD != null) uCGeneralesPD.lisenerKeyEvents = false;
           
        }

        private void btnColor()
        {
            btnProveedor.BackColor = Color.FromArgb(230, 231, 232);
            btnProveedor.BackColor = Color.FromArgb(230, 231, 232);
           
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
        #region ==================== Estados =====================
        internal void loadStateApp(bool state)
        {
            if (state)
            {
                progressBarApp.Style = ProgressBarStyle.Marquee;
                Cursor.Current = Cursors.WaitCursor;
            }
            else
            {
                progressBarApp.Style = ProgressBarStyle.Blocks;
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion

        private void FormProductoNuevo_Load(object sender, EventArgs e)
        {
            togglePanelMain("proveedor");
            btnProveedor.BackColor = Color.White;

        }

        private void btnGenerales_Click(object sender, EventArgs e)
        {
            togglePanelMain("proveedor");
            btnProveedor.BackColor = Color.White;
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            togglePanelMain("proveedor");
            btnProveedor.BackColor = Color.White;
        }

       
        #endregion

        #region ============================ Root Load ============================
        internal void reLoad()
        {
            
                this.btnProveedor.Enabled = true;
                this.Text = "MANTENIMIENTO PRODUCTO " ;
                cargarDatosModificar();

          
        }
        internal  void reLoadinStock()
        {
            
        }

        #endregion

        private async void cargarDatosModificar()
        {
           //urrentProducto = await productoModel.productoDatos(currentIDProducto);
            
        }

        #region ========================== Guardar ==========================
        

        

        internal void executeCerrar()
        {
            this.Close();
        }
        #endregion

        private void btnCompra_Click(object sender, EventArgs e)
        {
            //compra
            togglePanelMain("Compra");
            btnCompra.BackColor = Color.White;
        }

        private void btnProducto_Click(object sender, EventArgs e)
        {
            // productos
            togglePanelMain("Producto");
            btnProducto.BackColor = Color.White;
        }

       
    }
}
