using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        private bool soloNumeros(string cadena)
        {
            if (cadena.Contains(',')) cadena = cadena.Replace(",", "");
            foreach (char caracter in cadena)
            {

                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
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

                    if (ddlRubro != null && ddlRubro.SelectedValue != null && ddlRubro.SelectedValue.ToString() != "0") cargarServicioxIdRubroElegido();

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
                if (validaciones())
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
                        if (precioNegocio.ObtenerPrecioxCampos(precio.IdRubro, precio.IdServicio, precio.IdTipoVehiculo) == 0)
                        {
                            precioNegocio.AgregarPrecio(precio);
                            lblMensage.Text = "Precio agregado con éxito.";
                        }
                        else
                        {
                            lblMensage.Text = "El rubro, Servicio y tipo vehículo Seleccionado ya existe ...";
                            lblMensage.CssClass = "text-danger";
                            lblMensage.Visible = true;
                        }
                    }
                    else
                    {
                        precio.Id = int.Parse(txtId.Text);
                        precioNegocio.ModificarPrecio(precio);
                        lblMensage.Text = "Precio actualizado con éxito.";
                    }

                    lblMensage.CssClass = "text-success";
                    lblMensage.Visible = true;

                    Response.Redirect("Precios.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMensage.Text = "Error: " + ex.Message;
                lblMensage.CssClass = "text-danger";
                lblMensage.Visible = true;
            }
        }

        protected void ddlTipoVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void cargarServicioxIdRubroElegido()
        {
            int idRubro;
            txtPrecio.Text = "0";
            //ActualizarPrecio();
            if (int.TryParse(ddlRubro.SelectedValue, out idRubro) && idRubro > 0)
            {
                // Cargar servicios relacionados con el rubro seleccionado
                try
                {
                    ServicioNegocio servicioNegocio = new ServicioNegocio();
                    var servicios = servicioNegocio.ListarPorRubro(idRubro);

                    if (servicios != null && servicios.Count > 0)
                    {
                        ddlServicio.DataSource = servicios;
                        ddlServicio.DataTextField = "Nombre";
                        ddlServicio.DataValueField = "Id";
                        ddlServicio.DataBind();
                        ddlServicio.Items.Insert(0, new ListItem("Seleccione un servicio", "0"));
                    }
                    else
                    {
                        ddlServicio.Items.Clear();
                        ddlServicio.Items.Insert(0, new ListItem("No hay servicios disponibles", "0"));
                    }
                }
                catch (Exception ex)
                {
                    lblMensage.Text = "Error al cargar los servicios: " + ex.Message;
                    lblMensage.CssClass = "text-danger";
                    lblMensage.Visible = true;
                }
            }
            else
            {
                // Limpiar el dropdown si no se selecciona un rubro válido
                ddlServicio.Items.Clear();
                ddlServicio.Items.Insert(0, new ListItem("Seleccione un rubro primero", "0"));
            }
        }
        protected void ddlRubro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargarServicioxIdRubroElegido();
            }
            catch (Exception ex)
            {
                lblMensage.Text = "Error al cargar los servicios: " + ex.Message;
                lblMensage.CssClass = "text-danger";
                lblMensage.Visible = true;
            }
        }

        protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public bool validaciones()
        {
            try
            {
                if (ddlRubro == null || ddlRubro.SelectedValue == "0" || ddlRubro.SelectedValue == "")
                {
                    lblMensage.Text = "Debe seleccionar un Rubro";
                    lblMensage.CssClass = "text-danger";
                    lblMensage.Visible = true;
                    return false;
                }
                else if (ddlTipoVehiculo == null || ddlTipoVehiculo.SelectedValue == "0" || ddlTipoVehiculo.SelectedValue == "")
                {
                    lblMensage.Text = "Debe seleccionar un  Vehículo";
                    lblMensage.CssClass = "text-danger";
                    lblMensage.Visible = true;
                    return false;
                }
                else if (ddlServicio == null || ddlServicio.SelectedValue == "0" || ddlServicio.SelectedValue == "")
                {
                    lblMensage.Text = "Debe seleccionar un Servicio ";
                    lblMensage.CssClass = "text-danger";
                    lblMensage.Visible = true;
                    return false;
                }
                else if (String.IsNullOrEmpty(txtPrecio.Text))
                {
                    lblMensage.Text = "Precio incorrecto";
                    lblMensage.CssClass = "text-danger";
                    lblMensage.Visible = true;
                    return false;
                }
                else if (!soloNumeros(txtPrecio.Text))
                {

                    //decimal precio = decimal.Parse(txtPrecio.Text, System.Globalization.CultureInfo.CurrentCulture);
                    lblMensage.Text = "el campo Precio Debe ser númerico";
                    lblMensage.CssClass = "text-danger";
                    lblMensage.Visible = true;
                    return false;
                }
                else if (Convert.ToDecimal(txtPrecio.Text) < 0)
                {

                    lblMensage.Text = "el campo Precio Debe ser positivo";
                    lblMensage.CssClass = "text-danger";
                    lblMensage.Visible = true;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                lblMensage.Text = "Error al validar los datos ingresados : " + ex.Message;
                lblMensage.CssClass = "text-danger";
                lblMensage.Visible = true;
                return false;
            }
        }
    }
}