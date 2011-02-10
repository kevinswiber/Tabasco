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
        public void It_Should_Parse_Multiple_Named_Tokens()
        {
            var parser = new PatternParser("GET /locator/:country/:state/:city");
            var matches = parser.Match("GET /locator/usa/mi/detroit");

            Assert.AreEqual("usa", matches[":country"]);
        }
    }
}