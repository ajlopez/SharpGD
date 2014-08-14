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
        private IDictionary<string, IList<Node>> relations = new Dictionary<string, IList<Node>>();
        private IDictionary<string, IList<Node>> related = new Dictionary<string, IList<Node>>();

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

        public Node Relation(string name, Node node)
        {
            if (!this.relations.ContainsKey(name))
                this.relations[name] = new List<Node>();

            this.relations[name].Add(node);
            node.Related(name, this);
            return this;
        }

        public IEnumerable<Node> Relation(string name)
        {
            return this.relations[name];
        }

        public IEnumerable<Node> Related(string name)
        {
            return this.related[name];
        }

        internal void Related(string name, Node node)
        {
            if (!this.related.ContainsKey(name))
                this.related[name] = new List<Node>();

            this.related[name].Add(node);
        }
    }
}
