using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;


namespace negocio
{
    public class FechaHoraNegocio
    {
        private AccesoDatos accesoDatos;

        public FechaHoraNegocio()
        {
            accesoDatos = new AccesoDatos(); // Clase de acceso a datos ya existente
        }

        // Método para agregar una nueva FechaHora
        public bool Agregar(FechaHora nuevaFechaHora)
        {
            try
            {
                string query = "INSERT INTO FechaHora (Fecha, Hora, Disponible) VALUES (@Fecha, @Hora, @Disponible)";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Fecha", nuevaFechaHora.Fecha);
                accesoDatos.setearParametro("@Hora", nuevaFechaHora.Hora);
                accesoDatos.setearParametro("@Disponible", nuevaFechaHora.Disponible);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al agregar FechaHora: " + ex.Message);
                return false;
            }
        }

        // Método para obtener una lista de FechaHora (búsqueda general)
        public List<FechaHora> Listar()
        {
            List<FechaHora> lista = new List<FechaHora>();
            try
            {
                string query = "SELECT Fecha, Hora, Disponible FROM FechaHora";
                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    FechaHora fechaHora = new FechaHora
                    {
                        Fecha = (DateTime)accesoDatos.Lector["Fecha"],
                        Hora = (TimeSpan)accesoDatos.Lector["Hora"],
                        Disponible = (bool)accesoDatos.Lector["Disponible"]
                    };
                    lista.Add(fechaHora);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar FechaHora: " + ex.Message);
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return lista;
        }

        // Método para modificar un registro de FechaHora existente
        public bool Modificar(FechaHora fechaHoraModificada)
        {
            try
            {
                string query = "UPDATE FechaHora SET Fecha = @Fecha, Hora = @Hora, Disponible = @Disponible WHERE Fecha = @Fecha AND Hora = @Hora";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Fecha", fechaHoraModificada.Fecha);
                accesoDatos.setearParametro("@Hora", fechaHoraModificada.Hora);
                accesoDatos.setearParametro("@Disponible", fechaHoraModificada.Disponible);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar FechaHora: " + ex.Message);
                return false;
            }
        }

        // Método para eliminar un registro de FechaHora
        public bool Eliminar(FechaHora fechaHoraAEliminar)
        {
            try
            {
                string query = "DELETE FROM FechaHora WHERE Fecha = @Fecha AND Hora = @Hora";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Fecha", fechaHoraAEliminar.Fecha);
                accesoDatos.setearParametro("@Hora", fechaHoraAEliminar.Hora);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar FechaHora: " + ex.Message);
                return false;
            }
        }
    }
}
