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
    public partial class Vehiculo : System.Web.UI.Page
    {
        private TipoVehiculoNegocio tipoVehiculoNegocio = new TipoVehiculoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTipoVehiculo();
            }
        }

        private void CargarTipoVehiculo()
        {
            try
            {
                List<TipoVehiculo> listaTipoVehiculo = new List<TipoVehiculo>();
                TipoVehiculoNegocio negocio = new TipoVehiculoNegocio();

                if (cbxInactivos.Checked) listaTipoVehiculo = negocio.Listar(1);
                else listaTipoVehiculo = negocio.Listar();

                if (!String.IsNullOrEmpty(txtFiltro.Text)) listaTipoVehiculo = listaTipoVehiculo.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.Descripcion.ToUpper().Contains(txtFiltro.Text.ToUpper()));

                dgvVehiculos.DataSource = listaTipoVehiculo;
                dgvVehiculos.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los tipo de Vehiculo: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }


        protected void dgvVehiculos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ToggleEstado")
            {
                try
                {

                    if (int.TryParse(e.CommandArgument.ToString(), out int id))
                    {
                        TipoVehiculo tipoVehiculo = tipoVehiculoNegocio.ObtenerPorId(id);
                        if (tipoVehiculo != null)
                        {
                            tipoVehiculo.Estado = tipoVehiculo.Estado == 1 ? 0 : 1;

                            if (tipoVehiculoNegocio.Modificar(tipoVehiculo))
                            {
                                lblMensaje.Text = tipoVehiculo.Estado == 1
                                    ? "tipo de Vehiculo activado correctamente."
                                    : "tipo de Vehiculo desactivado correctamente.";
                                lblMensaje.CssClass = "text-success";
                            }
                            else
                            {
                                lblMensaje.Text = "No se pudo actualizar el estado del tipo de Vehiculo.";
                                lblMensaje.CssClass = "text-danger";
                            }
                        }
                        else
                        {
                            lblMensaje.Text = "No se encontró el tipo de Vehiculo.";
                            lblMensaje.CssClass = "text-warning";
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "El ID del tipo de Vehiculo no es válido.";
                        lblMensaje.CssClass = "text-danger";
                    }
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al cambiar el estado del tipo de Vehiculo: " + ex.Message;
                    lblMensaje.CssClass = "text-danger";
                }
                finally
                {
                    lblMensaje.Visible = true;
                    CargarTipoVehiculo();
                }
            }
        }

        protected void dgvVehiculos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvVehiculos.SelectedDataKey.Value);
            Response.Redirect($"FormularioVehiculo.aspx?id={id}",false);
        }
        protected void dgvVehiculos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvVehiculos.PageIndex = e.NewPageIndex;
            CargarTipoVehiculo();
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string idValue = hfVehiculoId.Value;
                System.Diagnostics.Debug.WriteLine($"Valor del HiddenField: {idValue}");

                if (string.IsNullOrEmpty(idValue) || !int.TryParse(idValue, out int id))
                {
                    lblMensaje.Text = "Error: ID no válido.";
                    lblMensaje.CssClass = "text-danger";
                    lblMensaje.Visible = true;
                    return;
                }

                //tipoVehiculoNegocio tipoVehiculoNegocio = new tipoVehiculoNegocio();
                tipoVehiculoNegocio.Eliminar(id);

                lblMensaje.Text = "tipo de Vehiculo eliminado con éxito.";
                lblMensaje.CssClass = "text-success";
                lblMensaje.Visible = true;

                CargarTipoVehiculo();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar el tipo de Vehiculo: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CargarTipoVehiculo();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al filtrar tipo de Vehiculo: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }

        protected void cbxInactivos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CargarTipoVehiculo();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al filtrar tipo de Vehiculo: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
            }
        }
    }
}