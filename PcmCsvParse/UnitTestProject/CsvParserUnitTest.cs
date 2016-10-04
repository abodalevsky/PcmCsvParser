using Ideafixxxer.CsvParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTestProject
{
    [TestClass]
    public class CsvParserUnitTest
    {
        [TestMethod()]
        public void EmptyTest()
        {
            string[][] actual = Parse("", true, 10);
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod()]
        public void EmptyLineTest()
        {
            string[][] actual = Parse("\r\n", false, 10);
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(0, actual[0].Length);
        }

        [TestMethod()]
        public void SingleCommaTest()
        {
            string[][] actual = Parse(",", false, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "", "" }, actual[0]);
        }

        [TestMethod()]
        public void QuoteCharTest()
        {
            string[][] actual = Parse("a\"b", false, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a\"b" }, actual[0]);
        }

        [TestMethod()]
        public void QuoteCharTest2()
        {
            string[][] actual = Parse("a\"", false, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a\"" }, actual[0]);
        }

        [TestMethod()]
        public void QuoteCharTest3()
        {
            string[][] actual = Parse("a\",b", false, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a\"", "b" }, actual[0]);
        }

        [TestMethod()]
        public void EmptyLineTestWithTrimTrailingEmptyLines()
        {
            string[][] actual = Parse("\r\n", true, 10);
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod()]
        public void SingleLineTest()
        {
            string[][] actual = Parse("a,b,c", true, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a", "b", "c" }, actual[0]);
        }

        [TestMethod()]
        public void SingleLineTest2()
        {
            string[][] actual = Parse("a,b,c\r\n", true, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a", "b", "c" }, actual[0]);
        }

        [TestMethod()]
        public void TwoLinesTest()
        {
            string[][] actual = Parse("a,b\r\nc,d", true, 10);
            Assert.AreEqual(2, actual.Length);
            AreEqual(new[] { "a", "b" }, actual[0]);
            AreEqual(new[] { "c", "d" }, actual[1]);
        }

        [TestMethod()]
        public void QuotedValueTest()
        {
            string[][] actual = Parse("a,\"b\",c", true, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a", "b", "c" }, actual[0]);
        }

        [TestMethod()]
        public void SingleQuotedValueTest()
        {
            string[][] actual = Parse("\"b\"", true, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "b" }, actual[0]);
        }

        [TestMethod()]
        public void EmptyQuotedValueTest()
        {
            string[][] actual = Parse("a,\"\",c", true, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a", "", "c" }, actual[0]);
        }

        [TestMethod()]
        public void EmptyValueTest()
        {
            string[][] actual = Parse("a,,c", true, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a", "", "c" }, actual[0]);
        }

        [TestMethod()]
        public void QuotedValueWithCommaTest()
        {
            string[][] actual = Parse("a,\"b,c\",d", true, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a", "b,c", "d" }, actual[0]);
        }

        [TestMethod()]
        public void QuotedValueWithQuoteTest()
        {
            string[][] actual = Parse("a,\"b\"\"c\",d", true, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a", "b\"c", "d" }, actual[0]);
        }

        [TestMethod()]
        public void QuotedValueWithEOLTest()
        {
            string[][] actual = Parse("a,\"b\r\nc\",d", true, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a", "b\r\nc", "d" }, actual[0]);
        }

        [TestMethod()]
        public void TrimTrailingEmptyLinesTest()
        {
            string[][] actual = Parse("a,b,c\r\n,,", true, 10);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a", "b", "c" }, actual[0]);
        }

        [TestMethod()]
        public void DontTrimTrailingEmptyLinesTest()
        {
            string[][] actual = Parse("a,b,c\r\n,,", false, 10);
            Assert.AreEqual(2, actual.Length);
            AreEqual(new[] { "a", "b", "c" }, actual[0]);
            AreEqual(new[] { "", "", "" }, actual[1]);
        }

        [TestMethod()]
        public void TrimTrailingEmptyLinesWithMaxColumnsTest()
        {
            string[][] actual = Parse("a,b,c\r\n,,d", true, 2);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a", "b" }, actual[0]);
        }

        [TestMethod()]
        public void MaxColumnsTest()
        {
            string[][] actual = Parse("a,b,c", true, 2);
            Assert.AreEqual(1, actual.Length);
            AreEqual(new[] { "a", "b" }, actual[0]);
        }

        private void AreEqual(string[] one, string[] two)
        {
            Assert.AreEqual(one.Length, two.Length);
            for (int i = 0; i < one.Length; i++)
            {
                Assert.AreEqual(one[i], two[i]);
            }
        }

        private string[][] Parse(string input, bool trimTrailingEmptyLines, int maxColumnsToRead)
        {
            CsvParser2 parser = new CsvParser2()
            {
                TrimTrailingEmptyLines = trimTrailingEmptyLines,
                MaxColumnsToRead = maxColumnsToRead
            };

            using (StringReader reader = new StringReader(input))
            {
                return parser.Parse(reader);
            }
        }
    }
}
