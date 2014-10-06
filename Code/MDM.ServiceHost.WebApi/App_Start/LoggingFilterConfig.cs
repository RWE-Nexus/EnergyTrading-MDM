using System.Web.Http;
using MDM.ServiceHost.WebApi.Filters;

namespace MDM.ServiceHost.WebApi
{
    /// <summary>
    /// Configures the logging filter to intercept all requests and output debug information
    /// </summary>
    public static class LoggingFilterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new LoggingAttribute());
        }
    }
}