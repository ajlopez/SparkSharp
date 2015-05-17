namespace SparkSharp.Core.Tests.Datasets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SparkSharp.Core.Datasets;

    [TestClass]
    public class EnumDatasetTests
    {
        [TestMethod]
        public void GetElements()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3 });
            var enumerator = ds.GetEnumerator();

            for (int k = 1; enumerator.MoveNext(); k++)
                Assert.AreEqual(k, enumerator.Current);

            Assert.AreEqual(3, ds.Count());
            Assert.IsNotNull(((IEnumerable)ds).GetEnumerator());
        }

        [TestMethod]
        public void MapIncrement()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3 });
            BaseDataset<int> mapds = ds.Map(i => i + 1);
            var enumerator = mapds.GetEnumerator();

            for (int k = 1; enumerator.MoveNext(); k++)
                Assert.AreEqual(k + 1, enumerator.Current);

            Assert.AreEqual(3, mapds.Count());
        }

        [TestMethod]
        public void ReduceSum()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3 });
            var result = ds.Reduce((x, y) => x + y);

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void ReduceSumWithTake()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3 });
            var result = ds.Take(2).Reduce((x, y) => x + y);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void ReduceSumWithSkip()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3, 4 });
            var result = ds.Skip(2).Reduce((x, y) => x + y);

            Assert.IsNotNull(result);
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void ReduceCountStrings()
        {
            EnumDataset<string> ds = new EnumDataset<string>(new string[] { "foo", "bar" });
            var result = ds.Map(s => 1).Reduce((x, y) => x + y);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }
    }
}
