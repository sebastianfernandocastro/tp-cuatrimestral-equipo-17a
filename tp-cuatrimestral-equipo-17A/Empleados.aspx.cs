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
    public partial class Empleados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarEmpleados();
            }
        }
        public void cargarEmpleados()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                List<Empleado> listaEmp = negocio.ListarEmpleados();

                if (!String.IsNullOrEmpty(txtFiltro.Text)) listaEmp = listaEmp.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.Apellido.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.legajo.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.NombreUsuario.ToUpper().Contains(txtFiltro.Text.ToUpper()));

                dgvEmpleados.DataSource = listaEmp;
                dgvEmpleados.DataBind();

            }
            catch (Exception ex)
            {
                lblMessage.Text = "error al cargar Empleados.";
                lblMessage.CssClass = "text-warning";
                lblMessage.Visible = true;
            }

        }

        protected void dgvEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEmpleados.PageIndex = e.NewPageIndex;
            cargarEmpleados();
        }

        protected void dgvEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvEmpleados.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioEmpleado.aspx?id=" + id);
        }



        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string idEmpleado = "";
                idEmpleado = hfEmpleadoId.Value;

                if (!String.IsNullOrEmpty(idEmpleado))
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.eliminarUsuario(Convert.ToInt32(idEmpleado));
                    cargarEmpleados();
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "error al eliminar Empleado.";
                lblMessage.CssClass = "text-warning";
                lblMessage.Visible = true;
            }



        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            cargarEmpleados();
        }
    }
}