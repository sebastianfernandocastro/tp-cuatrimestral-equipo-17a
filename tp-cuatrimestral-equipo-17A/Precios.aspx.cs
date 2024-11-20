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
    public partial class Precios : System.Web.UI.Page
    {
        List<Precio> listaPrecios = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPrecios();
            }
        }

        private void CargarPrecios(string filtroPrecios = "")
        {
            PrecioNegocio precioNegocio = new PrecioNegocio();
            try
            {
                listaPrecios = precioNegocio.ListarPrecios();

                if (!String.IsNullOrEmpty(filtroPrecios) || !String.IsNullOrEmpty(txtFiltro.Text)) listaPrecios = listaPrecios.FindAll(x => x.RubroNombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.ServicioNombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.TipoVehiculoNombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

                dgvPrecios.DataSource = listaPrecios;
                dgvPrecios.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los precios: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }


        protected void dgvPrecios_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvPrecios.SelectedDataKey.Value);

            Response.Redirect($"FormularioPrecio.aspx?id={id}");
        }


        protected void dgvPrecios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPrecios.PageIndex = e.NewPageIndex;
            CargarPrecios();
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string idPrecio = hfPrecioId.Value;

                if (string.IsNullOrEmpty(idPrecio))
                {
                    throw new Exception("El ID del precio no fue proporcionado.");
                }

                int id = Convert.ToInt32(idPrecio); 
                System.Diagnostics.Debug.WriteLine($"ID para eliminar: {id}");

                PrecioNegocio negocio = new PrecioNegocio();
                negocio.EliminarPrecio(id);

                CargarPrecios(); 
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al eliminar: {ex.Message}");
                //throw ex;
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CargarPrecios(txtFiltro.Text);
                //if (String.IsNullOrEmpty(txtFiltro.Text)) CargarPrecios();
                //else
                //{
                //    listaPrecios.F
                //}
            }
            catch (Exception ex)
            {

            }
        }
    }
}