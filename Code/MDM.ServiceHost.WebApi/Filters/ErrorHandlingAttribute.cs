using System;
using MDM.ServiceHost.WebApi.Infrastructure.Exceptions;

namespace MDM.ServiceHost.WebApi.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    using EnergyTrading.Extensions;
    using EnergyTrading.Mdm.Services;
    using EnergyTrading.Validation;

    using EnergyTrading.Mdm.Contracts;

    /// <summary>
    /// This handles exceptions thrown by the controllers and converts them to appropriate responses with status codes and error details as
    /// expected by the MDM Client library.
    /// </summary>
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

                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(statusCode, fault);
            }
            else if (exception is NotFoundException)
            {
                var fault = new Fault
                {
                    Message = exception.Message,
                    Reason = exception.Message
                };

                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.NotFound, fault);
            }
            else if (exception is MdmFaultException)
            {
                // At the moment, any such exceptions are returned as NotFound, but this may need to be changed if other exception types are bundled up
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.NotFound, ((MdmFaultException)exception).Fault);
            }
            else if (actionExecutedContext.Exception is NotImplementedException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            else
            {
                var fault = new Fault
                {
                    Message = actionExecutedContext.Exception.AllExceptionMessages(),
                    Reason = "Unknown"
                };

                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, fault);
            }
        }

    }
}