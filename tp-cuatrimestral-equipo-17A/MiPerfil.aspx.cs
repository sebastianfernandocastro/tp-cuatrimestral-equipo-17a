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
            usu = (Usuario)Session["usuario"];
            cl = (Cliente)Session["cliente"];
            emp = (Empleado)Session["empleado"]; 

            if (!IsPostBack)
            {
                //if (!Herramientas.sesionActiva(usu))
                //{
                //    Response.Redirect("Login.aspx", false);
                //    Response.End();
                //}
                

                if (usu != null)
                {
                    txtApellido.Text = usu.Apellido;
                    txtNombre.Text = usu.Nombre;
                    txtUsuario.Text = usu.NombreUsuario;
                    txtId.Text = usu.Id.ToString();
                    txtContraseña.Text = usu.Contraseña;
                }
                else
                {
                    btnEditar.Text = "Registrarse";
                }

            }


            if (cl != null && !IsPostBack)
            {
                txtEmail.Visible = true;
                txtDNI.Visible = true;
                txtTelefono.Visible = true;
                txtLegajo.Visible = false;
                ddlNivelAcceso.Visible = false;

                txtDNI.Text = cl.DNI;
                txtEmail.Text = cl.Mail;
                txtTelefono.Text = cl.Telefono;
            }
            else if(emp != null && !IsPostBack)
            {
                txtEmail.Visible = false;
                txtDNI.Visible = false;
                txtTelefono.Visible = false;
                txtLegajo.Visible = true;
                ddlNivelAcceso.Visible = true;

                txtLegajo.Text = emp.legajo.ToString();
                cargarddlNivelAcceso(emp.nivelAcceso.Id);

            }
        }

        public void cargarddlNivelAcceso(int id = 0)
        {

            UsuarioNegocio negocio = new UsuarioNegocio();

            ddlNivelAcceso.DataSource = negocio.listarNivelesAcceso();
            ddlNivelAcceso.DataValueField = "Id";
            ddlNivelAcceso.DataTextField = "Descripcion";
            ddlNivelAcceso.DataBind();

            if (id > 0) ddlNivelAcceso.SelectedValue = id.ToString();
            else ddlNivelAcceso.SelectedValue = "2";//empleado x defecto

        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaciones())
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();


                    if(usu == null)
                    {
                        cl = new Cliente();

                        cl.Nombre = txtNombre.Text;
                        cl.Apellido = txtApellido.Text;
                        cl.DNI = txtDNI.Text;
                        cl.NombreUsuario = txtUsuario.Text;
                        cl.Contraseña = txtContraseña.Text;
                        cl.Mail = txtEmail.Text;
                        cl.Telefono = txtTelefono.Text;

                        negocio.agregarCliente(cl);

                        usu = new Usuario();
                        usu.NombreUsuario = cl.NombreUsuario;
                        usu.Contraseña = cl.Contraseña;
                    }
                    else if(usu.tipo == 1)//modifico cliente
                    {
                        cl.Nombre = txtNombre.Text;
                        cl.Apellido = txtApellido.Text;
                        cl.DNI = txtDNI.Text;
                        cl.NombreUsuario = txtUsuario.Text;
                        cl.Contraseña = txtContraseña.Text;
                        cl.Mail = txtEmail.Text;
                        cl.Telefono = txtTelefono.Text;
                        cl.Id = Convert.ToInt32(txtId.Text);

                        negocio.ModificarMiPerfilCliente(cl);
                    }
                    else //modifico empleado
                    {
                        emp.Nombre = txtNombre.Text;
                        emp.Apellido = txtApellido.Text;
                        emp.NombreUsuario = txtUsuario.Text;
                        emp.Contraseña = txtContraseña.Text;
                        emp.Id = Convert.ToInt32(txtId.Text);

                        negocio.ModificarMiPerfilEmpleado(emp);
                    }


                    if (negocio.Login(usu, cl, emp))
                    {
                        Session.Add("usuario", usu);

                        if (usu.tipo == 1)
                        {
                            Session.Add("cliente", cl);
                            Response.Redirect("Turnos.aspx", false);
                        }
                        else
                        {
                            Session.Add("empleado", emp);
                            Response.Redirect("Turnos.aspx", false);
                        }
                    }
                    else
                    {
                        lblMensage.Visible = true;
                        lblMensage.Text = "Ocurrió un error";
                    }

                    Response.Redirect("Default.aspx", false);
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
                UsuarioNegocio negocio = new UsuarioNegocio();

            if (usu == null || usu.tipo == 1)
            {
                if (txtDNI.Text.Trim() == null || txtDNI.Text.Trim() == "")
                {
                    lblMensage.Visible = true;
                    lblMensage.Text = "Debe completar el campo DNI";
                    return false;
                }
                else if (txtEmail.Text.Trim() == null || txtEmail.Text.Trim() == "")
                {
                    lblMensage.Visible = true;
                    lblMensage.Text = "Debe completar el campo Mail";
                    return false;
                }
                else if (txtTelefono.Text.Trim() == null || txtTelefono.Text.Trim() == "")
                {
                    lblMensage.Visible = true;
                    lblMensage.Text = "Debe completar el campo Teléfono";
                    return false;
                }
               

                if (negocio.existeUsuarioByDNI(txtDNI.Text,txtId.Text))
                {
                    lblMensage.Text = "el DNI ya existe en el sistema, ingrese otro ...";
                    return false;
                }
                else if (negocio.existeUsuarioByUsuario(txtUsuario.Text, txtId.Text))
                {
                    lblMensage.Text = "el Usuario ya existe en el sistema, ingrese otro ...";
                    return false;
                }
                else if (negocio.existeUsuarioByMail(txtEmail.Text, txtId.Text))
                {
                    lblMensage.Text = "el Mail ya existe en el sistema, ingrese otro ...";
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
                if (ddlNivelAcceso.SelectedValue == null)
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