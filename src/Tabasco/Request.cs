using System.Collections.Generic;

namespace Tabasco
{
    public class Request : NRack.Request
    {
        public Request(IDictionary<string, dynamic> env)
            : base(env)
        {
        }
    }
}