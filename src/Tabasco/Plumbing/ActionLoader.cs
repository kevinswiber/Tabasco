using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tabasco.Plumbing
{
    public class ActionLoader
    {
        private static IDictionary<string, MethodInfo> _actionMap;

        public static IDictionary<string, MethodInfo> LoadActionMap(IDictionary<Type, string> resourceMap)
        {
            var types = resourceMap.Keys;

            /*
             * 1. Return action methods for each type.
             * 2. Remove empties.
             * 3. Flatten.
            */
            var actions = types.Select(GetActions)
                            .Where(methodInfos => methodInfos.Any())
                            .SelectMany(methodInfos => methodInfos);

            // Stage an empty seed.
            IEnumerable<KeyValuePair<string, MethodInfo>> actionMap = new Dictionary<string, MethodInfo>();

            // Add a newly created key-value pair to actionMap for each action
            actionMap = actions.Aggregate(actionMap, (current, action) => current.Concat(CreateActionMapItems(resourceMap, action)));

            _actionMap = actionMap.ToDictionary(pair => pair.Key, pair => pair.Value);

            return _actionMap;
        }

        private static IEnumerable<MethodInfo> GetActions(Type type)
        {
            return type.GetMethods().Where(
                methodInfo => methodInfo
                    .GetCustomAttributes(false)
                    .Where(attr => typeof(ActionAttribute).IsAssignableFrom(attr.GetType()))
                    .Any());
        }

        private static IEnumerable<KeyValuePair<string, MethodInfo>> CreateActionMapItems(IDictionary<Type, string> resourceMap, MethodInfo action)
        {
            var actionAttributes =
                action.GetCustomAttributes(false).Where(
                    attr => typeof(ActionAttribute).IsAssignableFrom(attr.GetType()));

            var resourceRoute = resourceMap[action.DeclaringType];

            return
                from ActionAttribute actionAttr in actionAttributes
                select new KeyValuePair<string, MethodInfo>(FormatRoute(GetHttpMethod(actionAttr.GetType()), resourceRoute, actionAttr.ActionRoute), action);
        }

        private static string FormatRoute(string method, string resourceRoute, string actionRoute)
        {
            if (resourceRoute.EndsWith("/"))
            {
                resourceRoute = resourceRoute.Remove(resourceRoute.Length - 1);
            }

            if (!actionRoute.StartsWith("/"))
            {
                actionRoute = "/" + actionRoute;
            }

            var fullPath = resourceRoute + actionRoute;

            if (fullPath.EndsWith("/") && fullPath.Length > 1)
            {
                fullPath = fullPath.Remove(fullPath.Length - 1);
            }

            return string.Format("{0} {1}", method, fullPath);
        }

        private static string GetHttpMethod(Type actionAttributeType)
        {
            return actionAttributeType.Name.Replace("Attribute", string.Empty).ToUpperInvariant();
        }

        public static IDictionary<string, MethodInfo> GetActionMap(IDictionary<Type, string> resourceMap)
        {
            if (_actionMap == null)
            {
                LoadActionMap(resourceMap);
            }

            return _actionMap;
        }
    }
}