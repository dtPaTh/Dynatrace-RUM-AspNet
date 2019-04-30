// Copyright (c) Patrick Thurner and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MM.Core.IO;
using System.IO;
using System.Web;

namespace MM.Web.Modules
{ 
    internal class FilterContext : IFilterContext
    {
        private HttpContext _httpContext;

        internal FilterContext(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public Stream ResponseBody
        {
            get { return _httpContext.Response.OutputStream; }
        }

        public string ResponseContentType
        {
            get { return _httpContext.Response.ContentType; }
        }
    }
}
