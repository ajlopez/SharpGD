namespace SharpGD.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface INodesProvider
    {
        IEnumerable<Node> Nodes();
    }
}
