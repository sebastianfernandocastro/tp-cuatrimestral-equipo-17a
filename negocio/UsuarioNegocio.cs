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
        public Usuario Login(string user, string pass)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usu = new Usuario();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, DNI, Mail, Telefono from Usuarios where Usuario = @usuario and Contrasenia = @contrasenia");
                datos.Comando.Parameters.AddWithValue("@usuario", user);
                datos.Comando.Parameters.AddWithValue("@contrasenia", pass);
                datos.EjecutarLectura();


                while (datos.Lector.Read())
                {
                    usu.Id = (int)datos.Lector["Id"];
                    usu.Nombre = (string)datos.Lector["Nombre"];
                    usu.Apellido = (string)datos.Lector["Apellido"];
                    usu.Dni = (int)datos.Lector["DNI"];
                    usu.Mail = (string)datos.Lector["Mail"];
                    usu.Telefono = (string)datos.Lector["Telefono"];
                    usu.User = user;
                    usu.Pass = pass;
                }

                return usu;
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

        public void agregarUsuario(Usuario usu)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Usuarios (Nombre,Apellido,DNI,Mail,Telefono,Usuario,Contrasenia) Values " +
                    "(@Nombre, @Apellido, @DNI, @mail, @Telefono, @Usuario,@contrasenia)");


                datos.Comando.Parameters.AddWithValue("@Nombre", usu.Nombre);
                datos.Comando.Parameters.AddWithValue("@Apellido", usu.Apellido);
                datos.Comando.Parameters.AddWithValue("@DNI", usu.Dni);
                datos.Comando.Parameters.AddWithValue("@mail", usu.Mail);
                datos.Comando.Parameters.AddWithValue("@Telefono", usu.Telefono);
                datos.Comando.Parameters.AddWithValue("@Usuario", usu.User);
                datos.Comando.Parameters.AddWithValue("@contrasenia", usu.Pass);

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
      
    }
}
