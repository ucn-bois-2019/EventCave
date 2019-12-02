using EventCaveWeb.Utils;
using System.Web.Http;

namespace EventCaveWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            var formatter = new BrowserJsonFormatter();
            formatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            config.Formatters.Add(formatter);
        }
    }
}