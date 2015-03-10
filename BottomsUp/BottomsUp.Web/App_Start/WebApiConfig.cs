using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace BottomsUp.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "proposals",
                routeTemplate: "api/v1/proposals/{pid}",
                defaults: new { controller="proposals", pid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "requirements",
                routeTemplate: "api/v1/proposals/{pid}/requirements/{rid}",
                defaults: new { controller = "requirements", rid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "tasks",
                routeTemplate: "api/v1/proposals/{pid}/requirements/{rid}/tasks/{tid}",
                defaults: new { controller = "Taskings", tid = RouteParameter.Optional }
            );

        }
    }
}
