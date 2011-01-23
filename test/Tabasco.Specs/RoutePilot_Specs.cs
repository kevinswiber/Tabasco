using System;
using System.IO;
using GivenFramework;
using NUnit.Framework;

namespace Tabasco.Specs
{
    [TestFixture]
    public class RoutePilot_Specs
    {
        private readonly string _controllerRoot = AppDomain.CurrentDomain.BaseDirectory + @"\Controllers";

        [Test]
        public void Fires_Controller_Action_From_Root_Directory()
        {
            var directoryRegistry = new DirectoryRegistry();
            var controllerRegistry = new ControllerRegistry();
            string response = null;

            new Scenario("Fires controller action from root")
                .Given("the root directory",
                    () => directoryRegistry.RegisterControllerDirectory(new DirectoryInfo(_controllerRoot)))
                .And("a LibraryController",
                    controllerRegistry.Register<LibraryController>)
                .When("routing to /library/book",
                    () => response = RoutePilot.FindAction("/library/book").Invoke())
                .Then("LibraryController#Index should fire.",
                    () => Assert.AreEqual("LibraryController#Index", response))
                .Run();
        }

        [Test]
        public void Fires_Controller_Action_From_SubDirectory()
        {
            var directoryRegistry = new DirectoryRegistry();
            var controllerRegistry = new ControllerRegistry();
            string response = null;

            new Scenario("Fires controller action from sub-directory")
                .Given("the directory /book/fiction",
                    () => directoryRegistry.RegisterControllerDirectory(new DirectoryInfo(_controllerRoot + @"\book\fiction")))
                .And("a FictionController",
                    controllerRegistry.Register<FictionController>)
                .When("routing to /book/fiction/",
                    () => response = RoutePilot.FindAction("/book/fiction/").Invoke())
                .Then("FictionController#Index should fire.",
                    () => Assert.AreEqual("FictionController#Index", response))
                .Run();
        }
    }
}