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
    public partial class busqueda_clientes : Form
    {
        conexion objConexion = new conexion();
        public int _idCliente;
        public busqueda_clientes()
        {
            InitializeComponent();
        }

        private void busqueda_clientes_Load(object sender, EventArgs e)
        {
            grdBusquedaClientes.DataSource = objConexion.obtener_datos().Tables["clientes"].DefaultView;

        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            if (grdBusquedaClientes.RowCount > 0)
            {
                _idCliente = int.Parse(grdBusquedaClientes.CurrentRow.Cells[0].Value.ToString());
                Close();
            }
            else
            {
                MessageBox.Show("NO hay datos que seleccionar", "Busqueda de Clientes",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
        void filtrar_datos(String valor)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = grdBusquedaClientes.DataSource;
            bs.Filter = "nombre like '%" + valor + "%'";
            grdBusquedaClientes.DataSource = bs;
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            filtrar_datos(txtbuscar.Text);
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grdBusquedaClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtbuscar_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void lblbuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
