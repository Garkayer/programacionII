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
    public partial class busqueda_peliculas : Form
    {
        conexion objConexion = new conexion();
        public int _IdPelicula;
        public busqueda_peliculas()
        {
            InitializeComponent();
        }

        private void busqueda_peliculas_Load(object sender, EventArgs e)
        {
            grdBusquedaPeliculas.DataSource = objConexion.obtener_datos().Tables["peliculas"].DefaultView;
        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {

            if (grdBusquedaPeliculas.RowCount > 0)
            {
                _IdPelicula = int.Parse(grdBusquedaPeliculas.CurrentRow.Cells[0].Value.ToString());
                Close();
            }
            else
            {
                MessageBox.Show("NO hay datos que seleccionar", "Busqueda de PELICULAS",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

           
        }
        void filtrar_datos(String valor)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = grdBusquedaPeliculas.DataSource;
            bs.Filter = "(descripcion + sinopsis + duracion + genero) like '%" + valor + "%'";
            grdBusquedaPeliculas.DataSource = bs;
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            filtrar_datos(txtbuscar.Text);
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
