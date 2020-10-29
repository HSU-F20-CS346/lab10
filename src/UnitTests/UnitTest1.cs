using Lab10;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GeneratePathTest()
        {
            Network network = Network.GenerateSampleNetwork();
            List<string> path = network.GeneratePath("R3", "R8");
            Assert.AreEqual(4, path.Count);
            Assert.AreEqual("R3", path[0]);
            Assert.AreEqual("R1", path[1]);
            Assert.AreEqual("R6", path[2]);
            Assert.AreEqual("R8", path[3]);
        }

        [Test]
        public void GenerateRoutingTablesTest()
        {
            Network network = Network.GenerateSampleNetwork();

            var table = network.Routers["R3"].ForwardingTable;
            Assert.AreEqual(3, table.Entries["R8"].Cost);
            Assert.AreEqual("R8", table.Entries["R8"].Destination);
            Assert.AreEqual("R1", table.Entries["R8"].NextRouter);

            table = network.Routers["R6"].ForwardingTable;
            Assert.AreEqual(1, table.Entries["R1"].Cost);
            Assert.AreEqual("R1", table.Entries["R8"].NextRouter);
            Assert.AreEqual("R1", table.Entries["R1"].Destination);
        }

        [Test]
        public void GenerateSampleNetworkTest()
        {
            Network network = Network.GenerateSampleNetwork();

            Assert.IsTrue(network.Routers.ContainsKey("R1"));
            Assert.IsTrue(network.Routers.ContainsKey("R3"));
            Assert.IsTrue(network.Routers.ContainsKey("R8"));

            Assert.IsTrue(network.Routers["R1"].DirectConnections.ContainsKey(network.Routers["R4"]));
            Assert.IsTrue(network.Routers["R6"].DirectConnections.ContainsKey(network.Routers["R7"]));
            Assert.IsTrue(network.Routers["R7"].DirectConnections.ContainsKey(network.Routers["R6"]));
            Assert.IsFalse(network.Routers["R7"].DirectConnections.ContainsKey(network.Routers["R8"]));

        }
    }
}