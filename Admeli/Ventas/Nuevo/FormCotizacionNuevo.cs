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

namespace Admeli.Ventas.Nuevo
{
    public partial class FormCotizacionNuevo : Form
    {
        private Cotizacion currentCotizacion;
        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }

        #region ========================== Constructor ==========================

        public FormCotizacionNuevo()
        {
            InitializeComponent();
        }

        public FormCotizacionNuevo(Cotizacion currentCotizacion)
        {
            this.currentCotizacion = currentCotizacion;
        }

        #endregion

        #region ============================== Paint And Decoration ==============================


        #endregion

        #region =============================== Root Load ===============================

        private void FormCotizacionNuevo_Load(object sender, EventArgs e)
        {

            this.reLoad();

        }


        internal void reLoad(bool refreshData = true)
        {
            if (refreshData)
            {
                cargarCliente();
                cargarDocumento();
                cargarSerie();
                cargarProductos();
                cargarDenominacion();
                cargarFecha();
                cargarRegistros();
                
                
            }
            lisenerKeyEvents = true; // Active lisener key events
        }

        #endregion


        #region ================================= Loads =================================

                        
        private async void cargarCliente() //datos de personales (idpersonal, nombre personal)
        {

        }
        private async void cargarSerie() //datos de personales ([{"serie":","correlativoActual":}])
        {

        }

        private async void cargarDocumento()// datos de tipo de documento de identidad (iddocumento, nombre, numerodeDigitos, TipoDocumento)
        {

        }

        private async void cargarProductos() //cargar datos de productos (idProducto, codigoProducto, nombreProducto,precioCompra,ventaVarianteSinStock,)
        {

        }
        private async void cargarDenominacion()//datos de moneda (idMoneda,moneda,simbolo,porDefecto,estado,tipoCambio,fechaCreacion,idPersonal,nombres,apellidos)
        {

        }
        private async void cargarFecha() //cargar fechas (fecha)
        {

        }

        private async void cargarRegistros() //cargar todos los registros (nro registros, datos )
        {


        }

        #endregion

        #region ===================== Eventos Páginación =====================

        #endregion

        #region =========================== Estados ===========================

        #endregion

        #region ==================== CRUD ====================

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        #endregion


    }
}
