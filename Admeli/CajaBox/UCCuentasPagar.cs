using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;
using Entidad;
using Admeli.Componentes;

namespace Admeli.CajaBox
{
    public partial class UCCuentasPagar : UserControl
    {
        public bool lisenerKeyEvents { get; set; }
        private FormPrincipal formPrincipal;        
        private Paginacion paginacion;

        #region =================================== CONSTRUCTOR ===================================
        public UCCuentasPagar()
        {
            InitializeComponent();
            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));

        }

        public UCCuentasPagar(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
            this.reLoad();



        }
        #endregion

        #region ==================================== ROOT LOAD ====================================
        internal void reLoad(bool refreshData = true)
        {
            if (refreshData)
            {
                //cargarRegistros();
            }
            lisenerKeyEvents = true; // Active lisener key events
        }

        #endregion

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }
    }
}
