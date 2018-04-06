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
    public partial class FormHome : Form
    {
        private FormLogin formLogin;

        public FormHome()
        {
            InitializeComponent();
        }

        public FormHome(FormLogin formLogin)
        {
            InitializeComponent();
            this.formLogin = formLogin;
        }
    }
}
