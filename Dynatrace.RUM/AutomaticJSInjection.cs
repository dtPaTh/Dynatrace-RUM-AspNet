using MM.Web.Configuration;
using MM.Web.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dynatrace.RUM
{
    public class AutomaticJSInjection: IHttpModule
    {
        private static async Task<string> GetJavascript(string apiEndpoint, string apiToken, string applicationId, bool inlineJavascript)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Api-Token " + apiToken);

                HttpResponseMessage response = await client.GetAsync(apiEndpoint + (inlineJavascript ? "/api/v1/rum/jsInlineScript/" : "/api/v1/rum/jsTag/") + applicationId);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }

            return null;
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            bool inlineScript;
            if (!Boolean.TryParse(ConfigurationManager.AppSettings["Dynatrace.InlineJavascript"], out inlineScript))
                inlineScript = false;

            var insert = GetJavascript(
                            ConfigurationManager.AppSettings["Dynatrace.ApiEndpoint"],
                            ConfigurationManager.AppSettings["Dynatrace.ApiToken"],
                            ConfigurationManager.AppSettings["Dynatrace.ApplicationId"],
                            inlineScript).Result;

            if (!string.IsNullOrEmpty(insert))
                HtmlInjectionConfiguration.Insert = insert;

            context.BeginRequest += HTMLInjectionModule.BeginRequest_RegisterFilter;

        }
        
    }
}
