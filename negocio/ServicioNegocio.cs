﻿using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    internal class ServicioNegocio
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
                string query = "INSERT INTO Servicios (Id, Nombre, Descripcion, Tiempo, Precio) VALUES (@Id, @Nombre, @Descripcion, @Precio, @Tiempo, @Precio)";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", nuevoServicio.Id);
                accesoDatos.setearParametro("@Nombre", nuevoServicio.Nombre);
                accesoDatos.setearParametro("@Descripcion", nuevoServicio.Descripcion);
                accesoDatos.setearParametro("@Tiempo", nuevoServicio.Tiempo);
                accesoDatos.setearParametro("@Precio", nuevoServicio.Precio);

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
                string query = "SELECT Id, Nombre, Descripcion, Tiempo, Precio FROM Servicios";
                accesoDatos.setearConsulta(query);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Servicio nuevoServicio = new Servicio
                    {
                        Id = (int)accesoDatos.Lector["Int"],
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                        Tiempo = (decimal)accesoDatos.Lector["Tiempo"],
                        Precio = (decimal)accesoDatos.Lector["Precio"]
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

        public bool Modificar(Servicio ServicioModificado)
        {
            try
            {
                string query = "UPDATE Servicios SET Nombre = @Nombre, Descripcion = @Descripcion, Tiempo = @Tiempo, Precio =  @Precio WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Codigo", ServicioModificado.Id);
                accesoDatos.setearParametro("@Nombre", ServicioModificado.Nombre);
                accesoDatos.setearParametro("@Descripcion", ServicioModificado.Descripcion);
                accesoDatos.setearParametro("@Tiempo", ServicioModificado.Tiempo);
                accesoDatos.setearParametro("@Precio", ServicioModificado.Precio);


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