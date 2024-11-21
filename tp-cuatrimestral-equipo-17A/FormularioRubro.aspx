<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioRubro.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.FormularioRubro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Formulario Rubro</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Formulario Rubro</h2>

        <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false"></asp:Label>

        <asp:HiddenField ID="hfRubroId" runat="server" />

        <!-- Nombre -->
        <div class="mb-3">
            <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" Text="Nombre del Rubro"></asp:Label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <!-- Descripción -->
        <div class="mb-3">
            <asp:Label ID="lblDescripcion" runat="server" AssociatedControlID="txtDescripcion" Text="Descripción"></asp:Label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>

        <!-- IdImagen -->
        <div class="mb-3">
            <asp:Label ID="lblIdImagen" runat="server" AssociatedControlID="ddlIdImagen" Text="Imagen"></asp:Label>
            <asp:DropDownList ID="ddlIdImagen" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>

        <!-- Estado -->
        <div class="form-group">
            <asp:Label ID="lblEstado" runat="server" AssociatedControlID="ddlEstado" Text="Estado:"></asp:Label>
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                <asp:ListItem Text="Inactivo" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Botones -->
        <div>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <a href="Rubros.aspx" class="btn btn-secondary">Cancelar</a>
        </div>
    </div>
</asp:Content>

