using MM.Core.IO;
using MM.Web.Configuration;
using System;
using System.Web;

namespace MM.Web.Modules
{
    public class HTMLInjectionModule : IHttpModule
    {
       

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest_RegisterFilter;
           
        }

        public static void BeginRequest_RegisterFilter(object sender, EventArgs e)
        {

            var foo = HttpContext.Current.Response.Filter; //Fix: https://stackoverflow.com/questions/22449603/httpcontext-current-response-filter-response-filter-is-not-valid/22450589

            HttpContext.Current.Response.Filter = new InjectionFilterStream(new FilterContext(HttpContext.Current),
                                           HtmlInjectionConfiguration.FilterContentTypes,
                                           HtmlInjectionConfiguration.InsertTags,
                                           HtmlInjectionConfiguration.InsertType,
                                           HtmlInjectionConfiguration.Insert);



        }
    }
}
