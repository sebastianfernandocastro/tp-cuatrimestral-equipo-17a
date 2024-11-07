using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class UsuarioNegocio
    {
        public bool Login(Usuario usu, Cliente cl, Empleado emp)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, DNI, Mail, Telefono,Legajo,NivelAcceso, Tipo,estado from Usuarios where Usuario = @usuario and Contrasenia = @contrasenia");
                datos.Comando.Parameters.AddWithValue("@usuario", usu.NombreUsuario);
                datos.Comando.Parameters.AddWithValue("@contrasenia", usu.Contraseña);
                datos.EjecutarLectura();


                while (datos.Lector.Read())
                {
                    usu.tipo = (int)datos.Lector["Tipo"];
                    usu.Id = (int)datos.Lector["Id"];
                    usu.Nombre = (string)datos.Lector["Nombre"];
                    usu.Apellido = (string)datos.Lector["Apellido"];
                    usu.Estado = (int)datos.Lector["Estado"];

                    if (usu.tipo == 1)
                    {
                        cl.Id = usu.Id;
                        cl.Nombre = usu.Nombre;
                        cl.Apellido = usu.Apellido;
                        cl.DNI = (string)datos.Lector["DNI"];
                        cl.Mail = (string)datos.Lector["Mail"];
                        cl.Telefono = (string)datos.Lector["Telefono"];
                        cl.tipo = usu.tipo;
                        cl.Estado = usu.Estado;
                    }
                    else
                    {
                        emp.Id = usu.Id;
                        emp.Nombre = usu.Nombre;
                        emp.Apellido = usu.Apellido;
                        emp.legajo = (string)datos.Lector["legajo"];
                        emp.nivelAcceso = (int)datos.Lector["NivelAcceso"];
                        emp.tipo = usu.tipo;
                        emp.Estado = usu.Estado;
                    }

                    return true;
                }

                return false;
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

        public void agregarCliente(Cliente cl)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Usuarios (Nombre,Apellido,DNI,Mail,Telefono,Usuario,Contrasenia,Estado,Tipo) Values " +
                    "(@Nombre, @Apellido, @DNI, @mail, @Telefono, @Usuario,@contrasenia,1,1)");


                datos.Comando.Parameters.AddWithValue("@Nombre", cl.Nombre);
                datos.Comando.Parameters.AddWithValue("@Apellido", cl.Apellido);
                datos.Comando.Parameters.AddWithValue("@DNI", cl.DNI);
                datos.Comando.Parameters.AddWithValue("@mail", cl.Mail);
                datos.Comando.Parameters.AddWithValue("@Telefono", cl.Telefono);
                datos.Comando.Parameters.AddWithValue("@Usuario", cl.NombreUsuario);
                datos.Comando.Parameters.AddWithValue("@contrasenia", cl.Contraseña);

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

        public void agregarEmpleado(Empleado emp)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Usuarios (Nombre,Apellido,Usuario,Contrasenia,Legajo,NivelAcceso,Estado,Tipo) Values " +
                    "(@Nombre, @Apellido, @Usuario,@contrasenia,@Legajo,@NivelAcceso,1,2)");


                datos.Comando.Parameters.AddWithValue("@Nombre", emp.Nombre);
                datos.Comando.Parameters.AddWithValue("@Apellido", emp.Apellido);
                datos.Comando.Parameters.AddWithValue("@Usuario", emp.NombreUsuario);
                datos.Comando.Parameters.AddWithValue("@contrasenia", emp.Contraseña);
                datos.Comando.Parameters.AddWithValue("@Legajo", emp.legajo);
                datos.Comando.Parameters.AddWithValue("@NivelAcceso", emp.nivelAcceso);

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
        public void eliminarUsuario(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update Usuarios set estado = 0 where Id = " + id);
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

        public void ModificarMiPerfilEmpleado(Empleado emp)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update Usuarios set Nombre = @nombre, Apellido = @Apellido, Usuario = @usu,Contrasenia = @pass where Id = @Id" );
                datos.setearParametro("@nombre",emp.Nombre);
                datos.setearParametro("@Apellido",emp.Apellido);
                datos.setearParametro("@usu",emp.NombreUsuario);
                datos.setearParametro("@pass",emp.Contraseña);
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

        public void ModificarEmpleado(Empleado emp)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update Usuarios set Nombre = @nombre, Apellido = @Apellido,Legajo = @legajo,NivelAcceso = @nivelAcceso, Usuario = @usu,Contrasenia = @pass where Id = @Id");
                datos.setearParametro("@nombre", emp.Nombre);
                datos.setearParametro("@Apellido", emp.Apellido);
                datos.setearParametro("@usu", emp.NombreUsuario);
                datos.setearParametro("@pass", emp.Contraseña);
                datos.setearParametro("@nivelAcceso", emp.nivelAcceso);
                datos.setearParametro("@legajo", emp.legajo);
                datos.setearParametro("@Id", emp.Id);
                
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
        public void ModificarMiPerfilCliente(Cliente cl)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update Usuarios set Nombre = @nombre, Apellido = @Apellido, DNI = @dni,Telefono = @Tel,Mail = @mail,Usuario = @usu,Contrasenia = @pass where Id = @Id");
                datos.setearParametro("@nombre", cl.Nombre);
                datos.setearParametro("@Apellido", cl.Apellido);
                datos.setearParametro("@usu", cl.NombreUsuario);
                datos.setearParametro("@pass", cl.Contraseña);
                datos.setearParametro("@dni", cl.DNI);
                datos.setearParametro("@tel", cl.Telefono);
                datos.setearParametro("@mail", cl.Mail);
                datos.setearParametro("@Id", cl.Id);
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
       

        public List<Cliente> ListarClientes()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Cliente> lista = new List<Cliente>();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, DNI, Mail, Telefono,Usuario,Contrasenia,Tipo,Estado from Usuarios where Tipo = 1 and estado = 1");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente cl = new Cliente();

                    cl.Id = (int)datos.Lector["Id"];
                    cl.Nombre = (string)datos.Lector["Nombre"];
                    cl.Apellido = (string)datos.Lector["Apellido"];
                    cl.DNI = (string)datos.Lector["DNI"];
                    cl.Mail = (string)datos.Lector["Mail"];
                    cl.Contraseña = (string)datos.Lector["Contrasenia"];
                    cl.NombreUsuario = (string)datos.Lector["Usuario"];
                    cl.Telefono = (string)datos.Lector["Telefono"];
                    cl.tipo = (int)datos.Lector["Tipo"];
                    cl.Estado = (int)datos.Lector["Estado"];

                    lista.Add(cl);
                }

                return lista;
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
        public Cliente ObtenerClienteById(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, DNI, Mail, Telefono,Usuario,Contrasenia,Tipo,Estado from Usuarios where Tipo = 1 and estado = 1");
                datos.EjecutarLectura();

                Cliente cl = new Cliente();

                while (datos.Lector.Read())
                {

                    cl.Id = (int)datos.Lector["Id"];
                    cl.Nombre = (string)datos.Lector["Nombre"];
                    cl.Apellido = (string)datos.Lector["Apellido"];
                    cl.DNI = (string)datos.Lector["DNI"];
                    cl.Mail = (string)datos.Lector["Mail"];
                    cl.Contraseña = (string)datos.Lector["Contrasenia"];
                    cl.NombreUsuario = (string)datos.Lector["Usuario"];
                    cl.Telefono = (string)datos.Lector["Telefono"];
                    cl.tipo = (int)datos.Lector["Tipo"];
                    cl.Estado = (int)datos.Lector["Estado"];

                    
                }

                return cl;
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

        public List<Empleado> ListarEmpleados()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Empleado> lista = new List<Empleado>();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido,Legajo,NivelAcceso,Tipo,Estado,Usuario,Contrasenia from Usuarios where Tipo = 2 and estado = 1");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Empleado emp = new Empleado();

                    emp.Id = (int)datos.Lector["Id"];
                    emp.Nombre = (string)datos.Lector["Nombre"];
                    emp.Apellido = (string)datos.Lector["Apellido"];
                    emp.legajo = (string)datos.Lector["Legajo"];
                    emp.nivelAcceso = (int)datos.Lector["NivelAcceso"];
                    emp.tipo = (int)datos.Lector["Tipo"];
                    emp.Estado = (int)datos.Lector["Estado"];
                    emp.NombreUsuario = (string)datos.Lector["Usuario"];
                    emp.Contraseña = (string)datos.Lector["Contrasenia"];

                    lista.Add(emp);
                }

                return lista;
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
        public Empleado ObtenerEmpleadoById(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido,Legajo,NivelAcceso,Tipo,Estado,Usuario,Contrasenia from Usuarios where Tipo = 2 and estado = 1 and id = @id");
                datos.setearParametro("@id", id);
                datos.EjecutarLectura();

                Empleado emp = new Empleado();
                while (datos.Lector.Read())
                {

                    emp.Id = (int)datos.Lector["Id"];
                    emp.Nombre = (string)datos.Lector["Nombre"];
                    emp.Apellido = (string)datos.Lector["Apellido"];
                    emp.legajo = (string)datos.Lector["Legajo"];
                    emp.nivelAcceso = (int)datos.Lector["NivelAcceso"];
                    emp.tipo = (int)datos.Lector["Tipo"];
                    emp.Estado = (int)datos.Lector["Estado"];
                    emp.NombreUsuario = (string)datos.Lector["Usuario"];
                    emp.Contraseña = (string)datos.Lector["Contrasenia"];
                    
                }

                return emp;
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
    }
}
