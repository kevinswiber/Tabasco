using System.Collections.Generic;
using NRack.Helpers;
using Tabasco.Example.AspNet.models;

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
            var model = new Doctor { Name = data["name"] };

            return new Spark(model);
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