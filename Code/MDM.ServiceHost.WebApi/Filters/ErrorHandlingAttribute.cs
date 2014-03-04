namespace MDM.ServiceHost.WebApi.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Web.Http.Filters;

    using EnergyTrading.Extensions;
    using EnergyTrading.MDM.Services;
    using EnergyTrading.Validation;

    using RWEST.Nexus.MDM.Contracts;

    public class ErrorHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;

            if (exception is VersionConflictException || exception is ValidationException)
            {
                var fault = new Fault
                {
                    Message = actionExecutedContext.Exception.Message,
                    Reason = "Validation failure"
                };

                var statusCode = exception is VersionConflictException
                                     ? HttpStatusCode.PreconditionFailed
                                     : HttpStatusCode.BadRequest;

                actionExecutedContext.Response = new HttpResponseMessage()
                    {
                        StatusCode = statusCode,
                        Content = new ObjectContent(typeof(Fault), fault, new XmlMediaTypeFormatter())
                    };
            }
            else
            {
                var fault = new Fault
                {
                    Message = actionExecutedContext.Exception.AllExceptionMessages(),
                    Reason = "Unknown"
                };

                actionExecutedContext.Response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new ObjectContent(typeof(Fault), fault, new XmlMediaTypeFormatter())
                };
            }
        }

    }
}