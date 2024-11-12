<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Empleados.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.Empleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:GridView ID="dgvEmpleados" runat="server" DataKeyNames="Id"
        CssClass="table" AutoGenerateColumns="false"
        OnSelectedIndexChanged="dgvEmpleados_SelectedIndexChanged"
        OnPageIndexChanging="dgvEmpleados_PageIndexChanging"
        AllowPaging="True" PageSize="5">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
            <asp:BoundField HeaderText="Legajo" DataField="legajo" />
            <asp:BoundField HeaderText="Acceso" DataField="nivelAcceso.Descripcion" />
            <asp:BoundField HeaderText="Usuario" DataField="NombreUsuario" />
            <asp:BoundField HeaderText="Contraseña" DataField="Contraseña" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Modificar" />
            <asp:TemplateField HeaderText="Acción">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" Text="Eliminar"
                        CommandArgument='<%# Eval("Id") %>'
                        OnClientClick="return confirmarEliminar(this);" 
                        data-id='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:HiddenField ID="hfEmpleadoId" runat="server" />

<asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar Confirmado" 
            OnClick="btnConfirmarEliminar_Click" style="display:none;" />

    <a href="FormularioEmpleado.aspx" class="btn btn-primary">Agregar</a>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>
        function confirmarEliminar(linkButton) {
            event.preventDefault(); // Evita el postback inmediato

            var idEmpleado = linkButton.getAttribute("data-id");

            document.getElementById('<%=hfEmpleadoId.ClientID%>').value = idEmpleado;

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

