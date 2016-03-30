using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Tcent.Api.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Web Api应用类.
        /// </summary>
        /// { Created At Time:[ 2016/3/10 17:23 ], By User:wcj21259, On Machine:WCJ }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();

            // 解决controller同名问题
            SolveSameName();
        }

        /// <summary>
        /// 解决controller同名问题.
        /// </summary>
        /// { Created At Time:[ 2016/3/8 18:24 ], By User:wcj21259, On Machine:WCJ }
        private void SolveSameName()
        {
            var suffix = typeof(DefaultHttpControllerSelector).GetField("ControllerSuffix", BindingFlags.Static | BindingFlags.Public);
            if (suffix != null)
            {
                suffix.SetValue(null, "ApiController");
            }
        }
    }
}
