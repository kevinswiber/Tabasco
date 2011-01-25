using System;

namespace Tabasco
{
    public class GetAttribute : Attribute
    {
        public GetAttribute(string actionRoute)
        {
            ActionRoute = actionRoute;
        }

        public string ActionRoute { get; set; }
    }
}