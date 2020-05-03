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
            String cadena = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_sistema_peliculas.mdf;Integrated Security=True";
            miConexion.ConnectionString = cadena;
            miConexion.Open();
        }

        public DataSet obtener_datos()
        {
            ds.Clear();
            comandosSQL.Connection = miConexion;

            comandosSQL.CommandText = "select * from clientes";
            miAdaptadorDatos.SelectCommand = comandosSQL;
            miAdaptadorDatos.Fill(ds, "clientes");

            comandosSQL.CommandText = "select * from alquiler";
            miAdaptadorDatos.SelectCommand = comandosSQL;
            miAdaptadorDatos.Fill(ds, "alquiler");

            comandosSQL.CommandText = "select * from peliculas";
            miAdaptadorDatos.SelectCommand = comandosSQL;
            miAdaptadorDatos.Fill(ds, "peliculas");

            return ds;
        }

        public void mantenimiento_datos(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {
                sql = "INSERT INTO clientes (nombre,direccion,telefono,dui) VALUES(" +
                    "'" + datos[1] + "'," +
                    "'" + datos[2] + "'," +
                    "'" + datos[3] + "'," +
                    "'" + datos[4] + "'," +
            

                    ")";
            }
            else if (accion == "modificar")
            {
                sql = "UPDATE clientes SET " +
                    
                    "nombre         = '" + datos[1] + "'," +
                    "direccion      = '" + datos[2] + "'," +
                    "telefono       = '" + datos[3] + "'," +
                    "dui            = '" + datos[4] + "'," +
                    
                    "WHERE idCliente = '" + datos[0] + "'";
            }
            else if (accion == "eliminar")
            {
                sql = "DELETE clientes FROM clientes WHERE idCliente='" + datos[0] + "'";
            }
            procesarSQL(sql);
        }

        public void mantenimiento_datos_productos(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {
                sql = "INSERT INTO peliculas (idPelicula,descripcion,sipnosis,genero,duracion) VALUES(" +
                    "'" + datos[1] + "'," +
                    "'" + datos[2] + "'," +
                    "'" + datos[3] + "'," +
                    "'" + datos[4] + "'," +
                    "'" + datos[5] + "'" +
                    ")";
            }
            else if (accion == "modificar")
            {
                sql = "UPDATE peliculas SET " +
                    "idPelicula      = '" + datos[1] + "'," +
                    "descripcion           = '" + datos[2] + "'," +
                    "sipsnosis          = '" + datos[3] + "'," +
                    "genero            = '" + datos[4] + "'," +
                    "duracion     = '" + datos[5] + "'" +
                    "WHERE idPelicula = '" + datos[0] + "'";
            }
            else if (accion == "eliminar")
            {
                sql = "DELETE peliculas FROM productos WHERE idPelicula='" + datos[0] + "'";
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
