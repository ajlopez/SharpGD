namespace SharpGD.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Match
    {
        private GraphDatabase gd;
        private string label;

        internal Match(GraphDatabase gd, string label)
        {
            this.gd = gd;
            this.label = label;
        }

        public IEnumerable<Node> Nodes()
        {
            foreach (var node in this.gd.Nodes())
                if (node.HasLabel(this.label))
                    yield return node;
        }
    }
}
