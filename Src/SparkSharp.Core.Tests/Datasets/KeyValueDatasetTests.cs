namespace SparkSharp.Core.Tests.Datasets
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

        [TestMethod]
        public void CountByKey()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3, 3, 3, 2 });
            var mapds = ds.Map(i => new KeyValuePair<int, int>(i, 0));
            var dict = mapds.CountByKey();

            Assert.AreEqual(3, dict.Keys.Count);
            Assert.AreEqual(1, dict[1]);
            Assert.AreEqual(2, dict[2]);
            Assert.AreEqual(3, dict[3]);
        }

        [TestMethod]
        public void GroupByKey()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3, 3, 3, 2 });
            var mapds = ds.Map(i => new KeyValuePair<int, int>(i % 2, i));
            var gds = mapds.GroupByKey();

            Assert.AreEqual(2, gds.Elements.Count());

            var values0 = gds.Elements.First(kv => kv.Key == 0).Value;

            Assert.AreEqual(2, values0.Count());
            Assert.AreEqual(2, values0.Count(v => v == 2));

            var values1 = gds.Elements.First(kv => kv.Key == 1).Value;

            Assert.AreEqual(4, values1.Count());
            Assert.AreEqual(1, values1.Count(v => v == 1));
            Assert.AreEqual(3, values1.Count(v => v == 3));
        }
    }
}
