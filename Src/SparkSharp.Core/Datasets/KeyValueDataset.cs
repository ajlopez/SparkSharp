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
    }
}
