﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.Registrarse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4" style="height:76.1vh;">
        <h2>Registrarse</h2>
        <asp:Label ID="lblMensage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

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
        <div class="row mb-3">
            <%--Usuario--%>
            <div class="col-md-6">
                <asp:Label ID="lblUsuario" runat="server" AssociatedControlID="txtUsuario" Text="Usuario"></asp:Label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <%--Contraseña--%>
            <div class="col-md-6">
                <asp:Label ID="lblContraseña" runat="server" AssociatedControlID="txtContraseña" Text="Contraseña"></asp:Label>
                <asp:TextBox ID="txtContraseña" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-6">
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
        </div>
    </div>
</asp:Content>
