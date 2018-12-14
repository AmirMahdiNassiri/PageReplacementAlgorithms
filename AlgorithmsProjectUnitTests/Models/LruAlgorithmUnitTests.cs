using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsProject.Models.CacheReplacementAlgorithms;

namespace AlgorithmsProjectUnitTests.Models
{
    /// <summary>
    /// Summary description for LruAlgorithmUnitTests
    /// </summary>
    [TestClass]
    public class LruAlgorithmUnitTests
    {
        [TestMethod]
        public void TestCacheSizeFour()
        {
            var lruAlgorithm = new LruAlgorithm(4);

            Assert.AreEqual(5, lruAlgorithm.HandleSequence("abcbadce"));

            Assert.AreEqual(6, lruAlgorithm.HandleSingleInput('f'));

            Assert.AreEqual(7, lruAlgorithm.HandleSingleInput('a'));
        }
    }
}
