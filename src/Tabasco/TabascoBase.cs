using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using NRack;
using NRack.Helpers;
using Tabasco.Plumbing;

namespace Tabasco
{
    public class TabascoBase : ICallable
    {
        private readonly IDictionary<string, MethodInfo> _actionMap;

        public Request Request { get; private set; }
        public IDictionary<string, dynamic> Params { get; set; }

        public TabascoBase()
        {
            _actionMap = ActionLoader.GetActionMap(GetType());
            _actionMap = _actionMap.OrderByDescending(x => x.Key.Length).ToDictionary(pair => pair.Key,
                                                                                      pair => pair.Value);
        }

        public dynamic[] Call(IDictionary<string, dynamic> environment)
        {
            return CallInternal(environment);
        }

        public dynamic[] CallInternal(IDictionary<string, dynamic> environment)
        {
            Request = new Request(environment);

            string method = environment["REQUEST_METHOD"].ToString();
            string pathInfo = environment["PATH_INFO"].ToString();
            string queryString = environment["QUERY_STRING"].ToString();

            var requestLine = RequestLine.Create(method, pathInfo, queryString);
            var requestLineStripped = requestLine.ToString().StripQueryString();

            string actionKey = requestLineStripped;
            IDictionary<string, dynamic> routeParameters = new Dictionary<string, dynamic>();

            if (!_actionMap.ContainsKey(actionKey))
            {
                foreach (var key in _actionMap.Keys)
                {
                    var parser = new PatternParser(key);
                    if (parser.IsMatch(requestLineStripped))
                    {
                        actionKey = key;
                        routeParameters = parser.Match(requestLineStripped);
                        break;
                    }
                }
            }

            if (!_actionMap.ContainsKey(actionKey))
            {
                return new dynamic[] { 404, new Hash { { "Content-Type", "text/plain" } }, "Not found: " + requestLine.GetUri().StripQueryString() };
            }

            var methodInfo = _actionMap[actionKey];

            var dataDictionary = new Dictionary<string, dynamic>();

            if (routeParameters.Count > 0)
            {
                dataDictionary = dataDictionary.Concat(routeParameters).ToDictionary(pair => pair.Key,
                                                                                     pair => pair.Value);
            }

            var requestParams = Request.Params.ToDictionary(kvp => kvp.Key, kvp => (dynamic)kvp.Value);
            Params = requestParams.Concat(dataDictionary)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var actionResponse = methodInfo.Invoke(this, null);

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
    }
}