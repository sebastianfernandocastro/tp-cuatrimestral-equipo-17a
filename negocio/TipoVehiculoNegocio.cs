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
                string query = "INSERT INTO TipoVehiculo (Codigo, Nombre, Descripcion, Imagen) VALUES (@Codigo, @Nombre, @Descripcion, @Imagen)";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Codigo", nuevoTipoVehiculo.Codigo);
                accesoDatos.setearParametro("@Nombre", nuevoTipoVehiculo.Nombre);
                accesoDatos.setearParametro("@Descripcion", nuevoTipoVehiculo.Descripcion);
                //accesoDatos.setearParametro("@Imagen", nuevoTipoVehiculo.Imagen);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar TipoVehiculo: " + ex.Message);
                return false;
            }
        }

        public List<TipoVehiculo> Listar()
        {
            List<TipoVehiculo> lista = new List<TipoVehiculo>();
            try
            {
                string query = "SELECT IdTipoVehiculo, Codigo, Nombre, Descripcion, IdImagen FROM TipoVehiculo"; 
                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    TipoVehiculo tipoVehiculo = new TipoVehiculo
                    {
                        Id = (int)accesoDatos.Lector["IdTipoVehiculo"], 
                        Codigo = (int)accesoDatos.Lector["Codigo"],
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                        IdImagen = accesoDatos.Lector["IdImagen"].ToString()

                    };
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


        public bool Modificar(TipoVehiculo tipoVehiculoModificado)
        {
            try
            {
                string query = "UPDATE TipoVehiculo SET Nombre = @Nombre, Descripcion = @Descripcion, Imagen = @Imagen WHERE Codigo = @Codigo";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Codigo", tipoVehiculoModificado.Codigo);
                accesoDatos.setearParametro("@Nombre", tipoVehiculoModificado.Nombre);
                accesoDatos.setearParametro("@Descripcion", tipoVehiculoModificado.Descripcion);
                //accesoDatos.setearParametro("@Imagen", tipoVehiculoModificado.Imagen);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar TipoVehiculo: " + ex.Message);
                return false;
            }
        }

        public bool Eliminar(int codigo)
        {
            try
            {
                string query = "DELETE FROM TipoVehiculo WHERE Codigo = @Codigo";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Codigo", codigo);

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
