using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ccmetricsparse;

namespace UnitTestProject
{
    [TestClass]
    public class CCOutParserUnitTest
    {
        static readonly string RejitHeader = "Rejit Reason,                             Count";
        static readonly string BailoutHeader = "Bailout Reason,                           Count";
        static readonly string Total = "TOTAL,                                     8498";

        [TestMethod]
        public void CheckInitialization()
        {
            var p = new CCOutParser();

            Assert.AreEqual(0, p.BailOut);
            Assert.AreEqual(0, p.ReJit);
        }

        [TestMethod]
        public void CheckTermination()
        {
            var p = new CCOutParser();

            Assert.IsTrue(p.ParseLine("NOt NULL"));
            Assert.IsTrue(p.ParseLine(String.Empty));
            Assert.IsFalse(p.ParseLine(null));
        }


        [TestMethod]
        public void CheckRejit()
        {
            var p = new CCOutParser();
            Assert.IsTrue(p.ParseLine("NOt NULL"));
            Assert.IsTrue(p.ParseLine(RejitHeader));
            Assert.IsTrue(p.ParseLine("some string"));
            Assert.IsTrue(p.ParseLine(Total));
            Assert.IsFalse(p.ParseLine(null));
            Assert.AreEqual(8498, p.ReJit);
        }

        [TestMethod]
        public void CheckRejitNegative()
        {
            var p = new CCOutParser();
            Assert.IsTrue(p.ParseLine("NOt NULL"));
            Assert.IsTrue(p.ParseLine(RejitHeader));
            Assert.IsTrue(p.ParseLine("some string"));
            Assert.IsFalse(p.ParseLine(null));
            Assert.AreEqual(0, p.ReJit);
        }

        [TestMethod]
        public void CheckBailOut()
        {
            var p = new CCOutParser();
            Assert.IsTrue(p.ParseLine(BailoutHeader));
            Assert.IsTrue(p.ParseLine("some string"));
            Assert.IsTrue(p.ParseLine(Total));
            Assert.IsFalse(p.ParseLine(null));
            Assert.AreEqual(8498, p.BailOut);
        }

        [TestMethod]
        public void CheckBailOutNegative()
        {
            var p = new CCOutParser();
            Assert.IsTrue(p.ParseLine(BailoutHeader));
            Assert.IsFalse(p.ParseLine(null));
            Assert.AreEqual(0, p.BailOut);
        }

        [TestMethod]
        public void CheckAll()
        {
            var p = new CCOutParser();

            Assert.IsTrue(p.ParseLine(string.Empty));
            Assert.IsTrue(p.ParseLine(string.Empty));
            Assert.IsTrue(p.ParseLine(BailoutHeader));
            Assert.IsTrue(p.ParseLine("some string"));
            Assert.IsTrue(p.ParseLine(Total));
            Assert.IsTrue(p.ParseLine(string.Empty));
            Assert.IsTrue(p.ParseLine(RejitHeader));
            Assert.IsTrue(p.ParseLine("some string"));
            Assert.IsTrue(p.ParseLine(Total));
            Assert.IsTrue(p.ParseLine(string.Empty));
            Assert.IsFalse(p.ParseLine(null));

            Assert.AreEqual(8498, p.ReJit);
            Assert.AreEqual(8498, p.BailOut);
        }


    }
}
