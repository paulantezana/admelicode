using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.Ventas.Nuevo.Detalle;
using Entidad;

namespace Admeli.Ventas.Nuevo
{
    public partial class FormClienteNuevo : Form
    {
       // private UCProveedorContacto uCProveedorContacto;
        private UCClienteGeneral uCClienteGeneral;
        private UCNuevoGrupo uCNuevoGrupo; 
        internal int currentIDProveedor { get; set; }
        internal bool nuevo { get; set; }
        internal Cliente currentCliente;

        public FormClienteNuevo()
        {
            InitializeComponent();
            this.nuevo = true;
        }

        public FormClienteNuevo(Cliente currentCliente)
        {
            InitializeComponent();
            this.currentCliente = currentCliente;
            this.currentIDProveedor = currentCliente.idCliente;
            this.nuevo = false;
        }

        #region ==================== Estados =====================
        internal void loadStateApp(bool state)
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

        public  void togglePanelMain(string panelName)
        {
            limpiarControles();
            btnColor();
            switch (panelName)
            {
              
                case "general":
                    if (uCClienteGeneral == null)
                    {
                        this.uCClienteGeneral = new UCClienteGeneral(this);
                        this.panelMainNP.Controls.Add(uCClienteGeneral);
                        this.uCClienteGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.uCClienteGeneral.Location = new System.Drawing.Point(0, 0);
                        this.uCClienteGeneral.Name = "uCProveedorGeneral";
                        this.uCClienteGeneral.Size = new System.Drawing.Size(1050, 776);
                        this.uCClienteGeneral.TabIndex = 0;
                    }
                    else
                    {
                        this.panelMainNP.Controls.Add(uCClienteGeneral);
                    }
                    break;
                case "Nuevorupo":
                    if (uCNuevoGrupo == null)
                    {
                        this.uCNuevoGrupo = new UCNuevoGrupo(this);
                        this.panelMainNP.Controls.Add(uCNuevoGrupo);
                        this.uCNuevoGrupo.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.uCNuevoGrupo.Location = new System.Drawing.Point(0, 0);
                        this.uCNuevoGrupo.Name = "uCNuevoGrupo";
                        this.uCNuevoGrupo.Size = new System.Drawing.Size(800, 776);
                        this.uCNuevoGrupo.TabIndex = 0;
                    }
                    else
                    {
                        this.panelMainNP.Controls.Add(uCNuevoGrupo);
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnColor()
        {
            btnGenerales.BackColor = Color.FromArgb(230, 231, 232);
            btnContacto.BackColor = Color.FromArgb(230, 231, 232);
        }

        private void limpiarControles()
        {
            this.panelMainNP.Controls.Clear();
        }

        protected void btnGenerales_Click(object sender, EventArgs e)
        {
            togglePanelMain("general");
            btnGenerales.BackColor = Color.White;
        }

        private void btnContacto_Click(object sender, EventArgs e)
        {
            togglePanelMain("Nuevorupo");
            btnContacto.BackColor = Color.White;
        }

        private void FormProveedorNuevo_Load(object sender, EventArgs e)
        {
            togglePanelMain("general");
            btnGenerales.BackColor = Color.White;
            
        }
    }
}
