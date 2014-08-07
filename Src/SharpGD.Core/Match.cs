namespace SharpGD.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Match : INodesProvider
    {
        private INodesProvider nodes;

        internal Match(INodesProvider nodes)
        {
            this.nodes = nodes;
        }

        public Match Label(string label)
        {
            return new LabelMatch(this, label);
        }

        public virtual IEnumerable<Node> Nodes()
        {
            return this.nodes.Nodes();
        }

        public Match Property(string name, object value)
        {
            return new PropertyMatch(this, name, value);
        }

        private class LabelMatch : Match
        {
            private string label;

            internal LabelMatch(INodesProvider nodes, string label)
                : base(nodes)
            {
                this.label = label;
            }

            public override IEnumerable<Node> Nodes()
            {
                foreach (var node in base.Nodes())
                    if (node.HasLabel(this.label))
                        yield return node;
            }
        }

        private class PropertyMatch : Match
        {
            private string name;
            private object value;

            internal PropertyMatch(INodesProvider nodes, string name, object value)
                : base(nodes)
            {
                this.name = name;
                this.value = value;
            }

            public override IEnumerable<Node> Nodes()
            {
                foreach (var node in base.Nodes())
                    if (this.value.Equals(node.Property(this.name)))
                        yield return node;
            }
        }
    }
}
