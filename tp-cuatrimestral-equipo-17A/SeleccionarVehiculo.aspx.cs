using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_cuatrimestral_equipo_17A
{
    public partial class SeleccionarVehiculo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ImgBtn1_Click(object sender, EventArgs e)
        {
            Session["IdTipoVehiculo"] = 1;
            Response.Redirect("SeleccionarRubro.aspx"); 
        }

        protected void ImgBtn2_Click(object sender, EventArgs e)
        {
            Session["IdTipoVehiculo"] = 2; // Wagon
            Response.Redirect("SeleccionarRubro.aspx");
        }

        protected void ImgBtn3_Click(object sender, EventArgs e)
        {
            Session["IdTipoVehiculo"] = 3; // Coupe
            Response.Redirect("SeleccionarRubro.aspx");
        }

        protected void ImgBtn4_Click(object sender, EventArgs e)
        {
            Session["IdTipoVehiculo"] = 4; // SUV
            Response.Redirect("SeleccionarRubro.aspx");
        }

        protected void ImgBtn5_Click(object sender, EventArgs e)
        {
            Session["IdTipoVehiculo"] = 5; // Deportivo
            Response.Redirect("SeleccionarRubro.aspx");
        }

        protected void ImgBtn6_Click(object sender, EventArgs e)
        {
            Session["IdTipoVehiculo"] = 6; // PickUp
            Response.Redirect("SeleccionarRubro.aspx");
        }

        protected void ImgBtn7_Click(object sender, EventArgs e)
        {
            Session["IdTipoVehiculo"] = 7; // Mini
            Response.Redirect("SeleccionarRubro.aspx");
        }

        protected void ImgBtn8_Click(object sender, EventArgs e)
        {
            Session["IdTipoVehiculo"] = 8; // Van
            Response.Redirect("SeleccionarRubro.aspx");
        }
    }
}