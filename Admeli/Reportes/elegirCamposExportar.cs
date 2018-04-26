using Admeli.Componentes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli.Reportes
{
    public partial class elegirCamposExportar : Form
    {
        private DataGridView dgvProductos;
        public elegirCamposExportar()
        {
            InitializeComponent();
        }
        public elegirCamposExportar(DataGridView dgv)
        {
            InitializeComponent();
            this.dgvProductos = dgv;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //crear dgv para exportar

            try
            {
                DataGridView dgvExportar = new DataGridView();
                DataGridViewColumn columna = new DataGridViewColumn();
                if (chkId.Checked == true) { columna.HeaderText = "Id"; dgvExportar.Columns.Add(columna); }
                if (chkCodigo.Checked == true) { columna.HeaderText = "Codigo"; dgvExportar.Columns.Add(columna); }
                if (chkNombre.Checked == true) { columna.HeaderText = "Nombre"; dgvExportar.Columns.Add(columna); }
                if (chkSucursal.Checked == true) { columna.HeaderText = "Sucursal"; dgvExportar.Columns.Add(columna); }
                if (chkAlmacen.Checked == true) { columna.HeaderText = "Alamcen"; dgvExportar.Columns.Add(columna); }
                if (chkPrecioCompra.Checked == true) { columna.HeaderText = "P. Compra"; dgvExportar.Columns.Add(columna); }
                if (chkPrecioVenta.Checked == true) { columna.HeaderText = "P. Venta"; dgvExportar.Columns.Add(columna); }
                if (chkStock.Checked == true) { columna.HeaderText = "Stock"; dgvExportar.Columns.Add(columna); }
                if (chkValor.Checked == true) { columna.HeaderText = "Valor"; dgvExportar.Columns.Add(columna); }
                ExternalFiles.ExportarDataGridViewExcel(dgvProductos);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
