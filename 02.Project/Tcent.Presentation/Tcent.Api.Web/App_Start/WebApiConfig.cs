using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Tcent.Api.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // 依赖注入解析器
            config.DependencyResolver = new UnityResolver(UnityConfig.GetConfiguredContainer());

            config.Formatters.XmlFormatter.UseXmlSerializer = true;
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter()
            {
                DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss"
            });

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
