namespace SparkSharp.Core.Datasets
{
    using System;
    using System.Collections.Generic;

    public interface IDataset<T> : IEnumerable<T>
    {
        IDataset<KeyValuePair<T, S>> Cartesian<S>(IDataset<S> ds);
        
        IList<T> Collect();
        
        IDataset<T> Distinct();
        
        IEnumerable<T> Elements { get; }
        
        IDataset<T> Filter(Func<T, bool> predicate);
        
        IDataset<S> FlatMap<S>(Func<T, IEnumerable<S>> map);
        
        void ForEach(Action<T> fn);
        
        IEnumerator<T> GetEnumerator();
        
        IDataset<T> Intersect(BaseDataset<T> ds);
        
        IKeyValueDataset<K, V> Map<K, V>(Func<T, KeyValuePair<K, V>> map);
        
        IDataset<S> Map<S>(Func<T, S> map);
        
        S Reduce<S>(Func<S, T, S> reduce);
        
        IDataset<T> Skip(int n);
        
        IDataset<S> Split<S>(Func<T, IEnumerable<S>> split);
        
        IDataset<T> Take(int n);
        
        IDataset<T> Union(IDataset<T> ds);
    }
}
