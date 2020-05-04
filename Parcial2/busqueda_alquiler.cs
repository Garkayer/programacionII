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
    public partial class busqueda_alquiler : Form
    {
        conexion objConexion = new conexion();
        public int _IdAlquilacion;
        public busqueda_alquiler()
        {
            InitializeComponent();
        }

        private void busqueda_alquiler_Load(object sender, EventArgs e)
        {
            grdBusquedaAlquiler.DataSource = objConexion.obtener_datos().Tables["alquiler_clientes_peliculas"].DefaultView;
        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            if (grdBusquedaAlquiler.RowCount > 0)
            {
                _IdAlquilacion = int.Parse(grdBusquedaAlquiler.CurrentRow.Cells["IdAlquiler"].Value.ToString());
                Close();
            }
            else
            {
                MessageBox.Show("No hay datos que seleccionar", "Busqueda de Alquiler",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            

            
        }
        void filtrar_datos(String valor)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = grdBusquedaAlquiler.DataSource;
            bs.Filter = "nombre like '%" + valor + "%' or descripcion like '%" + valor + "%' or sinopsis like '%" + valor + "%'";
            grdBusquedaAlquiler.DataSource = bs;
        }
        private void btncancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblbuscar_Click(object sender, EventArgs e)
        {

        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            filtrar_datos(txtbuscar.Text);
        }
    }
}
