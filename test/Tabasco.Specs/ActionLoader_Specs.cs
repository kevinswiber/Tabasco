using NUnit.Framework;

namespace Tabasco.Specs
{
    [TestFixture]
    public class ActionLoader_Specs
    {
        [Test]
        public void It_Returns_A_Method_From_A_Route()
        {
            var actionMap = ActionLoader.LoadActionMap(ResourceLoader.LoadResourceMap());

            Assert.AreEqual(typeof(Fiction).GetMethod("Create"), actionMap[@"POST /book/fiction/create"]);
        }
    }
}