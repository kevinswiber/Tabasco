using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Tabasco.Specs
{
    public class SparkTest
    {
        public IView Empty()
        {
            return new Spark();
        }

        public IView Model(dynamic model)
        {
            return new Spark(model);
        }

        public IView ViewPath()
        {
            return new Spark().Template(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "views", "viewpath_test.spark"));
        }

        public IView ViewPathAndModel(dynamic model)
        {
            return
                new Spark(model).Template(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "views", "viewpath_and_model_test.spark"));
        }
    }

    public class MessageViewModel
    {
        public string Message { get; set; }
    }

    [TestFixture]
    public class Spark_Specs
    {
        [Test]
        public void It_Renders_View_With_An_Empty_Constructor()
        {
            var spark = new SparkTest();
            var view = new StringBuilder();

            spark.Empty().Each(str => view.Append(str));

            Assert.AreEqual("<h1>Hello!</h1>", view.ToString());
        }

        [Test]
        public void It_Renders_View_Using_Model()
        {
            var spark = new SparkTest();
            var view = new StringBuilder();

            spark.Model(new MessageViewModel { Message = "Hello, Model!" }).Each(str => view.Append(str));

            Assert.AreEqual("<h1>Hello, Model!</h1>", view.ToString());
        }

        [Test]
        public void It_Renders_View_Using_View_Path()
        {
            var spark = new SparkTest();
            var view = new StringBuilder();

            spark.ViewPath().Each(str => view.Append(str));

            Assert.AreEqual("<h1>Hello, ViewPath!</h1>", view.ToString());
        }

        [Test]
        public void It_Renders_View_Using_View_Path_And_Model()
        {
            var spark = new SparkTest();
            var view = new StringBuilder();

            spark.ViewPathAndModel(new MessageViewModel { Message = "Hello, ViewPathAndModel!" })
                .Each(str => view.Append(str));

            Assert.AreEqual("<h1>Hello, ViewPathAndModel!</h1>", view.ToString());
        }
    }
}