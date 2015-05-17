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

        public IEnumerator<T> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }
    }
}
