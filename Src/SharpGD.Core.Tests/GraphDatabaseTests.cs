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

        [TestMethod]
        public void MatchByTwoPropertyValues()
        {
            var gdb = GraphDatabase.Create();

            gdb.Node().Label("Human").Property("Name", "Adam").Property("Age", 800);
            gdb.Node().Label("Human").Property("Name", "Eve").Property("Age", 600);

            var result = gdb.Match().Property("Name", "Adam").Property("Age", 800).Nodes();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            Assert.AreEqual("Adam", result.First().Property("Name"));
            Assert.AreEqual(800, result.First().Property("Age"));
        }

        [TestMethod]
        public void MatchByOrPropertyValues()
        {
            var gdb = GraphDatabase.Create();

            gdb.Node().Label("Human").Property("Name", "Adam").Property("Age", 800);
            gdb.Node().Label("Human").Property("Name", "Eve").Property("Age", 600);

            var result = gdb.Match().Property("Name", "Adam").OrProperty("Age", 600).Nodes();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());

            Assert.AreEqual("Adam", result.First().Property("Name"));
            Assert.AreEqual(800, result.First().Property("Age"));

            Assert.AreEqual("Eve", result.Skip(1).First().Property("Name"));
            Assert.AreEqual(600, result.Skip(1).First().Property("Age"));
        }

        [TestMethod]
        public void RelationInNode()
        {
            var gdb = GraphDatabase.Create();

            var adam = gdb.Node().Label("Human").Property("Name", "Adam").Property("Age", 800);
            var eve = gdb.Node().Label("Human").Property("Name", "Eve").Property("Age", 600);

            adam.Relation("Spouse", eve);

            var result = adam.Relation("Spouse");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            Assert.AreSame(eve, result.First());
        }

        [TestMethod]
        public void RelationInMatch()
        {
            var gdb = GraphDatabase.Create();

            var adam = gdb.Node().Label("Human").Property("Name", "Adam").Property("Age", 800);
            var eve = gdb.Node().Label("Human").Property("Name", "Eve").Property("Age", 600);

            adam.Relation("Spouse", eve);

            var result = gdb.Match().Relation("Spouse").Nodes();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            Assert.AreSame(eve, result.First());
        }

        [TestMethod]
        public void ManyNodesInRelation()
        {
            var gdb = GraphDatabase.Create();

            var adam = gdb.Node().Label("Human").Property("Name", "Adam").Property("Age", 800);
            var abel = gdb.Node().Label("Human").Property("Name", "Abel").Property("Age", 500);
            var caine = gdb.Node().Label("Human").Property("Name", "Caine").Property("Age", 500);

            adam.Relation("Child", abel);
            adam.Relation("Child", caine);

            var result = adam.Relation("Child");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());

            Assert.AreSame(abel, result.First());
            Assert.AreSame(caine, result.Skip(1).First());
        }

        [TestMethod]
        public void RelatedInNode()
        {
            var gdb = GraphDatabase.Create();

            var adam = gdb.Node().Label("Human").Property("Name", "Adam").Property("Age", 800);
            var abel = gdb.Node().Label("Human").Property("Name", "Abel").Property("Age", 500);
            var caine = gdb.Node().Label("Human").Property("Name", "Caine").Property("Age", 500);

            adam.Relation("Child", abel);
            adam.Relation("Child", caine);

            var result = abel.Related("Child");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            Assert.AreSame(adam, result.First());

            var result2 = caine.Related("Child");

            Assert.IsNotNull(result2);
            Assert.AreEqual(1, result2.Count());

            Assert.AreSame(adam, result2.First());
        }

        [TestMethod]
        public void RelatedInMatch()
        {
            var gdb = GraphDatabase.Create();

            var adam = gdb.Node().Label("Human").Property("Name", "Adam").Property("Age", 800);
            var abel = gdb.Node().Label("Human").Property("Name", "Abel").Property("Age", 500);
            var caine = gdb.Node().Label("Human").Property("Name", "Caine").Property("Age", 500);

            adam.Relation("Child", abel);
            adam.Relation("Child", caine);

            var result = gdb.Match().Related("Child").Nodes();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            Assert.AreSame(adam, result.First());
        }
    }
}
