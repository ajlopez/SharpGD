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

        [TestMethod]
        public void CreateNode()
        {
            var result = GraphDatabase.Create().Node();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateNodeWithProperty()
        {
            var result = GraphDatabase.Create().Node().Property("Name", "Adam");

            Assert.AreEqual("Adam", result.Property("Name"));
        }

        [TestMethod]
        public void CreateNodeWithLabel()
        {
            var result = GraphDatabase.Create().Node().Label("Human").Property("Name", "Adam");

            Assert.IsTrue(result.HasLabel("Human"));
            Assert.IsFalse(result.HasLabel("Dog"));
        }
    }
}
