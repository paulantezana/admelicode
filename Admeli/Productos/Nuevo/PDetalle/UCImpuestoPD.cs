using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.Componentes;

namespace Admeli.Productos.Nuevo.PDetalle
{
    public partial class UCImpuestoPD : UserControl
    {
        public bool lisenerKeyEvents { get; internal set; }
        private FormProductoNuevo formProductoNuevo;

        public UCImpuestoPD()
        {
            InitializeComponent();
        }

        public UCImpuestoPD(FormProductoNuevo formProductoNuevo)
        {
            InitializeComponent();
            this.formProductoNuevo = formProductoNuevo;
        }


        internal void reLoad()
        {
            // throw new NotImplementedException();
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.bottomLine(panelHeader);
        }

        private void UCImpuestoPD_Paint(object sender, PaintEventArgs e)
        {
            int containerWidth = this.Size.Width;
            int itemWidth = containerWidth / 2;
            panelItem1.Size = new Size(itemWidth, 100);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            formProductoNuevo.executeGuardar();
        }

        private void btnGuardarSalir_Click(object sender, EventArgs e)
        {
            formProductoNuevo.executeGuardarSalir();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            formProductoNuevo.executeCerrar();
        }
    }
}
