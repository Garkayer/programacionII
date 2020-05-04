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
    public partial class Alquiler : Form
    {
        conexion objConexion = new conexion();
        int posicion = 0;
        string accion = "nuevo";
        DataTable tbl = new DataTable();
        public Alquiler()
        {
            InitializeComponent();
        }

        private void Alquiler_Load(object sender, EventArgs e)
        {
            actualizarDs();
            mostrarDatos();

            CboClientes.AutoCompleteMode = AutoCompleteMode.Suggest;
            CboClientes.AutoCompleteSource = AutoCompleteSource.ListItems;

            CboPelicula.AutoCompleteMode = AutoCompleteMode.Suggest;
            CboPelicula.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        void actualizarDs()
        {
            tbl = objConexion.obtener_datos().Tables["alquiler"];
            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["IdAlquiler"] };

            CboClientes.DataSource = objConexion.obtener_datos().Tables["clientes"];
            CboClientes.DisplayMember = "nombre";
            CboClientes.ValueMember = "clientes.IdCliente";

            CboPelicula.DataSource = objConexion.obtener_datos().Tables["peliculas"];
            CboPelicula.DisplayMember = "descripcion";
            CboPelicula.ValueMember = "peliculas.IdPelicula";


        }

        void mostrarDatos()
        {
            try
            {
                CboClientes.SelectedValue = tbl.Rows[posicion].ItemArray[1].ToString();

                CboPelicula.SelectedValue = tbl.Rows[posicion].ItemArray[2].ToString();

                lblidalquiler.Text = tbl.Rows[posicion].ItemArray[0].ToString();
                txtfechaprestamo.Text = tbl.Rows[posicion].ItemArray[3].ToString();
                TxtFechaDevolucion.Text = tbl.Rows[posicion].ItemArray[4].ToString();
                TxtValor.Text = tbl.Rows[posicion].ItemArray[5].ToString();

                lblnregistros.Text = (posicion + 1) + " de " + tbl.Rows.Count;
            }

            catch (Exception ex)
            {
                MessageBox.Show("No hay Datos que mostrar", "Registros de Alquiler",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiar_cajas();
            }
        }

        private void btnprimero_Click(object sender, EventArgs e)
        {
            posicion = 0;
            mostrarDatos();
        }

        private void btnanterior_Click(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion--;
                mostrarDatos();
            }
            else
            {
                MessageBox.Show("Primer Registro...", "Registros De Alquiler",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnsiguiente_Click(object sender, EventArgs e)
        {
            if (posicion < tbl.Rows.Count - 1)
            {
                posicion++;
                mostrarDatos();
            }
            else
            {
                MessageBox.Show("Ultimo Registro...", "Registros de Alquiler",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnultimo_Click(object sender, EventArgs e)
        {
            posicion = tbl.Rows.Count - 1;
            mostrarDatos();
        }

        void limpiar_cajas()
        {
            txtfechaprestamo.Text = "";
            TxtFechaDevolucion.Text = "";
            TxtValor.Text = "";
        }

        void controles(Boolean valor)
        {
            grbNavegacion.Enabled = valor;
            btndelete.Enabled = valor;
            btnBuscar.Enabled = valor;
            grbAlquiler.Enabled = !valor;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (btnNuevo.Text == "Nuevo")
            {//boton de nuevo
                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                accion = "nuevo";

                limpiar_cajas();
                controles(false);
            }
            else
            { //boton de guardar
                string[] valores = {

             lblidalquiler.Text,
             CboClientes.SelectedValue.ToString(),
             CboPelicula.SelectedValue.ToString(),
             txtfechaprestamo.Text,
             TxtFechaDevolucion.Text,
             TxtValor.Text
                };


                objConexion.mantenimiento_datos_alquiler(valores, accion);
                actualizarDs();
                posicion = tbl.Rows.Count - 1;
                mostrarDatos();

                controles(true);

                btnNuevo.Text = "Nuevo";
                btnModificar.Text = "Editar";
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (btnModificar.Text == "Editar")
            {//boton de modificar
                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                accion = "modificar";

                controles(false);

                btnNuevo.Text = "Guardar";
                btnModificar.Text = "Cancelar";

            }
            else
            { //boton de cancelar
                controles(true);
                mostrarDatos();

                btnNuevo.Text = "Nuevo";
                btnModificar.Text = "Editar";
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de eliminar a " + txtfechaprestamo.Text, "Registro de Alquiler",
              MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                String[] valores = { lblidalquiler.Text };
                objConexion.mantenimiento_datos_alquiler(valores, "eliminar");

                actualizarDs();
                posicion = posicion > 0 ? posicion - 1 : 0;
                mostrarDatos();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            busqueda_alquiler buscarAlquiler = new busqueda_alquiler();
            buscarAlquiler.ShowDialog();

            if (buscarAlquiler._IdAlquilacion > 0)
            {
                posicion = tbl.Rows.IndexOf(tbl.Rows.Find(buscarAlquiler._IdAlquilacion));
                mostrarDatos();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            busqueda_clientes buscarcliente = new busqueda_clientes();
            buscarcliente.ShowDialog();

            if (buscarcliente._idCliente > 0)
            {
                CboClientes.SelectedValue = buscarcliente._idCliente;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            busqueda_peliculas frmBusqueda = new busqueda_peliculas();
            frmBusqueda.ShowDialog();

            if (frmBusqueda._IdPelicula > 0)
            {
                posicion = tbl.Rows.IndexOf(tbl.Rows.Find(frmBusqueda._IdPelicula));
                mostrarDatos();
            }
        }

    }
}

