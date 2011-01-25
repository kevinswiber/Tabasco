using System;
using System.Collections.Generic;
using System.Linq;

namespace Tabasco
{
    public class ResourceLoader
    {
        public IEnumerable<Type> LoadFromAssemblies()
        {
            return AssemblyScanner.FindPublicTypesInBaseDirectory(FilterResourceAttributes);
        }

        private static bool FilterResourceAttributes(Type type)
        {
            var typesWithResourceAttribute = type.GetCustomAttributes(typeof(ResourceAttribute), false);

            return typesWithResourceAttribute.Any();
        }

        public Dictionary<string, Type> LoadResourceMap()
        {
            var types = LoadFromAssemblies();
            IEnumerable<KeyValuePair<string, Type>> resourceMap = new Dictionary<string, Type>();

            resourceMap = types.Aggregate(resourceMap,
                                          (current, resource) => current.Concat(new[] { CreateResourceMapItem(resource) }));

            return resourceMap.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private KeyValuePair<string, Type> CreateResourceMapItem(Type resource)
        {
            var resourceAttribute = (ResourceAttribute)resource.GetCustomAttributes(typeof(ResourceAttribute), false).First();

            return new KeyValuePair<string, Type>(resourceAttribute.ResourceRoute, resource);
        }
    }
}