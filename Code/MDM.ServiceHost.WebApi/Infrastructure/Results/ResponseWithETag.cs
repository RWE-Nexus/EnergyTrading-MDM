namespace MDM.ServiceHost.WebApi.Infrastructure.Results
{
    using System.Globalization;
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
        private readonly long version;

        public ResponseWithETag(HttpRequestMessage request, T content, HttpStatusCode statusCode, long version)
        {
            this.request = request;
            this.content = content;
            this.statusCode = statusCode;
            this.version = version;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = this.request.CreateResponse(this.statusCode, this.content);
            var etag = string.Format("\"{0}\"", this.version.ToString(CultureInfo.InvariantCulture));
            response.Headers.ETag = new EntityTagHeaderValue(etag);
            response.Headers.CacheControl = new CacheControlHeaderValue();
            response.Headers.CacheControl.Private = true;
            return Task.FromResult(response);
        }
    }
}