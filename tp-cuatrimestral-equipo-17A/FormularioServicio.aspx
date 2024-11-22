<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioServicio.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.FormularioServicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Gestion de Servicios</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>
            <asp:Literal ID="lblTitulo" runat="server"></asp:Literal></h2>
        <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false"></asp:Label>
        <div class="mb-3">
            <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" Text="Nombre"></asp:Label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label ID="lblDescripcion" runat="server" AssociatedControlID="txtDescripcion" Text="Descripción"></asp:Label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label ID="lblRubro" runat="server" AssociatedControlID="ddlRubro" Text="Rubro"></asp:Label>
            <asp:DropDownList ID="ddlRubro" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </div>
        <div class="mb-3">
            <asp:Label ID="lblEstado" runat="server" AssociatedControlID="ddlEstado" Text="Estado"></asp:Label>
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                <asp:ListItem Text="Inactivo" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
        <a href="Servicios.aspx" class="btn btn-secondary">Cancelar</a>
    </div>
</asp:Content>