namespace SharpGD.Core.Matches
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class LabelMatch : Match
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
}
