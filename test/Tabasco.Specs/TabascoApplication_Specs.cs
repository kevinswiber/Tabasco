using NRack;
using NRack.Helpers;
using NRack.Mock;
using NUnit.Framework;

namespace Tabasco.Specs
{
    [TestFixture]
    public class TabascoApplication_Specs
    {
        public class TestApp : TabascoApplication
        {

        }

        [Test]
        public void It_Should_Be_Callable()
        {
            Assert.IsTrue(typeof(ICallable).IsAssignableFrom(typeof(TabascoApplication)));
        }

        [Test]
        public void It_Should_Process_Requests()
        {
            var response = new MockRequest(new TestApp()).Get("/");

            Assert.AreEqual(200, response.Status);
            Assert.AreEqual("Bam!", response.Body.ToString());
        }

        [Test]
        public void It_Should_Load_As_NRack_Application()
        {
            var builder = new Builder()
                .Run(new TestApp())
                .ToApp();

            var response = builder.Call(new MockRequest().EnvironmentFor("/"));
            Assert.AreEqual(200, response[0]);
            Assert.AreEqual("Bam!", response[2].ToString());
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
    }
}