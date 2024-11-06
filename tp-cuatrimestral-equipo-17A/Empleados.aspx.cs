using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_cuatrimestral_equipo_17A
{
    public partial class Empleados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Session.Add("listaEmpleados", negocio.ListarEmpleados());
                dgvEmpleados.DataSource = Session["listaEmpleados"];
                dgvEmpleados.DataBind();
            }
        }

        protected void dgvEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEmpleados.PageIndex = e.NewPageIndex;
            dgvEmpleados.DataBind();
        }

        protected void dgvEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvEmpleados.SelectedDataKey.Value.ToString();
            //Response.Redirect("FormularioEmpleado.aspx?id=" + id);
        }
    }
}