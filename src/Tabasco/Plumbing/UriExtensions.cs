namespace Tabasco.Plumbing
{
    public static class UriExtensions
    {
        public static string StripQueryString(this string uri)
        {
            if (uri.IndexOf('?') > -1)
            {
                return uri.Substring(0, uri.IndexOf('?'));
            }

            return uri;
        }
    }
}