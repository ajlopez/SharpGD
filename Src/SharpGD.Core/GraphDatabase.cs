namespace SharpGD.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GraphDatabase
    {
        private IList<Node> nodes = new List<Node>();

        public static GraphDatabase Create()
        {
            return new GraphDatabase();
        }

        private GraphDatabase()
        {
        }

        public Node Node()
        {
            var node = new Node();
            nodes.Add(node);
            return node;
        }

        public IEnumerable<Node> Nodes()
        {
            return this.nodes;
        }
    }
}
