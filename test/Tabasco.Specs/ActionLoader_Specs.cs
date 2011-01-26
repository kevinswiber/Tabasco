using NUnit.Framework;
using Tabasco.Specs.Controllers;

namespace Tabasco.Specs
{
    [TestFixture]
    public class ActionLoader_Specs
    {
        [Test]
        public void It_Returns_A_Method_From_A_Route()
        {
            var actionMap = ActionLoader.LoadActionMap(ResourceLoader.LoadResourceMap());

            Assert.AreEqual(typeof(FictionController).GetMethod("Create"), actionMap[@"/book/fiction/create"]);
        }
    }
}