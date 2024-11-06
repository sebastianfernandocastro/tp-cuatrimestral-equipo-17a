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
    public partial class Registrarse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            Cliente cl = new Cliente();

            if (validaciones())
            {
                try
                {
                    cl.Nombre = txtNombre.Text.Trim();
                    cl.Apellido = txtApellido.Text.Trim();
                    cl.DNI = txtDNI.Text.Trim();
                    cl.Mail = txtEmail.Text.Trim();
                    cl.Telefono = txtTelefono.Text.Trim();
                    cl.NombreUsuario = txtUsuario.Text.Trim();
                    cl.Contraseña = txtContraseña.Text.Trim();

                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.agregarCliente(cl);

                    Response.Redirect("Login.aspx", false);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }

        public bool validaciones()
        {
           
            if (txtNombre.Text.Trim() == null || txtNombre.Text.Trim() == "")
            {
                lblMensage.Visible = true;
                lblMensage.Text = "Debe completar el campo Nombre";
                return false;
            }
            if (txtApellido.Text.Trim() == null || txtApellido.Text.Trim() == "")
            {
                lblMensage.Visible = true;
                lblMensage.Text = "Debe completar el campo Apellido";
                return false;
            }
            if (txtDNI.Text.Trim() == null || txtDNI.Text.Trim() == "")
            {
                lblMensage.Visible = true;
                lblMensage.Text = "Debe completar el campo DNI";
                return false;
            }
            if (txtEmail.Text.Trim() == null || txtEmail.Text.Trim() == "")
            {
                lblMensage.Visible = true;
                lblMensage.Text = "Debe completar el campo Mail";
                return false;
            }
            if (txtUsuario.Text.Trim() == null || txtUsuario.Text.Trim() == "")
            {
                lblMensage.Visible = true;
                lblMensage.Text = "Debe completar el campo Usuario";
                return false;
            }
            if (txtContraseña.Text.Trim() == null || txtContraseña.Text.Trim() == "")
            {
                lblMensage.Visible = true;
                lblMensage.Text = "Debe completar el campo Contraseña";
                return false;
            }
            if (txtTelefono.Text.Trim() == null || txtTelefono.Text.Trim() == "")
            {
                lblMensage.Visible = true;
                lblMensage.Text = "Debe completar el campo Teléfono";
                return false;
            }

            return true;
        }
    }
}