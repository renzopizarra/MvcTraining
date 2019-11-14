using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DemoApp.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //AutofacConfig.Register();
            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            //        new CamelCasePropertyNamesContractResolver();

            //Change Case of JSON
            var settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;

            // Set JSON formatter as default one and remove XmlFormatter
            var appXmlType = config.Formatters.XmlFormatter
                .SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );




            //config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute("CustomApi", "{controller}/{action}/{id}", new { id = RouteParameter.Optional }
            //    );

            //   config.Routes.MapHttpRoute("CustomApi", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional }
            //);
            //   config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional }
            //       );

            //Enable Cors
            config.EnableCors(new EnableCorsAttribute("*", headers: "*", methods: "*"));
        }
    }
}
