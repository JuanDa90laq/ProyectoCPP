using System;
using System.Web.UI;

namespace SPF.UI
{
    public partial class AccesoDenegado : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = "Lo sentimos, no tienes permisos suficientes para " +
                "vizualizar la pagina " + (Session["SPF-AccesoDenegadoMensaje"] ?? " que intentas cargar ") + ". Por favor comunicate con el " +
                "Area encargada de administrar el Aplicativo.";
        }
    }
}