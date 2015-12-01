namespace SparkSharp.Core.Datasets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class KeyValueDataset<K, V> : BaseDataset<KeyValuePair<K, V>>, IKeyValueDataset<K,V>
    {
        private IEnumerable<KeyValuePair<K, V>> elements;

        public KeyValueDataset(IEnumerable<KeyValuePair<K, V>> elements) 
        {
            this.elements = elements;
        }

        public override IEnumerable<KeyValuePair<K, V>> Elements 
        {
            get
            {
                return this.elements;
            }
        }

        public IKeyValueDataset<K, IEnumerable<V>> GroupByKey()
        {
            IDictionary<K, IList<V>> keyvalues = new Dictionary<K, IList<V>>();

            foreach (var elem in this.elements)
            {
                var key = elem.Key;
                var value = elem.Value;

                if (keyvalues.ContainsKey(key))
                    keyvalues[key].Add(value);
                else
                    keyvalues[key] = new List<V>(new V[] { value });
            }

            return new KeyValueDataset<K, IEnumerable<V>>(keyvalues.Select(kv => new KeyValuePair<K, IEnumerable<V>>(kv.Key, kv.Value)));
        }

        public IKeyValueDataset<K, V> ReduceByKey(Func<V, V, V> reduce)
        {
            IDictionary<K, V> keyvalues = new Dictionary<K, V>();

            foreach (var elem in this.elements)
            {
                var key = elem.Key;
                var value = elem.Value;

                if (keyvalues.ContainsKey(key))
                    keyvalues[key] = reduce(keyvalues[key], value);
                else
                    keyvalues[key] = reduce(default(V), value);
            }

            return new KeyValueDataset<K, V>(keyvalues);
        }

        public IDictionary<K, int> CountByKey()
        {
            IDictionary<K, int> keycounts = new Dictionary<K, int>();

            foreach (var elem in this.elements)
            {
                var key = elem.Key;
                var value = elem.Value;

                if (keycounts.ContainsKey(key))
                    keycounts[key]++;
                else
                    keycounts[key] = 1;
            }

            return keycounts;
        }
    }
}
