using NUnit.Framework;

namespace Tabasco.Specs
{
    [TestFixture]
    public class PatternParser_Specs
    {
        [Test]
        public void It_Should_Parse_A_Named_Token()
        {
            var parser = new PatternParser("GET /person/:last_name");

            var matches = parser.Match("GET /person/Swiber");

            Assert.AreEqual("Swiber", matches[":last_name"]);
        }

        [Test]
        public void It_Should_Parse_Values_Unescaped()
        {
            var parser = new PatternParser("GET /:token");

            var matches = parser.Match("GET /Kevin+Swiber");

            Assert.AreEqual("Kevin Swiber", matches[":token"]);
        }

        [Test]
        public void It_Should_Parse_Multiple_Named_Tokens()
        {
            var parser = new PatternParser("GET /locator/:country/:state/:city");
            var matches = parser.Match("GET /locator/usa/mi/detroit");

            Assert.AreEqual("usa", matches[":country"]);
        }

        [Test]
        public void It_Should_Parse_A_Single_Catch_All()
        {
            var parser = new PatternParser("GET /catchy/*");
            var matches = parser.Match("GET /catchy/tune");

            Assert.AreEqual("tune", matches[":splat"][0]);
        }

        [Test]
        public void It_Should_Parse_Multiple_Catch_All_Values()
        {
            var parser = new PatternParser("GET /catchy/*/*/*");
            var matches = parser.Match("GET /catchy/tunes/from/radio");

            Assert.AreEqual(new[] { "tunes", "from", "radio" }, matches[":splat"]);
        }
    }
}