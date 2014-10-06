using System.Web.Http.Filters;
using EnergyTrading.Extensions;
using EnergyTrading.Logging;
using MDM.ServiceHost.WebApi.Infrastructure.Exceptions;

namespace MDM.ServiceHost.WebApi.Filters
{
    /// <summary>
    /// A logging action filter which intercepts requests and logs details if the service is configured to 
    /// log debug information
    /// </summary>
    public class LoggingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var log = LoggerFactory.GetLogger(actionContext.ControllerContext.Controller.GetType().Name);
            if (log.IsDebugEnabled)
            {
                log.DebugFormat("[{0}] [{1}] requested", actionContext.Request.Method, actionContext.Request.RequestUri);
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var log = LoggerFactory.GetLogger(actionExecutedContext.ActionContext.ControllerContext.Controller.GetType().Name);

            if (log.IsDebugEnabled)
            {
                if (actionExecutedContext.Response != null)
                {
                    log.DebugFormat("[{0}] [{1}] returning [{2}]", actionExecutedContext.Request.Method,
                        actionExecutedContext.Request.RequestUri, actionExecutedContext.Response.StatusCode);
                }
                else if (actionExecutedContext.Exception != null)
                {
                    var exception = actionExecutedContext.Exception as MdmFaultException;
                    if (exception != null)
                    {
                        log.DebugFormat("[{0}] [{1}] failed [{2}]", actionExecutedContext.Request.Method,
                            actionExecutedContext.Request.RequestUri,
                            exception.Fault.Message ?? exception.Fault.Reason);
                    }
                    else
                    {
                        log.DebugFormat("[{0}] [{1}] failed [{2}]", actionExecutedContext.Request.Method,
                            actionExecutedContext.Request.RequestUri,
                            actionExecutedContext.Exception.AllExceptionMessages());
                    }
                }
                else
                {
                    log.DebugFormat("[{0}] [{1}] returning without response or error..",
                        actionExecutedContext.Request.Method, actionExecutedContext.Request.RequestUri);
                }
            }
        }
    }
}