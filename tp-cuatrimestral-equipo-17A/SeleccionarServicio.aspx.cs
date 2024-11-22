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
        private FechaHoraNegocio fechaHoraNegocio = new FechaHoraNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //DateTime today = DateTime.Today;
                    //DateTime nextSunday = today.AddDays(7 - (int)today.DayOfWeek);

                    //string todayFormatted = today.ToString("yyyy-MM-dd");
                    //string tomorrowFormatted = nextSunday.ToString("yyyy-MM-dd");

                    //calendarInput.Attributes["min"] = todayFormatted;
                    //calendarInput.Attributes["max"] = tomorrowFormatted;
                    //calendarInput.Text = todayFormatted;


                    DateTime dFechaHoy = DateTime.Today;
                    DateTime dFechaHasta = dFechaHoy.AddDays(2);

<<<<<<< Updated upstream
                    if (dFechaHasta.DayOfWeek == DayOfWeek.Sunday)
                    {
                        dFechaHasta = dFechaHasta.AddDays(-1); 
=======
                    // Si el día máximo cae en domingo, reduce el rango
                    if (dFechaHasta.DayOfWeek == DayOfWeek.Sunday)
                    {
                        dFechaHasta = dFechaHasta.AddDays(-1); // Ajusta al sábado
>>>>>>> Stashed changes
                    }

                    string sFechHoy = dFechaHoy.ToString("yyyy-MM-dd");
                    string sFechMax = dFechaHasta.ToString("yyyy-MM-dd");

                    calendarInput.Attributes["min"] = sFechHoy;
                    calendarInput.Attributes["max"] = sFechMax;
                    calendarInput.Text = sFechHoy; 


                    CargarHorariosDisponibles(DateTime.Now.Date);



<<<<<<< Updated upstream
=======


                    //ddlHora.SelectedValue = fechaHoraNegocio.ObtenerIdFechaHoraxHora(turno.Hora).ToString();

>>>>>>> Stashed changes
                    if (Session["IdRubro"] != null)
                    {
                        ddlServicio.DataSource = servicioNegocio.ListarPorRubro((int)Session["IdRubro"]);
                        ddlServicio.DataTextField = "Nombre";
                        ddlServicio.DataValueField = "Id";
                        ddlServicio.DataBind();
                        ddlServicio.Items.Insert(0, new ListItem("Seleccione un servicio", "0"));
                    }
                    else
                    {
                        lblMessage.Text = $"Error al obtener el rubro";
                        lblMessage.CssClass = "text-danger";
                        lblMessage.Visible = true;
                    }

                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Error al cargar la página: {ex.Message}";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                }
            }

        }

        private void CargarHorariosDisponibles(DateTime fecha)
        {
            try
            {
                var horarios = fechaHoraNegocio.ListarHoraDisponible(fecha);

                if (horarios != null && horarios.Count > 0)
                {
                    ddlHora.DataSource = horarios
                        .Select(h => new
                        {
                            Text = h.Hora.ToString(@"hh\:mm"), // Muestra solo la hora
                            Value = h.Id // Usamos el ID para referencias posteriores
                        });

                    ddlHora.DataTextField = "Text";
                    ddlHora.DataValueField = "Value";
                    ddlHora.DataBind();
                }

                ddlHora.Items.Insert(0, new ListItem("Seleccione un horario", "0"));
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error al cargar horarios: {ex.Message}";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }

        protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void calendarInput_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (!String.IsNullOrEmpty(calendarInput.Text)) CargarHorariosDisponibles(Convert.ToDateTime(calendarInput.Text));
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error al cargar fecha: {ex.Message}";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }

        protected void btnReservar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaciones())
                {
                    Session["servicio"] = ddlServicio.SelectedValue;
                    Session["hora"] = ddlHora.SelectedValue;
                    Session["fecha"] = calendarInput.Text;

                    Response.Redirect("Turnos.aspx?clturn=1", false);
                }
               
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error al Reservar: {ex.Message}";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }
        public bool validaciones()
        {
            try
            {
                if (Session["IdRubro"] == null)
                {
                    lblMessage.Text = "Error al obtener Rubro";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                    return false;
                }
                else if (Session["IdTipoVehiculo"] == null)
                {
                    lblMessage.Text = "Error al obtener el vehículo";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                    return false;
                }
                else if(ddlServicio == null || ddlServicio.SelectedValue == null || ddlServicio.SelectedValue == "0")
                {
                    lblMessage.Text = "Debe seleccionar un servicio";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                    return false;
                }
                else if(ddlHora == null || ddlHora.SelectedValue == null || ddlHora.SelectedValue == "0")
                {
                    lblMessage.Text = "Debe seleccionar un horario";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                    return false;
                }
                else if (String.IsNullOrEmpty(calendarInput.Text) || Convert.ToDateTime(calendarInput.Text) == DateTime.MinValue)
                {
                    lblMessage.Text = "Fecha inválida";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}