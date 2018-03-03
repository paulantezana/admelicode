using Admeli.Componentes;
using Entidad;
using Entidad.Location;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli.Configuracion.Nuevo
{
    public partial class FormPersonalNuevo : Form
    {
        private SucursalModel sucursalModel = new SucursalModel();
        private LocationModel locationModel = new LocationModel();
        private DepartamentoModel departamentoModel = new DepartamentoModel();
        private DocumentoIdentificacionModel documentoIdentificacion = new DocumentoIdentificacionModel();
        private PersonalModel personalModel = new PersonalModel();

        private List<LabelUbicacion> labelUbicaciones { get; set; }
        private UbicacionGeografica ubicacionGeografica { get; set; }

        private int currentIDAlmacen { get; set; }
        private bool nuevo { get; set; }
        private Personal currentPersonal { get; set; }
        private PersonalAux currentPersonalAux { get; set; }
        private int currentIDSucursal { get; set; }
        private int currentIDPersonal { get; set; }
        private bool isValid { get; set; }

        public FormPersonalNuevo()
        {
            InitializeComponent();
            this.nuevo = true;
        }

        public FormPersonalNuevo(Personal currentPersonal)
        {
            InitializeComponent();
            this.currentPersonal = currentPersonal;
            this.currentIDPersonal = currentPersonal.idPersonal;
            this.nuevo = false;
        }

        #region ============================ PAINT ============================
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.bottomLine(panel2);
        }

        private void FormPersonalNuevo_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelLevelPais, 157, 157, 157);
            drawShape.lineBorder(panelLevel1, 157, 157, 157);
            drawShape.lineBorder(panelLevel2, 157, 157, 157);
            drawShape.lineBorder(panelLevel3, 157, 157, 157);
            drawShape.lineBorder(panel2, 157, 157, 157);
            drawShape.lineBorder(panel3, 157, 157, 157);
            drawShape.topLine(panelFooter);
        } 
        #endregion

        #region =============================== Root Load ===============================
        private void FormPersonalNuevo_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        private async void reLoad()
        {
            await cargarPaises();
            cargarGenero();
            crearNivelesPais();
            cargarDocIdentificacion();
            // cargarSucursales();
            cargarDatosModificar();
        }
        #endregion

        #region ============================ Loads ============================
        private void cargarDatosModificar()
        {
            if (nuevo) return;
            textApellidoUsuario.Text = currentPersonal.apellidos;
            textCelular.Text = currentPersonal.celular;
            textDirecionUsuario.Text = currentPersonal.direccion;
            textEmail.Text = currentPersonal.email;
            chkActivo.Checked = Convert.ToBoolean(currentPersonal.estado);
            dtpFechaNacimiento.Value = (currentPersonal.fechaNacimiento == null) ? DateTime.Now : currentPersonal.fechaNacimiento.date;
            cbxTipoDocumento.SelectedValue = currentPersonal.idDocumento;
            textNombreUsuario.Text = currentPersonal.nombres;
            textNumeroDocumento.Text = currentPersonal.numeroDocumento;
            cbxSexo.Text = currentPersonal.sexo;
            textTelefono.Text = currentPersonal.telefono;
        }

        private async void cargarSucursales()
        {
            sucursalBindingSource.DataSource = await sucursalModel.sucursales();
        }

        private void cargarGenero()
        {
            DataTable table = new DataTable();
            table.Columns.Add("idGenero", typeof(string));
            table.Columns.Add("genero", typeof(string));

            table.Rows.Add("1", "Masculino");
            table.Rows.Add("0", "Femenino");

            cbxSexo.DataSource = table;
            cbxSexo.DisplayMember = "genero";
            cbxSexo.ValueMember = "idGenero";
        }

        private async void cargarDocIdentificacion()
        {
            documentoIdentificacionBindingSource.DataSource = await documentoIdentificacion.docIdentificacionNatural();
        }

        private async Task cargarPaises()
        {
            // cargando los paises
            paisBindingSource.DataSource = await locationModel.paises();

            // cargando la ubicacion geografica por defecto
            if (nuevo)
            {
                ubicacionGeografica = await locationModel.ubigeoActual(ConfigModel.sucursal.idUbicacionGeografica);
            }
            else
            {
                ubicacionGeografica = await locationModel.ubigeoActual(currentPersonal.idUbicacionGeografica);
            }
            cbxPaises.SelectedValue = ubicacionGeografica.idPais;
        }

        private async void cargarDeprtametnoSucursal()
        {

            try
            {
                loadStateApp(true);
                // Cargando desde el web service
                int idPersonal = (nuevo) ? 0 : 1;
                int index = dataGridView1.CurrentRow.Index;
                currentIDSucursal = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);

                // Cargando las categorias desde el webservice
                List<Departamento> departamentos = await departamentoModel.listarAreasPorSucursal(currentIDSucursal, idPersonal);

                // Formando los listas
                treeView1.Nodes.Clear(); // Limpiando los nodos
                if (departamentos == null) return;
                foreach (Departamento departamento in departamentos)
                {
                    treeView1.Nodes.Add(departamento.idDepartamento.ToString(), departamento.nombre);
                    if (departamento.idPadre > 0)
                    {
                        // Cargando subcategorias
                        int nodeIndex = treeView1.Nodes.IndexOfKey(departamento.idPadre.ToString());
                        treeView1.Nodes[nodeIndex].ImageIndex = 1;
                        treeView1.Nodes[nodeIndex].Nodes.Add(departamento.idDepartamento.ToString(), departamento.nombre);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Ocurrio un error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadStateApp(false);
            }
        }
        #endregion

        #region ==================== Estados =====================
        private void loadStateApp(bool state)
        {
            if (state)
            {
                progressBarApp.Style = ProgressBarStyle.Marquee;
                Cursor.Current = Cursors.WaitCursor;
            }
            else
            {
                progressBarApp.Style = ProgressBarStyle.Blocks;
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion

        #region ================== Formando los niveles de cada pais ==================
        private async void crearNivelesPais()
        {
            try
            {
                loadStateApp(true);
                labelUbicaciones = await locationModel.labelUbicacion(Convert.ToInt32(cbxPaises.SelectedValue));
                ocultarNiveles(); // Ocultando todo los niveles

                // Mostrando los niveles uno por uno
                if (labelUbicaciones.Count >= 1)
                {
                    lblNivel1.Text = labelUbicaciones[0].denominacion;
                    panelLevel1.Visible = true;
                }

                if (labelUbicaciones.Count >= 2)
                {
                    lblNivel2.Text = labelUbicaciones[1].denominacion;
                    panelLevel2.Visible = true;
                }

                if (labelUbicaciones.Count >= 3)
                {
                    panelLevel3.Visible = true;
                    lblNivel3.Text = labelUbicaciones[2].denominacion;
                }

                // Cargar el primer nivel de la localizacion
                cargarNivel1();

            }
            catch (Exception)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                loadStateApp(false);
            }
        }

        private void ocultarNiveles()
        {
            panelLevel1.Visible = false;
            panelLevel2.Visible = false;
            panelLevel3.Visible = false;

            cbxNivel1.SelectedIndex = -1;
            cbxNivel2.SelectedIndex = -1;
            cbxNivel3.SelectedIndex = -1;
        }

        private async void cargarNivel1()
        {
            try
            {
                // No cargar directo al comobobox esto causara que el evento SelectedIndexChange de forma automatica
                if (labelUbicaciones.Count < 1) return;
                loadStateApp(true);
                nivel1BindingSource.DataSource = await locationModel.nivel1(Convert.ToInt32(cbxPaises.SelectedValue));
                // seleccionando el valor por defecto
                if (ubicacionGeografica.idNivel1 > 0)
                {
                    cbxNivel1.SelectedValue = ubicacionGeografica.idNivel1;
                }
                else
                {
                    cbxNivel1.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadStateApp(false);
                desactivarNivelDesde(2);
            }
        }

        private async void cargarNivel2()
        {
            try
            {
                if (labelUbicaciones.Count < 2) return;
                loadStateApp(true);
                nivel2BindingSource.DataSource = await locationModel.nivel2(Convert.ToInt32(cbxNivel1.SelectedValue));
                if (ubicacionGeografica.idNivel2 > 0)
                {
                    cbxNivel2.SelectedValue = ubicacionGeografica.idNivel2;
                }
                else
                {
                    cbxNivel2.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                desactivarNivelDesde(3);
                loadStateApp(false);
            }
        }

        private async void cargarNivel3()
        {
            try
            {
                if (labelUbicaciones.Count < 3) return;
                loadStateApp(true);
                nivel3BindingSource.DataSource = await locationModel.nivel3(Convert.ToInt32(cbxNivel2.SelectedValue));
                if (ubicacionGeografica.idNivel3 > 0)
                {
                    cbxNivel3.SelectedValue = ubicacionGeografica.idNivel3;
                }
                else
                {
                    cbxNivel3.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadStateApp(false);
                desactivarNivelDesde(4);
            }
        }

        private void desactivarNivelDesde(int n)
        {
            cbxNivel1.Enabled = true;
            cbxNivel2.Enabled = true;
            cbxNivel3.Enabled = true;

            if (n < 2) cbxNivel1.Enabled = false;
            if (n < 3) cbxNivel2.Enabled = false;
            if (n < 4) cbxNivel3.Enabled = false;
        }
        #endregion

        #region ====================== Niveles localizacion eventos =====================
        private void cbxPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            crearNivelesPais();
        }

        private void cbxNivel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarNivel2();
        }

        private void cbxNivel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarNivel3();
        }
        #endregion



        #region ========================== SAVE AND UPDATE ===========================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            guardarPersonal();
        }

        private async void guardarPersonal()
        {
            if (!validarCampos()) return;
            try
            {
                btnAceptar.Enabled = false;

                // Procediendo con el guardado
                crearObjetoSucursal();

                // Obteniendo de la ubicacion geografica del sucursal
                Response res = await locationModel.guardarUbigeo(ubicacionGeografica);
                currentPersonalAux.idUbicacionGeografica = res.id;

                if (nuevo)
                {
                    Response response = await personalModel.guardar(currentPersonalAux);
                    MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Response response = await personalModel.modificar(currentPersonalAux);
                    MessageBox.Show(response.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                btnAceptar.Enabled = true;
            }
        }

        private void crearObjetoSucursal()
        {
            currentPersonalAux = new PersonalAux();
            if (nuevo)
            {
                currentPersonalAux.usuario = " ";
                currentPersonalAux.password = "";
            }
            else
            {
                currentPersonalAux.idPersonal = currentIDPersonal; // Llenar el id categoria cuando este en esdo modificar
            }
            currentPersonalAux.apellidos = textApellidoUsuario.Text;
            currentPersonalAux.celular = textCelular.Text;
            currentPersonalAux.direccion = textDirecionUsuario.Text;
            currentPersonalAux.email = textEmail.Text;
            currentPersonalAux.estado = Convert.ToInt32(chkActivo.Checked);
            currentPersonalAux.fechaNacimiento = String.Format("{0}-{1}-{2}", dtpFechaNacimiento.Value.Year, dtpFechaNacimiento.Value.Month, dtpFechaNacimiento.Value.Day);
            currentPersonalAux.idDocumento = Convert.ToInt32(cbxTipoDocumento.SelectedValue);
            currentPersonalAux.nombres = textNombreUsuario.Text;
            currentPersonalAux.numeroDocumento = textNumeroDocumento.Text;
            currentPersonalAux.sexo = cbxSexo.Text;
            currentPersonalAux.telefono = textTelefono.Text;

            // Ubicacion geografica
            ubicacionGeografica.idPais = (cbxPaises.SelectedIndex == -1) ? ubicacionGeografica.idPais : Convert.ToInt32(cbxPaises.SelectedValue);
            ubicacionGeografica.idNivel1 = (cbxNivel1.SelectedIndex == -1) ? ubicacionGeografica.idNivel1 : Convert.ToInt32(cbxNivel1.SelectedValue);
            ubicacionGeografica.idNivel2 = (cbxNivel2.SelectedIndex == -1) ? ubicacionGeografica.idNivel2 : Convert.ToInt32(cbxNivel2.SelectedValue);
            ubicacionGeografica.idNivel3 = (cbxNivel3.SelectedIndex == -1) ? ubicacionGeografica.idNivel3 : Convert.ToInt32(cbxNivel3.SelectedValue);
         }

         private bool validarCampos()
         {
             this.isValid = true;        // IS Valid ============ TRUE

             switch (labelUbicaciones.Count)
             {
                 case 1:
                     if (cbxNivel1.SelectedIndex == -1)
                     {
                         errorProvider1.SetError(cbxNivel1, "No se seleccionó ningún elemento");
                         cbxNivel1.Focus();
                         this.isValid = false;
                     }
                     errorProvider1.Clear();
                     break;
                 case 2:
                     if (cbxNivel2.SelectedIndex == -1)
                     {
                         errorProvider1.SetError(cbxNivel2, "No se seleccionó ningún elemento");
                         cbxNivel2.Focus();
                         this.isValid = false;
                     }
                     errorProvider1.Clear();
                     break;
                 case 3:
                     if (cbxNivel3.SelectedIndex == -1)
                     {
                         errorProvider1.SetError(cbxNivel3, "No se seleccionó ningún elemento");
                         cbxNivel3.Focus();
                         this.isValid = false;
                     }
                     errorProvider1.Clear();
                     break;
                 default:
                     break;
             }

             if (textNombreUsuario.Text.Trim() == "")
             {
                 errorProvider1.SetError(textNombreUsuario, "Campo obligatorio");
                 Validator.textboxValidateColor(textNombreUsuario, 0);
                 this.isValid = false;
             }

             if (textApellidoUsuario.Text.Trim() == "")
             {
                 errorProvider1.SetError(textApellidoUsuario, "Campo obligatorio");
                 Validator.textboxValidateColor(textApellidoUsuario, 0);
                 this.isValid = false;
             }

             if (textNumeroDocumento.Text.Trim() == "")
             {
                 errorProvider1.SetError(textNumeroDocumento, "Campo obligatorio");
                 Validator.textboxValidateColor(textNumeroDocumento, 0);
                 this.isValid = false;
             }

             if (textEmail.Text.Trim() == "")
             {
                 errorProvider1.SetError(textEmail, "Campo obligatorio");
                 Validator.textboxValidateColor(textEmail, 0);
                 this.isValid = false;
             }

             if (textDirecionUsuario.Text.Trim() == "")
             {
                 errorProvider1.SetError(textDirecionUsuario, "Campo obligatorio");
                 Validator.textboxValidateColor(textDirecionUsuario, 0);
                 this.isValid = false;
             }

             if (dtpFechaNacimiento.Value.Year == DateTime.Now.Year)
             {
                 errorProvider1.SetError(dtpFechaNacimiento, "Fecha de nacimiento invalido");
                 this.isValid = false;
             }

             if (!this.isValid)
             {
                 return false;
             }
             return true;
         }

         #endregion

         private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
         {
            // cargarDeprtametnoSucursal();
         }

         #region ============================== Validacion tiempo real ==============================
         private void textNombreUsuario_Validated(object sender, EventArgs e)
         {
             if (textNombreUsuario.Text.Trim() == "")
             {
                 errorProvider1.SetError(textNombreUsuario, "Campo obligatorio");
                 Validator.textboxValidateColor(textNombreUsuario, 0);
                 return;
             }
             errorProvider1.Clear();
             Validator.textboxValidateColor(textNombreUsuario, 1);
         }

         private void textApellidoUsuario_Validated(object sender, EventArgs e)
         {
             if (textApellidoUsuario.Text.Trim() == "")
             {
                 errorProvider1.SetError(textApellidoUsuario, "Campo obligatorio");
                 Validator.textboxValidateColor(textApellidoUsuario, 0);
                 return;
             }
             errorProvider1.Clear();
             Validator.textboxValidateColor(textApellidoUsuario, 1);
         }

         private void textNumeroDocumento_Validated(object sender, EventArgs e)
         {
             if (textNumeroDocumento.Text.Trim() == "")
             {
                 errorProvider1.SetError(textNumeroDocumento, "Campo obligatorio");
                 Validator.textboxValidateColor(textNumeroDocumento, 0);
                 return;
             }
             errorProvider1.Clear();
             Validator.textboxValidateColor(textNumeroDocumento, 1);
         }

         private void textEmail_Validated(object sender, EventArgs e)
         {
             if (textEmail.Text.Trim() == "")
             {
                 errorProvider1.SetError(textEmail, "Campo obligatorio");
                 Validator.textboxValidateColor(textEmail, 0);
                 return;
             }
             errorProvider1.Clear();
             Validator.textboxValidateColor(textEmail, 1);

             if (!Validator.IsValidEmail(textEmail.Text))
             {
                 errorProvider1.SetError(textEmail, "Email invalido");
                 Validator.textboxValidateColor(textEmail, 0);
                 return;
             }
             errorProvider1.Clear();
             Validator.textboxValidateColor(textEmail, 1);
         }

         private void textTelefono_Validated(object sender, EventArgs e)
         {
             ////////////////////////////////////////
         }

         private void textCelular_Validated(object sender, EventArgs e)
         {
             /*  if (textCelular.Text.Trim() == "")
               {
                   errorProvider1.SetError(textCelular, "Campo obligatorio");
                   Validator.textboxValidateColor(textCelular, false);
                   return;
               }
               errorProvider1.Clear();
               Validator.textboxValidateColor(textCelular, true);*/
        }

        private void textTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void textCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void textDirecionUsuario_Validated(object sender, EventArgs e)
        {
            if (textDirecionUsuario.Text.Trim() == "")
            {
                errorProvider1.SetError(textDirecionUsuario, "Campo obligatorio");
                Validator.textboxValidateColor(textDirecionUsuario, 0);
                return;
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textDirecionUsuario, 1);
        }
        #endregion

        private void FormPersonalNuevo_Shown(object sender, EventArgs e)
        {
            textNombreUsuario.Focus();
        }
    }

    public class PersonalAux
    {
        public string apellidos { get; set; }
        public string celular { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public int estado { get; set; }
        public string fechaNacimiento { get; set; }
        public int idDocumento { get; set; }
        public int idPersonal { get; set; }
        public int idUbicacionGeografica { get; set; }
        public string nombres { get; set; }
        public string numeroDocumento { get; set; }
        public string password { get; set; }
        public string sexo { get; set; }
        public string telefono { get; set; }
        public string usuario { get; set; }
    }
}


