using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;
using System.Web.Services.Description;



namespace tp_cuatrimestral_equipo_17A
{
    public partial class Turnos : System.Web.UI.Page
    {
        private TurnoNegocio turnoNegocio = new TurnoNegocio();
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        private TipoVehiculoNegocio tipoVehiculoNegocio = new TipoVehiculoNegocio();
        private RubroNegocio rubroNegocio = new RubroNegocio();
        private ServicioNegocio servicioNegocio = new ServicioNegocio();
        private FechaHoraNegocio fechaHoraNegocio = new FechaHoraNegocio();
        private EstadoTurnosNegocio EstadoTurnoNegocio = new EstadoTurnosNegocio();
        private string id = null;
        private string clturn = null;
        private Usuario usu = null;
        private Turno turno = null;
        private Cliente cli = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            usu = (Usuario)Session["usuario"];
            cli = (Cliente)Session["cliente"];

            id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
            // si viene desde turnos cliente
            clturn = Request.QueryString["clturn"] != null ? Request.QueryString["clturn"].ToString() : "";

            if (!Page.IsPostBack)
            {
                try
                {
                    CargarListas();
                    txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    CargarHorariosDisponibles(DateTime.Now.Date);
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error al cargar la página: " + ex.Message;
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                }
            }

            if (!IsPostBack && !String.IsNullOrEmpty(id))
            {

                try
                {
                    turno = turnoNegocio.ObtenerPorId(Convert.ToInt32(id));

                    if (turno != null)
                    {
                        ddlVehiculo.SelectedValue = turno.Vehiculo.Id.ToString();
                        ddlRubro.SelectedValue = turno.Rubro.Id.ToString();

                        if (ddlRubro != null && Convert.ToInt32(ddlRubro.SelectedValue) > 0) cargarServicioxIdRubroElegido();

                        ddlUsuario.SelectedValue = turno.Usuario.Id.ToString();
                        ddlServicio.SelectedValue = turno.Servicio.Id.ToString();
                        ddlEstado.SelectedValue = turno.Estado.Id.ToString();
                        txtFecha.Text = turno.Fecha.ToString("yyyy-MM-dd");
                        //ddlFechaHora.SelectedValue = turno.Hora.ToString();
                        ddlFechaHora.SelectedValue = fechaHoraNegocio.ObtenerIdFechaHoraxHora(turno.Hora).ToString();
                        txtPrecio.Text = turno.Precio.ToString();
                        txtAclaraciones.Text = turno.Aclaracion;

                        if (usu.tipo == 1)
                        {
                            ddlEstado.Enabled = false;
                            ddlUsuario.Enabled = false;
                        }
                        else
                        {
                            ddlEstado.Enabled = true;
                            ddlUsuario.Enabled = true;
                        }

                        btnAgregar.Text = "Modifica";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error al cargar  el turno: " + ex.Message;
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                }
                //cargarPrecio();
            }
            else if (!IsPostBack && !String.IsNullOrEmpty(clturn) && clturn == "1")
            {
                try
                {
                    if (Session["IdTipoVehiculo"] != null && Session["IdRubro"] != null && Session["servicio"] != null && Session["fecha"] != null && Session["hora"] != null)
                    {
                        ddlUsuario.SelectedValue = usu.Id.ToString();
                        ddlVehiculo.SelectedValue = Session["IdTipoVehiculo"].ToString();
                        ddlRubro.SelectedValue = Session["IdRubro"].ToString();

                        ddlServicio.SelectedValue = Session["servicio"].ToString();
                        txtFecha.Text = Session["fecha"].ToString();

                        CargarHorariosDisponibles(Convert.ToDateTime(Session["fecha"]));

                        ddlFechaHora.SelectedValue = Session["hora"].ToString();

                        ddlEstado.SelectedValue = "1";

                        cargarPrecio();

                        ddlEstado.Enabled = false;
                        ddlUsuario.Enabled = false;
                    }
                    else
                    {
                        lblMessage.Text = "Error al cargar datos del turno ";
                        lblMessage.CssClass = "text-danger";
                        lblMessage.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error al cargar el turno: " + ex.Message;
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                }
            }

        }


        public void cargarPrecio()
        {
            try
            {
                if (ddlRubro.SelectedValue != null && Convert.ToInt32(ddlRubro.SelectedValue) > 0
                    && ddlServicio.SelectedValue != null && Convert.ToInt32(ddlServicio.SelectedValue) > 0
                    && ddlVehiculo.SelectedValue != null && Convert.ToInt32(ddlVehiculo.SelectedValue) > 0)
                {
                    int idServicio = Convert.ToInt32(ddlServicio.SelectedValue);
                    int idRubro = Convert.ToInt32(ddlRubro.SelectedValue);
                    int idVehiculo = Convert.ToInt32(ddlVehiculo.SelectedValue);
                    txtPrecio.Text = turnoNegocio.obtenerPrecio(idServicio, idRubro, idVehiculo).ToString();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error al cargar el precio: {ex.Message}";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }

        private void CargarListas()
        {
            try
            {
                // Cargar usuarios
                ddlUsuario.DataSource = usuarioNegocio.ListarClientes();
                ddlUsuario.DataTextField = "Nombre"; // Campo visible en el dropdown
                ddlUsuario.DataValueField = "Id";   // Valor oculto (ID)
                ddlUsuario.DataBind();
                ddlUsuario.Items.Insert(0, new ListItem("Seleccione un usuario", "0"));

                // Cargar tipos de vehículos
                ddlVehiculo.DataSource = tipoVehiculoNegocio.Listar();
                ddlVehiculo.DataTextField = "Nombre";
                ddlVehiculo.DataValueField = "Id";
                ddlVehiculo.DataBind();
                ddlVehiculo.Items.Insert(0, new ListItem("Seleccione un vehículo", "0"));

                // Cargar rubros
                ddlRubro.DataSource = rubroNegocio.Listar();
                ddlRubro.DataTextField = "Nombre"; // Campo que muestra el nombre del rubro
                ddlRubro.DataValueField = "Id";   // ID del rubro
                ddlRubro.DataBind();
                ddlRubro.Items.Insert(0, new ListItem("Seleccione un rubro", "0"));

                // Cargar servicios
                ddlServicio.DataSource = servicioNegocio.Listar();
                ddlServicio.DataTextField = "Nombre"; // Campo que muestra el nombre del servicio
                ddlServicio.DataValueField = "Id";   // ID del servicio
                ddlServicio.DataBind();
                ddlServicio.Items.Insert(0, new ListItem("Seleccione un servicio", "0"));

                // Cargar Estado Turnos
                ddlEstado.DataSource = EstadoTurnoNegocio.Listar();
                ddlEstado.DataTextField = "descripcion"; // Campo que muestra el nombre del Estado
                ddlEstado.DataValueField = "Id";   // ID del Estado
                ddlEstado.DataBind();
                ddlEstado.Items.Insert(0, new ListItem("Seleccione un Estado", "0"));
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al cargar las listas: " + ex.Message;
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
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
                    lblMessage.Text = "Error al cargar los servicios: " + ex.Message;
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
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
                lblMessage.Text = "Error al cargar los servicios: " + ex.Message;
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }

        }

        protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarPrecio();
        }

        protected void ddlVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarPrecio();
        }

        //private void ActualizarPrecio()
        //{
        //    try
        //    {

        //        int idVehiculo = int.Parse(ddlVehiculo.SelectedValue);
        //        int idRubro = int.Parse(ddlRubro.SelectedValue);
        //        int idServicio = int.Parse(ddlServicio.SelectedValue);


        //        if (idVehiculo > 0 && idRubro > 0 && idServicio > 0)
        //        {

        //            TurnoNegocio turnoNegocio = new TurnoNegocio();
        //            decimal precio = turnoNegocio.obtenerPrecio(idVehiculo, idRubro, idServicio);


        //            txtPrecio.Text = precio.ToString();
        //        }
        //        else
        //        {
        //            txtPrecio.Text = string.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Error al actualizar el precio: " + ex.Message;
        //        lblMessage.CssClass = "text-danger";
        //        lblMessage.Visible = true;
        //    }
        //}

        private void CargarHorariosDisponibles(DateTime fecha)
        {
            try
            {
                var horarios = fechaHoraNegocio.ListarHoraDisponible(fecha, id);

                if (horarios != null && horarios.Count > 0)
                {
                    ddlFechaHora.DataSource = horarios
                        .Select(h => new
                        {
                            Text = h.Hora.ToString(@"hh\:mm"), // Muestra solo la hora
                            Value = h.Id // Usamos el ID para referencias posteriores
                        });

                    ddlFechaHora.DataTextField = "Text";
                    ddlFechaHora.DataValueField = "Value";
                    ddlFechaHora.DataBind();
                }

                ddlFechaHora.Items.Insert(0, new ListItem("Seleccione un horario", "0"));
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error al cargar horarios: {ex.Message}";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }

        public bool validaciones()
        {
            bool rta = true;
            try
            {
                if (ddlUsuario.SelectedValue == null || int.Parse(ddlUsuario.SelectedValue) == 0)
                {
                    lblMessage.Text = "Debe seleccionar un usuario.";
                    lblMessage.CssClass = "text-danger";
                    return false;
                }
                else if (ddlVehiculo.SelectedValue == null || int.Parse(ddlVehiculo.SelectedValue) == 0)
                {
                    lblMessage.Text = "Debe seleccionar un vehículo.";
                    lblMessage.CssClass = "text-danger";
                    return false;
                }
                else if (ddlRubro.SelectedValue == null || int.Parse(ddlRubro.SelectedValue) == 0)
                {
                    lblMessage.Text = "Debe seleccionar un rubro.";
                    lblMessage.CssClass = "text-danger";
                    return false;
                }
                else if (ddlServicio.SelectedValue == null || int.Parse(ddlServicio.SelectedValue) == 0)
                {
                    lblMessage.Text = "Debe seleccionar un servicio.";
                    lblMessage.CssClass = "text-danger";
                    return false;
                }
                else if (ddlEstado.SelectedValue == null || int.Parse(ddlEstado.SelectedValue) == 0)
                {
                    lblMessage.Text = "Debe seleccionar un estado.";
                    lblMessage.CssClass = "text-danger";
                    return false;
                }
                else if (String.IsNullOrEmpty(txtPrecio.Text))
                {
                    lblMessage.Text = "Error en el precio.";
                    lblMessage.CssClass = "text-danger";
                    return false;
                }
                else if (String.IsNullOrEmpty(txtFecha.Text) || DateTime.MinValue == Convert.ToDateTime(txtFecha.Text))
                {
                    lblMessage.Text = "Debe seleccionar una fecha";
                    lblMessage.CssClass = "text-danger";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al validar el turno.";
                lblMessage.CssClass = "text-danger";
                return false;
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                //hacer validaciones de valores de los dropdown y textbox, que sea un valor no nullo o distinto de 0 el value.

                if (validaciones())
                {

                    Turno turno = new Turno();
                    turno.Usuario = new Usuario { Id = int.Parse(ddlUsuario.SelectedValue) };
                    turno.Vehiculo = new TipoVehiculo { Codigo = int.Parse(ddlVehiculo.SelectedValue) };
                    turno.Rubro = new Rubro { Id = int.Parse(ddlRubro.SelectedValue) };
                    turno.Servicio = new Servicio { Id = int.Parse(ddlServicio.SelectedValue) };
                    turno.Fecha = armarFecha();
                    turno.Estado = new EstadoTurnos { Id = int.Parse(ddlEstado.SelectedValue) };
                    turno.Aclaracion = txtAclaraciones.Text;
                    turno.Precio = Convert.ToDecimal(txtPrecio.Text);

                    if (String.IsNullOrEmpty(id))
                    {
                        if (turnoNegocio.Agregar(turno))
                        {
                            lblMessage.Text = "Turno agregado correctamente.";
                            lblMessage.CssClass = "text-success";
                            //LimpiarFormulario(); // Limpiar el formulario

                            Response.Redirect("TurnosListado.aspx", false);
                        }
                        else
                        {
                            lblMessage.Text = "Error al agregar el turno.";
                            lblMessage.CssClass = "text-danger";
                        }
                    }
                    else
                    {
                        turno.Id = Convert.ToInt32(id);

                        if (turnoNegocio.Modificar(turno))
                        {
                            lblMessage.Text = "Turno modificado correctamente.";
                            lblMessage.CssClass = "text-success";
                            //btnModificar.Visible = false;
                            //btnAgregar.Visible = true;

                            Response.Redirect("TurnosListado.aspx", false);
                        }
                        else
                        {
                            lblMessage.Text = "Error al modificar el turno.";
                            lblMessage.CssClass = "text-danger";
                        }
                    }

                }

                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que el HiddenField tiene un ID válido
                if (string.IsNullOrEmpty(hfTurnoId.Value))
                {
                    lblMessage.Text = "No se seleccionó un turno para modificar.";
                    lblMessage.CssClass = "text-warning";
                    lblMessage.Visible = true;
                    return;
                }

                Turno turnoEditado = new Turno
                {
                    Id = int.Parse(hfTurnoId.Value),
                    Usuario = new Usuario { Id = int.Parse(ddlUsuario.SelectedValue) },
                    Vehiculo = new TipoVehiculo { Codigo = int.Parse(ddlVehiculo.SelectedValue) },
                    Rubro = new Rubro { Id = int.Parse(ddlRubro.SelectedValue) },
                    Servicio = new Servicio { Id = int.Parse(ddlServicio.SelectedValue) },
                    Fecha = DateTime.Parse(ddlFechaHora.SelectedValue),
                    Estado = new EstadoTurnos { Id = int.Parse(ddlEstado.SelectedValue) }
                };

                if (turnoNegocio.Modificar(turnoEditado))
                {
                    lblMessage.Text = "Turno modificado correctamente.";
                    lblMessage.CssClass = "text-success";
                    //btnModificar.Visible = false;
                    //btnAgregar.Visible = true;
                }
                else
                {
                    lblMessage.Text = "Error al modificar el turno.";
                    lblMessage.CssClass = "text-danger";
                }

                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error al guardar los cambios: {ex.Message}";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Herramientas herramientas = new Herramientas();
                //validarFechaCliente();
                if (herramientas.esAdmin(Session["Empleado"]))
                {
                    validarFechaAdmin();
                }
                else
                {
                    validarFechaCliente();
                }

                if (!String.IsNullOrEmpty(txtFecha.Text)) CargarHorariosDisponibles(Convert.ToDateTime(txtFecha.Text));
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error al verificar fecha: {ex.Message}";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }


        }

        public void validarFechaCliente()
        {
            try
            {
                bool validado = true;
                DateTime dFecha = Convert.ToDateTime(txtFecha.Text);
                DateTime dFechaValidacion = DateTime.Now.Date;
                DateTime dFechaDosDias = DateTime.Now.AddDays(2).Date;

                if (dFecha.DayOfWeek != DayOfWeek.Sunday) // es domingo
                {
                    if (dFecha >= dFechaValidacion)
                    {
                        if (dFecha > dFechaDosDias)
                        {
                            lblMessage.Text = $"Error al verificar fecha: Solo puede sacar turnos a dos días posteriores a la fecha actual";
                            lblMessage.CssClass = "text-danger";
                            lblMessage.Visible = true;
                            validado = false;
                        }
                    }
                    else
                    {
                        lblMessage.Text = $"Error al verificar fecha: la Fecha debe ser mayor a la actual ";
                        lblMessage.CssClass = "text-danger";
                        lblMessage.Visible = true;
                        validado = false;
                    }
                }
                else
                {
                    lblMessage.Text = $"Error al verificar fecha: Solo puede sacar turnos de Lunes a Sábados";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                    validado = false;
                }

                if (!validado) txtFecha.Text = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {

            }
        }
        public void validarFechaAdmin()
        {
            try
            {
                bool validado = true;
                DateTime dFecha = Convert.ToDateTime(txtFecha.Text);
                DateTime dFechaDosDias = DateTime.Now.AddDays(2).Date;


                if (dFecha.DayOfWeek != DayOfWeek.Sunday) // es domingo
                {
                    if (dFecha > dFechaDosDias)
                    {
                        lblMessage.Text = $"Error al verificar fecha: Solo puede sacar turnos a dos días posteriores a la fecha actual";
                        lblMessage.CssClass = "text-danger";
                        lblMessage.Visible = true;
                        validado = false;
                    }
                }
                else
                {
                    lblMessage.Text = $"Error al verificar fecha: Solo puede sacar turnos de Lunes a Sábados";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                    validado = false;
                }


                if (!validado) txtFecha.Text = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {

            }
        }
        public DateTime armarFecha()
        {
            DateTime fech = DateTime.Now;
            try
            {


                DateTime fechaBase = DateTime.Parse(txtFecha.Text);
                string horaSeleccionada = ddlFechaHora.SelectedItem.Text;
                TimeSpan hora = TimeSpan.Parse(horaSeleccionada);

                DateTime fechaCompleta = fechaBase.Add(hora);

                return fechaCompleta;

            }
            catch (Exception ex)
            {

            }
            return fech;
        }

    }
}
