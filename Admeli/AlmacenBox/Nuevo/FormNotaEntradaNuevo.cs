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
using Modelo;
using Admeli.Componentes;
using Admeli.AlmacenBox.Nuevo;

namespace Admeli.AlmacenBox.Nuevo
{
    public partial class FormNotaEntradaNuevo : Form
    {
        private NotaEntrada currentNotaEntrada;
        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }


        #region ========================== Constructor ==========================

        public FormNotaEntradaNuevo()
        {
            InitializeComponent();
        }

        public FormNotaEntradaNuevo(NotaEntrada currentNotaEntrada)
        {
            InitializeComponent();
            this.currentNotaEntrada = currentNotaEntrada;
        }

        #endregion

        #region ============================== Paint And Decoration ==============================


        #endregion

        #region =============================== Root Load ===============================

        private void FormNotaEntradaNuevo_Load(object sender, EventArgs e)
        {

            this.reLoad();

        }


        internal void reLoad(bool refreshData = true)
        {
            if (refreshData)
            {
                
                cargarAlmacenes();
                cargarProductos();
                cargarDatosNotaEntrada();
                
            }
            lisenerKeyEvents = true; // Active lisener key events
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        #region ======================== KEYBOARD ========================


        #endregion

        #region ================================= Loads =================================

            

        private async void cargarAlmacenes() //cargar datos de almacenes (idalmacen, nombre almacen)
        {

        }

        private async void cargarProductos() //cargar datos de productos (idProducto, codigoProducto, nombreProducto,precioCompra,ventaVarianteSinStock,)
        {

        }

        private async void cargarDatosNotaEntrada()//cargar datos (serie,correlativoInicio,correlativoFin,correlativoActual,fin,estado,idAlmacenCorrelativo)
        {

        }




        #endregion

        #region ===================== Eventos Páginación =====================

        #endregion

        #region =========================== Estados ===========================

        #endregion

        #region ==================== CRUD ====================

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }
}
