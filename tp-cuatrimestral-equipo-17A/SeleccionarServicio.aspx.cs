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
    public partial class SeleccionarServicio : System.Web.UI.Page
    {
        private TurnoNegocio turnoNegocio = new TurnoNegocio();
        private Turno turno = null;
        private ServicioNegocio servicioNegocio = new ServicioNegocio();
        private Herramientas herramientos = new Herramientas();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime today = DateTime.Today;
                DateTime nextSunday = today.AddDays(7 - (int)today.DayOfWeek);

                string todayFormatted = today.ToString("yyyy-MM-dd");
                string tomorrowFormatted = nextSunday.ToString("yyyy-MM-dd");

                calendarInput.Attributes["min"] = todayFormatted;
                calendarInput.Attributes["max"] = tomorrowFormatted;
            }



            if (Session["IdRubro"] != null)
            {
                ddlServicio.DataSource = servicioNegocio.ListarPorRubro((int)Session["IdRubro"]);
                ddlServicio.DataTextField = "Nombre";
                ddlServicio.DataValueField = "Id";
                ddlServicio.DataBind();
                ddlServicio.Items.Insert(0, new ListItem("Seleccione un servicio", "0"));
            }

            
            
        }

        protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}