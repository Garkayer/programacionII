using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
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
            Program.Principal.Hide();
            Peliculas Peliculas = new Peliculas();
            Peliculas.Show();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.Principal.Hide();
            Clientes Clientes = new Clientes();
            Clientes.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Principal.Hide();
            Alquiler Alquiler = new Alquiler();
            Alquiler.Show();
            
        }
    }
}
