using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ZipClaim
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Код, выполняемый при запуске приложения
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public const string logonSesKey = "Logon";

        void Session_Start(object sender, EventArgs e)
        {
            Session[logonSesKey] = String.Format("ip={0}&dt={1}", Request.UserHostAddress, DateTime.Now);
        }

        void Session_End(object sender, EventArgs e)
        {
            Session.Remove(logonSesKey);
        }
    }
}