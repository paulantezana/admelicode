using Entidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli.CajaBox.Nuevo
{
    public partial class FormDetallePagoNuevo : Form
    {
        private bool nuevo { get; set; }
        private Pago currentPago = new Pago();

        public FormDetallePagoNuevo()
        {
            InitializeComponent();
        }

        public FormDetallePagoNuevo(Pago currentPago)
        {
            InitializeComponent();
            this.nuevo = true;
            this.currentPago = currentPago;
            this.reLoad();
        }

        private void reLoad()
        {
            //this.cargarMonedas();
            //this.cargarMediosPago();
        }



    }
}
