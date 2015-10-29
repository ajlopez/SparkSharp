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
        public void ForEach()
        {
            int accum = 0;
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3, 4, 5 });
            ds.ForEach(x => { accum += x; });
        }

        [TestMethod]
        public void Collect()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3 });

            var result = ds.Collect();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
        }

        [TestMethod]
        public void DistinctElements()
        {
            EnumDataset<int> ds = new EnumDataset<int>(new int[] { 1, 2, 3, 2, 3, 3 });
            var dds = ds.Distinct();
            Assert.AreEqual(3, dds.Count());
            Assert.IsTrue(dds.Contains(1));
            Assert.IsTrue(dds.Contains(2));
            Assert.IsTrue(dds.Contains(3));
        }

        [TestMethod]
        public void CartesianProduct()
        {
            EnumDataset<int> ds1 = new EnumDataset<int>(new int[] { 1, 2, 3 });
            EnumDataset<string> ds2 = new EnumDataset<string>(new string[] { "foo", "bar" });
            var cds = ds1.Cartesian(ds2);

            Assert.IsTrue(cds.Contains(new KeyValuePair<int, string>(1, "foo")));
            Assert.IsTrue(cds.Contains(new KeyValuePair<int, string>(1, "bar")));
            Assert.IsTrue(cds.Contains(new KeyValuePair<int, string>(2, "foo")));
            Assert.IsTrue(cds.Contains(new KeyValuePair<int, string>(2, "bar")));
            Assert.IsTrue(cds.Contains(new KeyValuePair<int, string>(3, "foo")));
            Assert.IsTrue(cds.Contains(new KeyValuePair<int, string>(3, "bar")));

            Assert.AreEqual(6, cds.Count());
        }

        [TestMethod]
        public void Union()
        {
            EnumDataset<int> ds1 = new EnumDataset<int>(new int[] { 1, 2, 3 });
            EnumDataset<int> ds2 = new EnumDataset<int>(new int[] { 4, 5, 2, 3 });
            var uds = ds1.Union(ds2);

            Assert.IsTrue(uds.Contains(1));
            Assert.IsTrue(uds.Contains(2));
            Assert.IsTrue(uds.Contains(3));
            Assert.IsTrue(uds.Contains(4));
            Assert.IsTrue(uds.Contains(5));

            Assert.AreEqual(5, uds.Count());
        }

        [TestMethod]
        public void Intersect()
        {
            EnumDataset<int> ds1 = new EnumDataset<int>(new int[] { 1, 2, 3 });
            EnumDataset<int> ds2 = new EnumDataset<int>(new int[] { 4, 5, 2, 3 });
            var uds = ds1.Intersect(ds2);

            Assert.IsTrue(uds.Contains(2));
            Assert.IsTrue(uds.Contains(3));

            Assert.AreEqual(2, uds.Count());
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
