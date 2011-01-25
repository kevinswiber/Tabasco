using System.Linq;
using NUnit.Framework;
using Tabasco.Specs.Controllers;

namespace Tabasco.Specs
{
    [TestFixture]
    public class ResourceLoader_Specs
    {
        [Test]
        public void It_Loads_Resources_From_Assembly()
        {
            var loader = new ResourceLoader();

            Assert.IsTrue(loader.LoadFromAssemblies().Contains(typeof(LibraryController)));
        }

        [Test]
        public void It_Loads_Resource_Map()
        {
            var loader = new ResourceLoader();
            var resourceMap = loader.LoadResourceMap();

            Assert.IsTrue(resourceMap.ContainsKey("/book/fiction"));
        }
    }
}