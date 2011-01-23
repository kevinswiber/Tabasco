using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tabasco
{
    public class RoutePilot
    {
        public IDictionary<string, MethodInfo> ActionMapper;
        public static Func<string> FindAction(string route)
        {
            throw new NotImplementedException();
        }
    }
}