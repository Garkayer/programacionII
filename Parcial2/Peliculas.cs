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
    public partial class Peliculas : Form
    {
        conexion objConexion = new conexion();
        int posicion = 0;
        string accion = "nuevo";
        DataTable tbl = new DataTable();
        public Peliculas()
        {
            InitializeComponent();
        }

        private void Peliculas_Load(object sender, EventArgs e)
        {
            actualizarDs();
            mostrarDatos();
        }

        void actualizarDs()
        {
            tbl = objConexion.obtener_datos().Tables["peliculas"];
            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["IdPelicula"] };
        }

        void mostrarDatos()
        {
            try
            {
                lblidpelicula.Text = tbl.Rows[posicion].ItemArray[0].ToString();
                txtdescripcion.Text = tbl.Rows[posicion].ItemArray[1].ToString();
                txtsipnosis.Text = tbl.Rows[posicion].ItemArray[2].ToString();
                txtgenero.Text = tbl.Rows[posicion].ItemArray[3].ToString();
                txtduracion.Text = tbl.Rows[posicion].ItemArray[4].ToString();




                lblnregistros.Text = (posicion + 1) + " de " + tbl.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No hay Datos que mostrar", "Registros de PELICULAS",
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
                MessageBox.Show("Primer Registro...", "Registros De PELICULAS",
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
                MessageBox.Show("Ultimo Registro...", "Registros de PELICULAS",
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

            txtdescripcion.Text = "";
            txtsipnosis.Text = "";
            txtgenero.Text = "";
            txtduracion.Text = "";

        }

        void controles(Boolean valor)
        {
            grbNavegacion.Enabled = valor;
            btndelete.Enabled = valor;
            btnBuscar.Enabled = valor;
            grbPeliculas.Enabled = !valor;
        }

        private void grbEdicion_Enter(object sender, EventArgs e)
        {

        }

        private void btneliminar_Click(object sender, EventArgs e)
        {

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
                String[] valores = {

             lblidpelicula.Text,
             txtdescripcion . Text ,
             txtsipnosis .Text,
             txtgenero .Text,
             txtduracion.Text,


                };
                objConexion.mantenimiento_datos_PELICULAS(valores, accion);
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
            if (MessageBox.Show("Esta seguro de eliminar a " + txtdescripcion.Text, "Registro de PELICULAS",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                String[] valores = { lblidpelicula.Text };
                objConexion.mantenimiento_datos_PELICULAS(valores, "eliminar");

                actualizarDs();
                posicion = posicion > 0 ? posicion - 1 : 0;
                mostrarDatos();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            busqueda_peliculas frmBusqueda = new busqueda_peliculas();
            frmBusqueda.ShowDialog();

            if (frmBusqueda._IdPelicula > 0)
            {
                posicion = tbl.Rows.IndexOf(tbl.Rows.Find(frmBusqueda._IdPelicula));
                mostrarDatos();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

       