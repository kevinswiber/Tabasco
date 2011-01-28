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
    }
}