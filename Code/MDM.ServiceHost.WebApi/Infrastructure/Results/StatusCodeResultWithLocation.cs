namespace MDM.ServiceHost.WebApi.Infrastructure.Results
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class StatusCodeResultWithLocation : IHttpActionResult
    {
        private readonly HttpRequestMessage request;
        private readonly HttpStatusCode statusCode;
        private readonly string location;

        public StatusCodeResultWithLocation(HttpRequestMessage request, HttpStatusCode statusCode, string location)
        {
            this.request = request;
            this.statusCode = statusCode;
            this.location = location;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = this.request.CreateResponse(this.statusCode);
            response.Headers.Location = new Uri(this.location, UriKind.Relative);
            response.Headers.CacheControl = new CacheControlHeaderValue();
            response.Headers.CacheControl.Private = true;
            return Task.FromResult(response);
        }
    }
}