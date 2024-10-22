<%@ Page Title="Turnos" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Turnos.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.Turnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Turnos</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Reservar Turno</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

        <!-- Información del Cliente -->
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" Text="Nombre"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" Text="Email"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <!-- Fecha y Hora del Turno -->
        <div class="row mt-3">
            <div class="col-md-6">
                <asp:Label ID="lblFecha" runat="server" AssociatedControlID="txtFecha" Text="Fecha del Turno"></asp:Label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblHora" runat="server" AssociatedControlID="ddlHora" Text="Hora del Turno"></asp:Label>
                <asp:DropDownList ID="ddlHora" runat="server" CssClass="form-select">
                    <asp:ListItem Text="09:00 AM" Value="09:00"></asp:ListItem>
                    <asp:ListItem Text="10:00 AM" Value="10:00"></asp:ListItem>
                    <asp:ListItem Text="11:00 AM" Value="11:00"></asp:ListItem>
                    <asp:ListItem Text="12:00 PM" Value="12:00"></asp:ListItem>
                    <asp:ListItem Text="01:00 PM" Value="13:00"></asp:ListItem>
                    <asp:ListItem Text="02:00 PM" Value="14:00"></asp:ListItem>
                    <asp:ListItem Text="03:00 PM" Value="15:00"></asp:ListItem>
                    <asp:ListItem Text="04:00 PM" Value="16:00"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <!-- Tipo de Vehículo con Iconos -->
        <div class="row mt-3">
            <div class="col-md-12">
                <asp:Label ID="lblTipoVehiculo" runat="server" Text="Selecciona el Tipo de Vehículo"></asp:Label>
                <div class="d-flex justify-content-between mt-2">
                    <div class="vehicle-option text-center">
                        <img src="Source/moto.png" alt="Motocicleta" class="vehicle-icon" id="motocicleta" onclick="selectVehicle('Motocicleta', 'motocicleta')" />
                        <p>Motocicleta</p>
                    </div>
                    <div class="vehicle-option text-center">
                        <img src="Source/auto.png" alt="Auto Sedán" class="vehicle-icon" id="sedan" onclick="selectVehicle('Auto Sedán', 'sedan')" />
                        <p>Auto Sedán</p>
                    </div>
                    <div class="vehicle-option text-center">
                        <img src="Source/auto.png" alt="Auto Hatchback" class="vehicle-icon" id="hatchback" onclick="selectVehicle('Auto Hatchback', 'hatchback')" />
                        <p>Auto Hatchback</p>
                    </div>
                    <div class="vehicle-option text-center">
                        <img src="Source/camioneta.png" alt="Camioneta" class="vehicle-icon" id="camioneta" onclick="selectVehicle('Camioneta', 'camioneta')" />
                        <p>Camioneta</p>
                    </div>
                </div>
                <asp:HiddenField ID="hfTipoVehiculo" runat="server" />
            </div>
        </div>


            <!-- Tipo de Servicio -->
            <div class="col-md-6">
                <asp:Label ID="lblTipoServicio" runat="server" AssociatedControlID="ddlTipoServicio" Text="Tipo de Servicio"></asp:Label>
                <asp:DropDownList ID="ddlTipoServicio" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Lavado Simple" Value="Lavado Simple"></asp:ListItem>
                    <asp:ListItem Text="Lavado Premium" Value="Lavado Premium"></asp:ListItem>
                    <asp:ListItem Text="Detailing" Value="Detailing"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <!-- Comentarios -->
        <div class="row mt-3">
            <div class="col-md-12">
                <asp:Label ID="lblComentario" runat="server" AssociatedControlID="txtComentario" Text="Comentario adicional"></asp:Label>
                <asp:TextBox ID="txtComentario" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
        </div>

        <!-- Botón de reserva -->
        <div class="row mt-3">
            <div class="col-md-12">
                <asp:Button ID="btnReservar" runat="server" Text="Reservar" CssClass="btn btn-primary" OnClick="btnReservar_Click" />
            </div>
        </div>
</asp:Content>


