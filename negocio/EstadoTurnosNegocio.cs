using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class EstadoTurnosNegocio
    {
        public List<EstadoTurnos> Listar()
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            List<EstadoTurnos> lista = new List<EstadoTurnos>();
            try
            {
                accesoDatos.setearConsulta("SELECT Id,Descripcion FROM EstadoTurnos");
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    EstadoTurnos est = new EstadoTurnos();

                    est.Id = (int)accesoDatos.Lector["Id"];
                    est.descripcion = accesoDatos.Lector["Descripcion"].ToString();

                    lista.Add(est);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Estado de Turnos: " + ex.Message);
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return lista;
        }

    }
}
