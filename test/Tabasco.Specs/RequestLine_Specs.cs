using System;
using NUnit.Framework;

namespace Tabasco.Specs
{
    public class RequestLine_Specs
    {
        [Test]
        public void It_Omits_Trailing_Slashes_From_Script_Name()
        {
            var requestLine = RequestLine.Create("GET", "/chickens/");
            Assert.AreEqual("GET /chickens", requestLine.ToString());
        }

        [Test]
        public void It_Ensures_Path_Info_Starts_With_A_Slash()
        {
            var requestLine = RequestLine.Create("GET", "/birds", "pigeons");
            Assert.AreEqual("GET /birds/pigeons", requestLine.ToString());
        }

        [Test]
        public void It_Ensures_The_RequestLine_Starts_With_A_Slash()
        {
            var requestLine = RequestLine.Create("GET", "animals", "/birds");
            Assert.AreEqual("GET /animals/birds", requestLine.ToString());
        }

        [Test]
        public void It_Ensure_The_Query_String_Starts_With_A_Question_Mark()
        {
            var requestLine = RequestLine.Create("GET", "/animals", "/birds", "beak=long&feathers=white");
            Assert.AreEqual("GET /animals/birds?beak=long&feathers=white", requestLine.ToString());
        }

        [Test]
        public void It_Requires_A_Method()
        {
            Assert.Throws<ArgumentNullException>(() => RequestLine.Create(null, "/birds"));
        }
    }
}