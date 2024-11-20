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
                string query = "INSERT INTO Rubros (Nombre, Descripcion, IdImagen) VALUES (@Nombre, @Descripcion, @IdImagen)";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Nombre", nuevoRubro.Nombre);
                accesoDatos.setearParametro("@Descripcion", nuevoRubro.Descripcion);
                accesoDatos.setearParametro("@IdImagen", nuevoRubro.IdImagen);

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
                accesoDatos.setearConsulta("SELECT Id, Nombre, Descripcion, IdImagen FROM Rubros");
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Rubro nuevoRubro = new Rubro
                    {
                        Id = (int)accesoDatos.Lector["Id"], 
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Descripcion = accesoDatos.Lector["Descripcion"].ToString(),
                        IdImagen = (int)accesoDatos.Lector["IdImagen"] 
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
                string query = "UPDATE Rubros SET Nombre = @Nombre, Descripcion = @Descripcion, IdImagen = @IdImagen WHERE Id = @Id";

                accesoDatos.setearConsulta(query);
                accesoDatos.setearParametro("@Id", rubroModificado.Id);
                accesoDatos.setearParametro("@Nombre", rubroModificado.Nombre);
                accesoDatos.setearParametro("@Descripcion", rubroModificado.Descripcion);
                accesoDatos.setearParametro("@IdImagen", rubroModificado.IdImagen);

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
