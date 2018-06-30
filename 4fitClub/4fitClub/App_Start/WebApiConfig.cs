using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace _4fitClub
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Desligar o formatador do XML
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //teste de Json
            config.Formatters.JsonFormatter.SerializerSettings.Formatting =
                Newtonsoft.Json.Formatting.Indented;






            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
