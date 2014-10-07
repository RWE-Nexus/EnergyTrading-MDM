using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using EnergyTrading.Extensions;
using EnergyTrading.Mdm.Contracts;
using EnergyTrading.Mdm.Services;
using EnergyTrading.Validation;
using MDM.ServiceHost.WebApi.Infrastructure.Exceptions;

namespace MDM.ServiceHost.WebApi.Filters
{
    /// <summary>
    /// A global exception handler which analyses the thrown exception and creates an appropriate response / status code
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;

            if (exception is VersionConflictException || exception is ValidationException)
            {
                var fault = new Fault
                {
                    Message = context.Exception.Message,
                    Reason = "Validation failure"
                };

                var statusCode = exception is VersionConflictException
                    ? HttpStatusCode.PreconditionFailed
                    : HttpStatusCode.BadRequest;

                context.Result = new ErrorMessageResult(context.Request.CreateResponse(statusCode, fault));
            }
            else if (exception is NotFoundException)
            {
                var fault = new Fault
                {
                    Message = exception.Message,
                    Reason = exception.Message
                };

                context.Result = new ErrorMessageResult(context.Request.CreateResponse(HttpStatusCode.NotFound, fault));
            }
            else if (exception is MdmFaultException)
            {
                // At the moment, any such exceptions are returned as NotFound, but this may need to be changed if other exception types are bundled up
                context.Result = new ErrorMessageResult(context.Request.CreateResponse(HttpStatusCode.NotFound, ((MdmFaultException)exception).Fault));
            }
            else if (context.Exception is NotImplementedException)
            {
                context.Result = new ErrorMessageResult(new HttpResponseMessage(HttpStatusCode.NotImplemented));
            }
            else
            {
                var fault = new Fault
                {
                    Message = context.Exception.AllExceptionMessages(),
                    Reason = "Unknown"
                };

                context.Result = new ErrorMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError, fault));
            }
        }

        class ErrorMessageResult : IHttpActionResult
        {
            private readonly HttpResponseMessage httpResponseMessage;


            public ErrorMessageResult(HttpResponseMessage httpResponseMessage)
            {
                this.httpResponseMessage = httpResponseMessage;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(httpResponseMessage);
            }
        }
    }
}