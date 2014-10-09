using System;
using System.Text;
using System.Web.Http.ExceptionHandling;
using EnergyTrading.Extensions;
using EnergyTrading.Logging;

namespace MDM.ServiceHost.WebApi.Filters
{
    /// <summary>
    /// Logs all exceptions to the Logger framework being used
    /// </summary>
    public class DefaultExceptionLogger : ExceptionLogger
    {
        private readonly ILogger logger = LoggerFactory.GetLogger<DefaultExceptionLogger>();

        public override void Log(ExceptionLoggerContext context)
        {
            logger.ErrorFormat("[{0}] [{1}] failed with:{2}{3}", context.Request.Method, context.Request.RequestUri, Environment.NewLine, ExceptionDetails(context.Exception));
        }

        private string ExceptionDetails(Exception contextException)
        {
            var sb = new StringBuilder();
            sb.AppendLine(contextException.AllExceptionMessages());
            sb.AppendLine("Stack Trace:");
            sb.AppendLine(contextException.StackTrace);
            return sb.ToString();
        }
    }
}