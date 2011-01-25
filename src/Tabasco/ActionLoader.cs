using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tabasco
{
    public class ActionLoader
    {
        public IDictionary<string, MethodInfo> LoadActionMap(IEnumerable<Type> types)
        {
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
            actionMap = actions.Aggregate(actionMap, (current, action) => current.Concat(CreateActionMapItems(action)));

            return actionMap.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static IEnumerable<MethodInfo> GetActions(Type type)
        {
            return type.GetMethods().Where(
                methodInfo => methodInfo.GetCustomAttributes(typeof(GetAttribute), false).Any());
        }

        private static IEnumerable<KeyValuePair<string, MethodInfo>> CreateActionMapItems(MethodInfo action)
        {
            var actionAttributes = action.GetCustomAttributes(typeof(GetAttribute), false);

            return
                from GetAttribute actionAttr in actionAttributes
                select new KeyValuePair<string, MethodInfo>(actionAttr.ActionRoute, action);
        }
    }
}