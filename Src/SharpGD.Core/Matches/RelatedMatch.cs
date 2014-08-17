namespace SharpGD.Core.Matches
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class RelatedMatch : Match
    {
        private string name;

        internal RelatedMatch(INodesProvider nodes, string name)
            : base(nodes)
        {
            this.name = name;
        }

        public override IEnumerable<Node> Nodes()
        {
            IList<Node> retrieved = new List<Node>();

            foreach (var node in base.Nodes())
                foreach (var rnode in node.Related(this.name))
                {
                    if (retrieved.Contains(rnode))
                        continue;

                    retrieved.Add(rnode);

                    yield return rnode;
                }
        }
    }
}
