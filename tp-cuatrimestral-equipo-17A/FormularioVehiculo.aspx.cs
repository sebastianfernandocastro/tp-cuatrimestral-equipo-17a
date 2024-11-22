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
    public partial class FormularioVehiculo : System.Web.UI.Page
    {
        private TipoVehiculoNegocio tipoVehiculoNegocio = new TipoVehiculoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarIdImagenes();

                string id = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    CargarVehiculo(int.Parse(id));
                }
                else
                {
                    lblTitulo.Text = "Agregar Vehículo";
                }
            }
        }

        private void CargarVehiculo(int id)
        {
            try
            {
                TipoVehiculo vehiculo = tipoVehiculoNegocio.ObtenerPorId(id);
                if (vehiculo != null)
                {
                    lblTitulo.Text = "Editar Vehículo";
                    txtNombre.Text = vehiculo.Nombre;
                    txtDescripcion.Text = vehiculo.Descripcion;
                    ddlEstado.SelectedValue = vehiculo.Estado.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar el vehículo: " + ex.Message;
                lblMensaje.CssClass = "alert-danger";
                lblMensaje.Visible = true;
            }
        }

        private void CargarIdImagenes()
        {
            try
            {
                ImagenNegocio imagenNegocio = new ImagenNegocio();
                List<Imagen> listaImagenes = imagenNegocio.listar();


                ddlImagen.DataSource = listaImagenes;
                ddlImagen.DataTextField = "UrlImagen";
                ddlImagen.DataValueField = "Id";
                ddlImagen.DataBind();

                ddlImagen.Items.Insert(0, new ListItem("-- Selecciona una imagen --", "0"));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar las imágenes: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                TipoVehiculo vehiculo = new TipoVehiculo();


                vehiculo.Nombre = txtNombre.Text;
                vehiculo.Descripcion = txtDescripcion.Text;
                vehiculo.imagen = new Imagen();
                vehiculo.imagen.Id = int.Parse(ddlImagen.SelectedValue);
                vehiculo.Estado = int.Parse(ddlEstado.SelectedValue);

                string id = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(id))
                {

                    vehiculo.Id = int.Parse(id);
                    tipoVehiculoNegocio.Modificar(vehiculo);
                    lblMensaje.Text = "Vehículo actualizado correctamente.";
                }
                else
                {

                    tipoVehiculoNegocio.Agregar(vehiculo);
                    lblMensaje.Text = "Vehículo agregado correctamente.";
                }

                lblMensaje.CssClass = "alert-success";
                lblMensaje.Visible = true;
                Response.Redirect("Vehiculo.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar el vehículo: " + ex.Message;
                lblMensaje.CssClass = "alert-danger";
                lblMensaje.Visible = true;
            }
        }


    }
}