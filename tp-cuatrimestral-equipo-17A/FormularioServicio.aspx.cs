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
    public partial class FormularioServicio : System.Web.UI.Page
    {
        private ServicioNegocio servicioNegocio = new ServicioNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarRubros();
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarServicio(id);
                    lblTitulo.Text = "Modificar Servicio";
                }
                else
                {
                    lblTitulo.Text = "Agregar Servicio";
                }
            }
        }
        private void CargarServicio(int id)
        {
            try
            {
                Servicio servicio = servicioNegocio.ObtenerPorId(id); // Asegúrate de tener este método en ServicioNegocio
                if (servicio != null)
                {
                    txtNombre.Text = servicio.Nombre;
                    txtDescripcion.Text = servicio.Descripcion;
                    txtTiempo.Text = servicio.Tiempo.ToString("F2");
                    ddlEstado.SelectedValue = servicio.Estado.ToString();
                }
                else
                {
                    MostrarMensaje("El servicio no existe.", "text-danger");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar el servicio: " + ex.Message, "text-danger");
            }
        }
        private void CargarRubros()
        {
            try
            {
                RubroNegocio rubroNegocio = new RubroNegocio();
                List<Rubro> listaRubros = rubroNegocio.ListarActivos();
                ddlRubro.DataSource = listaRubros;
                ddlRubro.DataTextField = "Nombre";
                ddlRubro.DataValueField = "Id";
                ddlRubro.DataBind();
                ddlRubro.Items.Insert(0, new ListItem("-- Seleccione un Rubro --", "0"));
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar los rubros: " + ex.Message, "text-danger");
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlRubro.SelectedValue == "0")
                {
                    MostrarMensaje("Debe seleccionar un rubro.", "text-danger");
                    return;
                }
                Servicio servicio = new Servicio
                {
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Tiempo = decimal.Parse(txtTiempo.Text.Trim()),
                    Estado = int.Parse(ddlEstado.SelectedValue)
                };
                int idRubro = int.Parse(ddlRubro.SelectedValue);
                if (Request.QueryString["id"] != null)
                {
                    servicio.Id = int.Parse(Request.QueryString["id"]);
                    servicioNegocio.Modificar(servicio, idRubro);
                    MostrarMensaje("Servicio actualizado correctamente.", "text-success");
                }
                else
                {
                    servicioNegocio.Agregar(servicio, idRubro);
                    MostrarMensaje("Servicio agregado correctamente.", "text-success");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al guardar el servicio: " + ex.Message, "text-danger");
            }
        }
        private void MostrarMensaje(string mensaje, string cssClass)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = cssClass;
            lblMensaje.Visible = true;
        }
    }
}