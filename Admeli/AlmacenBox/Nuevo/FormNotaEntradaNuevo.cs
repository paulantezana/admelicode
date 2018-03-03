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
    public partial class FormNotaEntradaNuevo : Form
    {
        private NotaEntrada currentNotaEntrada;

        public FormNotaEntradaNuevo()
        {
            InitializeComponent();
        }

        public FormNotaEntradaNuevo(NotaEntrada currentNotaEntrada)
        {
            InitializeComponent();
            this.currentNotaEntrada = currentNotaEntrada;
        }
    }
}
