using System.Web.Http;
using Newtonsoft.Json.Serialization;
using MyRoom.API.Filters;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;

namespace MyRoom.API
{
    public static class GlobalConfig
    {
        public static void CustomizeConfig(HttpConfiguration config)
        {
            // Remove Xml formatters. This means when we visit an endpoint from a browser,
            // Instead of returning Xml, it will return Json.
            // More information from Dave Ward: http://jpapa.me/P4vdx6
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Configure json camelCasing per the following post: http://jpapa.me/NqC2HH
            // Here we configure it to write JSON property names with camel casing
            // without changing our server-side data model:
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            json.UseDataContractJsonSerializer = true;
            config.Formatters.Add(json);


            // Add model validation, globally
            config.Filters.Add(new ValidationActionFilter());

           // config.SuppressDefaultHostAuthentication();
          //  config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Filters.Add(new AuthorizeAttribute());

        }
    }
}