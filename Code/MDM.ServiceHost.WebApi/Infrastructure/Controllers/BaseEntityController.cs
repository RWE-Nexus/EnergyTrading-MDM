namespace MDM.ServiceHost.WebApi.Infrastructure.Controllers
{
    using System.Collections.Specialized;
    using System.Net.Http;
    using System.Transactions;
    using System.Web.Http;

    public class BaseEntityController : ApiController
    {
        /// <summary>
        /// Standard transaction options for reading data
        /// </summary>
        /// <returns></returns>
        protected static TransactionOptions ReadOptions()
        {
            return new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted };
        }

        /// <summary>
        /// Standard transactions options for writing data
        /// </summary>
        /// <returns></returns>
        protected static TransactionOptions WriteOptions()
        {
            // Need this level to avoid identifiers being inserted for the same system against multiple master records
            return new TransactionOptions { IsolationLevel = IsolationLevel.Serializable };
        }

        protected NameValueCollection QueryParameters
        {
            // HACK
            get { return this.Request.RequestUri.ParseQueryString(); }
        }
    }
}
