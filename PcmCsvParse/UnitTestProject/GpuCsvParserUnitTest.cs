using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pcmcsvparse;
using System.IO;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class GpuCsvParserUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void OpenFileNegative()
        {
            var parser = new GpuCsvParser("nofile");
        }

        [TestMethod]
        public void OpenFileSuccess()
        {
            var parser = new GpuCsvParser("..//..//Fixtures//gpu_log.csv");
        }

        [TestMethod]
        public void HasColumn()
        {
            var parser = new GpuCsvParser("..//..//Fixtures//gpu_log.csv");
            Assert.IsTrue(parser.HasColumn("GPU load [%]")); // case insensitive
            Assert.IsFalse(parser.HasColumn("G1PU core Clock [MHz]")); 
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetMaxNegative()
        {
            var parser = new GpuCsvParser("..//..//Fixtures//gpu_log.csv");
            parser.GetMax("WRONG_COLUMN_NAME");
        }

        [TestMethod]
        public void GetMax()
        {
            var parser = new GpuCsvParser("..//..//Fixtures//gpu_log.csv");
            Assert.AreEqual(parser.GetMax("GPU Memory Clock [MHz]"), 793.0f);
            Assert.AreEqual(parser.GetMax("Fan Speed (%) [%]"), 12f);
            Assert.AreEqual(parser.GetMax("VDDC [V]"), 0.99f);
        }


    }
}
