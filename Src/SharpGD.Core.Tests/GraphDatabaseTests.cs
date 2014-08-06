namespace SharpGD.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        [TestMethod]
        public void CreateNodeWithLabels()
        {
            var result = GraphDatabase.Create().Node().Label("Human").Property("Name", "Adam").Label("Paradise");

            Assert.IsTrue(result.HasLabel("Human"));
            Assert.IsTrue(result.HasLabel("Paradise"));
            Assert.IsFalse(result.HasLabel("Dog"));
        }

        [TestMethod]
        public void RetrieveNodes()
        {
            var gdb = GraphDatabase.Create();

            gdb.Node().Label("Human").Property("Name", "Adam");
            gdb.Node().Label("Dog").Property("Name", "Fido");

            var result = gdb.Match().Label("Dog").Nodes().ToArray();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Fido", result[0].Property("Name"));
        }

        [TestMethod]
        public void MatchByLabel()
        {
            var gdb = GraphDatabase.Create();

            gdb.Node().Label("Human").Property("Name", "Adam");
            gdb.Node().Label("Dog").Property("Name", "Fido");

            var result = gdb.Match().Label("Human").Nodes();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            Assert.AreEqual("Adam", result.First().Property("Name"));
        }

        [TestMethod]
        public void MatchByPropertyValue()
        {
            var gdb = GraphDatabase.Create();

            gdb.Node().Label("Human").Property("Name", "Adam");
            gdb.Node().Label("Dog").Property("Name", "Fido");

            var result = gdb.Match().Property("Name", "Adam").Nodes();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            Assert.AreEqual("Adam", result.First().Property("Name"));
        }
    }
}
