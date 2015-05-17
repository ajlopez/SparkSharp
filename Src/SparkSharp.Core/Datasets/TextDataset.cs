namespace SparkSharp.Core.Datasets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class TextDataset : BaseDataset<string>
    {
        private string text;

        public TextDataset(string text) 
        {
            this.text = text;
        }

        public override IEnumerable<string> Elements 
        {
            get
            {
                StringReader reader = new StringReader(this.text);

                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    yield return line;
            }
        }
    }
}
