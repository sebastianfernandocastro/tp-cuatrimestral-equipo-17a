using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_cuatrimestral_equipo_17A
{
    public partial class Turnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnReservar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string email = txtEmail.Text;
            string fecha = txtFecha.Text;
            string hora = ddlHora.SelectedValue;
            

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fecha))
            {
                lblMessage.Text = "Todos los campos son obligatorios.";
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Turno reservado con éxito.";
                lblMessage.CssClass = "text-success";
                lblMessage.Visible = true;
            }
        }

    }
}