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
    public partial class FormCotizacionNuevo : Form
    {
        private Cotizacion currentCotizacion;

        public FormCotizacionNuevo()
        {
            InitializeComponent();
        }

        public FormCotizacionNuevo(Cotizacion currentCotizacion)
        {
            this.currentCotizacion = currentCotizacion;
        }
    }
}
