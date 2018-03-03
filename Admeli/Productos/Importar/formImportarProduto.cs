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

namespace Admeli.Productos.Importar
{
    public partial class FormImportarProduto : Form
    {
        public FormImportarProduto()
        {
            InitializeComponent();
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView.DataSource = ExternalFiles.ImporExcel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Importar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
