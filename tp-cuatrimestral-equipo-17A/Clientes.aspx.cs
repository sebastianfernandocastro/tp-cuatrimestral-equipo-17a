using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_cuatrimestral_equipo_17A
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarClientes();
            }
        }
        public void cargarClientes()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            dgvClientes.DataSource = negocio.ListarClientes();
            dgvClientes.DataBind();

            //si no tiene datos poner un texto no hay registros...
        }

        protected void dgvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClientes.PageIndex = e.NewPageIndex;
            dgvClientes.DataBind();
        }

        protected void dgvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvClientes.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioCliente.aspx?id=" + id);
        }



        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string idCliente = "";
                idCliente = hfClienteId.Value;

                if (!String.IsNullOrEmpty(idCliente))
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.eliminarUsuario(Convert.ToInt32(idCliente));
                    cargarClientes();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }



        }
    }
}