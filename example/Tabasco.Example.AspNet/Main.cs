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
        [Get("/doctor/:who")]
        public dynamic[] Pepper(IDictionary<string, string> data)
        {
            var who = data.ContainsKey(":who") ? NRack.Utils.Unescape(data[":who"]) : "you";

            return new dynamic[]
                       {
                           200, 
                           new Hash { { "Content-Type", "text/plain" } }, 
                           "Wouldn't " + who + " like to be a pepper, too?"
                       };
        }
    }

    [Resource("/dork")]
    public class Dork
    {
        [Get]
        public string Root()
        {
            return "<html><body><h1>Oh, Snap!</h1></body></html>";
        }
    }
}