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
            string contraseña = "";

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
            Usuario usu = new Usuario();
            Cliente cl = new Cliente();
            Empleado emp = new Empleado();

            usu.NombreUsuario = usuario;
            usu.Contraseña = contraseña;

            if(negocio.Login(usu,cl,emp))
            {
                Session.Add("usuario", usu);

                if(usu.tipo == 1)
                {
                    Session.Add("cliente", cl);
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    Session.Add("empleado", emp);
                    Response.Redirect("TurnosListado.aspx", false);
                }
            }
            else
            {
                lblMsgError.Visible = true;
                lblMsgError.Text = "Usuario y contraseña incorrectos o usuario inhabilitado.";
            }

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {

        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Cliente usu = new Cliente();
            Herramientas herramientas = new Herramientas();

            try
            {
                usu = negocio.ObtenerClienteByMail(txbMail.Text);
                string datos = "Hola " + usu.Nombre + " tu usuario es: " + usu.NombreUsuario + " y tu contraseña: " + usu.Contraseña + ". ¡Esperamos haberte ayudado!";
                herramientas.enviarMailRecuperacion(usu.Mail, datos, usu.Nombre);

            }
            catch (Exception ex)
            {
            }

        }
    }
}