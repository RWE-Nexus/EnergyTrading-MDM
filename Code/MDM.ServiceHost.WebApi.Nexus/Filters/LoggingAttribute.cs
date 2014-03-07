namespace EnergyTrading.MDM.ServiceHost.WebApi.Nexus.Filters
{
    using System.Web.Http.Filters;

    using log4net;
    using log4net.Config;

    public class LoggingAttribute : ActionFilterAttribute
    {
        public LoggingAttribute()
        {
            XmlConfigurator.Configure();
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var log = LogManager.GetLogger(actionContext.ControllerContext.Controller.GetType().Name);
            var method = actionContext.Request.Method;

            // Just a stub - actual logging can use the requested method and controller
            // Types to log
            log.Debug("Calling");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var log = LogManager.GetLogger(actionExecutedContext.ActionContext.ControllerContext.Controller.GetType().Name);

            // Just a stub - actual logging can use the requested method and controller
            // Types (and the response code) to log
            log.Debug("Called");
        }
    }
}