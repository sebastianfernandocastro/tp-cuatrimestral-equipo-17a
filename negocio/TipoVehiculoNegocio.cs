using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class TipoVehiculoNegocio
    {
        private AccesoDatos accesoDatos;

        public TipoVehiculoNegocio()
        {
            accesoDatos = new AccesoDatos();
        }


        public bool Agregar(TipoVehiculo nuevoTipoVehiculo)
        {
            try
            {
                string query = "INSERT INTO TipoVehiculo (Nombre, Descripcion, IdImagen,Estado) VALUES ( @Nombre, @Descripcion, @IdImagen,1)";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Nombre", nuevoTipoVehiculo.Nombre);
                accesoDatos.setearParametro("@Descripcion", nuevoTipoVehiculo.Descripcion);
                accesoDatos.setearParametro("@IdImagen", nuevoTipoVehiculo.imagen.Id);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar TipoVehiculo: " + ex.Message);
                return false;
            }
        }

        public List<TipoVehiculo> Listar(int inactivo = 0)
        {
            List<TipoVehiculo> lista = new List<TipoVehiculo>();
            try
            {
                string query = "SELECT tv.Id, tv.Nombre, tv.Descripcion, tv.IdImagen, tv.Estado, img.UrlImagen FROM TipoVehiculo tv " +
                    "inner join Imagenes img on tv.IdImagen = img.Id   ";

                if (inactivo == 1) query += " where tv.estado = 0 ";
                else query += " where tv.estado = 1 ";

                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    TipoVehiculo tipoVehiculo = new TipoVehiculo();

                    tipoVehiculo.Id = (int)accesoDatos.Lector["Id"];
                    tipoVehiculo.Nombre = accesoDatos.Lector["Nombre"].ToString();
                    tipoVehiculo.Descripcion = accesoDatos.Lector["Descripcion"].ToString();
                    tipoVehiculo.imagen = new Imagen();
                    tipoVehiculo.imagen.Id = accesoDatos.Lector["IdImagen"] != DBNull.Value ? (int)accesoDatos.Lector["IdImagen"] : 0;
                    tipoVehiculo.imagen.UrlImagen = accesoDatos.Lector["UrlImagen"] != DBNull.Value ? (string)accesoDatos.Lector["UrlImagen"] : "";

                    lista.Add(tipoVehiculo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar TipoVehiculo: " + ex.Message);
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return lista;
        }

        public TipoVehiculo ObtenerPorId(int id)
        {
            try
            {
                string query = "SELECT Id, Nombre, Descripcion, IdImagen FROM TipoVehiculo where id = " + id;

                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    TipoVehiculo tipoVehiculo = new TipoVehiculo();

                    tipoVehiculo.Id = (int)accesoDatos.Lector["Id"];
                    tipoVehiculo.Nombre = accesoDatos.Lector["Nombre"].ToString();
                    tipoVehiculo.Descripcion = accesoDatos.Lector["Descripcion"].ToString();
                    tipoVehiculo.imagen = new Imagen();
                    tipoVehiculo.imagen.Id = accesoDatos.Lector["IdImagen"] != DBNull.Value ? (int)accesoDatos.Lector["IdImagen"] : 0;


                    return tipoVehiculo;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar TipoVehiculo: " + ex.Message);
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return null;
        }


        public bool Modificar(TipoVehiculo tipoVehiculoModificado)
        {
            try
            {
                string query = "UPDATE TipoVehiculo SET Nombre = @Nombre, Descripcion = @Descripcion, IdImagen = @Imagen, Estado = @Estado WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", tipoVehiculoModificado.Id);
                accesoDatos.setearParametro("@Nombre", tipoVehiculoModificado.Nombre);
                accesoDatos.setearParametro("@Descripcion", tipoVehiculoModificado.Descripcion);
                accesoDatos.setearParametro("@Estado", tipoVehiculoModificado.Estado);
                accesoDatos.setearParametro("@Imagen", tipoVehiculoModificado.imagen.Id);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar TipoVehiculo: " + ex.Message);
                return false;
            }
        }

        public bool Eliminar(int id)
        {
            try
            {
                string query = "update TipoVehiculo set estado = 0 WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", id);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar TipoVehiculo: " + ex.Message);
                return false;
            }
        }
    }
}
