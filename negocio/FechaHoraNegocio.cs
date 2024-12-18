﻿using System;
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
            accesoDatos = new AccesoDatos();
        }


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

                Console.WriteLine("Error al agregar FechaHora: " + ex.Message);
                return false;
            }
        }
        private int ObtenerIdFechaHora(DateTime fecha)
        {
            int id = 0;
            try
            {
                string query = "SELECT IdFechaHora FROM FechaHora WHERE Fecha = @Fecha";
                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Fecha", fecha);

                accesoDatos.EjecutarLectura();
                if (accesoDatos.Lector.Read())
                {
                    id = (int)accesoDatos.Lector["IdFechaHora"];
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


        public List<FechaHora> Listar()
        {
            List<FechaHora> lista = new List<FechaHora>();
            try
            {
                string query = "SELECT Id,  Hora, Disponible FROM FechaHora";
                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    FechaHora fechaHora = new FechaHora
                    {
                        Id = (int)accesoDatos.Lector["Id"],
                        //Fecha = accesoDatos.Lector["Fecha"] != DBNull.Value ? (DateTime)accesoDatos.Lector["Fecha"] : DateTime.MinValue,
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

        public List<FechaHora> ListarHoraDisponible(DateTime fecha, string idTurno = "")
        {
            List<FechaHora> lista = new List<FechaHora>();
            try
            {
                string query = "select * from (select f.Id ,f.Hora,case when (select count(*) from Turnos t " +
                    "where cast(t.FechaHora as time) = f.Hora and cast(t.FechaHora as date) = cast('" + fecha + "' as date) and IdEstado <> 4 ";
                if (!String.IsNullOrEmpty(idTurno)) query += " and t.Id <> " + idTurno + " ";
                query += ") >= 3 then 0 else 1 end as Disponible from FechaHora f ) as Resultado where Disponible = 1";

                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    FechaHora fechaHora = new FechaHora();

                    fechaHora.Id = (int)accesoDatos.Lector["Id"];
                    //Fecha = accesoDatos.Lector["Fecha"] != DBNull.Value ? (DateTime)accesoDatos.Lector["Fecha"] : DateTime.MinValue,
                    fechaHora.Hora = (TimeSpan)accesoDatos.Lector["Hora"];
                    //fechaHora.Disponible = (bool)accesoDatos.Lector["Disponible"];

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
        public int ObtenerIdFechaHoraxHora(TimeSpan hora)
        {
            int id = 0;
            try
            {
                string query = "SELECT Id FROM FechaHora WHERE hORA = '" + hora + "'";
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
