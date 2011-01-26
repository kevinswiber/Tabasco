namespace Tabasco.Specs.Controllers
{
    [Resource("/book/fiction")]
    public class FictionController
    {
        [Get("/")]
        public string Root(string splat)
        {
            return "Hello" + splat;
        }

        [Post("/create")]
        public void Create(object formData)
        {

        }
    }
}