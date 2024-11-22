using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace tp_cuatrimestral_equipo_17A
{
    public partial class Servicios : System.Web.UI.Page
    {
        private ServicioNegocio servicioNegocio = new ServicioNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarServicios();
            }
        }
        private void CargarServicios()
        {
            try
            {
                List<Servicio> listaServicios = servicioNegocio.ListarTodos();
                dgvServicios.DataSource = listaServicios;
                dgvServicios.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar los servicios: " + ex.Message, "text-danger");
            }
        }
        protected void dgvServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvServicios.PageIndex = e.NewPageIndex;
            CargarServicios();
        }
        protected void dgvServicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CambiarEstado")
            {
                try
                {
                    string[] argumentos = e.CommandArgument.ToString().Split(',');
                    int idServicio = int.Parse(argumentos[0]);
                    int nuevoEstado = int.Parse(argumentos[1]);
                    servicioNegocio.ActualizarEstado(idServicio, nuevoEstado);
                    MostrarMensaje("Estado del servicio actualizado correctamente.", "text-success");
                    CargarServicios();
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al actualizar el estado: " + ex.Message, "text-danger");
                }
            }
            else if (e.CommandName == "Modificar")
            {
                try
                {
                    int idServicio = int.Parse(e.CommandArgument.ToString());
                    Response.Redirect($"FormularioServicio.aspx?id={idServicio}");
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al redirigir al formulario: " + ex.Message, "text-danger");
                }
            }
        }
        private void MostrarMensaje(string mensaje, string cssClass)
        {

            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = cssClass;
            lblMensaje.Visible = true;
            CargarServicios();
        }
    }
}