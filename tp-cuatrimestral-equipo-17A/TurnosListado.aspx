<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TurnosListado.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.TurnosListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">

         <h2>Listado de Turnos</h2>

                <!-- Mensaje de error o éxito -->
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

        <div class="row mt-3">
            <div class="col-md-12">
                <asp:GridView ID="gvTurnos" runat="server" CssClass="table table-striped" AutoGenerateColumns="false"
                    OnRowCommand="gvTurnos_RowCommand" DataKeyNames="Id">
                    <Columns>
                        <%--<asp:BoundField DataField="Id" HeaderText="ID" />--%>
                        <asp:BoundField DataField="Usuario.Nombre" HeaderText="Cliente" />
                        <asp:BoundField DataField="Vehiculo.Nombre" HeaderText="Vehículo" />
                        <asp:BoundField DataField="Rubro.Nombre" HeaderText="Rubro" />
                        <asp:BoundField DataField="Servicio.Nombre" HeaderText="Servicio" />
                        <asp:BoundField DataField="Estado.Descripcion" HeaderText="Estado" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                        <asp:BoundField DataField="Hora" HeaderText="Hora" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio" />
                        <asp:BoundField DataField="Aclaracion" HeaderText="Aclaración" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") %>'
                                    CssClass="btn btn-secondary btn-sm" Text="Modificar">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'
                                    CssClass="btn btn-danger btn-sm" Text="Cancelar"
                                    OnClientClick="return confirm('¿Está seguro de que desea cancelar este turno?');">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>

    </div>
</asp:Content>
