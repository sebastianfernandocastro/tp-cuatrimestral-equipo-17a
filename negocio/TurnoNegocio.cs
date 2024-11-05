using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class TurnoNegocio
    {
        private AccesoDatos accesoDatos;

        public TurnoNegocio()
        {
            accesoDatos = new AccesoDatos();
        }

        public bool Agregar(Turno nuevoTurno)
        {
            try
            {
                string query = "INSERT INTO Turnos (Id, Usuario, Vehiculo, Rubro, Servicio, Aclaracion, FechaHora, Estado) VALUES (@Id, @Usuario, @Vehiculo, @Rubro, @Servicio, @Aclaracion, @FechaHora, @Estado)";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", nuevoTurno.Id);
                accesoDatos.setearParametro("@Usuario", nuevoTurno.Usuario); // Asumiendo que Usuario es un objeto y se maneja adecuadamente
                accesoDatos.setearParametro("@Vehiculo", nuevoTurno.Vehiculo); // Similar para TipoVehiculo
                accesoDatos.setearParametro("@Rubro", nuevoTurno.Rubro); // Similar para Rubro
                accesoDatos.setearParametro("@Servicio", nuevoTurno.Servicio); // Similar para Servicio
                accesoDatos.setearParametro("@Aclaracion", nuevoTurno.Aclaracion);
                accesoDatos.setearParametro("@FechaHora", nuevoTurno.FechaHora); // Se asume que se puede convertir correctamente
                accesoDatos.setearParametro("@Estado", nuevoTurno.Estado);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar Turno: " + ex.Message);
                return false;
            }
        }

        public List<Turno> Listar()
        {
            List<Turno> lista = new List<Turno>();
            try
            {
                string query = "SELECT Id, Usuario, Vehiculo, Rubro, Servicio, Aclaracion, FechaHora, Estado FROM Turnos";
                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Turno nuevoTurno = new Turno
                    {
                        Id = (int)accesoDatos.Lector["Id"],
                        Usuario = (Usuario)accesoDatos.Lector["Usuario"], // Asegúrate de que Usuario se maneje correctamente
                        Vehiculo = (TipoVehiculo)accesoDatos.Lector["Vehiculo"], // Similar para TipoVehiculo
                        Rubro = (Rubro)accesoDatos.Lector["Rubro"], // Similar para Rubro
                        Servicio = (Servicio)accesoDatos.Lector["Servicio"], // Similar para Servicio
                        Aclaracion = accesoDatos.Lector["Aclaracion"].ToString(),
                        FechaHora = (FechaHora)accesoDatos.Lector["FechaHora"], // Asegúrate de que FechaHora se maneje correctamente
                        Estado = accesoDatos.Lector["Estado"].ToString()
                    };
                    lista.Add(nuevoTurno);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Turnos: " + ex.Message);
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return lista;
        }

        public bool Modificar(Turno turnoModificado)
        {
            try
            {
                string query = "UPDATE Turnos SET Usuario = @Usuario, Vehiculo = @Vehiculo, Rubro = @Rubro, Servicio = @Servicio, Aclaracion = @Aclaracion, FechaHora = @FechaHora, Estado = @Estado WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", turnoModificado.Id);
                accesoDatos.setearParametro("@Usuario", turnoModificado.Usuario); // Manejo de Usuario
                accesoDatos.setearParametro("@Vehiculo", turnoModificado.Vehiculo); // Manejo de TipoVehiculo
                accesoDatos.setearParametro("@Rubro", turnoModificado.Rubro); // Manejo de Rubro
                accesoDatos.setearParametro("@Servicio", turnoModificado.Servicio); // Manejo de Servicio
                accesoDatos.setearParametro("@Aclaracion", turnoModificado.Aclaracion);
                accesoDatos.setearParametro("@FechaHora", turnoModificado.FechaHora); // Manejo de FechaHora
                accesoDatos.setearParametro("@Estado", turnoModificado.Estado);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar Turno: " + ex.Message);
                return false;
            }
        }

        public bool Eliminar(int Id)
        {
            try
            {
                string query = "DELETE FROM Turnos WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", Id);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar Turno: " + ex.Message);
                return false;
            }
        }
    }
}
