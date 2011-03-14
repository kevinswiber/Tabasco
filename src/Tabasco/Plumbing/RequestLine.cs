
using System;

namespace Tabasco.Plumbing
{
    public class RequestLine
    {
        public string Method { get; set; }
        public string PathInfo { get; set; }
        public string QueryString { get; set; }

        public static RequestLine Create(string method, string pathInfo = null, string queryString = null)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (pathInfo == null)
            {
                pathInfo = string.Empty;
            }

            if (queryString == null)
            {
                queryString = string.Empty;
            }

            if (pathInfo != string.Empty && !pathInfo.StartsWith("/"))
            {
                pathInfo = "/" + pathInfo;
            }

            if (pathInfo.EndsWith("/"))
            {
                pathInfo = pathInfo.Remove(pathInfo.Length - 1);
            }

            var requestLine = new RequestLine
                                  {
                                      Method = method,
                                      PathInfo = pathInfo,
                                      QueryString = queryString
                                  };


            return requestLine;
        }

        public string GetUri()
        {
            var queryString = QueryString == string.Empty ? string.Empty : "?" + QueryString;

            var uri = PathInfo + queryString;

            if (!uri.StartsWith("/"))
            {
                uri = "/" + uri;
            }

            return uri;
        }

        public override string ToString()
        {
            var requestLineString = Method + " " + GetUri();

            return requestLineString;
        }
    }
}