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
using Admeli.Navigation.SubMenu;
namespace Admeli
{
    public partial class FormPrueba : Form
    {

        UCCompras uCCompras;
        public FormPrueba()
        {
            InitializeComponent();
        }

     

        private void pain()
        {

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
        }
    
    }
}
