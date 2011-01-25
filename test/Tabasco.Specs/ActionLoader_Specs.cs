using NUnit.Framework;

namespace Tabasco.Specs
{
    [TestFixture]
    public class ActionLoader_Specs
    {
        [Test]
        public void It_Returns_An_Action_Map_From_Resources_Keyed_By_Route()
        {
            var loader = new ActionLoader();

            var actionMap = loader.LoadActionMap(new ResourceLoader().LoadFromAssemblies());

            Assert.IsTrue(actionMap.ContainsKey("/book/fiction/*"));
        }
    }
}