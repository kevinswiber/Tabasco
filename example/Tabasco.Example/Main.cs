using System.Collections.Generic;
using NRack.Helpers;

namespace Tabasco.Example
{
    [Resource("/")]
    public class Main
    {
        [Get]
        public string Root()
        {
            return "Hello, Tabasco!";
        }

        [Get("/doctor")]
        public dynamic[] Pepper(IDictionary<string, string> data)
        {
            var who = data.ContainsKey("who") ? data["who"] : "you";

            return new dynamic[]
                       {
                           200, 
                           new Hash { { "Content-Type", "text/plain" } }, 
                           "Wouldn't " + who + " like to be a pepper, too?"
                       };
        }
    }
}