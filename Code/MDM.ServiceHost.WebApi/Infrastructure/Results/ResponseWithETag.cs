using MDM.ServiceHost.WebApi.Infrastructure.Extensions;

namespace MDM.ServiceHost.WebApi.Infrastructure.Results
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class ResponseWithETag<T> : IHttpActionResult
    {
        private readonly HttpRequestMessage request;
        private readonly T content;
        private readonly HttpStatusCode statusCode;
        private readonly ulong version;

        public ResponseWithETag(HttpRequestMessage request, T content, HttpStatusCode statusCode, ulong version)
        {
            this.request = request;
            this.content = content;
            this.statusCode = statusCode;
            this.version = version;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = this.request.CreateResponse(this.statusCode, this.content);
            response.Headers.ETag = new EntityTagHeaderValue(this.version.ToEtag());
            response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    Private = true
                };
            return Task.FromResult(response);
        }
    }
}