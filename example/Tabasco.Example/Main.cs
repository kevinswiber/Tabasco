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
        public dynamic[] Pepper()
        {
            return new dynamic[]
                       {
                           200, 
                           new Hash { { "Content-Type", "text/plain" } }, 
                           "Wouldn't you like to be a pepper, too?"
                       };
        }
    }
}