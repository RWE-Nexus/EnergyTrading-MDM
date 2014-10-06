namespace MDM.ServiceHost.WebApi.Filters
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http.Filters;

    /// <summary>
    /// Performs ETag IfNoneMatch checking on GET requests where an ETag is supplied in the header and updates the response accordingly.
    /// </summary>
    public class ETagCheckingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var requestEtagHeader = actionExecutedContext.Request.Headers.IfNoneMatch.FirstOrDefault();
            var requestEtag = requestEtagHeader == null ? null : requestEtagHeader.Tag;

            if (requestEtag == null)
            {
                return;
            }

            var responseEtagHeader = (actionExecutedContext.Response == null || actionExecutedContext.Response.Headers == null) ? null : actionExecutedContext.Response.Headers.ETag;
            var responseEtag = responseEtagHeader == null ? null : responseEtagHeader.Tag;

            if (responseEtag == requestEtag)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotModified);
                actionExecutedContext.Response.Headers.ETag = new EntityTagHeaderValue(requestEtag);
            }
        }
    }
}