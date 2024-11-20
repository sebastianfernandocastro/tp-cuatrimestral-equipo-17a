using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_cuatrimestral_equipo_17A
{
    public partial class FormularioRubro : System.Web.UI.Page
    {
        private RubroNegocio rubroNegocio = new RubroNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarIdImagenes(); // Cargar las opciones para el dropdown de imágenes

                // Verificar si se está modificando un rubro
                if (Request.QueryString["id"] != null)
                {
                    int id;
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        CargarDatosFormulario(id); // Cargar datos del rubro para modificar
                    }
                    else
                    {
                        lblMensaje.Text = "Error: ID no válido.";
                        lblMensaje.CssClass = "text-danger";
                        lblMensaje.Visible = true;
                    }
                }
            }
        }

        private void CargarIdImagenes()
        {
            try
            {
                ImagenNegocio imagenNegocio = new ImagenNegocio();
                List<Imagen> listaImagenes = imagenNegocio.listar();

                // Vincula la lista de imágenes al DropDownList
                ddlIdImagen.DataSource = listaImagenes;
                ddlIdImagen.DataTextField = "UrlImagen"; // Campo que se mostrará al usuario
                ddlIdImagen.DataValueField = "Id"; // Valor que se guardará en el backend
                ddlIdImagen.DataBind();

                // Opcional: Agrega un elemento por defecto
                ddlIdImagen.Items.Insert(0, new ListItem("-- Selecciona una imagen --", "0"));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar las imágenes: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }


        private void CargarDatosFormulario(int id)
        {
            try
            {
                Rubro rubro = rubroNegocio.Listar().Find(r => r.Id == id);
                if (rubro != null)
                {
                    hfRubroId.Value = rubro.Id.ToString();
                    txtNombre.Text = rubro.Nombre;
                    txtDescripcion.Text = rubro.Descripcion;
                    ddlIdImagen.SelectedValue = rubro.IdImagen.ToString();
                }
                else
                {
                    lblMensaje.Text = "Rubro no encontrado.";
                    lblMensaje.CssClass = "text-danger";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los datos: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Rubro rubro = new Rubro
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    IdImagen = int.Parse(ddlIdImagen.SelectedValue)
                };

                // Si estamos modificando
                if (!string.IsNullOrEmpty(hfRubroId.Value))
                {
                    rubro.Id = int.Parse(hfRubroId.Value);
                    rubroNegocio.Modificar(rubro);
                    lblMensaje.Text = "Rubro modificado con éxito.";
                }
                else // Si estamos agregando
                {
                    rubroNegocio.Agregar(rubro);
                    lblMensaje.Text = "Rubro agregado con éxito.";
                }

                lblMensaje.CssClass = "text-success";
                lblMensaje.Visible = true;

                // Redirigir al listado después de guardar
                Response.Redirect("Rubros.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar el rubro: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }
    }
}