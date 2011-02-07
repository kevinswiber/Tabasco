using System.Collections.Generic;
using NRack.Helpers;

namespace Tabasco.Example.AspNet
{
    [Resource("/")]
    public class Main
    {
        [Get]
        [Get("/default.aspx")] // Needed for Visual Studio Web Server.
        public IView Index()
        {
            return new Spark();
        }

        [Post("/name")]
        public IView Name(IDictionary<string, string> data)
        {
            return new Razor(new { Name = data["name"] });
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