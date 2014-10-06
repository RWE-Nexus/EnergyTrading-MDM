using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MDM.ServiceHost.WebApi.Filters
{
    /// <summary>
    /// Checks the model state and immediately sends back a BadRequest if invalid
    /// 
    /// This relies on System.ComponentModel.DataAnnotations on the MDM contracts (which there are none currently).
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}