<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Empleados.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.Empleados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:GridView ID="dgvEmpleados" runat="server" DataKeyNames="Id"
     CssClass="table" AutoGenerateColumns="false"
     OnSelectedIndexChanged="dgvEmpleados_SelectedIndexChanged"
     OnPageIndexChanging="dgvEmpleados_PageIndexChanging"
     AllowPaging="True" PageSize="5">
     <Columns>
         <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
         <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
         <asp:BoundField HeaderText="Legajo" DataField="legajo" />
         <asp:BoundField HeaderText="Acceso" DataField="nivelAcceso" />
         <asp:BoundField HeaderText="Usuario" DataField="NombreUsuario" />
         <asp:BoundField HeaderText="Contraseña" DataField="Contraseña" />
         <%--<asp:CheckBoxField HeaderText="Activo" DataField="Estado" />--%>
         <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Modificar" />
     </Columns>
 </asp:GridView>
 <%--<a href="FormularioPokemon.aspx" class="btn btn-primary">Agregar</a>--%>
</asp:Content>
