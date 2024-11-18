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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarTurnos();
                CargarListas();
                CargarHorariosDisponibles();
            }
            if(!IsPostBack && !String.IsNullOrEmpty(id))
            {
                //cargar precio ....
                cargarPrecio();
            }

        }

        private void CargarTurnos()
        {
            try
            {
                var turnos = turnoNegocio.Listar(); 

                if (turnos != null && turnos.Count > 0)
                {
                    gvTurnos.DataSource = turnos;
                    gvTurnos.DataBind();
                    lblMessage.Visible = false; 
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
        public void cargarPrecio()
        {
            try
            {
                if(ddlRubro.SelectedValue != null && Convert.ToInt32(ddlRubro.SelectedValue) > 0
                    &&   ddlServicio.SelectedValue != null && Convert.ToInt32(ddlServicio.SelectedValue) > 0
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

        protected void ddlRubro_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idRubro;
            ActualizarPrecio();
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

        protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarPrecio();
        }

        protected void ddlVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarPrecio();
        }

        private void ActualizarPrecio()
        {
            try
            {
               
                int idVehiculo = int.Parse(ddlVehiculo.SelectedValue);
                int idRubro = int.Parse(ddlRubro.SelectedValue);
                int idServicio = int.Parse(ddlServicio.SelectedValue);

                
                if (idVehiculo > 0 && idRubro > 0 && idServicio > 0)
                {
                   
                    TurnoNegocio turnoNegocio = new TurnoNegocio();
                    decimal precio = turnoNegocio.obtenerPrecio(idVehiculo, idRubro, idServicio);

                   
                    txtPrecio.Text = precio.ToString("C");
                }
                else
                {
                    txtPrecio.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al actualizar el precio: " + ex.Message;
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }

        protected void ddlRubro_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idRubro;
            ActualizarPrecio();
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

        private void CargarHorariosDisponibles()
        {
            try
            {
                var horarios = fechaHoraNegocio.Listar();

                if (horarios != null && horarios.Count > 0)
                {
                    ddlFechaHora.DataSource = horarios
                        .Where(h => h.Disponible) // Filtra los horarios disponibles
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


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Turno nuevoTurno = new Turno
                {
                    Usuario = new Usuario { Id = int.Parse(ddlUsuario.SelectedValue) },
                    Vehiculo = new TipoVehiculo { Codigo = int.Parse(ddlVehiculo.SelectedValue) },
                    Rubro = new Rubro { Id = int.Parse(ddlRubro.SelectedValue) },
                    Servicio = new Servicio { Id = int.Parse(ddlServicio.SelectedValue) },
                    FechaHora = new FechaHora { Fecha = DateTime.Parse(ddlFechaHora.SelectedValue) },
                    Estado = new EstadoTurnos { Id = int.Parse(ddlEstado.SelectedValue) } 
                };

                if (turnoNegocio.Agregar(nuevoTurno))
                {
                    lblMessage.Text = "Turno agregado correctamente.";
                    lblMessage.CssClass = "text-success";
                    //LimpiarFormulario(); // Limpiar el formulario
                }
                else
                {
                    lblMessage.Text = "Error al agregar el turno.";
                    lblMessage.CssClass = "text-danger";
                }

                lblMessage.Visible = true;
                CargarTurnos(); 
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }


        protected void gvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int idTurno = Convert.ToInt32(e.CommandArgument);

                Turno turno = turnoNegocio.ObtenerPorId(idTurno);

                if (turno != null)
                {
                    // Cargar los datos en el formulario
                    ddlUsuario.SelectedValue = turno.Usuario.Id.ToString();
                    ddlVehiculo.SelectedValue = turno.Vehiculo.Codigo.ToString();
                    ddlRubro.SelectedValue = turno.Rubro.Id.ToString();

                    // Cargar los servicios correspondientes al rubro seleccionado
                    int idRubro = turno.Rubro.Id;
                    var servicios = servicioNegocio.ListarPorRubro(idRubro);

                    ddlServicio.DataSource = servicios;
                    ddlServicio.DataTextField = "Nombre";
                    ddlServicio.DataValueField = "Id";
                    ddlServicio.DataBind();
                    ddlServicio.Items.Insert(0, new ListItem("Seleccione un servicio", "0"));

                    // Validar si el servicio existe en el DropDownList antes de asignarlo
                    ListItem item = ddlServicio.Items.FindByValue(turno.Servicio.Id.ToString());
                    if (item != null)
                    {
                        ddlServicio.SelectedValue = turno.Servicio.Id.ToString();
                    }
                    else
                    {
                        lblMessage.Text = "El servicio asociado al turno no está disponible.";
                        lblMessage.CssClass = "text-warning";
                        lblMessage.Visible = true;
                    }

                    ddlFechaHora.Text = turno.FechaHora.Fecha.ToString("yyyy-MM-dd");
                    ddlEstado.SelectedValue = turno.Estado.Id.ToString();

                    // Establecer el ID del turno en el HiddenField
                    hfTurnoId.Value = turno.Id.ToString();

                    // Cambiar visibilidad de los botones
                    btnModificar.Visible = true;
                    btnAgregar.Visible = false;
                }
                else
                {
                    lblMessage.Text = "No se encontró el turno para editar.";
                    lblMessage.CssClass = "text-warning";
                    lblMessage.Visible = true;
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                int idTurno = Convert.ToInt32(e.CommandArgument);
                try
                {
                    if (turnoNegocio.Eliminar(idTurno))
                    {
                        lblMessage.Text = "Turno eliminado correctamente.";
                        lblMessage.CssClass = "text-success";
                    }
                    else
                    {
                        lblMessage.Text = "Error al eliminar el turno.";
                        lblMessage.CssClass = "text-danger";
                    }

                    lblMessage.Visible = true;
                    CargarTurnos(); // Refrescar el GridView
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Error al eliminar el turno: {ex.Message}";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                }
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
                    FechaHora = new FechaHora { Fecha = DateTime.Parse(ddlFechaHora.SelectedValue) },
                    Estado = new EstadoTurnos { Id = int.Parse(ddlEstado.SelectedValue) }
                };

                if (turnoNegocio.Modificar(turnoEditado))
                {
                    lblMessage.Text = "Turno modificado correctamente.";
                    lblMessage.CssClass = "text-success";
                    //LimpiarFormulario();
                    btnModificar.Visible = false;
                    btnAgregar.Visible = true;
                }
                else
                {
                    lblMessage.Text = "Error al modificar el turno.";
                    lblMessage.CssClass = "text-danger";
                }

                lblMessage.Visible = true;
                CargarTurnos(); // Refrescar el listado
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error al guardar los cambios: {ex.Message}";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }
    }
}
 