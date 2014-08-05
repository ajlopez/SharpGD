namespace SharpGD.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GraphDatabase
    {
        private IList<Node> nodes = new List<Node>();

        private GraphDatabase()
        {
        }

        public static GraphDatabase Create()
        {
            return new GraphDatabase();
        }

        public Node Node()
        {
            var node = new Node();
            this.nodes.Add(node);
            return node;
        }

        public IEnumerable<Node> Nodes()
        {
            return this.nodes;
        }

        public Match Match(string label)
        {
            return new Match(this, label);
        }

        public Match Match()
        {
            return new Match(this, null);
        }
    }
}
