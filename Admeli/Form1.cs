using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli
{
    public partial class Form1 : Form
    {


        private Bunifu.Framework.UI.BunifuMetroTextbox txtprueba;
        public Form1()
        {
            InitializeComponent();

            // 
            // bunifuMetroTextbox1
            // 
            txtprueba = new Bunifu.Framework.UI.BunifuMetroTextbox();

            txtprueba.BorderColorFocused = System.Drawing.Color.Blue;
            txtprueba.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            txtprueba.BorderColorMouseHover = System.Drawing.Color.Blue;
            txtprueba.BorderThickness = 3;
            txtprueba.Cursor = System.Windows.Forms.Cursors.IBeam;
            txtprueba.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            txtprueba.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            txtprueba.isPassword = false;
            txtprueba.Location = new System.Drawing.Point(53, 156);
            txtprueba.Margin = new System.Windows.Forms.Padding(4);
            txtprueba.Name = "bunifuMetroTextbox1";
            txtprueba.Size = new System.Drawing.Size(370, 64);
            txtprueba.TabIndex = 5;
            txtprueba.Text = "s";
            txtprueba.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            this.Controls.Add(this.txtprueba);



        }

        private void bunifuCustomTextbox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            textBox1.Focus();
        }
    }
}
