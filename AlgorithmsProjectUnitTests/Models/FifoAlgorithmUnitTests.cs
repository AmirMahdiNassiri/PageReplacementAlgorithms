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
    public class FifoAlgorithmUnitTests
    {
        [TestMethod]
        public void TestCacheSizeFour()
        {
            var fifoAlgorithm = new FifoAlgorithm(4);

            Assert.AreEqual(5, fifoAlgorithm.HandleSequence("abcbadce"));
            Assert.AreEqual(6, fifoAlgorithm.HandleSingleInput('f'));
            Assert.AreEqual(7, fifoAlgorithm.HandleSingleInput('a'));

            fifoAlgorithm = new FifoAlgorithm(4);

            Assert.AreEqual(1, fifoAlgorithm.HandleSingleInput('a'));
            Assert.AreEqual(2, fifoAlgorithm.HandleSingleInput('b'));
            Assert.AreEqual(3, fifoAlgorithm.HandleSingleInput('c'));
            Assert.AreEqual(4, fifoAlgorithm.HandleSingleInput('d'));
            Assert.AreEqual(4, fifoAlgorithm.HandleSingleInput('a'));
            Assert.AreEqual(4, fifoAlgorithm.HandleSingleInput('b'));
            Assert.AreEqual(5, fifoAlgorithm.HandleSingleInput('e'));
            Assert.AreEqual(6, fifoAlgorithm.HandleSingleInput('a'));
            Assert.AreEqual(7, fifoAlgorithm.HandleSingleInput('b'));
            Assert.AreEqual(8, fifoAlgorithm.HandleSingleInput('c'));
            Assert.AreEqual(9, fifoAlgorithm.HandleSingleInput('d'));
            Assert.AreEqual(10, fifoAlgorithm.HandleSingleInput('e'));
        }

        [TestMethod]
        public void TestCacheSizeThree()
        {
            var fifoAlgorithm = new FifoAlgorithm(3);

            Assert.AreEqual(1, fifoAlgorithm.HandleSingleInput('a'));
            Assert.AreEqual(2, fifoAlgorithm.HandleSingleInput('b'));
            Assert.AreEqual(3, fifoAlgorithm.HandleSingleInput('c'));
            Assert.AreEqual(4, fifoAlgorithm.HandleSingleInput('d'));
            Assert.AreEqual(5, fifoAlgorithm.HandleSingleInput('a'));
            Assert.AreEqual(6, fifoAlgorithm.HandleSingleInput('b'));
            Assert.AreEqual(7, fifoAlgorithm.HandleSingleInput('e'));
            Assert.AreEqual(7, fifoAlgorithm.HandleSingleInput('a'));
            Assert.AreEqual(7, fifoAlgorithm.HandleSingleInput('b'));
            Assert.AreEqual(8, fifoAlgorithm.HandleSingleInput('c'));
            Assert.AreEqual(9, fifoAlgorithm.HandleSingleInput('d'));
            Assert.AreEqual(9, fifoAlgorithm.HandleSingleInput('e'));
        }
    }
}
