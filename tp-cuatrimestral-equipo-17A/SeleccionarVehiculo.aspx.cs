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

        protected void ImgBtn1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("SeleccionarRubro.aspx");
        }
    }
}