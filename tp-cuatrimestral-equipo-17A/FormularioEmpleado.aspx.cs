using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using System.Collections;

namespace tp_cuatrimestral_equipo_17A
{
    public partial class FormularioEmpleado : System.Web.UI.Page
    {
        string id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

            UsuarioNegocio negocio = new UsuarioNegocio();
            if(id != "" && !IsPostBack)
            {
                Empleado emp = negocio.ObtenerEmpleadoById(Convert.ToInt32(id));
                btnAgregar.Text = "Modificar";
                txtApellido.Text = emp.Apellido;
                txtContraseña.Text = emp.Contraseña;
                txtLegajo.Text = emp.legajo;
                txtUsuario.Text = emp.NombreUsuario;
                txtNombre.Text = emp.Nombre;
                txtId.Text = emp.Id.ToString();

                btnAgregar.Text = "Modificar";

                cargarddlNivelAcceso(emp.nivelAcceso.Id);

            }
            else if (!IsPostBack)
            {
                btnAgregar.Text = "Agregar";
                cargarddlNivelAcceso();
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
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Empleado empleado = new Empleado();
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                if (!validaciones())
                {
                    lblMensage.Visible = true;
                    return;
                }

                empleado.legajo = txtLegajo.Text;
                
                empleado.nivelAcceso = new NivelAcceso();
                empleado.nivelAcceso.Id = Convert.ToInt32(ddlNivelAcceso.SelectedValue);
                empleado.Apellido = txtApellido.Text;
                empleado.Nombre= txtNombre.Text;
                empleado.NombreUsuario= txtUsuario.Text;
                empleado.Contraseña= txtContraseña.Text;

                if (!String.IsNullOrEmpty(txtId.Text))
                {
                    empleado.Id = Convert.ToInt32(txtId.Text);
                    negocio.ModificarEmpleado(empleado);
                }
                else negocio.agregarEmpleado(empleado);

                Response.Redirect("Empleados.aspx",false);
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
            else if (String.IsNullOrEmpty(txtLegajo.Text))
            {
                lblMensage.Text = "El campo Legajo no puede estar vacío";
                return false;
            }
            else if(txtLegajo.Text.Length > 5)
            {
                lblMensage.Text = "El campo Legajo tiene máximo 5 carácteres";
                return false;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();

            if (negocio.existeUsuarioByLegajo(txtLegajo.Text,id))
            {
                lblMensage.Text = "el Legajo ya existe en el sistema, ingrese otro ...";
                return false;
            }
            else if (negocio.existeUsuarioByUsuario(txtUsuario.Text, id))
            {
                lblMensage.Text = "el Usuario ya existe en el sistema, ingrese otro ...";
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