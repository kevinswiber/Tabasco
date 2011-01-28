using NUnit.Framework;

namespace Tabasco.Specs
{
    [TestFixture]
    public class ResourceLoader_Specs
    {
        [Test]
        public void It_Returns_A_Route_From_A_Type()
        {
            var resourceMap = ResourceLoader.LoadResourceMap();

            Assert.AreEqual("/book/fiction", resourceMap[typeof(Fiction)]);
        }
    }
}