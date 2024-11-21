<%@ Page Title="Gestión de Turnos" Language="C#" AutoEventWireup="true" CodeBehind="Turnos.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.Turnos" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Gestión de Turnos</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Gestión de Turnos</h2>

        <!-- Mensaje de error o éxito -->
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>


        <!-- Formulario de Gestión -->
        <div class="row mt-3">
            <div class="col-md-6">
                <asp:Label ID="lblUsuario" runat="server" AssociatedControlID="ddlUsuario" Text="Usuario"></asp:Label>
                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblVehiculo" runat="server" AssociatedControlID="ddlVehiculo" Text="Vehículo"></asp:Label>
                <asp:DropDownList ID="ddlVehiculo" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlVehiculo_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-md-6">
                <asp:Label ID="lblRubro" runat="server" AssociatedControlID="ddlRubro" Text="Rubro"></asp:Label>
                <asp:DropDownList ID="ddlRubro" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlRubro_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblServicio" runat="server" AssociatedControlID="ddlServicio" Text="Servicio"></asp:Label>
                <asp:DropDownList ID="ddlServicio" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged" CssClass="form-select"></asp:DropDownList>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                <asp:Label ID="lblPrecio" runat="server" AssociatedControlID="txtPrecio" Text="Precio"></asp:Label>
                <asp:TextBox ID="txtPrecio" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                <asp:Label ID="lblFecha" runat="server" AssociatedControlID="txtFecha" Text="Fecha"></asp:Label>
                <asp:TextBox ID="txtFecha" runat="server" AutoPostBack="true" OnTextChanged="txtFecha_TextChanged" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblFechaHora" runat="server" AssociatedControlID="ddlFechaHora" Text="Hora"></asp:Label>
                <asp:DropDownList ID="ddlFechaHora" runat="server" CssClass="form-select">
                </asp:DropDownList>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                <asp:Label ID="lblEstado" runat="server" AssociatedControlID="ddlEstado" Text="Estado"></asp:Label>
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblAclaraciones" runat="server" AssociatedControlID="txtAclaraciones" Text="Aclaraciones"></asp:Label>
                <asp:TextBox ID="txtAclaraciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </div>
        </div>


        <!-- HiddenField para almacenar el ID del turno -->
        <asp:HiddenField ID="hfTurnoId" runat="server" />

        <div class="row mt-3">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar Turno" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar Turno" CssClass="btn btn-secondary" OnClick="btnModificar_Click" Visible="false" />
                <a href="TurnosListado.aspx" class="btn btn-light">Salir</a>
            </div>
        </div>
    </div>
</asp:Content>





