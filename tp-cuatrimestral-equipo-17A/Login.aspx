<%@ Page Title="Login" Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Turnos</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="container mt-5" style="height:73.4vh; align-content:center;">
        <div class="row justify-content-center">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-header text-center">
                        <h3>Login</h3>
                    </div>
                    <div class="card-body">
                        <asp:Label ID="lblMsgError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control mb-3" Placeholder="Usuario" />
                        <asp:TextBox ID="txtContraseña" runat="server" TextMode="Password" CssClass="form-control mb-3" Placeholder="Contraseña" />
                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-success w-100" Text="Iniciar Sesión" OnClick="btnLogin_Click" />
                    </div>
                </div>
                <br />
                <div class="text-center">
                    <p>¿No tienes una cuenta?</p>
                    <a href="Registrarse.aspx?Accion=1">Registrarse</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
