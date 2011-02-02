
using System;

namespace Tabasco
{
    public class RequestLine
    {
        public string Method { get; set; }
        public string ScriptName { get; set; }
        public string PathInfo { get; set; }
        public string QueryString { get; set; }

        public static RequestLine Create(string method, string scriptName, string pathInfo = null, string queryString = null)
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

            if (scriptName.EndsWith("/"))
            {
                scriptName = scriptName.Remove(scriptName.Length - 1);
            }

            if (pathInfo != string.Empty && !pathInfo.StartsWith("/"))
            {
                pathInfo = "/" + pathInfo;
            }

            var requestLine = new RequestLine
                                  {
                                      Method = method,
                                      ScriptName = scriptName,
                                      PathInfo = pathInfo,
                                      QueryString = queryString
                                  };


            return requestLine;
        }

        public string GetUri()
        {
            var queryString = QueryString == string.Empty ? string.Empty : "?" + QueryString;

            var uri = ScriptName + PathInfo + queryString;

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