<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioPrecio.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.FormularioPrecio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Gestionar Precio</h2>
        <asp:Label ID="lblMensage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
        <asp:TextBox ID="txtId" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>

        <div class="row mb-3">
            <div class="col-md-4">
                <asp:Label ID="lblTipoVehiculo" runat="server" AssociatedControlID="ddlTipoVehiculo" Text="Tipo de Vehículo"></asp:Label>
                <asp:DropDownList ID="ddlTipoVehiculo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlTipoVehiculo_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblRubro" runat="server" AssociatedControlID="ddlRubro" Text="Rubro"></asp:Label>
                <asp:DropDownList ID="ddlRubro" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlRubro_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblServicio" runat="server" AssociatedControlID="ddlServicio" Text="Servicio"></asp:Label>
                <asp:DropDownList ID="ddlServicio" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <asp:Label ID="lblPrecio" runat="server" AssociatedControlID="txtPrecio" Text="Precio"></asp:Label>
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6">
            <asp:Button ID="btnAgregar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
            <a href="Precios.aspx" class="btn btn-light">Cancelar</a>
        </div>
    </div>
</asp:Content>

