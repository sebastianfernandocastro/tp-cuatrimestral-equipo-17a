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
    public partial class FormularioPrecio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CargarTipoVehiculo();
                CargarRubros();
                CargarServicios();

                if (Request.QueryString["id"] != null)
                {
                    int id;
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        CargarDatosFormulario(id);
                    }
                    else
                    {
                        lblMensage.Text = "ID no válido para cargar los datos.";
                        lblMensage.Visible = true;
                    }
                }
            }
        }

        private void CargarTipoVehiculo()
        {
            TipoVehiculoNegocio tipoVehiculoNegocio = new TipoVehiculoNegocio();
            try
            {
                ddlTipoVehiculo.DataSource = tipoVehiculoNegocio.Listar(); 
                ddlTipoVehiculo.DataTextField = "Nombre"; 
                ddlTipoVehiculo.DataValueField = "Id";   
                ddlTipoVehiculo.DataBind();

                ddlTipoVehiculo.Items.Insert(0, new ListItem("Seleccione un tipo de vehículo", ""));
            }
            catch (Exception ex)
            {
                lblMensage.Text = "Error al cargar los tipos de vehículo: " + ex.Message;
                lblMensage.Visible = true;
            }
        }


        private void CargarRubros()
        {
            RubroNegocio rubroNegocio = new RubroNegocio();
            try
            {
                ddlRubro.DataSource = rubroNegocio.Listar(); 
                ddlRubro.DataTextField = "Nombre"; 
                ddlRubro.DataValueField = "Id";   
                ddlRubro.DataBind();

                ddlRubro.Items.Insert(0, new ListItem("Seleccione un rubro", ""));
            }
            catch (Exception ex)
            {
                lblMensage.Text = "Error al cargar los rubros: " + ex.Message;
                lblMensage.Visible = true;
            }
        }


        private void CargarServicios()
        {
            ServicioNegocio servicioNegocio = new ServicioNegocio();
            try
            {
                ddlServicio.DataSource = servicioNegocio.Listar();
                ddlServicio.DataTextField = "Nombre";
                ddlServicio.DataValueField = "Id";
                ddlServicio.DataBind();

                ddlServicio.Items.Insert(0, new ListItem("Seleccione un servicio", ""));
            }
            catch (Exception ex)
            {
                lblMensage.Text = "Error al cargar los servicios: " + ex.Message;
                lblMensage.Visible = true;
            }
        }

        private void CargarDatosFormulario(int id)
        {
            PrecioNegocio precioNegocio = new PrecioNegocio();
            try
            {
                Precio precio = precioNegocio.ListarPrecios().FirstOrDefault(p => p.Id == id);

                if (precio != null)
                {
                    txtId.Text = precio.Id.ToString();
                    ddlTipoVehiculo.SelectedValue = precio.IdTipoVehiculo.ToString();
                    ddlRubro.SelectedValue = precio.IdRubro.ToString();
                    ddlServicio.SelectedValue = precio.IdServicio.ToString();
                    txtPrecio.Text = precio.PrecioValor.ToString("F2");
                }
            }
            catch (Exception ex)
            {
                lblMensage.Text = "Error al cargar los datos: " + ex.Message;
                lblMensage.Visible = true;
            }
        }



        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            PrecioNegocio precioNegocio = new PrecioNegocio();
            try
            {
                Precio precio = new Precio
                {
                    IdTipoVehiculo = int.Parse(ddlTipoVehiculo.SelectedValue),
                    IdRubro = int.Parse(ddlRubro.SelectedValue),
                    IdServicio = int.Parse(ddlServicio.SelectedValue),
                    PrecioValor = decimal.Parse(txtPrecio.Text)
                };

                if (string.IsNullOrEmpty(txtId.Text)) 
                {
                    precioNegocio.AgregarPrecio(precio);
                    lblMensage.Text = "Precio agregado con éxito.";
                }
                else 
                {
                    precio.Id = int.Parse(txtId.Text);
                    precioNegocio.ModificarPrecio(precio);
                    lblMensage.Text = "Precio actualizado con éxito.";
                }

                lblMensage.CssClass = "text-success";
                lblMensage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensage.Text = "Error: " + ex.Message;
                lblMensage.CssClass = "text-danger";
                lblMensage.Visible = true;
            }
        }


    }
}