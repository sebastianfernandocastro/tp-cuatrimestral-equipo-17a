using dominio;
using System;
using System.Collections.Generic;
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


        public bool Agregar(Servicio nuevoServicio)
        {
            try
            {
                string query = "INSERT INTO Servicios (Id, Nombre, Descripcion, Tiempo) VALUES (@Id, @Nombre, @Descripcion, @Tiempo)";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", nuevoServicio.Id);
                accesoDatos.setearParametro("@Nombre", nuevoServicio.Nombre);
                accesoDatos.setearParametro("@Descripcion", nuevoServicio.Descripcion);

                accesoDatos.EjecutarAccion();
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
                string query = "SELECT Id, Nombre, Descripcion, Tiempo FROM Servicios";
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




        public bool Modificar(Servicio ServicioModificado)
        {
            try
            {
                string query = "UPDATE Servicios SET Nombre = @Nombre, Descripcion = @Descripcion, Tiempo = @Tiempo WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Codigo", ServicioModificado.Id);
                accesoDatos.setearParametro("@Nombre", ServicioModificado.Nombre);
                accesoDatos.setearParametro("@Descripcion", ServicioModificado.Descripcion);
                accesoDatos.setearParametro("@Tiempo", ServicioModificado.Tiempo);


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
