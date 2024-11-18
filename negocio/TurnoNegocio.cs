using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                string query = @"
        INSERT INTO Turnos (IdCliente, IdTipoVehiculo, IdRubro, IdServicio, IdFechaHora, Estado, Aclaracion)
        VALUES (@IdCliente, @IdTipoVehiculo, @IdRubro, @IdServicio, @IdFechaHora, @Estado, @Aclaracion)";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@IdCliente", nuevoTurno.Usuario.Id);
                accesoDatos.setearParametro("@IdTipoVehiculo", nuevoTurno.Vehiculo.Codigo);
                accesoDatos.setearParametro("@IdRubro", nuevoTurno.Rubro.Id);
                accesoDatos.setearParametro("@IdServicio", nuevoTurno.Servicio.Id);
                accesoDatos.setearParametro("@IdFechaHora", nuevoTurno.FechaHora.Id);
                accesoDatos.setearParametro("@Estado", nuevoTurno.Estado.Id);
                accesoDatos.setearParametro("@Aclaracion", nuevoTurno.Aclaracion ?? "");

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar Turno: {ex.Message}");
                return false;
            }
        }


        public List<Turno> Listar()
        {
            List<Turno> lista = new List<Turno>();
            try
            {
                string query = @"
                SELECT T.Id, T.IdCliente, T.IdTipoVehiculo, T.IdRubro, T.IdServicio, T.IdFechaHora, T.IdEstado, et.descripcion descripEstado , T.Aclaracion,
                       U.Nombre AS ClienteNombre,
                       TV.Nombre AS VehiculoNombre,
                       R.Nombre AS RubroNombre,
                       S.Nombre AS ServicioNombre,
                       FH.Fecha
                FROM Turnos T
                INNER JOIN Usuarios U ON T.IdCliente = U.Id
                INNER JOIN TipoVehiculo TV ON T.IdTipoVehiculo = TV.Id
                INNER JOIN Rubros R ON T.IdRubro = R.Id
                INNER JOIN Servicios S ON T.IdServicio = S.Id
                INNER JOIN FechaHora FH ON T.IdFechaHora = FH.Id
                inner join EstadoTurnos et on et.Id = t.IdEstado";

                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Turno turno = new Turno
                    {
                        Id = (int)accesoDatos.Lector["Id"],
                        Usuario = new Usuario
                        {
                            Id = (int)accesoDatos.Lector["IdCliente"],
                            Nombre = accesoDatos.Lector["ClienteNombre"].ToString()
                        },
                        Vehiculo = new TipoVehiculo
                        {
                            Codigo = (int)accesoDatos.Lector["IdTipoVehiculo"],
                            Nombre = accesoDatos.Lector["VehiculoNombre"].ToString()
                        },
                        Rubro = new Rubro
                        {
                            Id = (int)accesoDatos.Lector["IdRubro"],
                            Nombre = accesoDatos.Lector["RubroNombre"].ToString()
                        },
                        Servicio = new Servicio
                        {
                            Id = (int)accesoDatos.Lector["IdServicio"],
                            Nombre = accesoDatos.Lector["ServicioNombre"].ToString()
                        },
                        FechaHora = new FechaHora
                        {
                            Id = (int)accesoDatos.Lector["IdFechaHora"],
                            Fecha = (DateTime)accesoDatos.Lector["Fecha"]
                        },
                        Estado = new EstadoTurnos
                        {
                          Id = (int)accesoDatos.Lector["IdEstado"],
                          descripcion = accesoDatos.Lector["descripEstado"].ToString()
                        },
                        Aclaracion = accesoDatos.Lector["Aclaracion"].ToString()
                    };
                    lista.Add(turno);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar turnos: {ex.Message}");
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return lista;
        }

        public Turno ObtenerPorId(int idTurno)
        {
            Turno turno = null;
            try
            {
                string query = @"
                SELECT T.Id, 
                       U.Id AS IdCliente, U.Nombre AS Cliente,
                       TV.Id AS IdVehiculo, TV.Nombre AS Vehiculo,
                       R.Id AS IdRubro, R.Nombre AS Rubro,
                       S.Id AS IdServicio, S.Nombre AS Servicio,
                       T.IdFechaHora, T.IdEstado, et.descripcion descripEstado, T.Aclaracion
                FROM Turnos T
                INNER JOIN Usuarios U ON T.IdCliente = U.Id
                INNER JOIN TipoVehiculo TV ON T.IdTipoVehiculo = TV.Id
                INNER JOIN Rubros R ON T.IdRubro = R.Id
                INNER JOIN Servicios S ON T.IdServicio = S.Id
                inner join EstadoTurnos et on et.Id = t.IdEstado
                WHERE T.Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", idTurno);
                accesoDatos.EjecutarLectura();

                if (accesoDatos.Lector.Read())
                {
                    turno = new Turno
                    {
                        Id = (int)accesoDatos.Lector["Id"],
                        Usuario = new Usuario
                        {
                            Id = (int)accesoDatos.Lector["IdCliente"],
                            Nombre = accesoDatos.Lector["Cliente"].ToString()
                        },
                        Vehiculo = new TipoVehiculo
                        {
                            Codigo = (int)accesoDatos.Lector["IdVehiculo"],
                            Nombre = accesoDatos.Lector["Vehiculo"].ToString()
                        },
                        Rubro = new Rubro
                        {
                            Id = (int)accesoDatos.Lector["IdRubro"],
                            Nombre = accesoDatos.Lector["Rubro"].ToString()
                        },
                        Servicio = new Servicio
                        {
                            Id = (int)accesoDatos.Lector["IdServicio"],
                            Nombre = accesoDatos.Lector["Servicio"].ToString()
                        },
                        FechaHora = new FechaHora
                        {
                            Id = (int)accesoDatos.Lector["IdFechaHora"]
                        },
                        Estado = new EstadoTurnos
                        {
                            Id = (int)accesoDatos.Lector["IdEstado"],
                            descripcion = accesoDatos.Lector["descripEstado"].ToString()
                        },
                        Aclaracion = accesoDatos.Lector["Aclaracion"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener turno por ID: {ex.Message}");
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return turno;
        }

        public bool Modificar(Turno turno)
        {
            try
            {
                string query = @"
                UPDATE Turnos
                SET IdCliente = @IdCliente,
                    IdTipoVehiculo = @IdTipoVehiculo,
                    IdRubro = @IdRubro,
                    IdServicio = @IdServicio,
                    IdFechaHora = @IdFechaHora,
                    Estado = @Estado,
                    Aclaracion = @Aclaracion
                WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", turno.Id);
                accesoDatos.setearParametro("@IdCliente", turno.Usuario.Id);
                accesoDatos.setearParametro("@IdTipoVehiculo", turno.Vehiculo.Codigo);
                accesoDatos.setearParametro("@IdRubro", turno.Rubro.Id);
                accesoDatos.setearParametro("@IdServicio", turno.Servicio.Id);
                accesoDatos.setearParametro("@IdFechaHora", turno.FechaHora.Id);
                accesoDatos.setearParametro("@Estado", turno.Estado.Id);
                accesoDatos.setearParametro("@Aclaracion", turno.Aclaracion);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar turno: {ex.Message}");
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
                Console.WriteLine($"Error al eliminar Turno: {ex.Message}");
                return false;
            }
        }
        public decimal obtenerPrecio(int IdServicio, int idRubro, int idTipoVehiculo)
        {
            decimal precio = 0;
            
            try
            {
                string query = @"
                SELECT Precio FROM Precios WHERE IdTipoVehiculo = @IdTipoVehiculo AND IdRubro = @IdRubro AND IdServicio = @IdServicio";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@IdTipoVehiculo", idTipoVehiculo);
                accesoDatos.setearParametro("@IdRubro", idRubro);
                accesoDatos.setearParametro("@IdServicio", IdServicio);
                accesoDatos.EjecutarLectura();

                if (accesoDatos.Lector.Read())
                {
                    precio = (decimal)accesoDatos.Lector["precio"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener turno por ID: {ex.Message}");
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return precio;
        }
    }
}

