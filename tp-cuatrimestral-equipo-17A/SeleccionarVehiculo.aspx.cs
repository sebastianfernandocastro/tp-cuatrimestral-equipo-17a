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
            int cantidadDiv = (int)Math.Ceiling((double)vehiculos.Count / 5);
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
                        Width = 300,
                        Height = 300,
                        CommandArgument = vehiculos[b].Id.ToString()
                    };

                    imgButton.Click += ImgBtn_Click;
                    div.Controls.Add(imgButton);
                    phVehiculos.Controls.Add(div);
                    if(b==4)
                    {
                        auxiliar = 5;
                        break;
                    }
                    if (b == 9)
                    {
                        auxiliar = 10;
                        break;
                    }
                    if (b == 14)
                    {
                        auxiliar = 15;
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