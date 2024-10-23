using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ClienteNegocio
    {

        //public List<Cliente> listar()
        //{
        //    //List<Cliente> Clientes = new List<Cliente>();
        //    //AccesoDatos datos = new AccesoDatos();

        //    //try
        //    //{
        //    //    datos.setearConsulta("SELECT A.Id, A.Precio, A.IdCategoria, A.IdMarca, A.Codigo, A.Nombre, A.Descripcion ,C.Descripcion AS DesCategoria, M.Descripcion AS DesMarca FROM ClienteS A JOIN CATEGORIAS C ON A.IdCategoria = C.Id JOIN MARCAS M ON A.IdMarca = M.Id");
        //    //    datos.EjecutarLectura();
        //    //    while (datos.Lector.Read())
        //    //    {
        //    //        Cliente Cliente = new Cliente();
        //    //        Cliente.Id = (int)datos.Lector["Id"];
        //    //        Cliente.Codigo = (string)datos.Lector["Codigo"];
        //    //        Cliente.Nombre = (string)datos.Lector["Nombre"];
        //    //        Cliente.Descripcion = (string)datos.Lector["Descripcion"];
        //    //        Cliente.Precio = (decimal)datos.Lector["Precio"];
        //    //        Cliente.Categoria = new Categoria();
        //    //        Cliente.Categoria.Id = (int)datos.Lector["IdCategoria"];
        //    //        Cliente.Categoria.Descripcion = (string)datos.Lector["DesCategoria"];
        //    //        Cliente.Marca = new Marca();
        //    //        Cliente.Marca.Id = (int)datos.Lector["IdMarca"];
        //    //        Cliente.Marca.Descripcion = (string)datos.Lector["DesMarca"];
        //    //        Clientes.Add(Cliente);

        //    //    }


        //    //    return Clientes;
        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //    throw ex;
        //    //}
        //    //finally
        //    //{
        //    //    datos.CerrarConexion();
        //    //}
        //}

        public Cliente buscarCliente(string dni)
        {
            Cliente cliente = new Cliente();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP FROM Clientes where Documento = @dni");
                datos.Comando.Parameters.AddWithValue("@dni", dni);
                datos.EjecutarLectura();

                while(datos.Lector.Read())
                {
                    cliente.id = (int)datos.Lector["Id"];
                    cliente.nombre = (string)datos.Lector["Nombre"];
                    cliente.apellido = (string)datos.Lector["Apellido"];
                    cliente.dni = (string)datos.Lector["Documento"];
                    cliente.email = (string)datos.Lector["Email"];
                    cliente.direccion = (string)datos.Lector["Direccion"];
                    cliente.ciudad = (string)datos.Lector["Ciudad"];
                    cliente.cp = (int)datos.Lector["CP"];
                }

                return cliente;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM ClienteS WHERE Id = @Id");
                datos.Comando.Parameters.AddWithValue("@Id", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void agregarCliente(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Clientes (Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP) VALUES (@dni, @nombre, @apellido, @email, @direccion, @ciudad, @cp)");

                // Agregar parámetros
                datos.Comando.Parameters.AddWithValue("@dni", cliente.dni);
                datos.Comando.Parameters.AddWithValue("@nombre", cliente.nombre);
                datos.Comando.Parameters.AddWithValue("@apellido", cliente.apellido);
                datos.Comando.Parameters.AddWithValue("@email", cliente.email);
                datos.Comando.Parameters.AddWithValue("@direccion", cliente.direccion);
                datos.Comando.Parameters.AddWithValue("@ciudad", cliente.ciudad);
                datos.Comando.Parameters.AddWithValue("@cp", cliente.cp);

                // Ejecutar la consulta de inserción
                datos.EjecutarAccion(); // Asumiendo que este método ejecuta acciones que no devuelven resultados.
            }
            catch (Exception ex)
            {
                throw ex; // Lanza la excepción para manejarla en otro lugar, si es necesario
            }
            finally
            {
                datos.CerrarConexion(); // Asegúrate de cerrar la conexión
            }
        }
            //public void Editar(Cliente ClienteNuevo)
            //{
            //    AccesoDatos datos = new AccesoDatos();
            //    try
            //    {

            //        datos.setearConsulta("UPDATE Clientes" +
            //            " set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, " +
            //            " IdCategoria = @IdCategoria, IdMarca = @IdMarca WHERE ID = @Id");

            //        datos.ConvertirDatos(ClienteNuevo);
            //        datos.EjecutarAccion();
            //    }
            //    catch (Exception ex)
            //    {
            //        // Manejar cualquier error
            //        throw ex;
            //    }
            //    finally
            //    {
            //        // Asegurar que la conexión se cierre correctamente
            //        datos.CerrarConexion();
            //    }
            //}
    }
}
