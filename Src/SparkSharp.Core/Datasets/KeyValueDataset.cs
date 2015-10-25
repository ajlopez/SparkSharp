namespace SparkSharp.Core.Datasets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class KeyValueDataset<K, V> : BaseDataset<KeyValuePair<K, V>>
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

        public KeyValueDataset<K, V> ReduceByKey(Func<V, V, V> reduce)
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
    }
}
