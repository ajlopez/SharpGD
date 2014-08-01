namespace SharpGD.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Node
    {
        private IList<string> labels = new List<string>();
        private IDictionary<string, object> properties = new Dictionary<string, object>();

        internal Node()
        {
        }

        public Node Property(string name, object value)
        {
            this.properties[name] = value;
            return this;
        }

        public Node Label(string label)
        {
            this.labels.Add(label);
            return this;
        }

        public bool HasLabel(string label)
        {
            return this.labels.Contains(label);
        }

        public object Property(string name)
        {
            return this.properties[name];
        }
    }
}
