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
            dgvEmpleados.DataSource = negocio.ListarEmpleados();
            dgvEmpleados.DataBind();

            //si no tiene datos poner un texto no hay registros...
        }

        protected void dgvEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEmpleados.PageIndex = e.NewPageIndex;
            dgvEmpleados.DataBind();
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

                throw ex;
            }



        }
    }
}