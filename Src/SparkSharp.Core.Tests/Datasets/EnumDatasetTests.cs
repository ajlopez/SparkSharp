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
        public void CountElements()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3 });
            Assert.AreEqual(3, ds.Count());
        }

        [TestMethod]
        public void FilterElements()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3, 4, 5 });
            var fds = ds.Filter(x => x % 2 == 1);
            Assert.AreEqual(3, fds.Count());
            Assert.AreEqual(1, fds.First());
            Assert.AreEqual(3, fds.Skip(1).First());
            Assert.AreEqual(5, fds.Skip(2).First());
        }

        [TestMethod]
        public void FirstElement()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3 });
            Assert.AreEqual(1, ds.First());
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
        public void MapKeyValue()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3 });
            var mapds = ds.Map(i => new KeyValuePair<int, int>(i, i * i));
            var enumerator = mapds.GetEnumerator();

            for (int k = 1; enumerator.MoveNext(); k++)
                Assert.AreEqual(new KeyValuePair<int, int>(k, k * k), enumerator.Current);

            Assert.AreEqual(3, mapds.Count());
        }

        [TestMethod]
        public void ReduceSum()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3 });
            var result = ds.Reduce<int>((x, y) => x + y);

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void ReduceSumWithTake()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3 });
            var result = ds.Take(2).Reduce<int>((x, y) => x + y);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void ReduceSumWithSkip()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3, 4 });
            var result = ds.Skip(2).Reduce<int>((x, y) => x + y);

            Assert.IsNotNull(result);
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void ReduceCountStrings()
        {
            EnumDataset<string> ds = new EnumDataset<string>(new string[] { "foo", "bar" });
            var result = ds.Map(s => 1).Reduce<int>((x, y) => x + y);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }
    }
}
