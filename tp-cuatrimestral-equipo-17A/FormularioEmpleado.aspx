<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioEmpleado.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.FormularioEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Empleado</h2>
        <asp:Label ID="lblMensage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
        <asp:TextBox ID="txtId" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>

        <div class="row mb-3">
            <%--Legajo--%>
            <div class="col-md-6">
                <asp:Label ID="lblLegajo" runat="server" AssociatedControlID="txtLegajo" Text="Legajo"></asp:Label>
                <asp:TextBox ID="txtLegajo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <%--NivelAcceso--%>
            <div class="col-md-6">
                <asp:Label ID="lblNivelAcceso" runat="server" AssociatedControlID="txtNivelAcceso" Text="Nivel Acceso"></asp:Label>
                <asp:TextBox ID="txtNivelAcceso" runat="server"  CssClass="form-control"></asp:TextBox>
            </div>
        </div>
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


        <div class="row mb-3">
            <%--Usuario--%>
            <div class="col-md-6">
                <asp:Label ID="lblUsuario" runat="server" AssociatedControlID="txtUsuario" Text="Usuario"></asp:Label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <%--Contraseña--%>
            <div class="col-md-6">
                <asp:Label ID="lblContraseña" runat="server" AssociatedControlID="txtContraseña" Text="Contraseña"></asp:Label>
                <asp:TextBox ID="txtContraseña" runat="server"  CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-6">
            <asp:Button ID="btnAgregar" runat="server" Text="Editar" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
            <a href="Empleados.aspx" class="btn btn-light">Cancelar</a>

        </div>
    </div>
</asp:Content>
