namespace SparkSharp.Core.Tests.Datasets
{
    using System;
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
        }
    }
}
