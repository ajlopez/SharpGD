namespace SharpGD.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SharpGD.Core.Matches;

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

        public Match Relation(string name)
        {
            return new RelationMatch(this, name);
        }

        public virtual Match OrProperty(string name, object value)
        {
            throw new NotImplementedException();
        }
    }
}
