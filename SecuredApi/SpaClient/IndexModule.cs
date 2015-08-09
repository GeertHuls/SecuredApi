using System.Configuration;
using System.Text.RegularExpressions;
using Nancy;
using Newtonsoft.Json.Linq;

namespace SpaClient
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                var appsettings = GetAppsettings();
                return View["index", new { appSettings = appsettings }];

            };
        }

        private static string GetAppsettings()
        {
            var appSettings = new
            {
                appSettings = new
                {
                    resourceServerUrl = ConfigurationManager.AppSettings["resourceServerUrl"],
                    identityServerUrl = ConfigurationManager.AppSettings["identityServerUrl"]
                }
            };

            var json = JObject.FromObject(appSettings)
                .ToString();
            json = RemoveWhitespace(json);
            return json;
        }

        private static string RemoveWhitespace(string json)
        {
            return Regex.Replace(json, @"\s+", "");
        }
    }
}