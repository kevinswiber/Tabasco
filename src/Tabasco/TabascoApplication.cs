using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using NRack;
using NRack.Helpers;

namespace Tabasco
{
    public class TabascoApplication : ICallable
    {
        private readonly IDictionary<string, MethodInfo> _actionMap;

        public TabascoApplication()
        {
            var resourceMap = ResourceLoader.LoadResourceMap();
            _actionMap = ActionLoader.LoadActionMap(resourceMap);
        }

        #region Implementation of ICallable

        public dynamic[] Call(IDictionary<string, dynamic> environment)
        {
            string method = environment["REQUEST_METHOD"].ToString();
            string scriptName = environment["SCRIPT_NAME"].ToString();
            string pathInfo = environment["PATH_INFO"].ToString();
            string queryString = environment["QUERY_STRING"].ToString();

            var requestLine = RequestLine.Create(method, scriptName, pathInfo, queryString);
            var actionKey = requestLine.ToString().StripQueryString();

            if (!_actionMap.ContainsKey(actionKey))
            {
                return new dynamic[] { 404, new Hash { { "Content-Type", "text/plain" } }, "Not found: " + requestLine.GetUri().StripQueryString() };
            }

            var methodInfo = _actionMap[actionKey];

            var resource = Activator.CreateInstance(methodInfo.DeclaringType);

            object[] parameters = null;

            if (methodInfo.GetParameters().Length > 0)
            {
                if (methodInfo.GetParameters()[0].ParameterType == typeof(IDictionary<string, string>))
                {
                    NameValueCollection queryStringData = null;

                    if (!string.IsNullOrEmpty(requestLine.QueryString))
                    {
                        queryStringData = Utils.ParseQuery(requestLine.QueryString);
                    }

                    var dataDictionary = new Dictionary<string, string>();

                    if (queryStringData != null)
                    {
                        foreach (var key in queryStringData.Keys)
                        {
                            dataDictionary[key.ToString()] = queryStringData[key.ToString()];
                        }
                    }

                    if (environment["rack.input"] != null)
                    {
                        var stream = (Stream)environment["rack.input"];

                        stream.Position = 0;

                        var streamReader = new StreamReader(stream);

                        var requestBody = streamReader.ReadToEnd();

                        var postData = Utils.ParseQuery(requestBody);

                        foreach (var key in postData.Keys)
                        {
                            dataDictionary[key.ToString()] = postData[key.ToString()];
                        }
                    }

                    parameters = new[] { dataDictionary };
                }
            }

            var actionResponse = methodInfo.Invoke(resource, parameters);

            if (actionResponse is string || actionResponse is IIterable)
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