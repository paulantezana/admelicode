using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli.Configuracion.Modificar
{
    class vineta
    {
        public Label label { get; set; }
        public string nombre { get; set; }
        public int usado { get; set; }
        public int posicionX { get; set; }
        public int posicionY { get; set; }
        public bool mover { get; set; }
        public int inicialX { get; set; }
        public int inicialY { get; set; }
        public bool redimensionar { get; set; }
        public int w { get; set; }
        public int h { get; set; }

        public vineta()
        {
            this.usado = 0;// 0  no utilizado, 2 usadada en comprobante ,-1 no usadado esta abajo arriba,1usado arriba
            label = new Label();
            label.AutoSize = false;
            mover = false;

        }
    }
}
