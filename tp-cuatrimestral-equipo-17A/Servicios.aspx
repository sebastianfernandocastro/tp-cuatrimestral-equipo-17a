<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Servicios.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.Servicios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Gestión de Servicios</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Gestión de Servicios</h2>
    <ContentTemplate>
        <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false"></asp:Label>
        
        <asp:GridView ID="dgvServicios" runat="server" DataKeyNames="Id" AutoGenerateColumns="false"
            OnRowCommand="dgvServicios_RowCommand" AllowPaging="True" PageSize="5" CssClass="table">
            <Columns>
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                <asp:BoundField HeaderText="Tiempo (Horas)" DataField="Tiempo" DataFormatString="{0:F2}" />
                <asp:BoundField HeaderText="Estado" DataField="Estado" />
                
                <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                        <asp:Button ID="btnActivar" runat="server" Text="Activar"
                            CommandName="CambiarEstado" CommandArgument='<%# Eval("Id") + ",1" %>'
                            Visible='<%# Eval("Estado").ToString() == "0" %>' CssClass="btn btn-success" />
                        <asp:Button ID="btnDesactivar" runat="server" Text="Desactivar"
                            CommandName="CambiarEstado" CommandArgument='<%# Eval("Id") + ",0" %>'
                            Visible='<%# Eval("Estado").ToString() == "1" %>' CssClass="btn btn-danger" />
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar"
                            CommandName="Modificar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-warning" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
        <a href="FormularioServicio.aspx" class="btn btn-primary mt-3">Agregar Servicio</a>
    </div>
</asp:Content>