using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab10
{
    public class Network
    {
        public Dictionary<string, NetworkRouter> Routers { get; set; }

        public Network()
        {
            Routers = new Dictionary<string, NetworkRouter>();
        }

        /// <summary>
        /// Handles adjacent connections
        /// </summary>
        private void InitializeForwardingTables()
        {
            foreach (var kvp in Routers)
            {
                var router = kvp.Value;
                foreach (var directConnectionKvp in router.DirectConnections)
                {
                    var adjacentRouter = directConnectionKvp.Key;
                    var weight = directConnectionKvp.Value;
                    ForwardingTableEntry entry = new ForwardingTableEntry()
                    {
                        Cost = weight,
                        Destination = adjacentRouter.Name,
                        NextRouter = adjacentRouter.Name
                    };
                    if(router.ForwardingTable.Entries.ContainsKey(adjacentRouter.Name) == false)
                    {
                        router.ForwardingTable.Entries.Add(adjacentRouter.Name, entry);
                    }
                }
            }
        }

        /// <summary>
        /// Generates routing tables for each NetworkRouter in the Network. 
        /// </summary>
        public void GenerateRoutingTables()
        {
            InitializeForwardingTables();

            bool newInformationFound = false;
            do
            {
                newInformationFound = false;
                foreach (var kvp in Routers)
                {
                    bool result = kvp.Value.UpdateForwardingTable();
                    if (result == true)
                    {
                        newInformationFound = true;
                    }
                }
            } while (newInformationFound == true);
        }

        /// <summary>
        /// Generates a path of routers that a packet will follow.  E.g. the path from R3 to R8 would be R3, R1, R6, R8
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sink"></param>
        /// <returns></returns>
        public List<string> GeneratePath(string source, string sink)
        {
            List<string> path = new List<string>();
            NetworkRouter current = Routers[source];
            path.Add(current.Name);
            while(current.Name != sink)
            {
                current = Routers[current.ForwardingTable.Entries[sink].NextRouter];
                path.Add(current.Name);
            }
            return path;
        }

        /// <summary>
        /// Generates a sample network for us to test against
        /// </summary>
        /// <returns></returns>
        public static Network GenerateSampleNetwork()
        {
            Network network = new Network();
            network.Routers.Add("R1", new NetworkRouter("R1"));
            network.Routers.Add("R2", new NetworkRouter("R2"));
            network.Routers.Add("R3", new NetworkRouter("R3"));
            network.Routers.Add("R4", new NetworkRouter("R4"));
            network.Routers.Add("R5", new NetworkRouter("R5"));
            network.Routers.Add("R6", new NetworkRouter("R6"));
            network.Routers.Add("R7", new NetworkRouter("R7"));
            network.Routers.Add("R8", new NetworkRouter("R8"));

            network.Routers["R1"].DirectConnections.Add(network.Routers["R2"], 1);
            network.Routers["R1"].DirectConnections.Add(network.Routers["R3"], 1);
            network.Routers["R1"].DirectConnections.Add(network.Routers["R4"], 1);
            network.Routers["R1"].DirectConnections.Add(network.Routers["R5"], 1);
            network.Routers["R1"].DirectConnections.Add(network.Routers["R6"], 1);

            network.Routers["R2"].DirectConnections.Add(network.Routers["R1"], 1);

            network.Routers["R3"].DirectConnections.Add(network.Routers["R1"], 1);

            network.Routers["R4"].DirectConnections.Add(network.Routers["R1"], 1);

            network.Routers["R5"].DirectConnections.Add(network.Routers["R1"], 1);

            network.Routers["R6"].DirectConnections.Add(network.Routers["R1"], 1);
            network.Routers["R6"].DirectConnections.Add(network.Routers["R7"], 1);
            network.Routers["R6"].DirectConnections.Add(network.Routers["R8"], 1);

            network.Routers["R7"].DirectConnections.Add(network.Routers["R6"], 1);

            network.Routers["R8"].DirectConnections.Add(network.Routers["R8"], 1);

            return network;
        }
    }
}
