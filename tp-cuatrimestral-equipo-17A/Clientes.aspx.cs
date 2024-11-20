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
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                List<Cliente> listaCli = negocio.ListarClientes();

                if(!String.IsNullOrEmpty(txtFiltro.Text)) listaCli = listaCli.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.Apellido.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.DNI.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.NombreUsuario.ToUpper().Contains(txtFiltro.Text.ToUpper()));

                dgvClientes.DataSource = listaCli;
                dgvClientes.DataBind();

                if(listaCli.Count == 0)
                {
                    lblMessage.Text = "No hay Clientes registrados.";
                    lblMessage.CssClass = "text-warning";
                    lblMessage.Visible = true;
                }
                else lblMessage.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "error al listar Clientes.";
                lblMessage.CssClass = "text-warning";
                lblMessage.Visible = true;
            }
            //si no tiene datos poner un texto no hay registros...
        }

        protected void dgvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClientes.PageIndex = e.NewPageIndex;
            cargarClientes();
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
                lblMessage.Text = "error al eliminar Clientes.";
                lblMessage.CssClass = "text-warning";
                lblMessage.Visible = true;
            }



        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            cargarClientes();
        }
    }
}