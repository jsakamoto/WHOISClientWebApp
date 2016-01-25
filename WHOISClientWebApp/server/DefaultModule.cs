using System;
using System.Linq;
using Nancy;

namespace WHOISClientWebApp
{
    /// <summary>
    /// Default Nancy module
    /// </summary>
    public class DefaultModule : NancyModule
    {
        /// <summary>
        /// Default Nancy module
        /// </summary>
        public DefaultModule()
        {
            Get["/"] = _ => View["index.cshtml", new
            {
                Context.Request.Url.SiteBase,
                Startup.AvailableSwagger
            }];

            After += context =>
            {
                if (context.Response.ContentType == "text/html")
                    context.Response.ContentType = "text/html; charset=utf-8";
            };
        }
    }
}