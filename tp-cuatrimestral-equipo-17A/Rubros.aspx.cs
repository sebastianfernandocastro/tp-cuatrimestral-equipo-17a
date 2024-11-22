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
    public partial class Rubros : System.Web.UI.Page
    {
        private RubroNegocio rubroNegocio = new RubroNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRubros();
            }
        }

        private void CargarRubros()
        {
            try
            {
                List<Rubro> listaRubros = rubroNegocio.Listar();

                dgvRubros.DataSource = listaRubros;
                dgvRubros.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los rubros: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }


        protected void dgvRubros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ToggleEstado")
            {
                try
                {
                    
                    if (int.TryParse(e.CommandArgument.ToString(), out int id))
                    {
                        Rubro rubro = rubroNegocio.ObtenerPorId(id);
                        if (rubro != null)
                        {
                            rubro.Estado = rubro.Estado == 1 ? 0 : 1;

                            if (rubroNegocio.Modificar(rubro))
                            {
                                lblMensaje.Text = rubro.Estado == 1
                                    ? "Rubro activado correctamente."
                                    : "Rubro desactivado correctamente.";
                                lblMensaje.CssClass = "text-success";
                            }
                            else
                            {
                                lblMensaje.Text = "No se pudo actualizar el estado del rubro.";
                                lblMensaje.CssClass = "text-danger";
                            }
                        }
                        else
                        {
                            lblMensaje.Text = "No se encontró el rubro.";
                            lblMensaje.CssClass = "text-warning";
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "El ID del rubro no es válido.";
                        lblMensaje.CssClass = "text-danger";
                    }
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al cambiar el estado del rubro: " + ex.Message;
                    lblMensaje.CssClass = "text-danger";
                }
                finally
                {
                    lblMensaje.Visible = true;
                    CargarRubros(); 
                }
            }
        }

        protected void dgvRubros_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvRubros.SelectedDataKey.Value);
            Response.Redirect($"FormularioRubro.aspx?id={id}");
        }
        protected void dgvRubros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvRubros.PageIndex = e.NewPageIndex; 
            CargarRubros(); 
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string idValue = hfRubroId.Value;
                System.Diagnostics.Debug.WriteLine($"Valor del HiddenField: {idValue}");

                if (string.IsNullOrEmpty(idValue) || !int.TryParse(idValue, out int id))
                {
                    lblMensaje.Text = "Error: ID no válido.";
                    lblMensaje.CssClass = "text-danger";
                    lblMensaje.Visible = true;
                    return;
                }

                RubroNegocio rubroNegocio = new RubroNegocio();
                rubroNegocio.Eliminar(id);

                lblMensaje.Text = "Rubro eliminado con éxito.";
                lblMensaje.CssClass = "text-success";
                lblMensaje.Visible = true;

                CargarRubros();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar el rubro: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }

    }
}