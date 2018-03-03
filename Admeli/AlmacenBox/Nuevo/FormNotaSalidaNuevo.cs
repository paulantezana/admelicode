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

namespace Admeli.AlmacenBox.Nuevo
{
    public partial class FormNotaSalidaNuevo : Form
    {
        private NotaSalida currentNotaSalida;

        public FormNotaSalidaNuevo()
        {
            InitializeComponent();
        }

        public FormNotaSalidaNuevo(NotaSalida currentNotaSalida)
        {
            InitializeComponent();
            this.currentNotaSalida = currentNotaSalida;
        }
    }
}
