using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> listar()
        {
            List<Imagen> imagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, UrlImagen FROM IMAGENES");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Imagen imagen = new Imagen();
                    imagen.Id = (int)datos.Lector["Id"];
                    imagen.UrlImagen = (string)datos.Lector["UrlImagen"];
                    imagenes.Add(imagen);   
                }


                return imagenes;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public bool cargar(Imagen imagenNueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("INSERT INTO IMAGENES ( UrlImagen) VALUES ( @ImagenUrl)");

                datos.Comando.Parameters.AddWithValue("@UrlImagen", imagenNueva.UrlImagen);
                datos.EjecutarAccion();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                // Asegurar que la conexión se cierre correctamente
                datos.CerrarConexion();
            }
        }
    }
}
