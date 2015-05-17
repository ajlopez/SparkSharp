namespace SparkSharp.Core.Datasets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BaseDataset<T> : IEnumerable<T>
    {
        public EnumDataset<S> Map<S>(Func<T, S> map)
        {
            return new EnumDataset<S>(this.ApplyMap(map));
        }

        public T Reduce(Func<T, T, T> reduce)
        {
            T result = default(T);

            foreach (var elem in this)
                result = reduce(elem, result);

            return result;
        }

        public BaseDataset<T> Take(int n)
        {
            return new EnumDataset<T>(this.Elements.Take(n));
        }

        public BaseDataset<T> Skip(int n)
        {
            return new EnumDataset<T>(this.Elements.Skip(n));
        }

        public abstract IEnumerable<T> Elements { get; }

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
    }
}
