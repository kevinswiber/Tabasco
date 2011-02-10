using System;
using System.Collections.Generic;
using System.Linq;

namespace Tabasco.Plumbing
{
    public class ResourceLoader
    {
        public static Dictionary<Type, string> LoadResourceMap()
        {
            var types = LoadFromAssemblies();
            IEnumerable<KeyValuePair<Type, string>> resourceMap = new Dictionary<Type, string>();

            resourceMap = types.Aggregate(resourceMap,
                                          (current, resource) => current.Concat(new[] { CreateResourceMapItem(resource) }));

            return resourceMap.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static IEnumerable<Type> LoadFromAssemblies()
        {
            return AssemblyScanner.FindPublicTypesInBaseDirectory(FilterResourceAttributes);
        }

        private static bool FilterResourceAttributes(Type type)
        {
            var typesWithResourceAttribute = type.GetCustomAttributes(typeof(ResourceAttribute), false);

            return typesWithResourceAttribute.Any();
        }

        private static KeyValuePair<Type, string> CreateResourceMapItem(Type resource)
        {
            var resourceAttribute = (ResourceAttribute)resource.GetCustomAttributes(typeof(ResourceAttribute), false).First();

            return new KeyValuePair<Type, string>(resource, resourceAttribute.ResourceRoute);
        }
    }
}