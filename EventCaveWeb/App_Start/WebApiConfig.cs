using EventCaveWeb.Utils;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EventCaveWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();
            var formatter = new BrowserJsonFormatter();
            formatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            config.Formatters.Add(formatter);
        }
    }
}