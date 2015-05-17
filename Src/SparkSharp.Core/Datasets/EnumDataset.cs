namespace SparkSharp.Core.Datasets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EnumDataset<T> : IEnumerable<T>
    {
        private IEnumerable<T> elements;

        public EnumDataset(IEnumerable<T> elements) 
        {
            this.elements = elements;
        }

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

        public IEnumerator<T> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        private IEnumerable<S> ApplyMap<S>(Func<T, S> map)
        {
            foreach (var elem in this)
                yield return map(elem);
        }
    }
}
