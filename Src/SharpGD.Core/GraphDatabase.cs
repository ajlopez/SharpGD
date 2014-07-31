namespace SharpGD.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GraphDatabase
    {
        public static GraphDatabase Create()
        {
            return new GraphDatabase();
        }

        private GraphDatabase()
        {
        }

        public Node Node()
        {
            return new Node();
        }
    }
}
