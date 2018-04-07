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
using Admeli.Navigation;
namespace Admeli
{
    public partial class FormPrueba : Form
    {

        UCCompras uCCompras;

        private UCTiendaRoot uCTiendaRoot;
        public FormPrueba()
        {
            InitializeComponent();
        }

     

        private void pain()
        {

            this.panel4.Controls.Clear();
            if (this.uCCompras == null)
            {
                this.uCCompras = new UCCompras();
                this.panel4.Controls.Add(uCCompras);
                this.uCCompras.Dock = System.Windows.Forms.DockStyle.Fill;
                this.uCCompras.Location = new System.Drawing.Point(0, 0);
                this.uCCompras.Name = "uCCompras";
                this.uCCompras.Size = new System.Drawing.Size(250, 776);
                this.uCCompras.TabIndex = 0;
            }
            else
            {
                this.panel4.Controls.Add(uCCompras);
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


    }
}
