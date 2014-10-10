using System;
using System.Text;
using System.Web.Http.ExceptionHandling;
using EnergyTrading.Extensions;
using EnergyTrading.Logging;
using EnergyTrading.Mdm.Services;
using EnergyTrading.Validation;
using MDM.ServiceHost.WebApi.Infrastructure.Exceptions;

namespace MDM.ServiceHost.WebApi.Filters
{
    /// <summary>
    /// Logs all exceptions to the Logger framework being used, either as error or debug 
    /// depending on the type of exception thrown
    /// </summary>
    public class DefaultExceptionLogger : ExceptionLogger
    {
        private readonly ILogger logger = LoggerFactory.GetLogger<DefaultExceptionLogger>();

        public override void Log(ExceptionLoggerContext context)
        {
            if (ShouldLogAsError(context))
            {
                logger.ErrorFormat("[{0}] [{1}] failed, see error details below{2}{3}", context.Request.Method,
                    context.Request.RequestUri, Environment.NewLine, ExceptionDetails(context.Exception));
            }
            else
            {
                logger.DebugFormat("[{0}] [{1}] failed, see error details below{2}{3}", context.Request.Method,
                    context.Request.RequestUri, Environment.NewLine, ExceptionDetails(context.Exception));
            }
        }

        private bool ShouldLogAsError(ExceptionLoggerContext context)
        {
            return (!(context.Exception is MdmFaultException) && 
                    !(context.Exception is NotFoundException) &&
                    !(context.Exception is ValidationException) &&
                    !(context.Exception is NotImplementedException) &&
                    !(context.Exception is VersionConflictException)
                    );
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