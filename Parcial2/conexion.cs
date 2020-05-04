using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Parcial2
{
    class conexion
    {
        SqlConnection miConexion = new SqlConnection();
        SqlCommand comandosSQL = new SqlCommand();
        SqlDataAdapter miAdaptadorDatos = new SqlDataAdapter();

        DataSet ds = new DataSet();

        public conexion()
        {
            String Cadena = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_sistema_peliculas.mdf;Integrated Security=True";
            miConexion.ConnectionString = Cadena;
            miConexion.Open();
        }
        public DataSet obtener_datos()
        {

            ds.Clear();
            comandosSQL.Connection = miConexion;


            comandosSQL.CommandText = "select * from alquiler";
            miAdaptadorDatos.SelectCommand = comandosSQL;
            miAdaptadorDatos.Fill(ds, "alquiler");


            comandosSQL.CommandText = "select * from clientes";
            miAdaptadorDatos.SelectCommand = comandosSQL;
            miAdaptadorDatos.Fill(ds, "clientes");


            comandosSQL.CommandText = "select * from peliculas";
            miAdaptadorDatos.SelectCommand = comandosSQL;
            miAdaptadorDatos.Fill(ds, "peliculas");



           


            return ds;
        }


        public void mantenimiento_datos_alquiler(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {
                sql = "INSERT INTO alquiler (IdClientes, IdPelicula, fechaPrestamo, fechaDevolucion, valor) VALUES (" +

                "'" + datos[1] + "'," +
                "'" + datos[2] + "'," +
                "'" + datos[3] + "'," +
                "'" + datos[4] + "'," +
                "'" + datos[5] + "'" +
                ")";
            }
            else if (accion == "modificar")
            {
                sql = "UPDATE alquiler SET" +

                " IdClientes             = '" + datos[1] + "'," +
                " IdPelicula             = '" + datos[2] + "'," +
                " fechaPrestamo          = '" + datos[3] + "'," +
                " fechaDevolucion        = '" + datos[4] + "'," +
                " valor                  = '" + datos[5] + "'" +
                 " WHERE IdAlquiler      = '" + datos[0] + "'";
            }

            else if (accion == "eliminar")

            {
                sql = " DELETE alquiler FROM alquiler WHERE IdAlquiler = '" + datos[0] + "'";
            }

            procesarSQL(sql);
        }


        public void mantenimiento_datos_PELICULAS(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {

                sql = "INSERT INTO peliculas (descripcion, sipnosis, genero, duracion) VALUES(" +

                    "'" + datos[1] + "'," +
                    "'" + datos[2] + "'," +
                    "'" + datos[3] + "'," +
                    "'" + datos[4] + "'" +
                    ")";

            }

            else if (accion == "modificar")
            {

                sql = "UPDATE peliculas SET " +
                " descripcion            = '" + datos[1] + "'," +
                " sipnosis               = '" + datos[2] + "'," +
                " genero                 = '" + datos[3] + "'," +
                " duracion               = '" + datos[4] + "'" +
                "WHERE IdPelicula         = '" + datos[0] + "'";





            }
            else if (accion == "eliminar")
            {
                sql = "DELETE peliculas FROM peliculas WHERE IdPelicula='" + datos[0] + "'";

            }
            procesarSQL(sql);
        }


        public void mantenimiento_datos_Cliente(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {

                sql = "INSERT INTO clientes (nombre, direccion, cel, dui) VALUES(" +

                    "'" + datos[1] + "'," +
                    "'" + datos[2] + "'," +
                    "'" + datos[3] + "'," +
                    "'" + datos[4] + "'" +
                    ")";

            }

            else if (accion == "modificar")
            {

                sql = "UPDATE clientes SET " +
                " nombre              = '" + datos[1] + "'," +
                " direccion           = '" + datos[2] + "'," +
                " cel           = '" + datos[3] + "'," +
                " dui                 = '" + datos[4] + "'" +
                " WHERE IdCliente     = '" + datos[0] + "'";


            }
            else if (accion == "eliminar")
            {
                sql = "DELETE clientes FROM clientes WHERE IdCliente='" + datos[0] + "'";

            }
            procesarSQL(sql);
        }

        void procesarSQL(String sql)
        {
            comandosSQL.Connection = miConexion;
            comandosSQL.CommandText = sql;
            comandosSQL.ExecuteNonQuery();
        }

    }

}
