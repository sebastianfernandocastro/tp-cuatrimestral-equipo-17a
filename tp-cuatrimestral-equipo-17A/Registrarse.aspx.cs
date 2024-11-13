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
            if (String.IsNullOrEmpty(txtNombre.Text))
            {
                lblMensage.Text = "El campo Nombre no puede estar vacío";
                return false;
            }
            else if (String.IsNullOrEmpty(txtApellido.Text))
            {
                lblMensage.Text = "El campo Apellido no puede estar vacío";
                return false;
            }
            else if (String.IsNullOrEmpty(txtUsuario.Text))
            {
                lblMensage.Text = "El campo Usuario no puede estar vacío";
                return false;
            }
            else if (String.IsNullOrEmpty(txtContraseña.Text))
            {
                lblMensage.Text = "El campo Contraseña no puede estar vacío";
                return false;
            }
            else if (String.IsNullOrEmpty(txtDNI.Text))
            {
                lblMensage.Text = "El campo DNI no puede estar vacío";
                return false;
            }
            else if (String.IsNullOrEmpty(txtEmail.Text))
            {
                lblMensage.Text = "El campo Mail no puede estar vacío";
                return false;
            }
            else if (String.IsNullOrEmpty(txtTelefono.Text))
            {
                lblMensage.Text = "El campo Teléfono no puede estar vacío";
                return false;
            }

            if (!soloNumeros(txtTelefono.Text))
            {
                lblMensage.Text = "El campo Teléfono son solo números";
                return false;
            }
            else if (!soloNumeros(txtDNI.Text))
            {
                lblMensage.Text = "El campo DNI son solo números";
                return false;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();

            if (negocio.existeUsuarioByDNI(txtDNI.Text))
            {
                lblMensage.Text = "el DNI ya existe en el sistema, ingrese otro ...";
                return false;
            }
            else if (negocio.existeUsuarioByUsuario(txtUsuario.Text))
            {
                lblMensage.Text = "el Usuario ya existe en el sistema, ingrese otro ...";
                return false;
            }
            else if (negocio.existeUsuarioByMail(txtEmail.Text))
            {
                lblMensage.Text = "el Mail ya existe en el sistema, ingrese otro ...";
                return false;
            }

            return true;
        }

        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }

        //public bool validaciones()
        //{

        //    if (txtNombre.Text.Trim() == null || txtNombre.Text.Trim() == "")
        //    {
        //        lblMensage.Visible = true;
        //        lblMensage.Text = "Debe completar el campo Nombre";
        //        return false;
        //    }
        //    if (txtApellido.Text.Trim() == null || txtApellido.Text.Trim() == "")
        //    {
        //        lblMensage.Visible = true;
        //        lblMensage.Text = "Debe completar el campo Apellido";
        //        return false;
        //    }
        //    if (txtDNI.Text.Trim() == null || txtDNI.Text.Trim() == "")
        //    {
        //        lblMensage.Visible = true;
        //        lblMensage.Text = "Debe completar el campo DNI";
        //        return false;
        //    }
        //    if (txtEmail.Text.Trim() == null || txtEmail.Text.Trim() == "")
        //    {
        //        lblMensage.Visible = true;
        //        lblMensage.Text = "Debe completar el campo Mail";
        //        return false;
        //    }
        //    if (txtUsuario.Text.Trim() == null || txtUsuario.Text.Trim() == "")
        //    {
        //        lblMensage.Visible = true;
        //        lblMensage.Text = "Debe completar el campo Usuario";
        //        return false;
        //    }
        //    if (txtContraseña.Text.Trim() == null || txtContraseña.Text.Trim() == "")
        //    {
        //        lblMensage.Visible = true;
        //        lblMensage.Text = "Debe completar el campo Contraseña";
        //        return false;
        //    }
        //    if (txtTelefono.Text.Trim() == null || txtTelefono.Text.Trim() == "")
        //    {
        //        lblMensage.Visible = true;
        //        lblMensage.Text = "Debe completar el campo Teléfono";
        //        return false;
        //    }

        //    return true;
        //}
    }
}