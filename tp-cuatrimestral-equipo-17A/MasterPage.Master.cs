using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace tp_cuatrimestral_equipo_17A
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public Usuario usu { get; set; }
        public int sesionActiva { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!(Page is Login || Page is Default || Page is About || Page is Registrarse))
            {
                if (!Herramientas.sesionActiva(Session["usuario"]))
                    Response.Redirect("Login.aspx", false);
                else if ((Page is Empleados || Page is FormularioEmpleado || Page is Clientes || Page is FormularioCliente) && !Herramientas.sesionActiva(Session["empleado"]))
                    Response.Redirect("Default.aspx", false);
            }

            if ((Usuario)Session["usuario"] != null)
            {
                usu = (Usuario)Session["usuario"];
                sesionActiva = usu.tipo;
            }
            else
            {
                sesionActiva = 0;
            }
        }
    }
}