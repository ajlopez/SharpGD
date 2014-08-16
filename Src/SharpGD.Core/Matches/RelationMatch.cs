namespace SharpGD.Core.Matches
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class RelationMatch : Match
    {
        private string name;

        internal RelationMatch(INodesProvider nodes, string name)
            : base(nodes)
        {
            this.name = name;
        }

        public override IEnumerable<Node> Nodes()
        {
            IList<Node> retrieved = new List<Node>();

            foreach (var node in base.Nodes())
                foreach (var rnode in node.Relation(this.name))
                {
                    if (retrieved.Contains(rnode))
                        continue;

                    retrieved.Add(rnode);

                    yield return rnode;
                }
        }
    }
}
