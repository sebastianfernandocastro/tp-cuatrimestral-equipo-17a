using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace tp_cuatrimestral_equipo_17A
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMsgError.Visible = false;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = "";
            string contraseña = txtContraseña.Text.Trim();

            if(txtUsuario.Text.Trim() == null || txtUsuario.Text.Trim() == "")
            {
                lblMsgError.Visible = true;
                lblMsgError.Text = "Debe ingresar el Usuario.";
                return;
            }
            else usuario = txtUsuario.Text.Trim();

            if (txtContraseña.Text.Trim() == null || txtContraseña.Text.Trim() == "")
            {
                lblMsgError.Visible = true;
                lblMsgError.Text = "Debe ingresar la contrasenia.";
                return;
            }
            else contraseña = txtContraseña.Text.Trim();

            UsuarioNegocio negocio = new UsuarioNegocio();

            Usuario usu = negocio.Login(usuario, contraseña);

            if (usu != null && usu.Id > 0) 
            {
                
                Session.Add("usuario", usuario);
                Response.Redirect("Turnos.aspx", false);
            }
            else
            {
                lblMsgError.Visible = true;
                lblMsgError.Text = "Usuario o contraseña incorrectos.";
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {

        }
    }
}