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
                RubroNegocio rubroNegocio = new RubroNegocio();
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


        protected void dgvRubros_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvRubros.SelectedDataKey.Value);
            Response.Redirect($"FormularioRubro.aspx?id={id}");
        }
        protected void dgvRubros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvRubros.PageIndex = e.NewPageIndex; // Cambiar el índice de la página
            CargarRubros(); // Recargar los datos en el GridView
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