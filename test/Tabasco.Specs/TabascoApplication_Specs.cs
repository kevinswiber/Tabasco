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

        [Test]
        public void It_Passes_Form_Post_Data_Into_Action()
        {
            var response = new MockRequest(new TabascoApplication()).Post("/post",
                                                                         new Dictionary<string, dynamic> { { "input", "testData=horticulture" } });

            Assert.AreEqual("horticulture", response.Body.ToString());
        }

        [Test]
        public void It_Routes_To_Non_Root_Resources()
        {
            var response = new MockRequest(new TabascoApplication()).Get("/dork");

            Assert.AreEqual(200, response.Status);
        }

        [Test]
        public void It_Passes_Route_Parameters_As_Data()
        {
            var response = new MockRequest(new TabascoApplication()).Get("/params/test");

            Assert.AreEqual("test", response.Body.ToString());
        }
    }

    [Resource("/dork")]
    public class DorkController
    {
        [Get]
        public string Root()
        {
            return "Yo.";
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
        public string GetData(IDictionary<string, dynamic> data)
        {
            return data["testData"];
        }

        [Post("/post")]
        public string PostData(IDictionary<string, dynamic> data)
        {
            return data["testData"];
        }

        [Get("/params/:id")]
        public string RouteParams(IDictionary<string, dynamic> data)
        {
            return data[":id"];
        }
    }
}