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
    public partial class FormularioCliente : System.Web.UI.Page
    {
        string id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

            UsuarioNegocio negocio = new UsuarioNegocio();
            if (id != "" && !IsPostBack)
            {
                Cliente cl = negocio.ObtenerClienteById(Convert.ToInt32(id));
                btnAgregar.Text = "Modificar";
                txtApellido.Text = cl.Apellido;
                txtContraseña.Text = cl.Contraseña;
                txtDNI.Text = cl.DNI;
                txtUsuario.Text = cl.NombreUsuario;
                txtNombre.Text = cl.Nombre;
                txtTelefono.Text = cl.Telefono.ToString();
                txtEmail.Text = cl.Mail.ToString();
                txtId.Text = cl.Id.ToString();

            }
            else if (!IsPostBack)
            {
                btnAgregar.Text = "Agregar";
            }
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Cliente Cliente = new Cliente();
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                if (!validaciones())
                {
                    lblMensage.Visible = true;
                    return;
                }

                Cliente.DNI = txtDNI.Text;
                Cliente.Telefono = txtTelefono.Text;
                Cliente.Mail= txtEmail.Text;
                Cliente.Apellido = txtApellido.Text;
                Cliente.Nombre = txtNombre.Text;
                Cliente.NombreUsuario = txtUsuario.Text;
                Cliente.Contraseña = txtContraseña.Text;

                if (!String.IsNullOrEmpty(txtId.Text))
                {
                    Cliente.Id = Convert.ToInt32(txtId.Text);
                    negocio.ModificarMiPerfilCliente(Cliente);
                }
                else negocio.agregarCliente(Cliente);

                Response.Redirect("Clientes.aspx", false);
            }
            catch (Exception ex)
            {

                throw ex;
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

            if (negocio.existeUsuarioByDNI(txtDNI.Text, id))
            {
                lblMensage.Text = "el DNI ya existe en el sistema, ingrese otro ...";
                return false;
            }
            else if (negocio.existeUsuarioByUsuario(txtUsuario.Text, id))
            {
                lblMensage.Text = "el Usuario ya existe en el sistema, ingrese otro ...";
                return false;
            }
            else if (negocio.existeUsuarioByMail(txtEmail.Text, id))
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
    }
}