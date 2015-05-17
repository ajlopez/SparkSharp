namespace SparkSharp.Core.Datasets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EnumDataset<T> : BaseDataset<T>
    {
        private IEnumerable<T> elements;

        public EnumDataset(IEnumerable<T> elements) 
        {
            this.elements = elements;
        }

        public override IEnumerable<T> Elements 
        {
            get
            {
                return this.elements;
            }
        }
    }
}
