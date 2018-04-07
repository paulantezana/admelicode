using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.Compras;
using Admeli.NavDarck;
using Admeli.Navigation;
using Admeli.Productos;
using Admeli.Ventas;
using Entidad;
using Modelo;

namespace Admeli
{
    public partial class FormPrincipal : Form
    {

        private UCCompras uCCompras;
        private UCTiendaRoot uCTiendaRoot { get; set; }
        private UCMessageRoot uCMessageRoot { get; set; }
        private UCConfigRoot uCConfigRoot { get; set; }
        // private UCOTro uCTiendaRoot { get; set; }


        private UCConfigNav uCConfigNav;
        private UCHome uCHome;

        // Formularios
        private FormLogin formLogin;

        // Accessos directos
        private UCVentas uCVentas;
        private UCListadoProducto uCListadoProducto;
       
        

        // Modelos
        private SucursalModel sucursalModel = new SucursalModel();
        private ConfigModel configModel = new ConfigModel();
        private PersonalModel personalModel = new PersonalModel();
        private SunatModel sunatModel = new SunatModel();


        //datos generales usados por todos los uc
        public static Asignacion asignacion;

        // Metodos
        private bool notCloseApp { get; set; }


 

        public FormPrincipal()
        {
            InitializeComponent();
        }
        public FormPrincipal(FormLogin formLogin)
        {
            InitializeComponent();
            this.formLogin = formLogin;
        }



        private void pain()
        {

            this.panelMain.Controls.Clear();
            if (this.uCCompras == null)
            {
                this.uCCompras = new UCCompras();
                this.panelMain.Controls.Add(uCCompras);
                this.uCCompras.Dock = System.Windows.Forms.DockStyle.Fill;
                this.uCCompras.Location = new System.Drawing.Point(0, 0);
                this.uCCompras.Name = "uCCompras";
                this.uCCompras.Size = new System.Drawing.Size(250, 776);
                this.uCCompras.TabIndex = 0;
            }
            else
            {
                this.panelMain.Controls.Add(uCCompras);
            }

        }
        public void showMenuLeft()
        {
            if (panelAsideContainer.Size.Width < 100)
            {
                panelAsideContainer.Size = new Size(250, 700);
            }
        }
        private void FormPrueba_Load(object sender, EventArgs e)
        {
            pain();
            toggleRootMenu();
        }
        internal void toggleRootMenu()
        {
            if (this.uCTiendaRoot == null)
            {
                this.uCTiendaRoot = new UCTiendaRoot(this);
                this.panelAsideMain.Controls.Add(uCTiendaRoot);
                this.uCTiendaRoot.Dock = System.Windows.Forms.DockStyle.Fill;
                this.uCTiendaRoot.Location = new System.Drawing.Point(0, 0);
                this.uCTiendaRoot.Name = "uCTiendaRoot";
                this.uCTiendaRoot.Size = new System.Drawing.Size(250, 776);
                this.uCTiendaRoot.TabIndex = 0;
            }
            else
            {
                this.panelAsideMain.Controls.Add(uCTiendaRoot);
            }


        }
        #region =============================== SATATES ===============================
        public void appLoadState(bool state)
        {
            if (state)
            {
                progressBarApp.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                progressBarApp.Style = ProgressBarStyle.Blocks;
            }
        }
        #endregion
        public void hideMenuRight()
        {
            if (panelMenuRight.Size.Width > 1)
            {
                panelMenuRight.Size = new Size(0, 700);
            }
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!notCloseApp)
            {
                Application.Exit();
            }
        }
    }
}
