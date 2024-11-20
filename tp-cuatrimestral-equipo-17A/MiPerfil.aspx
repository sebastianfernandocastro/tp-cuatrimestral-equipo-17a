<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Mi Perfíl</h2>
        <asp:Label ID="lblMensage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

        <asp:TextBox ID="txtId" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
        <div class="row mb-3">
            <%--Nombre--%>
            <div class="col-md-6">
                <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" Text="Nombre"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <%--apellido--%>
            <div class="col-md-6">
                <asp:Label ID="lblApellido" runat="server" AssociatedControlID="txtApellido" Text="Apellido"></asp:Label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <%if (usu == null || usu.tipo == 1)

            { %>
        <div class="row mb-3">
            <%--DNI--%>
            <div class="col-md-6">
                <asp:Label ID="lblDNI" runat="server" AssociatedControlID="txtDNI" Text="DNI"></asp:Label>
                <asp:TextBox ID="txtDNI" runat="server" MaxLength="8" CssClass="form-control"></asp:TextBox>
            </div>
            <%--Telefono--%>
            <div class="col-md-6">
                <asp:Label ID="lblTelefono" runat="server" AssociatedControlID="txtTelefono" Text="Teléfono"></asp:Label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    <div class="row mb-3">
        <%--mail--%>
        <div class="col-md-6">
            <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" Text="Email"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <%} %>
    <%else
        { %>
    <div class="row mb-3">
        <%--Legajo--%>
        <div class="col-md-6">
            <asp:Label ID="lblLegajo" runat="server" AssociatedControlID="txtLegajo" Text="Legajo"></asp:Label>
            <asp:TextBox ID="txtLegajo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <%--NivelAcceso--%>
        <div class="col-md-6">
            <asp:Label ID="lblNivelAcceso" runat="server" AssociatedControlID="ddlNivelAcceso" Text="Nivel Acceso"></asp:Label>
            <asp:DropDownList ID="ddlNivelAcceso" Enabled="false" CssClass="form-control" runat="server"></asp:DropDownList>
        </div>
    </div>
    <%} %>
    <div class="row mb-3">
        <%--Usuario--%>
        <div class="col-md-6">
            <asp:Label ID="lblUsuario" runat="server" AssociatedControlID="txtUsuario" Text="Usuario"></asp:Label>
            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <%--Contraseña--%>
        <div class="col-md-6">
            <asp:Label ID="lblContraseña" runat="server" AssociatedControlID="txtContraseña" Text="Contraseña"></asp:Label>
            <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="col-md-6">
        <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnEditar_Click" />
    </div>
    </div>
</asp:Content>
