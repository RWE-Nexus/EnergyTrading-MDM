namespace MDM.ServiceHost.WebApi.Infrastructure.ETags
{
    using System.Web.Http;
    using System.Web.Http.Controllers;

    public abstract class ETagMatchAttribute : ParameterBindingAttribute
    {
        private ETagMatch _match;

        public ETagMatchAttribute(ETagMatch match)
        {
            this._match = match;
        }

        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            if (parameter.ParameterType == typeof(ETag))
            {
                return new ETagParameterBinding(parameter, this._match);
            }
            return parameter.BindAsError("Wrong parameter type");
        }
    }

    public class IfMatchAttribute : ETagMatchAttribute
    {
        public IfMatchAttribute()
            : base(ETagMatch.IfMatch)
        {
        }
    }

    public class IfNoneMatchAttribute : ETagMatchAttribute
    {
        public IfNoneMatchAttribute()
            : base(ETagMatch.IfNoneMatch)
        {
        }
    }
}