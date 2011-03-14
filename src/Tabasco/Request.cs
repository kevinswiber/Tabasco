using System.Collections.Generic;

namespace Tabasco
{
    public class Request : NRack.Request
    {
        public Request(IDictionary<string, dynamic> env)
            : base(env)
        {
            Params = new Dictionary<string, dynamic>();
        }

        public IDictionary<string, dynamic> Params { get; set; }
    }
}