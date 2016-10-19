using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pcmcsvparse;

namespace UnitTestProject
{
    [TestClass]
    public class PcmArgsParserUnitTest
    {
        [TestMethod]
        public void EmptyArgsNegative()
        {
            string[] args1 = { };
            var pars1 = new ArgsParser(args1);
            Assert.IsTrue(String.IsNullOrEmpty(pars1.GetFileName()));

            string[] args2 = {"-K", "DD" };
            var pars2 = new ArgsParser(args2);
            Assert.IsTrue(String.IsNullOrEmpty(pars2.GetFileName()));

            string[] args3 = { "-f"};
            var pars3 = new ArgsParser(args3);
            Assert.IsTrue(String.IsNullOrEmpty(pars3.GetFileName()));
        }

        [TestMethod]
        public void ParseFileName()
        {
            string[] args = { "-f", "file.name" };
            var pars = new ArgsParser(args);
            Assert.AreEqual("file.name", pars.GetFileName());

            string[] args1 = { "-F", "file.name" };
            var pars1 = new ArgsParser(args1);
            Assert.AreEqual("file.name", pars1.GetFileName()); // case insensitive
        }

        [TestMethod]
        public void ParseParameters()
        {
            string[] args = { "-p", "MISS2", "0.045", "-P", "MISS3", "100.04" };
            var pars = new ArgsParser(args);
            Assert.AreEqual(2, pars.Parameters.Count);
            Assert.AreEqual("MISS3", pars.Parameters[1].Key);
            Assert.AreEqual(0.045f, pars.Parameters[0].Value);
        }

        [TestMethod]
        public void ParseFullSet()
        {
            string[] args = { "-f", "file.name", "-p", "MISS2", "0.045", "-P", "MISS3", "100.04" };
            var pars = new ArgsParser(args);
            Assert.AreEqual("file.name", pars.GetFileName()); // case insensitive
            Assert.AreEqual(2, pars.Parameters.Count);
            Assert.AreEqual("MISS2", pars.Parameters[0].Key);
            Assert.AreEqual(100.04f, pars.Parameters[1].Value);

        }


    }
}
