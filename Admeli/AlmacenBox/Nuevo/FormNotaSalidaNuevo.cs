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
    public partial class FormNotaSalidaNuevo : Form
    {
        private NotaSalida currentNotaSalida;
        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }


        #region ========================== Constructor ==========================

        public FormNotaSalidaNuevo()
           {
             InitializeComponent();
           }

         public FormNotaSalidaNuevo(NotaSalida currentNotaSalida)
           {
             InitializeComponent();
             this.currentNotaSalida = currentNotaSalida;
           }

        #endregion

        #region ============================== Paint And Decoration ==============================

        #endregion

        #region =============================== Root Load ===============================

        private void FormNotaSalidaNuevo_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }


            internal void reLoad(bool refreshData = true)
            {
                if (refreshData)
                {
                cargarProductos();
                cargarAlmacenes();
                cargarDatosNotaSalida();
                
                    
                    
            }
                lisenerKeyEvents = true; // Active lisener key events
            }


        #endregion

        #region ======================== KEYBOARD ========================


        #endregion

        #region ================================= Loads =================================

        
        
        private async void cargarAlmacenes() //cargar almacenes (idalmacen, nombre almacen)
        {
            
        }

        

        private async void cargarProductos() //cargar datos de productos (idProducto, codigoProducto, nombreProducto,precioCompra,ventaVarianteSinStock,)
        {

        }

        private async void cargarDatosNotaSalida()//cargar datos (serie,correlativoInicio,correlativoFin,correlativoActual,fin,estado,idAlmacenCorrelativo)
        {

        }

        #endregion

        #region ===================== Eventos Páginación =====================

        #endregion

        #region =========================== Estados ===========================

        #endregion

        #region ==================== CRUD ====================
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


    }
}

