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
        private IList<Func<Node, bool>> filters = new List<Func<Node, bool>>();

        internal Match(GraphDatabase gd, string label)
        {
            this.gd = gd;
            this.label = label;
        }

        public IEnumerable<Node> Nodes()
        {
            foreach (var node in this.gd.Nodes())
                if (this.label == null || node.HasLabel(this.label))
                {
                    bool filtered = false;
                    foreach (var filter in this.filters)
                        if (!filter(node))
                        {
                            filtered = true;
                            break;
                        }

                    if (!filtered)
                        yield return node;
                }
        }

        public Match Property(string name, object value)
        {
            this.filters.Add(node => value.Equals(node.Property(name)));
            return this;
        }
    }
}
