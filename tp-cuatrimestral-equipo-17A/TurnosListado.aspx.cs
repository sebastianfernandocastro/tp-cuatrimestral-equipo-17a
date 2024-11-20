using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using System.Web.Services.Description;

namespace tp_cuatrimestral_equipo_17A
{
    public partial class TurnosListado : System.Web.UI.Page
    {
        TurnoNegocio turnoNegocio = new TurnoNegocio();
        private Cliente cl = null ;
        protected void Page_Load(object sender, EventArgs e)
        {
            cl = (Cliente)Session["Cliente"];
            if (!IsPostBack)
            {
                CargarTurnos();
            }
        }

        private void CargarTurnos()
        {
            try
            {
                string id = "";
                if (cl != null) id =  cl.Id.ToString();

                var turnos = turnoNegocio.Listar(id);

                if (turnos != null && turnos.Count > 0)
                {
                    if(!String.IsNullOrEmpty(txtFiltro.Text)) turnos = turnos.FindAll(x => x.Usuario.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.Servicio.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.Rubro.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.Vehiculo.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

                    gvTurnos.DataSource = turnos;
                    //gvTurnos.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    gvTurnos.DataBind();
                    lblMessage.Visible = false;

                    //gvTurnos.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                else
                {
                    gvTurnos.DataSource = null;
                    gvTurnos.DataBind();
                    lblMessage.Text = "No hay turnos registrados.";
                    lblMessage.CssClass = "text-warning";
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error al cargar los turnos: {ex.Message}";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }

        protected void gvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int idTurno = Convert.ToInt32(e.CommandArgument);

            Turno turno = turnoNegocio.ObtenerPorId(idTurno);
            if (e.CommandName == "Editar")
            {
                if (Session["empleado"] != null || turno.Estado.Id == 1) Response.Redirect("Turnos.aspx?id=" + idTurno, false);
                else
                {
                    lblMessage.Text = "No se puede editar el turno.";
                    lblMessage.CssClass = "text-warning";
                    lblMessage.Visible = true;
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {

                    if (Session["empleado"] != null || (Session["cliente"] != null && turno.Estado.Id == 1))
                    {
                        eliminar(idTurno);
                        lblMessage.Visible = true;
                        CargarTurnos(); // Refrescar el GridView

                        //Response.Redirect("TurnosListado.aspx", false);
                    }
                    else
                    {
                        lblMessage.Text = "No se puede Cancelar el turno.";
                        lblMessage.CssClass = "text-warning";
                        lblMessage.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Error al Cancelar el turno: {ex.Message}";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                }
            }
        }
        public void eliminar(int idTurno)
        {
            if (turnoNegocio.Eliminar(idTurno))
            {
                lblMessage.Text = "Turno cancelado correctamente.";
                lblMessage.CssClass = "text-success";
            }
            else
            {
                lblMessage.Text = "Error al cancelar el turno.";
                lblMessage.CssClass = "text-danger";
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            CargarTurnos();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["empleado"] != null) Response.Redirect("Turnos.aspx", false);
                else Response.Redirect("SeleccionarVehiculo.aspx", false);
            }
            catch (Exception ex)
            {
            }
        }
    }
}