using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli.Navigation.SubMenu
{
    public partial class UCReporteNav : UserControl
    {
        private UCTiendaRoot uCTiendaRoot;
        private FormPrincipal formPrincipal;

        public UCReporteNav()
        {
            InitializeComponent();
        }

        public UCReporteNav(FormPrincipal formPrincipal, UCTiendaRoot uCTiendaRoot)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;
            this.uCTiendaRoot = uCTiendaRoot;
        }
    }
}
