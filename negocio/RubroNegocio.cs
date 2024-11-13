using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class RubroNegocio
    {
        private AccesoDatos accesoDatos;

        public RubroNegocio()
        {
            accesoDatos = new AccesoDatos();
        }

        public bool Agregar(Rubro nuevoRubro)
        {
            try
            {
                string query = "INSERT INTO Rubros (Id, Nombre, Descripcion) VALUES (@Id, @Nombre, @Descripcion)";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", nuevoRubro.Id);
                accesoDatos.setearParametro("@Nombre", nuevoRubro.Nombre);
                accesoDatos.setearParametro("@Descripcion", nuevoRubro.Descripcion);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar Rubro: " + ex.Message);
                return false;
            }
        }

        public List<Rubro> Listar()
        {
            List<Rubro> lista = new List<Rubro>();
            try
            {
                accesoDatos.setearConsulta("SELECT IdRubro, Nombre, Descripcion FROM Rubros");
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Rubro nuevoRubro = new Rubro
                    {
                        Id = (int)accesoDatos.Lector["IdRubro"], 
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString()
                    };
                    lista.Add(nuevoRubro);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Rubros: " + ex.Message);
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
            return lista;
        }

        public bool Modificar(Rubro rubroModificado)
        {
            try
            {
                string query = "UPDATE Rubros SET Nombre = @Nombre, Descripcion = @Descripcion WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", rubroModificado.Id);
                accesoDatos.setearParametro("@Nombre", rubroModificado.Nombre);
                accesoDatos.setearParametro("@Descripcion", rubroModificado.Descripcion);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar Rubro: " + ex.Message);
                return false;
            }
        }

        public bool Eliminar(int Id)
        {
            try
            {
                string query = "DELETE FROM Rubros WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", Id);

                accesoDatos.EjecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar Rubro: " + ex.Message);
                return false;
            }
        }
    }
}
