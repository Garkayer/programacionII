using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial2
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Peliculas frmpeliculas = new Peliculas();
            frmpeliculas.MdiParent = this;
            frmpeliculas.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clientes frmClientes = new Clientes();
            frmClientes.MdiParent = this;
            frmClientes.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Alquiler frmAlquiler = new Alquiler();
            frmAlquiler.MdiParent = this;
            frmAlquiler.Show();
        }
    }
}
