using System.Collections.Generic;
using NRack;
using NRack.Helpers;
using NRack.Mock;
using NUnit.Framework;

namespace Tabasco.Specs
{
    [TestFixture]
    public class TabascoApplication_Specs
    {
        [Test]
        public void It_Is_Callable()
        {
            Assert.IsTrue(typeof(ICallable).IsAssignableFrom(typeof(TabascoApplication)));
        }

        [Test]
        public void It_Processes_Requests()
        {
            var response = new MockRequest(new TabascoApplication()).Get("/");

            Assert.AreEqual(200, response.Status);
            Assert.AreEqual("Bam!", response.Body.ToString());
        }

        [Test]
        public void It_Loads_As_An_NRack_Application()
        {
            var builder = new Builder()
                .Run(new TabascoApplication())
                .ToApp();

            var response = builder.Call(new MockRequest().EnvironmentFor("/"));
            Assert.AreEqual(200, response[0]);
            Assert.AreEqual("Bam!", response[2].ToString());
        }

        [Test]
        public void It_Passes_Query_String_Data_Into_Action()
        {
            var response = new MockRequest(new TabascoApplication()).Get("/data?testData=horticulture");

            Assert.AreEqual("horticulture", response.Body.ToString());
        }
    }

    [Resource("/")]
    public class TestController
    {
        [Get]
        public dynamic[] GetSpicy()
        {
            return new dynamic[] { 200, new Hash { { "Content-Type", "text/html" } }, "Bam!" };
        }

        [Get("/data")]
        public string GetData(IDictionary<string, string> data)
        {
            return data["testData"];
        }
    }
}