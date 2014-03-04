namespace MDM.ServiceHost.WebApi.Infrastructure
{
    using System;

    using MDM.ServiceHost.WebApi.Infrastructure.ETags;

    public static class Extensions
    {
        public static long ToVersion(this ETag etag)
        {
            if (etag == null)
            {
                return 0;
            }

            return long.Parse(etag.Tag.Replace("\"", ""));
        }

        public static string ToEtag(this long version)
        {
            return "\"" + version + "\"";
        }

        public static bool Contains(this string source, string comparison, StringComparison stringComparison)
        {
            return source.IndexOf(comparison, stringComparison) >= 0;
        }
    }
}