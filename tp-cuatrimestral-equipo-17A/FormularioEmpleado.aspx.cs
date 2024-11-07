using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace tp_cuatrimestral_equipo_17A
{
    public partial class FormularioEmpleado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

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
                txtNivelAcceso.Text = emp.nivelAcceso.ToString();
                txtId.Text = emp.Id.ToString();

            }
            else if (!IsPostBack)
            {
                btnAgregar.Text = "Agregar";
            }
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Empleado empleado = new Empleado();
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                empleado.legajo = txtLegajo.Text;
                empleado.nivelAcceso = Convert.ToInt32(txtNivelAcceso.Text);
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
    }
}