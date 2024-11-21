﻿using System;
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
                string query = "INSERT INTO TipoVehiculo (Id, Nombre, Descripcion, Imagen,Estado) VALUES (@Id, @Nombre, @Descripcion, @Imagen,1)";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", nuevoTipoVehiculo.Id);
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

        public List<TipoVehiculo> Listar(int inactivo = 0)
        {
            List<TipoVehiculo> lista = new List<TipoVehiculo>();
            try
            {
                string query = "SELECT Id, Nombre, Descripcion, IdImagen FROM TipoVehiculo";

                if (inactivo == 1) query += " where estado = 0 ";
                else query += " where estado = 1 ";

                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    TipoVehiculo tipoVehiculo = new TipoVehiculo
                    {
                        Id = (int)accesoDatos.Lector["Id"],
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                        IdImagen = accesoDatos.Lector["IdImagen"] != DBNull.Value ? (int)accesoDatos.Lector["IdImagen"] : 0 
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

        public TipoVehiculo ObtenerPorId(int id)
        {
            try
            {
                string query = "SELECT Id, Nombre, Descripcion, IdImagen FROM TipoVehiculo where id = " + id;

                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    TipoVehiculo tipoVehiculo = new TipoVehiculo
                    {
                        Id = (int)accesoDatos.Lector["Id"],
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                        IdImagen = accesoDatos.Lector["IdImagen"] != DBNull.Value ? (int)accesoDatos.Lector["IdImagen"] : 0
                    };
                    return tipoVehiculo;
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
            return null;
        }


        public bool Modificar(TipoVehiculo tipoVehiculoModificado)
        {
            try
            {
                string query = "UPDATE TipoVehiculo SET Nombre = @Nombre, Descripcion = @Descripcion, Imagen = @Imagen, Estado = @Estado WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", tipoVehiculoModificado.Id);
                accesoDatos.setearParametro("@Nombre", tipoVehiculoModificado.Nombre);
                accesoDatos.setearParametro("@Descripcion", tipoVehiculoModificado.Descripcion);
                accesoDatos.setearParametro("@Estado", tipoVehiculoModificado.Estado);
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

        public bool Eliminar(int id)
        {
            try
            {
                string query = "DELETE FROM TipoVehiculo WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", id);

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
