﻿namespace SparkSharp.Core.Tests.Datasets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SparkSharp.Core.Datasets;

    [TestClass]
    public class KeyValueDatasetTests
    {
        [TestMethod]
        public void CountIntegersUsingReduceByKey()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3, 3, 3, 2 });
            var mapds = ds.Map(i => new KeyValuePair<int, int>(i, 1));
            var cds = mapds.ReduceByKey((x, y) => x + y);

            Assert.AreEqual(3, cds.Count());
            Assert.IsTrue(cds.Contains(new KeyValuePair<int, int>(1, 1)));
            Assert.IsTrue(cds.Contains(new KeyValuePair<int, int>(2, 2)));
            Assert.IsTrue(cds.Contains(new KeyValuePair<int, int>(3, 3)));
        }
    }
}
