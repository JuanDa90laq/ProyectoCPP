using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace CPPPresentacion
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    DevExpress.Web.ASPxWebControl.GlobalTheme = "Finagro";
        //}

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxWebControl.GlobalTheme = "FinagroMaterialCompact";
            //DevExpress.Web.ASPxWebControl.GlobalTheme = "FinagroMaterial";
            //DevExpress.Web.ASPxWebControl.GlobalTheme = "FinagroiOS";
        }
    }
}