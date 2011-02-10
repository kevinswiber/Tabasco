using NUnit.Framework;
using Tabasco.Plumbing;

namespace Tabasco.Specs
{
    [TestFixture]
    public class ActionLoader_Specs
    {
        [Test]
        public void It_Returns_A_Method_From_A_Route()
        {
            var actionMap = ActionLoader.LoadActionMap(ResourceLoader.LoadResourceMap());

            Assert.AreEqual(typeof(Fiction).GetMethod("Create"), actionMap["POST /book/fiction/create"]);
        }

        [Test]
        public void It_Returns_A_Root_Path_Without_A_Trailing_Slash()
        {
            var actionMap = ActionLoader.LoadActionMap(ResourceLoader.LoadResourceMap());

            Assert.IsTrue(actionMap.ContainsKey("GET /book/fiction"));
        }
    }
}