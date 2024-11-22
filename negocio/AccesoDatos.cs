using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Collections;
using System.Configuration;

namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public SqlCommand Comando
            { get { return comando; } }

        public AccesoDatos()
        {
            //conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]);
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=LAVADERO_DB; integrated security=true");
            // Le digo a donde me quiero conectar
            comando = new SqlCommand(); // creo la instancia de comando
            comando.Connection = conexion; // le digo que se contecte, donde? Lo que esta guardado en conexion
                                           // que ya esta instanciado arriba
        } 
        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text; 
            comando.CommandText = consulta; //El nombre de las tablas 
        }
        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //Convierte los datos de Artiuclo para poder ser leidos por la BBDD.
        //public void ConvertirDatos(Articulo articulo)
        //{
        //    comando.Parameters.AddWithValue("@Id", articulo.Id);
        //    comando.Parameters.AddWithValue("@Codigo", articulo.Codigo);
        //    comando.Parameters.AddWithValue("@Nombre", articulo.Nombre);
        //    comando.Parameters.AddWithValue("@Descripcion", articulo.Descripcion);
        //    comando.Parameters.AddWithValue("@IdMarca", articulo.Marca.Id);
        //    comando.Parameters.AddWithValue("@IdCategoria", articulo.Categoria.Id);
        //    comando.Parameters.AddWithValue("@Precio", articulo.Precio);
        //}

        //Ejecuta el comando previamente cargado
        public void EjecutarAccion()
        {
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close(); 
            conexion.Close(); 
        } 


    }
}
