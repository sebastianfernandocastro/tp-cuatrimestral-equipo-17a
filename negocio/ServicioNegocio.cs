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
                //string queryServicio = " INSERT INTO Servicios (DECLARE @NewIdServicio INT; " +
                string queryServicio = " DECLARE @NewIdServicio INT; " +
                    " INSERT INTO Servicios (Nombre, Descripcion, Estado) " +
                    " VALUES (@Nombre, @Descripcion,@Estado); " +
                    " SET @NewIdServicio = SCOPE_IDENTITY();" +
                    " INSERT INTO RubroServicio (IdRubro, IdServicio)VALUES (@IdRubro, @NewIdServicio) ";

                accesoDatos.setearConsulta(queryServicio);
                accesoDatos.setearParametro("@Id", nuevoServicio.Id);
                accesoDatos.setearParametro("@Nombre", nuevoServicio.Nombre);
                accesoDatos.setearParametro("@Descripcion", nuevoServicio.Descripcion);
                accesoDatos.setearParametro("@IdRubro", idRubro);
                accesoDatos.setearParametro("@Estado", nuevoServicio.Estado);
                accesoDatos.EjecutarAccion();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar Servicio: " + ex.Message);
                return false;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }

        public List<Servicio> Listar()
        {
            List<Servicio> lista = new List<Servicio>();
            try
            {
                string query = @"SELECT s.Id, s.Nombre, s.Descripcion, r.Nombre nombreRubro ,s.Precio, s.Estado 
                         FROM Servicios s
                         INNER JOIN RubroServicio rs ON rs.IdServicio = s.Id
                         INNER JOIN Rubros r ON r.Id = rs.IdRubro
                         WHERE r.Estado = 1 AND s.Estado = 1";
                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();
                while (accesoDatos.Lector.Read())
                {
                    Servicio nuevoServicio = new Servicio();

                    nuevoServicio.Id = (int)accesoDatos.Lector["Id"];
                    nuevoServicio.Nombre = accesoDatos.Lector["Nombre"].ToString();
                    nuevoServicio.Descripcion = accesoDatos.Lector["Descripcion"].ToString();
                    nuevoServicio.Estado = (int)accesoDatos.Lector["Estado"];

                    nuevoServicio.rubro = new Rubro();
                    nuevoServicio.rubro.Descripcion = accesoDatos.Lector["nombreRubro"].ToString();

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
            SELECT s.Id, s.Nombre, s.Descripcion , r.Nombre nombreRubro, s.Estado
            FROM Servicios s
            INNER JOIN RubroServicio rs ON rs.IdServicio = s.Id
            INNER JOIN Rubros r ON r.Id = rs.IdRubro
            WHERE r.Estado = 1";


                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();
                while (accesoDatos.Lector.Read())
                {
                    Servicio nuevoServicio = new Servicio();

                    nuevoServicio.Id = (int)accesoDatos.Lector["Id"];
                    nuevoServicio.Nombre = accesoDatos.Lector["Nombre"].ToString();
                    nuevoServicio.Descripcion = accesoDatos.Lector["Descripcion"].ToString();
                    nuevoServicio.Estado = (int)accesoDatos.Lector["Estado"];

                    nuevoServicio.rubro = new Rubro();
                    nuevoServicio.rubro.Descripcion = accesoDatos.Lector["nombreRubro"].ToString();
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
                string query = " SELECT s.Id, s.Nombre, s.Descripcion FROM Servicios s " +
                    "INNER JOIN RubroServicio rs ON rs.IdServicio = s.Id " +
                    "WHERE s.Estado = 1 and rs.IdRubro = " + idRubro;

                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Servicio nuevoServicio = new Servicio
                    {
                        Id = (int)accesoDatos.Lector["Id"],
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
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
                string query = "SELECT Id, Nombre, Descripcion, Estado FROM Servicios WHERE Id = @Id";
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
                string query = "UPDATE Servicios SET Nombre = @Nombre, Descripcion = @Descripcion,  Estado = @Estado WHERE Id = @Id " +
                    "UPDATE RubroServicio SET IdRubro = @IdRubro WHERE IdServicio = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", servicioModificado.Id);
                accesoDatos.setearParametro("@Nombre", servicioModificado.Nombre);
                accesoDatos.setearParametro("@Descripcion", servicioModificado.Descripcion);
                accesoDatos.setearParametro("@Estado", servicioModificado.Estado);
                accesoDatos.setearParametro("@IdRubro", idRubro);

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
