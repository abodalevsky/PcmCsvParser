using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ccmetricsparse;

namespace UnitTestProject
{
    [TestClass]
    public class CCArgsParserUnitTest
    {
        [TestMethod]
        public void EmptyArgsNegative()
        {
            string[] args1 = { };
            var pars1 = new ArgsParser(args1);
            Assert.AreEqual(0, pars1.BailOutMax);
            Assert.AreEqual(0, pars1.RejitMax);

            string[] args2 = { "-K", "DD" };
            var pars2 = new ArgsParser(args2);
            Assert.AreEqual(0, pars2.BailOutMax);
            Assert.AreEqual(0, pars2.RejitMax);

            string[] args3 = { "-BailOut:"};
            var pars3 = new ArgsParser(args3);
            Assert.AreEqual(0, pars3.BailOutMax);
            Assert.AreEqual(0, pars3.RejitMax);
        }

        [TestMethod]
        public void TestBailOut()
        {
            string[] args = { "-BailOut:", "22" };
            var p = new ArgsParser(args);

            Assert.AreEqual(22, p.BailOutMax);
            Assert.AreEqual(0, p.RejitMax);
        }

        [TestMethod]
        public void TestBailOutNegative()
        {
            string[] args = { "-BailOut:", "XYZ" };
            var p = new ArgsParser(args);

            Assert.AreEqual(0, p.BailOutMax);
            Assert.AreEqual(0, p.RejitMax);
        }

        [TestMethod]
        public void TestReJit()
        {
            string[] args = { "-ReJit:", "2" };
            var p = new ArgsParser(args);

            Assert.AreEqual(0, p.BailOutMax);
            Assert.AreEqual(2, p.RejitMax);
        }

        [TestMethod]
        public void TestReJitNegative()
        {
            string[] args = { "-ReJit:", "3D" };
            var p = new ArgsParser(args);

            Assert.AreEqual(0, p.BailOutMax);
            Assert.AreEqual(0, p.RejitMax);
        }


    }
}
