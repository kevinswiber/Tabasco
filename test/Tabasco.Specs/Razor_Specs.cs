using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Tabasco.Specs
{
    public class RazorTest
    {
        public IView Empty()
        {
            return new Razor();
        }

        public IView Model(dynamic model)
        {
            return new Razor(model);
        }

        public IView ViewPath()
        {
            return new Razor().Template(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "views", "viewpath_test.cshtml"));
        }

        public IView ViewPathAndModel(dynamic model)
        {
            return
                new Razor(model).Template(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "views", "viewpath_and_model_test.cshtml"));
        }
    }

    [TestFixture]
    public class Razor_Specs
    {
        [Test]
        public void It_Renders_View_With_An_Empty_Constructor()
        {
            var razor = new RazorTest();
            var view = new StringBuilder();

            razor.Empty().Each(str => view.Append(str));

            Assert.AreEqual("<h1>Hello!</h1>", view.ToString());
        }

        [Test]
        public void It_Renders_View_Using_Model()
        {
            var razor = new RazorTest();
            var view = new StringBuilder();

            razor.Model(new { Message = "Hello, Model!" }).Each(str => view.Append(str));

            Assert.AreEqual("<h1>Hello, Model!</h1>", view.ToString());
        }

        [Test]
        public void It_Renders_View_Using_View_Path()
        {
            var razor = new RazorTest();
            var view = new StringBuilder();

            razor.ViewPath().Each(str => view.Append(str));

            Assert.AreEqual("<h1>Hello, ViewPath!</h1>", view.ToString());
        }

        [Test]
        public void It_Renders_View_Using_View_Path_And_Model()
        {
            var razor = new RazorTest();
            var view = new StringBuilder();

            razor.ViewPathAndModel(new { Message = "Hello, ViewPathAndModel!" })
                .Each(str => view.Append(str));

            Assert.AreEqual("<h1>Hello, ViewPathAndModel!</h1>", view.ToString());
        }
    }
}