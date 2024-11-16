using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_cuatrimestral_equipo_17A
{
    public partial class SeleccionarRubro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerarRubros();
            }
            else
            {
                GenerarRubros();
            }

        }

        private void GenerarRubros()
        {
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            RubroNegocio rubroNegocio = new RubroNegocio();

            List<Rubro> rubros = rubroNegocio.Listar();
            List<Imagen> imagenes = imagenNegocio.listar();

            var div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            div.Attributes["class"] = "divisorRubros";

            for(int i=0; i<rubros.Count; i++)
            {
                Imagen img = imagenes.Find(o => o.Id == rubros[i].IdImagen);
                ImageButton imageButton = new ImageButton
                {
                    ID = $"{rubros[i].Id}",
                    CssClass = "rectangulo",
                    ImageUrl = img.UrlImagen,
                    CommandArgument = rubros[i].Id.ToString()
                };
                imageButton.Click += ImgBtn_Click;
                div.Controls.Add(imageButton);
                phRubros.Controls.Add(div);
            }
        }
        protected void ImgBtn_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            Session["IdRubro"] = int.Parse(btn.CommandArgument);
            Response.Redirect("SeleccionarServicio.aspx");
        }
    }
}