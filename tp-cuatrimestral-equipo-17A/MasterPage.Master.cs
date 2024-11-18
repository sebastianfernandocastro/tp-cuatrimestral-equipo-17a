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
        public Empleado empleado { get; set; }
        public Cliente cliente { get; set; }
        public int sesionActiva { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Herramientas herramientas = new Herramientas();

            if (!(Page is Login || Page is Default || Page is About || Page is Registrarse))
            {
                if (!Herramientas.sesionActiva(Session["usuario"]))
                    Response.Redirect("Login.aspx", false);
                else if ((Page is Empleados || Page is FormularioEmpleado || Page is Clientes || Page is FormularioCliente) && !Herramientas.sesionActiva(Session["empleado"]))
                    Response.Redirect("Default.aspx", false);
            }

            if ((Empleado)Session["empleado"] != null)
            {
                empleado = (Empleado)Session["empleado"];
                if (herramientas.esAdmin(empleado))
                {
                    sesionActiva = 1;
                }
                else
                {
                    sesionActiva = 2;
                }

            }
            else if ((Cliente)Session["cliente"] != null)
            {
                cliente = (Cliente)Session["cliente"];
                sesionActiva = 3;
            }
            else
            {
                sesionActiva = 0;
            }
        }
        protected void CerrarSesion(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}