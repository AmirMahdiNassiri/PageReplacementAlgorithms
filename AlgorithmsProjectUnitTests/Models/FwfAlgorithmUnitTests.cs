using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsProject.Models.CacheReplacementAlgorithms;

namespace AlgorithmsProjectUnitTests.Models
{
    [TestClass]
    public class FwfAlgorithmUnitTests
    {
        [TestMethod]
        public void TestCacheSizeFour()
        {
            var fifoAlgorithm = new FwfAlgorithm(4);

            Assert.AreEqual(5, fifoAlgorithm.HandleSequence("abcbadce"));

            Assert.AreEqual(6, fifoAlgorithm.HandleSingleInput('f'));

            Assert.AreEqual(7, fifoAlgorithm.HandleSingleInput('a'));
        }
    }
}
