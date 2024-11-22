using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ServicioNegocio
    {
        private AccesoDatos accesoDatos;

        public ServicioNegocio()
        {
            accesoDatos = new AccesoDatos();
        }


        public bool Agregar(Servicio nuevoServicio, int idRubro)
        {
            try
            {
                // Insertar el servicio
                string queryServicio = " INSERT INTO Servicios (DECLARE @NewIdServicio INT; " +
                    " INSERT INTO Servicios (Nombre, Descripcion, Tiempo, Estado) " +
                    " VALUES (@Nombre, @Descripcion, @Tiempo, @Estado); " +
                    " SET @NewIdServicio = SCOPE_IDENTITY();" +
                    " INSERT INTO RubroServicio (IdRubro, IdServicio)VALUES (@IdRubro, @NewIdServicio) ";

                accesoDatos.setearConsulta(queryServicio);
                accesoDatos.setearParametro("@Nombre", nuevoServicio.Nombre);
                accesoDatos.setearParametro("@Descripcion", nuevoServicio.Descripcion);
                accesoDatos.setearParametro("@Tiempo", nuevoServicio.Tiempo);
                accesoDatos.setearParametro("@IdRubro", idRubro);
                accesoDatos.setearParametro("@Estado", nuevoServicio.Estado);
                accesoDatos.EjecutarAccion();

                // Obtener el ID del servicio recién insertado
               /* int idServicio = accesoDatos.ObtenerUltimoId();
                if (idServicio <= 0)
                {
                    throw new Exception("No se pudo obtener el ID del nuevo servicio.");
                }*/

                // Insertar la relación en RubroServicio
             /*   string queryRubroServicio = "INSERT INTO RubroServicio (IdRubro, IdServicio) VALUES (@IdRubro, @IdServicio)";
                accesoDatos.setearConsulta(queryRubroServicio);
                accesoDatos.setearParametro("@IdRubro", idRubro);
                accesoDatos.setearParametro("@IdServicio", idServicio);
                accesoDatos.EjecutarAccion();*/

                /*int filasAfectadas = accesoDatos.EjecutarAccionConResultado();
                if (filasAfectadas == 0)
                {
                    throw new Exception("No se pudo insertar en RubroServicio. Verifica los valores de IdRubro e IdServicio.");
                }*/

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar Servicio: " + ex.Message);
                return false;
            }
        }




        public List<Servicio> Listar()
        {
            List<Servicio> lista = new List<Servicio>();
            try
            {
                string query = @"SELECT s.Id, s.Nombre, s.Descripcion, s.Tiempo, s.Precio, s.Estado 
                         FROM Servicios s
                         INNER JOIN RubroServicio rs ON rs.IdServicio = s.Id
                         INNER JOIN Rubros r ON r.Id = rs.IdRubro
                         WHERE r.Estado = 1 AND s.Estado = 1";

                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Servicio nuevoServicio = new Servicio
                    {
                        Id = (int)accesoDatos.Lector["Id"],
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                        Tiempo = (decimal)accesoDatos.Lector["Tiempo"],
                        Estado = (int)accesoDatos.Lector["Estado"]
                    };
                    lista.Add(nuevoServicio);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Servicios: " + ex.Message);
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return lista;
        }


        public List<Servicio> ListarTodos()
        {
            List<Servicio> lista = new List<Servicio>();
            try
            {
                string query = @"
            SELECT s.Id, s.Nombre, s.Descripcion, s.Tiempo, s.Estado
            FROM Servicios s
            INNER JOIN RubroServicio rs ON rs.IdServicio = s.Id
            INNER JOIN Rubros r ON r.Id = rs.IdRubro
            WHERE r.Estado = 1"; 

                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Servicio nuevoServicio = new Servicio
                    {
                        Id = (int)accesoDatos.Lector["Id"],
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                        Tiempo = (decimal)accesoDatos.Lector["Tiempo"],
                        Estado = (int)accesoDatos.Lector["Estado"]
                    };
                    lista.Add(nuevoServicio);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Servicios: " + ex.Message);
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return lista;
        }

        public List<Servicio> ListarPorRubro(int idRubro)
        {
            List<Servicio> lista = new List<Servicio>();
            try
            {
                string query = " SELECT s.Id, s.Nombre, s.Descripcion, s.Tiempo FROM Servicios s " +
                    "INNER JOIN RubroServicio rs ON rs.IdServicio = s.Id " +
                    "WHERE rs.IdRubro = " + idRubro;

                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Servicio nuevoServicio = new Servicio
                    {
                        Id = (int)accesoDatos.Lector["Id"],
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                        Tiempo = (decimal)accesoDatos.Lector["Tiempo"],
                    };
                    lista.Add(nuevoServicio);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar servicios por rubro: " + ex.Message);
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return lista;
        }

        public bool ActualizarEstado(int id, int nuevoEstado)
        {
            try
            {
                string query = "UPDATE Servicios SET Estado = @Estado WHERE Id = @Id";
                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Estado", nuevoEstado);
                accesoDatos.setearParametro("@Id", id);
                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Error SQL: " + sqlEx.Message);
                throw; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                throw;
            }
        }
        public Servicio ObtenerPorId(int id)
        {
            try
            {
                string query = "SELECT Id, Nombre, Descripcion, Tiempo, Estado FROM Servicios WHERE Id = @Id";
                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", id);
                accesoDatos.EjecutarLectura();

                if (accesoDatos.Lector.Read())
                {
                    return new Servicio
                    {
                        Id = (int)accesoDatos.Lector["Id"],
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                        Tiempo = (decimal)accesoDatos.Lector["Tiempo"],
                        Estado = (int)accesoDatos.Lector["Estado"]
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }



        public bool Modificar(Servicio servicioModificado, int idRubro)
        {
            try
            {
                string query = "UPDATE Servicios SET Nombre = @Nombre, Descripcion = @Descripcion, Tiempo = @Tiempo, Estado = @Estado WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", servicioModificado.Id);
                accesoDatos.setearParametro("@Nombre", servicioModificado.Nombre);
                accesoDatos.setearParametro("@Descripcion", servicioModificado.Descripcion);
                accesoDatos.setearParametro("@Tiempo", servicioModificado.Tiempo);
                accesoDatos.setearParametro("@Estado", servicioModificado.Estado);

                accesoDatos.EjecutarAccion();

                string queryRubroServicio = "UPDATE RubroServicio SET IdRubro = @IdRubro WHERE IdServicio = @IdServicio";
                accesoDatos.setearConsulta(queryRubroServicio);
                accesoDatos.setearParametro("@IdRubro", idRubro);
                accesoDatos.setearParametro("@IdServicio", servicioModificado.Id);
                accesoDatos.EjecutarAccion();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar Servicio: " + ex.Message);
                return false;
            }
        }


        public bool Eliminar(int Id)
        {
            try
            {
                string query = "DELETE FROM Servicios WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", Id);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar Servicio: " + ex.Message);
                return false;
            }
        }
    }

}
