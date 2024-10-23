using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            if (usuario == "admin" && contraseña == "admin") 
            {
                //FormsAuthentication.RedirectFromLoginPage(username, false);
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