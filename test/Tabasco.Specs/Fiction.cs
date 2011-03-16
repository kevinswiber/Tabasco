namespace Tabasco.Specs
{
    public class Fiction
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