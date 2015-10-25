﻿namespace SparkSharp.Core.Datasets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BaseDataset<T> : IEnumerable<T>
    {
        public abstract IEnumerable<T> Elements { get; }

        public BaseDataset<S> Map<S>(Func<T, S> map)
        {
            return new EnumDataset<S>(this.ApplyMap(map));
        }

        public BaseDataset<S> Split<S>(Func<T, IEnumerable<S>> split)
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

        public BaseDataset<T> Take(int n)
        {
            return new EnumDataset<T>(this.Elements.Take(n));
        }

        public BaseDataset<T> Filter(Func<T, bool> predicate)
        {
            return new EnumDataset<T>(this.Elements.Where(predicate));
        }

        public BaseDataset<T> Skip(int n)
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

        private IEnumerable<S> ApplySplit<S>(Func<T, IEnumerable<S>> split)
        {
            foreach (var elem in this)
                foreach (var elem2 in split(elem))
                    yield return elem2;
        }
    }
}
