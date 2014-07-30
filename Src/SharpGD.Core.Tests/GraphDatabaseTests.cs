namespace SharpGD.Core.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GraphDatabaseTests
    {
        [TestMethod]
        public void Create()
        {
            var result = GraphDatabase.Create();

            Assert.IsNotNull(result);
        }
    }
}
