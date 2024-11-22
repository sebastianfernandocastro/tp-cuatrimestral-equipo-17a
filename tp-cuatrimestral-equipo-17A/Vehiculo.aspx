<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Vehiculo.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.Vehiculo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
    <h2>Gestión de Tipo de Vehiculos</h2>

    <div class="row mt-3">
        <div class="col-md-1">
            <asp:Label ID="lblFiltro" runat="server" Text="filtro:"></asp:Label>
        </div>
        <div class="col-md-6">
            <asp:TextBox runat="server" ID="txtFiltro" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" CssClass="form-control" />

        </div>
    </div>
    <div class="row mt-3">
        <div class="col-md-4">
            <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:CheckBox Text="Mostrar Vehiculos Inactivos" AutoPostBack="true" ID="cbxInactivos" OnCheckedChanged="cbxInactivos_CheckedChanged" runat="server" />
        </div>
    </div>

    <div class="row mt-3">
        <asp:GridView ID="dgvVehiculos" runat="server" DataKeyNames="Id"
            CssClass="table" AutoGenerateColumns="false"
            OnSelectedIndexChanged="dgvVehiculos_SelectedIndexChanged"
            OnPageIndexChanging="dgvVehiculos_PageIndexChanging"
            OnRowCommand="dgvVehiculos_RowCommand"
            AllowPaging="True" PageSize="5">
            <Columns>
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                <asp:BoundField HeaderText="Url Imagen" DataField="IdImagen" />
                <asp:BoundField HeaderText="Estado" DataField="Estado"
                    DataFormatString="{0:Activo;Inactivo}" />
                <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Modificar" />
                <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" Text="Eliminar"
                            CommandArgument='<%# Eval("Id") %>'
                            OnClientClick="return confirmarEliminar(this);"
                            data-id='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderText="Eliminar">
            <ItemTemplate>
                <a href="#" onclick="return confirmarEliminar(this);" data-id='<%# Eval("Id") %>'>Eliminar</a>
            </ItemTemplate>
        </asp:TemplateField>--%>
                <%-- <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button
                    ID="btnActivar"
                    runat="server"
                    Text='<%# Eval("Estado").ToString() == "1" ? "Desactivar" : "Activar" %>'
                    CssClass="btn btn-sm btn-warning"
                    CommandName="ToggleEstado"
                    CommandArgument='<%# Eval("Id") %>' 
                    data-id='<%# Eval("Id") %>'/>
                   
            </ItemTemplate>
        </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
    </div>

    <asp:HiddenField ID="hfVehiculoId" runat="server" />
    <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar Confirmado"
        OnClick="btnConfirmarEliminar_Click" Style="display: none;" />

    <a href="FormularioVehiculo.aspx" class="btn btn-primary mt-3">Agregar Vehiculo</a>
</div>

<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
<script>
    function confirmarEliminar(linkElement) {
        event.preventDefault();

        var idVehiculo = linkElement.getAttribute("data-id");

        if (!idVehiculo) {
            Swal.fire('Error', 'No se pudo obtener el ID del Vehiculo.', 'error');
            return false;
        }
        document.getElementById('<%= hfVehiculoId.ClientID %>').value = idVehiculo;

        Swal.fire({
            title: '¿Estás seguro?',
            text: "No podrás deshacer esta acción.",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                document.getElementById('<%= btnConfirmarEliminar.ClientID %>').click();
            }
        });

        return false;
    }


</script>
</asp:Content>
