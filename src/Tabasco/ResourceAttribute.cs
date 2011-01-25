using System;

namespace Tabasco
{
    public class ResourceAttribute : Attribute
    {
        public string ResourceRoute { get; set; }

        public ResourceAttribute(string resourceRoute)
        {
            ResourceRoute = resourceRoute;
        }
    }
}