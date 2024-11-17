using dominio;
using negocio;
using System;
using System.Collections;
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
            if (!IsPostBack)
            {
                GenerarVehiculos();
            }
            else
            {
                GenerarVehiculos();
            }
        }

        private void GenerarVehiculos()
        {
            List<TipoVehiculo> vehiculos;
            List<Imagen> imagenes;

            ImagenNegocio imagenNegocio = new ImagenNegocio();
            imagenes = imagenNegocio.listar();

            TipoVehiculoNegocio negocio = new TipoVehiculoNegocio();

            vehiculos = negocio.Listar();
            int cantidadDiv = (int)Math.Ceiling((double)vehiculos.Count / 4);
            int auxiliar = 0;
            for (int i = 0; i < cantidadDiv; i++)
            {
                var div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                div.Attributes["class"] = "divisorVehiculos";
                for (int b = auxiliar; b < vehiculos.Count; b++)
                {
                    Imagen img  = imagenes.Find(o => o.Id == vehiculos[b].IdImagen);
                    ImageButton imgButton = new ImageButton
                    {
                        ID = $"{vehiculos[b].Id}",
                        CssClass = "cuadrado",
                        ImageUrl = img.UrlImagen,
                        CommandArgument = vehiculos[b].Id.ToString()
                    };
                    imgButton.Attributes.Add("title", vehiculos[b].Nombre);
                    imgButton.Click += ImgBtn_Click;
                    div.Controls.Add(imgButton);
                    phVehiculos.Controls.Add(div);
                    if(b==3)
                    {
                        auxiliar = 4;
                        break;
                    }
                    if (b == 7)
                    {
                        auxiliar = 8;
                        break;
                    }
                    if (b == 11)
                    {
                        auxiliar = 12;
                        break;
                    }
                }
            }
        }

        protected void ImgBtn_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            Session["IdTipoVehiculo"] = int.Parse(btn.CommandArgument);
            Response.Redirect("SeleccionarRubro.aspx");
        }

    }
}