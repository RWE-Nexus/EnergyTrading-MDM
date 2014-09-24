using EnergyTrading.Mdm.Notifications;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

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

    /// <summary>
    /// 
    /// </summary>
    public class MdmNotificationAttribute : ActionFilterAttribute
    {
        public MdmNotificationAttribute()
        {
            MdmNotificationService = ServiceLocator.Current.GetInstance<IMdmNotificationService>();
        }

        private IMdmNotificationService MdmNotificationService { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            if (context.Response == null || !context.Response.IsSuccessStatusCode)
            {
                return;
            }

            var content = context.Response.Content as ObjectContent;
        }
    }
}