using System;
using NUnit.Framework;
using Tabasco.Plumbing;

namespace Tabasco.Specs
{
    [TestFixture]
    public class RequestLine_Specs
    {
        [Test]
        public void It_Ensures_Path_Info_Starts_With_A_Slash()
        {
            var requestLine = RequestLine.Create("GET", "birds/pigeons");
            Assert.AreEqual("GET /birds/pigeons", requestLine.ToString());
        }

        [Test]
        public void It_Ensure_The_Query_String_Starts_With_A_Question_Mark()
        {
            var requestLine = RequestLine.Create("GET", "/animals/birds", "beak=long&feathers=white");
            Assert.AreEqual("GET /animals/birds?beak=long&feathers=white", requestLine.ToString());
        }

        [Test]
        public void It_Requires_A_Method()
        {
            Assert.Throws<ArgumentNullException>(() => RequestLine.Create(null, "/birds"));
        }
    }
}