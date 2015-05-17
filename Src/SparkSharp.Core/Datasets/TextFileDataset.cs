namespace SparkSharp.Core.Datasets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class TextFileDataset : BaseDataset<string>
    {
        private string filename;

        public TextFileDataset(string filename) 
        {
            this.filename = filename;
        }

        public override IEnumerable<string> Elements 
        {
            get
            {
                StreamReader reader = File.OpenText(this.filename);

                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    yield return line;
            }
        }
    }
}
