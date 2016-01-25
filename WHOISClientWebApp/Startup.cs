using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System.Net.Http;
using System.Threading;
using Swashbuckle.Application;
using System.IO;
using System.Web.Http.ExceptionHandling;

[assembly: OwinStartup(typeof(WHOISClientWebApp.Startup))]

namespace WHOISClientWebApp
{
    /// <summary>
    /// OWIN StartUp
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Get the runtime type is mono or not.
        /// </summary>
        public static bool IsRuntimeMono { get; } = Type.GetType("Mono.Runtime") != null;

        /// <summary>
        /// Get the status of Swagger feature avaliable or not.
        /// </summary>
        public static bool AvailableSwagger { get { return !IsRuntimeMono; } }

        /// <summary>
        /// Configure OWIN web application.
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            UseWebAPI(app);

            var fileSystem = new PhysicalFileSystem(AppDomain.CurrentDomain.BaseDirectory);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileSystem = fileSystem,
                ServeUnknownFileTypes = false
            });

            app.UseNancy();
        }

        private static void UseWebAPI(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Install unhandled exception global logger
            config.Services.Replace(typeof(IExceptionLogger), new ExceptionLoggerTraceRedirector());

            // Fix: CORS support of WebAPI doesn't work on mono. http://stackoverflow.com/questions/31590869/web-api-2-post-request-not-working-on-mono
            if (IsRuntimeMono) config.MessageHandlers.Add(new MonoPatchingDelegatingHandler());
            config.EnableCors();

            // Configure Swashbuckle.
            if (AvailableSwagger)
            {
                config.EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", null);
                    c.IncludeXmlComments(GetXmlCommentsPath());
                })
                .EnableSwaggerUi(c =>
                {
                    c.EnableDiscoveryUrlSelector();
                    c.DocExpansion(DocExpansion.List);
                });
            }

            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
        }

        /// <summary>
        /// Work around a bug in mono's implementation of System.Net.Http where calls to HttpRequestMessage.Headers.Host will fail unless we set it explicitly.
        /// This should be transparent and cause no side effects.
        /// </summary>
        private class MonoPatchingDelegatingHandler : DelegatingHandler
        {
            protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Headers.Host = request.Headers.GetValues("Host").FirstOrDefault();
                return await base.SendAsync(request, cancellationToken);
            }
        }

        private static string GetXmlCommentsPath()
        {
            return Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "bin",
                typeof(Startup).Assembly.GetName().Name + ".xml");
        }
    }
}
