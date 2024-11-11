<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SeleccionarVehiculo.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.SeleccionarVehiculo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: flex; gap: 10px; justify-content: center; margin-bottom: 15px; margin-top: 100px;">
    <div class="cuadrado">
        <asp:ImageButton ID="ImgBtn1" runat="server" ImageUrl="Source/Sedan.png" Width="300px" Height="300px" OnClick="ImgBtn1_Click" />
    </div>
    <div class="cuadrado">
        <asp:ImageButton ID="ImgBtn2" runat="server" ImageUrl="Source/Wagon.png" Width="300px" Height="300px" OnClick="ImgBtn2_Click" />
    </div>
    <div class="cuadrado">
        <asp:ImageButton ID="ImgBtn3" runat="server" ImageUrl="Source/Coupe.png" Width="300px" Height="300px" OnClick="ImgBtn3_Click" />
    </div>
    <div class="cuadrado">
        <asp:ImageButton ID="ImgBtn4" runat="server" ImageUrl="Source/Suv.png" Width="300px" Height="300px" OnClick="ImgBtn4_Click" />
    </div>
</div>
<div style="display: flex; gap: 10px; justify-content: center;">
    <div class="cuadrado">
        <asp:ImageButton ID="ImgBtn5" runat="server" ImageUrl="Source/Deportivo.png" Width="300px" Height="300px" OnClick="ImgBtn5_Click" />
    </div>
    <div class="cuadrado">
        <asp:ImageButton ID="ImgBtn6" runat="server" ImageUrl="Source/PickUp.png" Width="300px" Height="300px" OnClick="ImgBtn6_Click" />
    </div>
    <div class="cuadrado">
        <asp:ImageButton ID="ImgBtn7" runat="server" ImageUrl="Source/Mini.png" Width="300px" Height="300px" OnClick="ImgBtn7_Click" />
    </div>
    <div class="cuadrado">
        <asp:ImageButton ID="ImgBtn8" runat="server" ImageUrl="Source/Van.png" Width="300px" Height="300px" OnClick="ImgBtn8_Click" />
    </div>
</div>

</asp:Content>
