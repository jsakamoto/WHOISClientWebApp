using System;
using System.Diagnostics;
using System.Text;
using System.Web.Http.ExceptionHandling;

namespace WHOISClientWebApp
{
    /// <summary>
    /// Represents an unhandled exception logger.
    /// </summary>
    public class ExceptionLoggerTraceRedirector : ExceptionLogger
    {
        /// <summary>
        /// When overridden in a derived class, logs the exception synchronously.
        /// </summary>
        /// <param name="context"></param>
        public override void Log(ExceptionLoggerContext context)
        {
            var logtext = new StringBuilder();
            logtext.AppendFormat("Request: {0}", context.Request)
                .AppendLine()
                .AppendLine();
            logtext.AppendFormat("Exception: {0}", context.Exception)
                .AppendLine();
            Trace.TraceError(logtext.ToString());
        }
    }
}