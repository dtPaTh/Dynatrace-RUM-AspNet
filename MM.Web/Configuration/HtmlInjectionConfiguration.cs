using MM.Core.IO;
using System;
using System.Configuration;

namespace MM.Web.Configuration
{
    public class HtmlInjectionConfiguration
    {
        public static string[] FilterContentTypes
        {
            get
            {
                var cfgValue = ConfigurationManager.AppSettings["HtmlInjection.FilterContentTypes"];
                if (!String.IsNullOrEmpty(cfgValue))
                    return cfgValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                else
                    return new string[] { "text/html", "application/xhtml+xml", "application/xhtml+xml" };
            }
        }

        public static string[] InsertTags
        {
            get
            {
                var cfgValue = ConfigurationManager.AppSettings["HtmlInjection.InsertTags"];
                if (!String.IsNullOrEmpty(cfgValue))
                    return cfgValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                else
                    return new string[] { "<script ", "</head>" };
                
            }
        }

        public static InsertType InsertType
        {
            get
            {
                InsertType ret;
                if (!Enum.TryParse<InsertType>(ConfigurationManager.AppSettings["HtmlInjection.InsertType"], out ret))
                    ret = InsertType.Before;
                return ret;
            }
        }
        public static string Insert { get; set; }
    }
}
