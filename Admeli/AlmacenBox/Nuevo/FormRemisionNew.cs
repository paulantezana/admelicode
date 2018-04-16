using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.AlmacenBox.buscar;
using Admeli.AlmacenBox.Nuevo.detalle;
using Admeli.Componentes;
using Entidad;
using Entidad.Configuracion;
using Entidad.Location;
using Modelo;


namespace Admeli.AlmacenBox.Nuevo
{
    public partial class FormRemisionNew : Form
    {

        // para guardar 
        GuiaRemisionGuardar guiaRemisionGuardar { get; set; }

        Dictionary<string, DetalleNotaSalida> ListDetallesGuiaRemision { get; set; }

        NotaSalidaGuardarRemision notaSalidaGuardarRemision { get; set; }

        AlmacenGuardarRemision almacenGuardarRemision { get; set; }

        List<object> listObjetos { get; set; }


        // servicios necesarios

        AlmacenModel AlmacenModel = new AlmacenModel();
        ProductoModel productoModel = new ProductoModel();
        FechaModel fechaModel = new FechaModel();
        GuiaRemisionModel guiaRemisionModel = new GuiaRemisionModel();
        LocationModel locationModel = new LocationModel();
        // objetos que cargan a un inicio


        private List<Producto> listProducto { get; set; }
        private List<Presentacion> listPresentacion { get; set; }
        private List<Almacen> listAlmacen { get; set; }
        private List<AlmacenCorrelativo> listCorrelativoA { get; set; }
        private List<DetalleNotaSalida> listDetalleNotaSalida { get; set; }
        private List<MotivoTraslado> listMotivoTraslado { get; set; }
        private List<EmpresaTransporte> listEmpresaTransporte { get; set; }

        // entidadades auxiliares

        private bool nuevo { get; set; }
        private string formato { get; set; }
        private int nroDecimales = 2;

        private string cadena { get; set; }
        private FechaSistema fechaSistema { get; set; }



        //objetos en tiempo real

        private Almacen currentAlmacen { get; set; }
        private AlmacenCorrelativo currentCorrelativoA { get; set; }
        private Producto currentProducto { get; set; }
        private Presentacion currentPresentacion { get; set; }
        private NotaSalidaR currentNotaSalida { get; set; }
        private GuiaRemision currentguiaRemision { get; set; }
        private UbicacionGeografica ubicacionGeograficaDestino { get; set; }
        private UbicacionGeografica ubicacionGeograficaOrigen { get; set; }
        public FormRemisionNew()
        {
            InitializeComponent();
            this.nuevo = true;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();


        }
        public FormRemisionNew(NotaSalidaR notaSalidaR)
        {
            InitializeComponent();
            this.nuevo = true;
            this.currentNotaSalida = notaSalidaR;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();
            btnEliminar.Enabled = false;
        }
        public FormRemisionNew(GuiaRemision currentguiaRemision)
        {
            InitializeComponent();
            this.nuevo = false;
            this.currentguiaRemision = currentguiaRemision;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();

            btnEliminar.Enabled = false;
            btnSeleccionarNotaSalida.Enabled = false;
        }

        public FormRemisionNew(NotaSalida notaSalida)
        {
            InitializeComponent();

            NotaSalidaR notaSalidaR = new NotaSalidaR();

            notaSalidaR.correlativo = notaSalida.correlativo;
            notaSalidaR.descripcion = notaSalida.descripcion;
            notaSalidaR.destino = notaSalida.destino;
            notaSalidaR.estado = notaSalida.estado;
            notaSalidaR.estadoEnvio = notaSalida.estadoEnvio;
            notaSalidaR.fecha = notaSalida.fecha;
            notaSalidaR.fechaSalida = notaSalida.fechaSalida;
            notaSalidaR.idAlmacen = notaSalida.idAlmacen;
            notaSalidaR.idNotaSalida = notaSalida.idNotaSalida;         
            notaSalidaR.idTipoDocumento = notaSalida.idTipoDocumento;
            notaSalidaR.idVenta = notaSalida.idVenta;
           notaSalidaR.motivo = notaSalida.motivo;
   
            notaSalidaR.nombreCliente = notaSalida.nombreCliente;
            notaSalidaR.numeroDocumento = notaSalida.numeroDocumento;
    
            notaSalidaR.rucDni = notaSalida.rucDni;
            notaSalidaR.serie = notaSalida.serie;

            this.currentNotaSalida = notaSalidaR;


            this.nuevo = true;
            this.currentguiaRemision = currentguiaRemision;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();

            btnEliminar.Enabled = false;
            
        }



        #region=======================metodos de apoyo
        private string darformato(object dato)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
        }
        #endregion
        #region ================================ Root Load ================================

        private void FormNotaSalidaNew_Load(object sender, EventArgs e)
        {
            if (nuevo == true)
            {
                this.reLoad();
                cargarNotaSalida();

            }
            else
            {
                this.reLoad();
                cargarGuiaRemision();
            }

        }
        private void reLoad()
        {

            cargarProductos();
            cargarMotivo();
            cargarEmpresa();

            if(nuevo)
                cargarDocCorrelativo(currentNotaSalida.idAlmacen);
           else
                cargarDocCorrelativo(currentguiaRemision.idAlmacen);
            cargarObjetos();

        }

        #endregion

        #region ============================== Load ==============================

        private async void cargarubigeoActual(int idUbicacionGeografica,bool destino=true)
        {

            cadena = "";
            try
            {
                 UbicacionGeografica CurrentUbicacionGeografica = await locationModel.ubigeoActual(idUbicacionGeografica);

                cadena = CurrentUbicacionGeografica.nombreP + " - " + CurrentUbicacionGeografica.nombreN1;
                if (CurrentUbicacionGeografica.nombreN2 != "")
                {
                    cadena += " - " + CurrentUbicacionGeografica.nombreN2;
                    if (CurrentUbicacionGeografica.nombreN3 != "")
                    {
                        cadena += " - " + CurrentUbicacionGeografica.nombreN3;

                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (destino)
            {

                txtDestino.Text = cadena;
             

            }
            else
            {
                txtOrigen.Text =cadena;

            }

        }



        private  async void cargarGuiaRemision()
        {
            //datos

            
            txtNroDocumento.Text = currentguiaRemision.numeroDocumento;
            txtMotivo.Text = currentguiaRemision.motivo;
            // serie
            txtCorrelativo.Text = currentguiaRemision.correlativo;
            txtSerie.Text = currentguiaRemision.serie;
            txtMarca.Text = currentguiaRemision.marcaYPlaca;
            txtLicConducir.Text = currentguiaRemision.licenciaDeConducir;
            txtDireccionDestino.Text = currentguiaRemision.direccionDestino;
            txtDirreccionOrigen.Text = currentguiaRemision.direccionOrigen;

            cargarubigeoActual(currentguiaRemision.destino);
            cargarubigeoActual(currentguiaRemision.origen, false);

            string estado = "";
            switch (currentguiaRemision.estado)
            {
                case 3:
                    estado = "pendiente";
                    break;
                case 2:
                    estado = "Revisado";
                    break;
                case 1:
                    estado = "Enviado";
                    break;


            }
            cbxEstado.Text = estado;
            txtObservacion.Text = currentguiaRemision.observacion;



            // cargar detalles de la nota
            listDetalleNotaSalida = await guiaRemisionModel.cargarDetallesNota(currentguiaRemision.idGuiaRemision);
            detalleNotaSalidaBindingSource.DataSource = listDetalleNotaSalida;


        }
        private  void cargarObjetos()
        {
            guiaRemisionGuardar = new GuiaRemisionGuardar();
            ListDetallesGuiaRemision = new Dictionary<string, DetalleNotaSalida>();
            notaSalidaGuardarRemision = new NotaSalidaGuardarRemision();
            almacenGuardarRemision = new AlmacenGuardarRemision();

            listObjetos = new List<object>();
        }
        private async  void cargarMotivo(){


            listMotivoTraslado = await guiaRemisionModel.Motivo();
            motivoTrasladoBindingSource.DataSource = listMotivoTraslado;
            if (!nuevo)
            {

                cbxMotivo.SelectedValue = currentguiaRemision.idMotivoTraslado;

            }

         }

        private async void cargarEmpresa()
        {
            listEmpresaTransporte = await guiaRemisionModel.ETransporte();
            empresaTransporteBindingSource.DataSource = listEmpresaTransporte;
            if (!nuevo)
            {

                cbxETransporte.SelectedValue = currentguiaRemision.idEmpresaTransporte;

            }

        }
        private  void cargarNotaSalida()
        {
            txtNroDocumento.Text = currentNotaSalida.serie + "-" + currentNotaSalida.correlativo;
            txtMotivo.Text = currentNotaSalida.motivo;

        }


      
        private async void cargarDocCorrelativo(int idAlmacen)
        {

            if (nuevo)
            {
                listCorrelativoA = await AlmacenModel.DocCorrelativoAlmacen(idAlmacen,9);
                currentCorrelativoA = listCorrelativoA[0];
                txtSerie.Text = currentCorrelativoA.serie;
                txtCorrelativo.Text = currentCorrelativoA.correlativoActual;

            }
          
        }

        private async void cargarProductos()
        {
            try
            {
                listProducto  = await productoModel.productos();
                productoBindingSource.DataSource = listProducto;


        
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void cargarFechaSistema()
        {
            try
            {
                if (!nuevo) return;
                fechaSistema = await fechaModel.fechaSistema();
              

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel30_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {

            FormBuscardetalleNotaSalida formBuscardetalleNotaSalida = null;

            if (nuevo)
            {
            formBuscardetalleNotaSalida = new FormBuscardetalleNotaSalida(currentNotaSalida);
            }
            else
            {
                NotaSalidaR notaSalidaR = new NotaSalidaR();
                notaSalidaR.idNotaSalida = currentguiaRemision.idNotaSalida;
                formBuscardetalleNotaSalida = new FormBuscardetalleNotaSalida(notaSalidaR);

            }
            
            formBuscardetalleNotaSalida.ShowDialog();
            detalleNotaSalidaBindingSource.DataSource = null;
            listDetalleNotaSalida = formBuscardetalleNotaSalida.listDestalleSubmit;
            detalleNotaSalidaBindingSource.DataSource = listDetalleNotaSalida;
            dgvDetalleSalida.Refresh();

            lbinfo.Text = "ninguno esta seleccionado";



        }

        private void btnBuscarOrigen_Click(object sender, EventArgs e)
        {

            formGeografia formGeografia = new formGeografia();
            formGeografia.ShowDialog();

            ubicacionGeograficaOrigen = formGeografia.ubicacionGeografica;
            txtOrigen.Text = formGeografia.cadena;


        }

        private void btnBuscarDestino_Click(object sender, EventArgs e)
        {
            formGeografia formGeografia = new formGeografia();
            formGeografia.ShowDialog();
            ubicacionGeograficaDestino = formGeografia.ubicacionGeografica;
            txtDestino.Text = formGeografia.cadena;
        }

        private void btnNewMotivo_Click(object sender, EventArgs e)
        {

            FormMotivoNew formMotivo = new FormMotivoNew();
            formMotivo.ShowDialog();
            this.reLoad();
        }

        private void btnNewTransporte_Click(object sender, EventArgs e)
        {
            FormTransporteNew formTransporteNew = new FormTransporteNew();
            formTransporteNew.ShowDialog();
            
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void btnSeleccionarNotaSalida_Click(object sender, EventArgs e)
        {
            FormBuscarNotaSalida formBuscarNotaSalida = new FormBuscarNotaSalida(this);
            formBuscarNotaSalida.ShowDialog();
            if (formBuscarNotaSalida.currentNotaSalida != null)
            {
                currentNotaSalida = formBuscarNotaSalida.currentNotaSalida;
           
                cargarNotaSalida();
            }
                
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleSalida.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dgvDetalleSalida.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleSalida.Rows[index].Cells[1].Value); // obteniedo el idRegistro del datagridview
            DetalleNotaSalida aux = listDetalleNotaSalida.Find(x => x.idPresentacion == idPresentacion);           
            btnEliminar.Enabled = false;
            dgvDetalleSalida.Rows.RemoveAt(index);
            listDetalleNotaSalida.Remove(aux);
            lbinfo.Text = aux.codigoProducto + " eliminado";
        }

        private void dgvDetalleSalida_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalleSalida.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dgvDetalleSalida.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleSalida.Rows[index].Cells[1].Value); // obteniedo el idRegistro del datagridview
            DetalleNotaSalida aux = listDetalleNotaSalida.Find(x => x.idPresentacion == idPresentacion);
            lbinfo.Text = aux.codigoProducto + " seleccionado";  
            btnEliminar.Enabled = true;

        }

        private async void guardar_Click(object sender, EventArgs e)
        {
            guiaRemisionGuardar.correlativo = txtCorrelativo.Text;
            guiaRemisionGuardar.destino = ubicacionGeograficaDestino!=null ? ubicacionGeograficaDestino.idUbicacionGeografica: currentguiaRemision.destino;
            guiaRemisionGuardar.direccionDestino = txtDireccionDestino.Text;
            guiaRemisionGuardar.direccionOrigen = txtDirreccionOrigen.Text;
            int estado = 0;
            switch (cbxEstado.Text)
            {
                case "Pendiente":
                    estado = 3;
                    break;
                case "Revisado":
                    estado = 2;
                    break;
                case "Enviado":
                    estado = 1;
                    break;


            }
            guiaRemisionGuardar.estado = estado;
            guiaRemisionGuardar.idEmpresaTransporte =(int) cbxETransporte.SelectedValue;
            guiaRemisionGuardar.idMotivoTraslado = (int)cbxMotivo.SelectedValue;
            guiaRemisionGuardar.idTipoDocumento = 9;// guia remision
            guiaRemisionGuardar.licenciaDeConducir = txtLicConducir.Text;
            guiaRemisionGuardar.marcaYPlaca = txtMarca.Text;
            guiaRemisionGuardar.observacion = txtObservacion.Text;
            guiaRemisionGuardar.origen = ubicacionGeograficaOrigen != null ? ubicacionGeograficaOrigen.idUbicacionGeografica : currentguiaRemision.origen;
            guiaRemisionGuardar.serie = txtSerie.Text;
            // datos para modifcar guia remision
            guiaRemisionGuardar.idGuiaRemision = currentguiaRemision.idGuiaRemision;

            guiaRemisionGuardar.fecha = currentguiaRemision.fecha.date;


            // hallamos el ETransporte

            EmpresaTransporte empresaTransporte = listEmpresaTransporte.Find(x => x.idEmpresaTransporte == (int)cbxETransporte.SelectedValue);
            MotivoTraslado motivo = listMotivoTraslado.Find(x => x.idMotivoTraslado == (int)cbxMotivo.SelectedValue);

            guiaRemisionGuardar.razonSocial = empresaTransporte.razonSocial;
            guiaRemisionGuardar.nombre = motivo.nombre;
            guiaRemisionGuardar.idNotaSalida = currentNotaSalida != null ? currentNotaSalida.idNotaSalida : currentguiaRemision.idNotaSalida;
            guiaRemisionGuardar.motivo = txtMotivo.Text;
            guiaRemisionGuardar.numeroDocumento = txtNroDocumento.Text;
            guiaRemisionGuardar.idAlmacen= currentNotaSalida != null ? currentNotaSalida.idAlmacen : currentguiaRemision.idAlmacen;
            int numert = 0;
            foreach (DetalleNotaSalida detalle in listDetalleNotaSalida)
            {
                detalle.idGuiaRemision = currentguiaRemision != null ? currentguiaRemision.idGuiaRemision : 0;
                ListDetallesGuiaRemision.Add("id" + numert, detalle);                
                numert++;
            }
            notaSalidaGuardarRemision.IdNotaSalida = currentNotaSalida != null ? currentNotaSalida.idNotaSalida : currentguiaRemision.idNotaSalida;            
            almacenGuardarRemision.IdAlmacen = currentNotaSalida != null ? currentNotaSalida.idAlmacen : currentguiaRemision.idAlmacen;           
            listObjetos.Add(guiaRemisionGuardar);
            listObjetos.Add(ListDetallesGuiaRemision);
            listObjetos.Add(notaSalidaGuardarRemision);
            listObjetos.Add(almacenGuardarRemision);



            ResponseNotaGuardar responseNotaGuardar = null;
            Response response = null;
            if (nuevo)
            {

                responseNotaGuardar = await guiaRemisionModel.guardar(listObjetos);
                guardar.Enabled = false;
            }
            else
            {
                responseNotaGuardar = new ResponseNotaGuardar();
                response = await guiaRemisionModel.modificar(listObjetos);
                responseNotaGuardar.id = response.id;
                responseNotaGuardar.msj = response.msj;
            }
             

            if(responseNotaGuardar.id > 0 )
            {


                MessageBox.Show(responseNotaGuardar.msj, "guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListDetallesGuiaRemision.Clear();
                guardar.Enabled = false;
                this.Close();
            }
            else
            {
                MessageBox.Show(responseNotaGuardar.msj, "guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
