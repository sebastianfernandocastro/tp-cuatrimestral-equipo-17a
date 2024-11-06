using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace tp_cuatrimestral_equipo_17A
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        public Usuario usu = null;
        public Cliente cl = null;
        public Empleado emp = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usu = (Usuario)Session["usuario"];
                cl = (Cliente)Session["cliente"];
                emp = (Empleado)Session["empleado"];
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaciones())
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    if(usu.tipo == 1)//modifico cliente
                    {
                        cl.Nombre = txtNombre.Text;
                        cl.Apellido = txtApellido.Text;
                        cl.DNI = txtDNI.Text;
                        cl.NombreUsuario = txtUsuario.Text;
                        cl.Contraseña = txtContraseña.Text;
                        cl.Mail = txtEmail.Text;
                        cl.Telefono = txtTelefono.Text;

                        negocio.ModificarMiPerfilCliente(cl);
                    }
                    else //modifico empleado
                    {
                        emp.Nombre = txtNombre.Text;
                        emp.Apellido = txtApellido.Text;
                        emp.NombreUsuario = txtUsuario.Text;
                        emp.Contraseña = txtContraseña.Text;

                        negocio.ModificarMiPerfilEmpleado(emp);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
            
            if(usu.tipo == 1)
            {
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
                if (txtTelefono.Text.Trim() == null || txtTelefono.Text.Trim() == "")
                {
                    lblMensage.Visible = true;
                    lblMensage.Text = "Debe completar el campo Teléfono";
                    return false;
                }
            }
            else
            {
                if (txtLegajo.Text.Trim() == null || txtLegajo.Text.Trim() == "")
                {
                    lblMensage.Visible = true;
                    lblMensage.Text = "Debe completar el campo Legajo";
                    return false;
                }
                if (txtNivelAcceso.Text.Trim() == null || txtNivelAcceso.Text.Trim() == "")
                {
                    lblMensage.Visible = true;
                    lblMensage.Text = "Debe completar el campo Nivel Acceso";
                    return false;
                }
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
            

            return true;
        }
    }
}