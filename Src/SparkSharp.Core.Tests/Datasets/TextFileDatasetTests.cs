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
    [DeploymentItem("Files", "Files")]
    public class TextFileDatasetTests
    {
        [TestMethod]
        public void GetElements()
        {
            TextFileDataset ds = new TextFileDataset("Files\\Lines.txt");
            var enumerator = ds.GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("foo", enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("bar", enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("zoo", enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("war", enumerator.Current);

            Assert.IsFalse(enumerator.MoveNext());
            Assert.AreEqual(4, ds.Count());
        }

        [TestMethod]
        public void MapConcat()
        {
            TextFileDataset ds = new TextFileDataset("Files\\Lines.txt");
            IDataset<string> mapds = ds.Map(i => i + "a");
            var enumerator = mapds.GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("fooa", enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("bara", enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("zooa", enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("wara", enumerator.Current);

            Assert.IsFalse(enumerator.MoveNext());
            Assert.AreEqual(4, mapds.Count());
        }

        [TestMethod]
        public void ReduceConcat()
        {
            TextFileDataset ds = new TextFileDataset("Files\\Lines.txt");
            var result = ds.Reduce<string>((x, y) => x + y);

            Assert.IsNotNull(result);
            Assert.AreEqual("foobarzoowar", result);
        }

        [TestMethod]
        public void ReduceConcatWithTake()
        {
            TextFileDataset ds = new TextFileDataset("Files\\Lines.txt");
            var result = ds.Take(2).Reduce<string>((x, y) => x + y);

            Assert.IsNotNull(result);
            Assert.AreEqual("foobar", result);
        }

        [TestMethod]
        public void ReduceSumWithSkip()
        {
            TextFileDataset ds = new TextFileDataset("Files\\Lines.txt");
            var result = ds.Skip(2).Reduce<string>((x, y) => x + y);

            Assert.IsNotNull(result);
            Assert.AreEqual("zoowar", result);
        }

        [TestMethod]
        public void ReduceCountStrings()
        {
            TextFileDataset ds = new TextFileDataset("Files\\Lines.txt");
            var result = ds.Map(s => 1).Reduce<int>((x, y) => x + y);

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void FlatMap()
        {
            TextFileDataset ds = new TextFileDataset("Files\\Words.txt");
            IDataset<string> mapds = ds.FlatMap(i => i.Split(' '));
            var enumerator = mapds.GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("foo", enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("bar", enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("zoo", enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("war", enumerator.Current);

            Assert.IsFalse(enumerator.MoveNext());
            Assert.AreEqual(4, mapds.Count());
        }
    }
}
