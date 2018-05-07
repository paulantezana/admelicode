using Admeli.Componentes;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidad;
using Entidad.Configuracion;

namespace Admeli
{
    public partial class FormConfigInicial : Form
    {
        public FormLogin formLogin { get; set; }
        private SucursalModel sucursalModel = new SucursalModel();
        private ConfigModel configModel = new ConfigModel();
        private List<Almacen> listAlmacenes { get; set; }
        private List<PuntoDeVenta> listpuntos { get; set; }
        private int nLoads { get; set; }
        public FormConfigInicial()
        {
            InitializeComponent();
            nLoads = 0;
            listAlmacenes = new List<Almacen>();
            listpuntos = new List<PuntoDeVenta>();
        }

        public FormConfigInicial(FormLogin formLogin)
        {
            this.formLogin = formLogin;
            InitializeComponent();
            nLoads = 0;
            listAlmacenes = new List<Almacen>();
            listpuntos = new List<PuntoDeVenta>();

        }

        private  async void FormConfigInicial_Shown(object sender, EventArgs e)
        {

            foreach(Sucursal S  in ConfigModel.listSucursales )
            {

                await configModel.loadAlmacenes(PersonalModel.personal.idPersonal, S.idSucursal);


                await configModel.loadPuntoDeVenta(PersonalModel.personal.idPersonal, S.idSucursal);
                List<Almacen> list = ConfigModel.alamacenes;
                foreach(Almacen a in list)
                {

                    a.idSucursal = S.idSucursal;

                }
                List<PuntoDeVenta> list2 = ConfigModel.puntosDeVenta;
                foreach (PuntoDeVenta a in list2)
                {

                    a.idSucursal = S.idSucursal;

                }

                listAlmacenes.AddRange(list);
                listpuntos.AddRange(list2);
            }       
              
            almacenBindingSource.DataSource = listAlmacenes;
            cbxAlmacenes.SelectedIndex = -1;
          


            puntoDeVentaBindingSource.DataSource = listpuntos;
            cbxPuntosVenta.SelectedIndex =-1;
            cbxAlmacenes.SelectedIndex = 0;

        }

        private async void btnContinuar_Click(object sender, EventArgs e)
        {

            try
            {
                btnContinuar.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                if (validarCampos())
                {
                    

                    // cargar componentes desde el webservice
                    await cargarComponente();

                    // esperar a que cargen todo los web service
                    await Task.Run(() =>
                    {
                        while (true)
                        {
                            Thread.Sleep(50);
                            if (nLoads >= 1) // IMPORTANTE IMPORTANTE el numero tiene que ser igual al numero de web service que se este llamando
                            {
                                break;
                            }
                        }
                    });

                    if (cbxAlmacenes.SelectedIndex == -1)
                    {
                        errorProvider1.SetError(cbxAlmacenes, "No se seleccionó nungun almacen");
                        cbxAlmacenes.Focus();
                        return;
                    }
                    errorProvider1.Clear();

                    if (cbxPuntosVenta.SelectedIndex == -1)
                    {
                        errorProvider1.SetError(cbxPuntosVenta, "No se seleccionó nungun puntos de venta");
                        cbxPuntosVenta.Focus();
                        return;
                    }
                    errorProvider1.Clear();

                    // Estableciendo el almacen y punto de venta al personal asignado
                    ConfigModel.currentIdAlmacen = Convert.ToInt32(cbxAlmacenes.SelectedValue.ToString());
                    ConfigModel.currentPuntoVenta = Convert.ToInt32(cbxPuntosVenta.SelectedValue.ToString());

                    // Mostrando el formulario principal
                    this.Hide();
                    FormPrincipal formPrincipal = new FormPrincipal(this.formLogin);
                    formPrincipal.ShowDialog();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "configuracion Inicial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                progressbar.Value = 0;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnContinuar.Enabled = true;
            }


        }



        private async Task cargarComponente()
        {           
           loadState("asignacion del personal");
            await configModel.loadAsignacionPersonales(PersonalModel.personal.idPersonal, ConfigModel.sucursal.idSucursal);
            this.nLoads++;
            
          
            // await configModel.loadCierreIngresoEgreso(1, ConfigModel.cajaSesion.idCajaSesion); // Falta Buscar de donde viene el primer parametro
        }
        private void loadState(string message)
        {
            progressbar.Value += 10;
            lblProgress.Text = String.Format("Cargando {0}", message);
        }

        private bool validarCampos()
        {
            if (cbxAlmacenes.Text.Trim() == "")
            {
                errorProvider1.SetError(cbxAlmacenes, "Campo obligatorio");
               
                cbxAlmacenes.Focus();
                return false;
            }
            errorProvider1.Clear();

            if (cbxAlmacenes.Text.Trim() == "")
            {
                errorProvider1.SetError(cbxAlmacenes, "Campo obligatorio");
               
                cbxAlmacenes.Focus();
                return false;
            }
            errorProvider1.Clear();

            return true;
        }










        #region =============================== Paint ===============================
        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panel2);
            drawShape.lineBorder(panel3);
        }
        #endregion

        private void btnCLose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormConfigInicial_Load(object sender, EventArgs e)
        {

        }

        private void cbxAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlmacenes.SelectedIndex == -1) return;
            Almacen almacen = listAlmacenes.Find(X=>X.idAlmacen== (int)cbxAlmacenes.SelectedValue);
            ConfigModel.sucursal=ConfigModel.listSucursales.Find(X => X.idSucursal == almacen.idSucursal);
            cbxPuntosVenta.DataSource= listpuntos.Where(X => X.idSucursal == ConfigModel.sucursal.idSucursal).ToList();
        }
    }
}
