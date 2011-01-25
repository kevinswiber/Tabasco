namespace Tabasco.Specs.Controllers
{
    [Resource("/library")]
    public class LibraryController
    {
        [Get("/catalog")]
        public string Catalog()
        {
            return "Catalog!";
        }
    }
}