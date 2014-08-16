namespace SharpGD.Core.Matches
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class PropertyMatch : Match
    {
        private string name;
        private object value;
        private IList<string> names = new List<string>();
        private IList<object> values = new List<object>();

        internal PropertyMatch(INodesProvider nodes, string name, object value)
            : base(nodes)
        {
            this.name = name;
            this.value = value;
        }

        public override IEnumerable<Node> Nodes()
        {
            foreach (var node in base.Nodes())
            {
                if (this.value.Equals(node.Property(this.name)))
                    yield return node;

                for (int k = 0; k < this.names.Count; k++)
                    if (this.values[k].Equals(node.Property(this.names[k])))
                    {
                        yield return node;
                        break;
                    }
            }
        }

        public override Match OrProperty(string name, object value)
        {
            this.names.Add(name);
            this.values.Add(value);
            return this;
        }
    }
}
