<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.About" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <div class="divContenedor" style="background-color: #222222; height: 100px; color: white; justify-content: center; align-items: center; font-size: 100px; margin-bottom: 5px;">
        <h1>SOBRE NOSOTROS</h1>
    </div>
    <div class="divContenedor" style="margin-bottom: 5px; align-items: center;">
        <img style="width: 50%; border-radius: 0;height: 405px" src="https://iili.io/27EmC42.jpg" alt="Foto del local" />
        <div style="width: 50%; background-color: #222222; color: white; padding: 20px;">
            <h3>Nuestra Historia</h3>
            <p style="font-size: 18px; text-align: justify;">
                Nuestro negocio comenzó con un sueño: ofrecer el mejor cuidado y mantenimiento para vehículos en nuestra comunidad. 
                Nos especializamos en servicios de alta calidad, combinando experiencia y dedicación para superar las expectativas de nuestros clientes.
                <br /><br />
                A lo largo de los años, nos hemos convertido en un referente gracias a nuestra pasión por los autos y el compromiso con cada detalle.
                ¡Vení a conocernos y dejá que tu vehículo esté en las mejores manos!
            </p>
        </div>
    </div>
    <div class="divContenedor" style="margin-bottom: 5px; flex-direction: column; align-items: center; text-align: center;">
        <div style="width: 100%; background-color: #222222; color: white; padding: 20px;">
            <h3>Ubicación</h3>
            <p style="font-size: 18px; text-align: justify;">
                Nos encontramos en una ubicación estratégica para brindar un acceso rápido y cómodo a nuestros clientes. 
                Aquí tenés el lugar donde cuidaremos de tu vehículo con la atención que se merece.
            </p>
        </div>
        <img style="width: 100%; margin-top: 5px; " src="https://iili.io/27Emo2S.png" alt="Mapa de ubicación" />
    </div>
</div>
</asp:Content>
