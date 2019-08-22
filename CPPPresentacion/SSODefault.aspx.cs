using System;
using SSO.Finagro;

namespace SPF.UI
{
    public partial class SSODefault : System.Web.UI.Page
    {
        private SSOMaster Maestra => (SSOMaster)Master;

        protected void Page_Load(object sender, EventArgs e)
        {
            lSesion.Visible = false;
            lAplicacion.Visible = false;
        }
    }
}