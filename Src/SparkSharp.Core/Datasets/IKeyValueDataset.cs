namespace SparkSharp.Core.Datasets
{
    using System;
    using System.Collections.Generic;

    public interface IKeyValueDataset<K, V> : IDataset<KeyValuePair<K, V>>
    {
        IDictionary<K, int> CountByKey();

        IKeyValueDataset<K, IEnumerable<V>> GroupByKey();

        IKeyValueDataset<K, V> ReduceByKey(Func<V, V, V> reduce);
    }
}
