using System;
using System.Collections.Generic;
using NRack;
using NRack.Helpers;

namespace Tabasco
{
    public class TabascoApplication : ICallable
    {
        #region Implementation of ICallable

        public dynamic[] Call(IDictionary<string, dynamic> environment)
        {
            var resourceMap = ResourceLoader.LoadResourceMap();
            var actionMap = ActionLoader.LoadActionMap(resourceMap);

            string method = environment["REQUEST_METHOD"].ToString();
            string scriptName = environment["SCRIPT_NAME"].ToString();
            string pathInfo = environment["PATH_INFO"].ToString();
            string queryString = environment["QUERY_STRING"].ToString();
            queryString = queryString == string.Empty ? string.Empty : "?" + queryString;

            if (scriptName.EndsWith("/"))
            {
                scriptName = scriptName.Remove(scriptName.Length - 1);
            }

            if (!pathInfo.StartsWith("/"))
            {
                pathInfo = "/" + pathInfo;
            }

            var path = scriptName + pathInfo + queryString;

            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }

            var key = method + " " + path;

            if (!actionMap.ContainsKey(key))
            {
                return new dynamic[] { 404, new Hash { { "Content-Type", "text/plain" } }, "Not found: " + path };
            }

            var methodInfo = actionMap[method + " " + path];

            var resource = Activator.CreateInstance(methodInfo.DeclaringType);
            var actionResponse = methodInfo.Invoke(resource, null);

            if (actionResponse is string)
            {
                return new dynamic[] { 200, new Hash { { "Content-Type", "text/html" } }, actionResponse };
            }

            if (actionResponse is dynamic[])
            {
                return actionResponse as dynamic[];
            }

            return new dynamic[] { 500, new Hash { { "Content-Type", "text/plain" } }, "Internal server error." };
        }

        #endregion
    }
}