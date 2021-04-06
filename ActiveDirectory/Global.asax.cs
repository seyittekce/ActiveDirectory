using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ActiveDirectory.Actions;

namespace ActiveDirectory
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FirstSettings ss = new FirstSettings();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception Ex = Server.GetLastError();
            Logger.AddLog(Ex);
        }
    }
}