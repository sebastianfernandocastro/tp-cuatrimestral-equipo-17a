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
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

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
                    //negocio.ModificarCliente(Cliente);
                }
                else negocio.agregarCliente(Cliente);

                Response.Redirect("Clientes.aspx", false);
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }
    }
}