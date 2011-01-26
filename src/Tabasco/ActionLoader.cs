using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tabasco
{
    public class ActionLoader
    {
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

            return actionMap.ToDictionary(pair => pair.Key, pair => pair.Value);
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
                select new KeyValuePair<string, MethodInfo>(resourceRoute + actionAttr.ActionRoute, action);
        }
    }
}