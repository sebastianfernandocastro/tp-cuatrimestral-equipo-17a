using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PrecioNegocio
    {
        private AccesoDatos accesoDatos;

            public PrecioNegocio()
        {
            accesoDatos = new AccesoDatos();
        }

        public List<Precio> ListarPrecios()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Precio> lista = new List<Precio>();

            try
            {
                datos.setearConsulta(@"
            SELECT 
                P.Id,
                P.IdTipoVehiculo,
                TV.Nombre AS TipoVehiculoNombre,
                P.IdRubro,
                R.Nombre AS RubroNombre,
                P.IdServicio,
                S.Nombre AS ServicioNombre,
                P.Precio AS PrecioValor
            FROM Precios AS P
            INNER JOIN TipoVehiculo AS TV ON P.IdTipoVehiculo = TV.Id
            INNER JOIN Rubros AS R ON P.IdRubro = R.Id
            INNER JOIN Servicios AS S ON P.IdServicio = S.Id
            where s.Estado = 1 and R.Estado = 1 and Tv.Estado = 1            
;");

                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Precio precio = new Precio
                    {
                        Id = (int)datos.Lector["Id"],
                        IdTipoVehiculo = (int)datos.Lector["IdTipoVehiculo"],
                        TipoVehiculoNombre = datos.Lector["TipoVehiculoNombre"].ToString(),
                        IdRubro = (int)datos.Lector["IdRubro"],
                        RubroNombre = datos.Lector["RubroNombre"].ToString(),
                        IdServicio = (int)datos.Lector["IdServicio"],
                        ServicioNombre = datos.Lector["ServicioNombre"].ToString(),
                        PrecioValor = (decimal)datos.Lector["PrecioValor"]
                    };

                    lista.Add(precio);
                }

                return lista;
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


        public void AgregarPrecio(Precio nuevoPrecio)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Precios (IdTipoVehiculo, IdRubro, IdServicio, Precio) " +
                                     "VALUES (@IdTipoVehiculo, @IdRubro, @IdServicio, @Precio)");

                datos.setearParametro("@IdTipoVehiculo", nuevoPrecio.IdTipoVehiculo);
                datos.setearParametro("@IdRubro", nuevoPrecio.IdRubro);
                datos.setearParametro("@IdServicio", nuevoPrecio.IdServicio);
                datos.setearParametro("@Precio", nuevoPrecio.PrecioValor);

                datos.EjecutarAccion();
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


        public void ModificarPrecio(Precio precio)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Precios SET IdTipoVehiculo = @IdTipoVehiculo, IdRubro = @IdRubro, " +
                                     "IdServicio = @IdServicio, Precio = @Precio WHERE Id = @Id");

                datos.setearParametro("@Id", precio.Id);
                datos.setearParametro("@IdTipoVehiculo", precio.IdTipoVehiculo);
                datos.setearParametro("@IdRubro", precio.IdRubro);
                datos.setearParametro("@IdServicio", precio.IdServicio);
                datos.setearParametro("@Precio", precio.PrecioValor);

                datos.EjecutarAccion();
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


        public void EliminarPrecio(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM Precios WHERE Id = @Id");
                datos.setearParametro("@Id", id);

                datos.EjecutarAccion();
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

        public int ObtenerPrecioxCampos(int idRubro, int idServicio, int idVehiculo)
        {
            int id = 0;
            try
            {
                string query = "SELECT Id from Precios where IdRubro = " + idRubro + " and IdTipoVehiculo= " + idVehiculo + " and IdServicio= " + idServicio;
                accesoDatos.setearConsulta(query);

                accesoDatos.EjecutarLectura();
                if (accesoDatos.Lector.Read())
                {
                    id = (int)accesoDatos.Lector["Id"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener IdFechaHora: {ex.Message}");
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return id;
        }
    }
}
