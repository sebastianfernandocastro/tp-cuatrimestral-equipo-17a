<%@ Page Title="Listado de Precios" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Precios.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.Precios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Gestión de Precios</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Gestión de Precios</h2>


        <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false"></asp:Label>

        <asp:GridView ID="dgvPrecios" runat="server" DataKeyNames="Id"
            CssClass="table" AutoGenerateColumns="false"
            OnSelectedIndexChanged="dgvPrecios_SelectedIndexChanged"
            OnPageIndexChanging="dgvPrecios_PageIndexChanging"
            AllowPaging="True" PageSize="5">
            <Columns>
                <asp:BoundField HeaderText="Tipo de Vehículo" DataField="TipoVehiculoNombre" />
                <asp:BoundField HeaderText="Rubro" DataField="RubroNombre" />
                <asp:BoundField HeaderText="Servicio" DataField="ServicioNombre" />
                <asp:BoundField HeaderText="Precio" DataField="PrecioValor" DataFormatString="{0:C}" />
                <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                        <a href="#" onclick="return confirmarEliminar(this);" data-id='<%# Eval("Id") %>'>Eliminar</a>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>



        <asp:HiddenField ID="hfPrecioId" runat="server" />


        <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar Confirmado"
            OnClick="btnConfirmarEliminar_Click" Style="display: none;" />

        <a href="FormularioPrecio.aspx" class="btn btn-primary mt-3">Agregar Precio</a>
    </div>


    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        function confirmarEliminar(linkElement) {
            event.preventDefault(); 

            
            var idPrecio = linkElement.getAttribute("data-id");


            if (!idPrecio) {
                Swal.fire('Error', 'No se pudo obtener el ID del precio.', 'error');
                return false;
            }
            document.getElementById('<%= hfPrecioId.ClientID %>').value = idPrecio;

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



