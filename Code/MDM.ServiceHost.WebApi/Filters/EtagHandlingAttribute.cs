namespace MDM.ServiceHost.WebApi.Filters
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http.Filters;

    public class ETagCheckingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var requestEtagHeader = actionExecutedContext.Request.Headers.IfNoneMatch.FirstOrDefault();
            var requestEtag = requestEtagHeader == null ? null : requestEtagHeader.Tag;
            var responseEtagHeader = actionExecutedContext.Response.Headers.ETag;
            var responseEtag = responseEtagHeader == null ? null : responseEtagHeader.Tag;

            if (responseEtag == requestEtag)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotModified);
                actionExecutedContext.Response.Headers.ETag = new EntityTagHeaderValue(requestEtag);
            }
        }
    }
}