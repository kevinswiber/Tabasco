using System.Collections.Generic;
using NRack.Helpers;

namespace Tabasco.Example
{
    [Resource("/")]
    public class Main
    {
        const string HtmlSkeleton = @"<html>
                                        <head>
                                            <title>Super Tabasco Example</title>
                                            <link href='/styles/site.css' rel='stylesheet' type='text/css' media='screen' />
                                        </head>
                                        <body>{0}</body>
                                      </html>";

        [Get]
        public string Root()
        {

            const string header = "<h1>Hello, Tabasco!</h1><br/>";

            const string form = @"<form method='post' action='/name'>
                                    <label for='name'>What's your name?</label><br/>
                                    <input type='text' name='name'/><br/><br/>
                                    <input type='submit'/>
                                  </form>";

            return string.Format(HtmlSkeleton, header + form);
        }

        [Post("/name")]
        public string Name(IDictionary<string, dynamic> data)
        {
            var name = System.Web.HttpUtility.HtmlEncode(data["name"]);

            var header = string.Format("<h1>Ahoy, {0}!</h1>", name);
            var link = "<a href='/doctor?who=" + name + "'>Click to see your secret message.</a>";

            return string.Format(HtmlSkeleton, header + link);
        }

        [Get("/doctor")]
        public dynamic[] Pepper(IDictionary<string, dynamic> data)
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