namespace SparkSharp.Core.Datasets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BaseDataset<T> : IDataset<T>
    {
        public abstract IEnumerable<T> Elements { get; }

        public IDataset<S> Map<S>(Func<T, S> map)
        {
            return new EnumDataset<S>(this.ApplyMap(map));
        }

        public KeyValueDataset<K, V> Map<K, V>(Func<T, KeyValuePair<K, V>> map)
        {
            return new KeyValueDataset<K, V>(this.ApplyMap(map));
        }

        public IDataset<S> FlatMap<S>(Func<T, IEnumerable<S>> map)
        {
            return new EnumDataset<S>(this.ApplyFlatMap(map));
        }

        public void ForEach(Action<T> fn)
        {
            foreach (var elem in this.Elements)
                fn(elem);
        }

        public IList<T> Collect()
        {
            return new List<T>(this.Elements);
        }

        public IDataset<S> Split<S>(Func<T, IEnumerable<S>> split)
        {
            return new EnumDataset<S>(this.ApplySplit(split));
        }

        public S Reduce<S>(Func<S, T, S> reduce)
        {
            S result = default(S);

            foreach (var elem in this)
                result = reduce(result, elem);

            return result;
        }

        public IDataset<T> Take(int n)
        {
            return new EnumDataset<T>(this.Elements.Take(n));
        }

        public IDataset<T> Filter(Func<T, bool> predicate)
        {
            return new EnumDataset<T>(this.Elements.Where(predicate));
        }

        public IDataset<T> Distinct()
        {
            return new EnumDataset<T>(this.Elements.Distinct());
        }

        public IDataset<T> Union(IDataset<T> ds)
        {
            return new EnumDataset<T>(this.Elements.Union(ds.Elements));
        }

        public IDataset<T> Intersect(BaseDataset<T> ds)
        {
            return new EnumDataset<T>(this.Elements.Intersect(ds.Elements));
        }

        public IDataset<KeyValuePair<T, S>> Cartesian<S>(IDataset<S> ds)
        {
            return new EnumDataset<KeyValuePair<T, S>>(this.ApplyCartesian<S>(ds.Elements));
        }

        public IDataset<T> Skip(int n)
        {
            return new EnumDataset<T>(this.Elements.Skip(n));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Elements.GetEnumerator();
        }

        private IEnumerable<S> ApplyMap<S>(Func<T, S> map)
        {
            foreach (var elem in this)
                yield return map(elem);
        }

        private IEnumerable<S> ApplyFlatMap<S>(Func<T, IEnumerable<S>> map)
        {
            foreach (var elem in this)
                foreach (var elem2 in map(elem))
                    yield return elem2;
        }

        private IEnumerable<S> ApplySplit<S>(Func<T, IEnumerable<S>> split)
        {
            foreach (var elem in this)
                foreach (var elem2 in split(elem))
                    yield return elem2;
        }

        private IEnumerable<KeyValuePair<T, S>> ApplyCartesian<S>(IEnumerable<S> elements)
        {
            foreach (var elem in this.Elements)
                foreach (var elem2 in elements)
                    yield return new KeyValuePair<T, S>(elem, elem2);
        }
    }
}
