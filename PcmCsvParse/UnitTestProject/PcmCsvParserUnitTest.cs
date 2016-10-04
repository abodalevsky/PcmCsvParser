using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pcmcsvparse;
using System.IO;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class PcmCsvParserUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void OpenFileNegative()
        {
            var parser = new PcmCsvParser("nofile");
        }

        [TestMethod]
        public void OpenFileSuccess()
        {
            var parser = new PcmCsvParser("..//..//Fixtures//test.csv");
        }

        [TestMethod]
        public void HasColumn()
        {
            var parser = new PcmCsvParser("..//..//Fixtures//test.csv");
            Assert.IsTrue(parser.HasColumn("L2MISS"));
            Assert.IsFalse(parser.HasColumn("L2KISS"));
            Assert.IsTrue(parser.HasColumn("L2MiSs")); //case insensitive
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetMaxNegative()
        {
            var parser = new PcmCsvParser("..//..//Fixtures//test.csv");
            parser.GetMax("WRONG_COLUMN_NAME");
        }

        [TestMethod]
        public void GetMax()
        {
            var parser = new PcmCsvParser("..//..//Fixtures//test.csv");
            Assert.AreEqual(parser.GetMax("L3MISS"), 1.22f);
            Assert.AreEqual(parser.GetMax("FREQ"), 0.0541f);
            Assert.AreEqual(parser.GetMax("DATE"), 0.0f); //Invalid format, should be ignored
            Assert.AreEqual(parser.GetMax("INST"), 1080.0f);//Scientific form transformation (1.08E+03)
        }


    }
}
