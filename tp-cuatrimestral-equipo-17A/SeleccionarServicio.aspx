<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SeleccionarServicio.aspx.cs" Inherits="tp_cuatrimestral_equipo_17A.SeleccionarServicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        h2 {
            margin: 0 0 10px 0;
            font-size: 1.5em;
        }

        .container {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            justify-content: center;
            height: 78.7vh;
            align-content: center;
        }

        .box {
            flex: 1 1 calc(25% - 20px);
            min-width: 200px;
            padding: 20px;
            box-sizing: border-box;
            background-color: #ffffff;
            border: 1px solid #ddd;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            text-align: center;
            height: 150px;
        }

        .box:hover {
            transform: scale(1.1);
        }


        @media (max-width: 768px) {
            .box {
                flex: 1 1 calc(50% - 20px);
            }
        }

        @media (max-width: 480px) {
            .box {
                flex: 1 1 100%;
            }
        }

        .calendar-container {
            text-align: center;
            background: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        input[type="date"] {
            font-size: 16px;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            outline: none;
        }

        input[type="date"]:disabled {
            background-color: #eaeaea;
            cursor: not-allowed;
        }
    </style>
    <div class="container">
        <div class="box" id="calendario">
            <h2>Calendario</h2>
            <asp:TextBox ID="calendarInput" runat="server" CssClass="calendar" TextMode="Date"></asp:TextBox>
        </div>
        <div class="box" id="servicio">
            <h2>Servicios</h2>
            <div class="col-md-6" style="width: 100%;">
                <asp:DropDownList ID="ddlServicio" runat="server" AutoPostBack="true" CssClass="form-select" OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="box" id="horario">
            <h2>Horario</h2>
            <!-- Aquí puedes insertar horarios -->
            <select>
                <option>09:00 AM</option>
                <option>09:30 AM</option>
                <option>10:00 AM</option>
                <option>10:30 AM</option>
                <option>11:00 AM</option>
                <option>11:30 AM</option>
                <option>02:00 PM</option>

            </select>
        </div>
        <div class="box" id="confirmacion" style="align-content: center;">
            <asp:Button CssClass="btn btn-secondary" Text="Reservar" runat="server" />
        </div>
    </div>
</asp:Content>
