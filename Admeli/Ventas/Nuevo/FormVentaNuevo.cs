using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidad;

namespace Admeli.Ventas.Nuevo
{
    public partial class FormVentaNuevo : Form
    {
        private Venta currentVenta;

        public FormVentaNuevo()
        {
            InitializeComponent();
        }

        public FormVentaNuevo(Venta currentVenta)
        {
            this.currentVenta = currentVenta;
        }

        private void FormVentaNuevo_Load(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
